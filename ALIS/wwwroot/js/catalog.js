var dataTable;

var api_url = window.api_url || null;
var model_name = window.model_name || null;

$(document).ready(function () {
    loadDataTable("");
    $('#myTable').DataTable();

});

function loadDataTable(url) {
    dataTable = $("#tblData").DataTable({

        "initComplete":
            function () {
                this.api().column(4).every(function () {
                    var column = this;
                    column.search('false').draw();
                    var select = $('#show_archive_records')
                        .on('change', function () {
                            if (this.checked)
                                column.search('^(true|false)', true, false).draw();
                            else
                                column.search(false).draw();
                        });
                });
            },

        /*"order": [[2, "asc"], [1, 'asc']],*/
        "order": [1, 'asc'],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Russian.json"
        },
        "ajax": {
            "url": api_url + url
        },
        "columns": [
            { "data": "id", "width": "3%" },
            { "data": "name", "width": "50%" },
            {
                "data": "isArchive",
                "render": function (data) {
                    if (data === true) {
                        return `<div style="text-align: center;"><span style="color:red"><i class="fas fa-archive"></i></span></div>`;
                    }
                    return ``;
                    //return data;
                },
                "width": "1%"
            },

            {
                "data": "id",
                "render": function (data, type, row, meta) {
                    if (row.isArchive === true) {
                        return `
                        <div style="text-align: center;">
                            <div class="w-75 btn-group" role="group">
                                <a href="/${model_name}/Edit/${row.id}" class="btn btn-success text-white" style="cursor:pointer" title="Редактировать запись">
                                   <i class="fas fa-edit mx-2"></i>
                                 <a href="/${model_name}/Restore/${row.id}" class="btn btn-warning text-white" style="cursor:pointer" title="Восстановить запись">
                                     <i class="fas fa-trash-restore-alt mx-2"></i>
                                 </a>
                             </div>
                        </div>
                    `;
                    }
                    return `
                        <div style="text-align: center;">
                            <div class="w-75 btn-group" role="group">
                                <a href="/${model_name}/Edit/${row.id}" class="btn btn-success text-white" style="cursor:pointer" title="Редактировать запись">
                                   <i class="fas fa-edit mx-2"></i>
                                 <a href="/${model_name}/Delete/${row.id}" class="btn btn-danger text-white" style="cursor:pointer" title="Удалить запись")">
                                     <i class="fas fa-trash-alt mx-2"></i>
                                 </a>
                             </div>
                        </div>
                    `;


                },
                "width": "3%"
            },
            {
                "data": "isArchive",
                "render": function (data) {
                    if (data === true) {
                        return `true`;
                    }
                    return `false`;
                },
                "visible": false
            },
        ]

    });

    $('#show_archive_records').on('click', function () {
        if (this.checked) {
            $.fn.dataTable.ext.search.push(
                function (settings, data, dataIndex) {
                    return true;
                }
            );
        }

        dataTable.draw();

        if (this.checked) {
            $.fn.dataTable.ext.search.pop();
        }
    });

}
