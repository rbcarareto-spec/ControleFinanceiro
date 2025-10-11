$(document).ready(function () {

    // Só executa se a tabela existir na página
    if ($('#tabelaTransacoes').length) {

        // --- 1️ Registra o filtro customizado ---
        $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
            var categoriaColuna = $(settings.aoData[dataIndex].anCells[1]).text().trim();
            var tipoColuna = $(settings.aoData[dataIndex].anCells[2]).data('tipo');
            var dataAttr = $(settings.aoData[dataIndex].anCells[4]).data('data');

            var filtroData = $('#filtroData').val();
            var filtroCategoria = $('#filtroCategoria').val();
            var filtroTipo = $('#filtroTipo').val();

            // --- Filtro por data ---
            if (filtroData && new Date(dataAttr) > new Date(filtroData)) {
                return false;
            }

            // --- Filtro por categoria ---
            if (filtroCategoria && categoriaColuna !== filtroCategoria) {
                return false;
            }

            // --- Filtro por tipo ---
            if (filtroTipo && tipoColuna != filtroTipo) {
                return false;
            }

            return true;
        });

        // --- 2️ Cria a tabela ---
        var tabela = $('#tabelaTransacoes').DataTable({
            language: {
                url: "//cdn.datatables.net/plug-ins/2.0.8/i18n/pt-BR.json"
            },
            pageLength: 10,
            lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
            order: [[4, "desc"]],
            columnDefs: [{ orderable: false, targets: 5 }]
        });

        // --- 3️ Eventos de filtro ---
        $('#filtroData, #filtroCategoria, #filtroTipo').on('change', function () {
            tabela.draw();
        });

        $('#btnLimpar').on('click', function () {
            $('#filtroData, #filtroCategoria, #filtroTipo').val('');
            tabela.draw();
        });
    }

});
