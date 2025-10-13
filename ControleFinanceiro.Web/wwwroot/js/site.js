$(document).ready(function () {

    if ($('#tabelaTransacoes').length) {

        $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
            var categoriaColuna = $(settings.aoData[dataIndex].anCells[1]).text().trim();
            var tipoColuna = $(settings.aoData[dataIndex].anCells[2]).data('tipo');
            var dataAttr = $(settings.aoData[dataIndex].anCells[4]).data('data');

            var filtroData = $('#filtroData').val();
            var filtroCategoria = $('#filtroCategoria').val();
            var filtroTipo = $('#filtroTipo').val();

            // --- Filtro por data (igual) ---
            if (filtroData) {
                const dataLinha = new Date(dataAttr);
                const dataFiltro = new Date(filtroData);

                dataLinha.setHours(0, 0, 0, 0);
                dataFiltro.setHours(0, 0, 0, 0);

                if (dataLinha.getTime() !== dataFiltro.getTime()) {
                    return false;
                }
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

        var tabela = $('#tabelaTransacoes').DataTable({
            language: {
                url: "//cdn.datatables.net/plug-ins/2.0.8/i18n/pt-BR.json"
            },
            pageLength: 10,
            lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
            order: [[4, "desc"]],
            columnDefs: [{ orderable: false, targets: 5 }]
        });

        $('#filtroData, #filtroCategoria, #filtroTipo').on('change', function () {
            tabela.draw();
        });

        $('#btnLimpar').on('click', function () {
            $('#filtroData, #filtroCategoria, #filtroTipo').val('');
            tabela.draw();
        });
    }

});
