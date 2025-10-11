using ControleFinanceiro.Aplicacao.DTOs;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Dominio.Enums;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ControleFinanceiro.Aplicacao.Services
{
    public class RelatorioService
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public RelatorioService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<RelatorioSaldoDto> GerarRelatorioAsync(DateTime? dataInicio, DateTime? dataFim)
        {
            var transacoes = await _transacaoRepository.ObterTodasComCategoriaAsync();

            // Filtrar por período
            if (dataInicio.HasValue)
                transacoes = transacoes.Where(t => t.Data.Date >= dataInicio.Value.Date);
            if (dataFim.HasValue)
                transacoes = transacoes.Where(t => t.Data.Date <= dataFim.Value.Date);

            var listaTransacoes = transacoes.ToList();

            var receitas = listaTransacoes
                .Where(t => t.Categoria.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);

            var despesas = listaTransacoes
                .Where(t => t.Categoria.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);

            var transacoesDto = listaTransacoes
                .Select(t => new TransacaoDto
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    Data = t.Data,
                    NomeCategoria = t.Categoria.Nome,
                    TipoCategoria = t.Categoria.Tipo
                })
                .OrderByDescending(t => t.Data)
                .ToList();

            return new RelatorioSaldoDto
            {
                DataInicio = dataInicio,
                DataFim = dataFim,
                TotalReceitas = receitas,
                TotalDespesas = despesas,
                Saldo = receitas - despesas,
                Transacoes = transacoesDto
            };
        }

        public byte[] GerarExcel(RelatorioSaldoDto relatorio)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Para uso não comercial

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Relatório de Saldo");

            // Título
            worksheet.Cells["A1"].Value = "Relatório de Saldo por Período";
            worksheet.Cells["A1:D1"].Merge = true;
            worksheet.Cells["A1"].Style.Font.Size = 16;
            worksheet.Cells["A1"].Style.Font.Bold = true;
            worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Período
            var inicio = relatorio.DataInicio?.ToString("dd/MM/yyyy") ?? "Desde o início";
            var fim = relatorio.DataFim?.ToString("dd/MM/yyyy") ?? "Até hoje";
            worksheet.Cells["A2"].Value = $"Período: {inicio} a {fim}";
            worksheet.Cells["A2:D2"].Merge = true;
            worksheet.Cells["A2"].Style.Font.Italic = true;
            worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Resumo
            worksheet.Cells["A4"].Value = "Receitas";
            worksheet.Cells["B4"].Value = relatorio.TotalReceitas;
            worksheet.Cells["B4"].Style.Numberformat.Format = "#,##0.00";

            worksheet.Cells["A5"].Value = "Despesas";
            worksheet.Cells["B5"].Value = relatorio.TotalDespesas;
            worksheet.Cells["B5"].Style.Numberformat.Format = "#,##0.00";

            worksheet.Cells["A6"].Value = "Saldo";
            worksheet.Cells["B6"].Value = relatorio.Saldo;
            worksheet.Cells["B6"].Style.Numberformat.Format = "#,##0.00";
            worksheet.Cells["B6"].Style.Font.Bold = true;
            worksheet.Cells["B6"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["B6"].Style.Fill.BackgroundColor.SetColor(
                relatorio.Saldo >= 0 ?
                System.Drawing.Color.FromArgb(220, 255, 220) : // Verde claro
                System.Drawing.Color.FromArgb(255, 220, 220)   // Vermelho claro
            );

            // Cabeçalho da tabela de transações
            worksheet.Cells["A8"].Value = "Data";
            worksheet.Cells["B8"].Value = "Descrição";
            worksheet.Cells["C8"].Value = "Categoria";
            worksheet.Cells["D8"].Value = "Tipo";
            worksheet.Cells["E8"].Value = "Valor";

            // Estilo do cabeçalho
            using (var range = worksheet.Cells["A8:E8"])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            // Dados das transações
            int linha = 9;
            foreach (var transacao in relatorio.Transacoes)
            {
                worksheet.Cells[$"A{linha}"].Value = transacao.Data;
                worksheet.Cells[$"A{linha}"].Style.Numberformat.Format = "dd/MM/yyyy";

                worksheet.Cells[$"B{linha}"].Value = transacao.Descricao;
                worksheet.Cells[$"C{linha}"].Value = transacao.NomeCategoria;
                worksheet.Cells[$"D{linha}"].Value = transacao.TipoCategoria == TipoTransacao.Receita ? "Receita" : "Despesa";

                worksheet.Cells[$"E{linha}"].Value = transacao.Valor;
                worksheet.Cells[$"E{linha}"].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[$"E{linha}"].Style.Font.Color.SetColor(
                    transacao.TipoCategoria == TipoTransacao.Receita ?
                    System.Drawing.Color.Green :
                    System.Drawing.Color.Red
                );

                linha++;
            }

            // Ajustar largura das colunas
            worksheet.Column(1).Width = 12;
            worksheet.Column(2).Width = 30;
            worksheet.Column(3).Width = 20;
            worksheet.Column(4).Width = 12;
            worksheet.Column(5).Width = 15;

            return package.GetAsByteArray();
        }
    }
}