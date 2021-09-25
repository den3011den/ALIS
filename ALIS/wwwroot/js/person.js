//const { error } = require("jquery");

//const { post } = require("jquery");

var dataTable;

var api_url = window.api_url || null;
var model_name = window.model_name || null;
var print_selected_url = window.print_selected_url || null;
//var alis_utility_wc_photopath = window.alis_utility_wc_photopath || null;
//var alis_utility_wc_nophotopath = window.alis_utility_wc_nophotopath || null;
    
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
        "order": [2, 'asc'],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Russian.json"
        },
        "ajax": {
            "url": api_url + url
        },
        "columns": [
            {
                "data": "isSelected",
                "render": function (data, type, row, meta) {
                        if (data === true) {
                            return `
                                   <div style="text-align: center;">
                                         <input type="checkbox" checked>
                                   </div>`
                                ;
                        } else {
                            return `
                                    <div style="text-align: center;">
                                        <input type="checkbox">
                                    </div>`;
                        }
                        return data;
                    },
                "width": "3%"
            },
            { "data": "id", "width": "3%" },
            //{
            //    "data": "id",
            //    "render": function (data, type, row, meta) {
            //        if ((row.photoPath === "") || (row.photoPath === null)) {
            //            return `<img src="${alis_utility_wc_nophotopath}" width="50" height="50" style="border-radius:5px; border:1px; solid #bbb" />`;
            //        }
            //        else {
            //            return `<img src="${alis_utility_wc_photopath}${row.photoPath}" width="50" height="50" style="border-radius:5px; border:1px; solid #bbb" />`;
            //        }
                         
            //    },
            //    "width": "5%"
            //},

            {
                "data": "name",
                "render": function (data, type, row, meta) {
                    return `<a href="/${model_name}/Edit/${row.id}" style="cursor:pointer" title="Редактировать запись">${row.surname} ${row.name} ${row.patronymic}</a>`;
                },
                "width": "30%"
            },
            {
                "data": "barcode",
                "render": function (data, type, row, meta) {
                    return `<a href="/${model_name}/PrintBarCode/${row.id}" style="cursor:pointer" target="_blank" title="Печатать карточку сотрудника">${row.barcode}</a>`;     
                },
                "width": "10%"
            },
            { "data": "groupNumber", "width": "5%" },
            {
                "data": "userId",
                "render": function (data, type, row, meta) {

                    if ((row.userId === "") || (row.userId === null) || (row.isNotActive === true)) {
                        return `
                        <div style="text-align: center;">
                                <a href="/${model_name}/ActivateUserForPerson/${row.id}" class="btn text-primary" style="cursor:pointer" title="Дать доступ в систему">
                                   <i class="far fa-square"></i>
                                 </a>
                        </div>
                    `;
                    }
                    else {
                        return `
                        <div style="text-align: center;">                            
                                <a href="/${model_name}/DeactivateUserForPerson/${row.id}" class="btn text-primary" style="cursor:pointer" title="Запретить доступ в систему">
                                   <i class="far fa-check-square"></i>
                                 </a>
                        </div>
                    `;
                    }
                },
                "width": "5%"
            },
            {
                "data": "roleName",
                "render": function (data, type, row, meta) {
                    if ((row.roleName === "") || (row.roleName === null)) {
                        return ``;
                    }
                    else {
                        return `<a href="/${model_name}/EditUserRoleForPerson/${row.id}" style="cursor:pointer" title="Редактировать роль пользователя в системе">${row.roleName}</a>`;
                    }
                },
                "width": "10%"
            },
            {
                "data": "isArchive",
                "render": function (data, type, row, meta) {
                    if (row.isArchive === true) {
                        return `                               
                            <div style = "text-align: center;" >
                                <a href="/${model_name}/RestorePersonFromArchive/${row.id}" class="btn text-danger" style="cursor:pointer" title="Восстановить из архива">
                                    <i class="far fa-check-square"></i>
                                </a>
                            </div >
                        `;
                    }
                    else {
                        return `                               
                            <div style = "text-align: center;" >
                                <a href="/${model_name}/MovePersonToArchive/${row.id}" class="btn text-primary" style="cursor:pointer" title="Удалить в архив">
                                    <i class="far fa-square"></i>
                                </a>
                            </div >
                        `;
                    }

                },
                "width": "1%"
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

    $('#print_bar_code_selected').on('click', function () {

        window.selected_items.clear();        

         dataTable.rows().eq(0).each(function (index) {
            var row = dataTable.row(index);
            var data = row.data();

            if (data.isSelected === true)
                window.selected_items.add(data.id);
            else
                window.selected_items.delete(data.id);
            }        
        );

        let array = [];
        window.selected_items.forEach(v => array.push(v));

        var url_new = print_selected_url + '?selected_items=' + JSON.stringify(array);
        
        window.open(url_new, '_blank');

        return true;
    });


    $('#tblData tbody').on('click', 'input', function () {
        var data = dataTable.row($(this).parents('tr')).data();
        if (data.isSelected === true)
            data.isSelected = false;
        else
            data.isSelected = true;
        //alert(data[0] + "'s salary is: " + data[5]);
        //editMember(data[0]);
    });
}

