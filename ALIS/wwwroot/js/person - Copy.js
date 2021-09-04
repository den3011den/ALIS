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
                this.api().column(8).every(function () {
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
            {
                "data": "name",
                "render": function (data, type, row, meta)
                    {
                    return `<a href="/${model_name}/Edit/${row.id}" style="cursor:pointer" title="Редактировать запись">${row.surname} ${row.name} ${row.patronymic}</a>`;
                    },                 
                "width": "30%"
            },
            {
                "data": "barcode",
                "render": function (data, type, row, meta) {
                    return `<a href="/${model_name}/PrintBarCode/${row.id}" style="cursor:pointer" title="Печатать карточку сотрудника">${row.barcode}</a>`;
                },
                "width": "10%"
            },
            { "data": "groupnumber", "width": "5%" },
            { "data": "user_id",             
                "render": function (data, type, row, meta) {
                    //if (isEmpty(row.user_id) || isBlank(row.user_id) || row.isnotactive)
                    if (isBlank(row.user_id) || row.isnotactive || (row.user_id==="")
                    {
                        return `
                        <div style="text-align: center;">                            
                                <a href="/${model_name}/ActivateUserForPerson/${row.id}" class="btn btn-success text-white" style="cursor:pointer" title="Дать доступ в систему">
                                   <i class="far fa-square"></i>
                                 </a>
                        </div>
                    `;
                    }
                    else
                    {
                        return `
                        <div style="text-align: center;">                            
                                <a href="/${model_name}/DeactivateUserForPerson/${row.id}" class="btn btn-success text-white" style="cursor:pointer" title="Запретить доступ в систему">
                                   <i class="far fa-check-square"></i>
                                 </a>
                        </div>
                    `;
                    }
                },
            "width": "5%"
            },
            {
                "data": "rolename",
                "render": function (data, type, row, meta) {
                    if (isEmpty(row.rolename) || isBlank(row.rolename)) {
                        return ``;
                    }
                    else {
                        return `<a href="/${model_name}/EditUserRoleForPerson/${row.id}" style="cursor:pointer" title="Редактировать роль пользователя в системе">${row.rolename}</a>`;
                    }
                },
                "width": "10%"
            },
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
                            < div style = "text-align: center;" >
                                <a href="/${model_name}/RestorePersonFromArchive/${row.id}" class="btn btn-success text-white" style="cursor:pointer" title="Восстановить из архива">
                                    <i class="far fa-check-square"></i>
                                </a>
                            </div >
                        `;
                    }
                    else {
                        return `                               
                            < div style = "text-align: center;" >
                                <a href="/${model_name}/MovePerdonToArchive/${row.id}" class="btn btn-success text-white" style="cursor:pointer" title="Удалить в архив">
                                    <i class="far fa-square"></i>
                                </a>
                            </div >
                        `;
                    }

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
