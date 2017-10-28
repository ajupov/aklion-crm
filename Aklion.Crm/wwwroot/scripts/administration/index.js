'use strict';

const ui = {
    usersTable: $('#users-table'),
    usersTablePagination: $('#users-table-pagination')
};

$(document).ready(() => {
    ui.usersTable.jqGrid({
        url: '/Administration/Users/GetList',
        datatype: 'json',
        colModel: [
            {
                name: 'Id',
                label: '№',
                width: 60,
                formatter: 'integer',
                key: true
            },
            {
                name: 'Email',
                label: 'Email',
                width: 140,
                editable: true,
                formatter: 'email',
                editoptions: {
                    maxlength: 256
                }
            },
            {
                name: 'Phone',
                label: 'Телефон',
                align: 'center',
                width: 90,
                editoptions: {
                    maxlength: 10
                },
                editable: true
            },
            {
                name: 'Surname',
                label: 'Фамилия',
                width: 140,
                editoptions: {
                    maxlength: 256
                },
                editable: true
            },
            {
                name: 'Name',
                label: 'Имя',
                width: 120,
                editoptions: {
                    maxlength: 256
                },
                editable: true
            },
            {
                name: 'Patronymic',
                label: 'Отчество',
                width: 140,
                editoptions: {
                    maxlength: 256
                },
                editable: true
            },
            {
                name: 'Gender',
                label: 'Пол',
                width: 90,
                editable: true,
                stype: 'select',
                align: 'center',
                formatter: 'select',
                edittype: 'select',
                editoptions: {
                    value: '1:Мужской;2:Женский'
                }
            },
            {
                name: 'BirthDate',
                label: 'Дата рождения',
                width: 110,
                editable: true,
                align: 'center',
                formatter: 'date',
                formatoptions: {
                    newformat: 'd.m.Y'
                }
            },
            {
                name: 'IsEmailConfirmed',
                label: 'Email подтвержден',
                align: 'center',
                width: 140,
                edittype: 'checkbox',
                editoptions: {
                     value: 'true:false'
                },
                formatter: 'checkbox',
                editable: true
            },
            {
                name: 'IsPhoneConfirmed',
                label: 'Телефон подтвержден',
                formatter: 'checkbox',
                align: 'center',
                width: 150,
                edittype: 'checkbox',
                editoptions: {
                    value: 'true:false'
                },
                editable: true
            },
            {
                name: 'IsLocked',
                label: 'Заблокирован',
                formatter: 'checkbox',
                align: 'center',
                width: 100,
                edittype: 'checkbox',
                editoptions: {
                    value: 'true:false'
                },
                editable: true
            },
            {
                name: 'IsDeleted',
                formatter: 'checkbox',
                label: 'Удален',
                align: 'center',
                width: 80,
                edittype: 'checkbox',
                editoptions: {
                    value: 'true:false'
                },
                editable: true
            },
            {
                name: 'CreateDate',
                label: 'Дата создания',
                formatter: 'date',
                align: 'center',
                width: 130,
                editable: true,
                formatoptions: {
                    srcformat: 'Y-m-dTh:i:s.u',
                    newformat: 'd.m.Y H:i:s'
                },

                sorttype: 'date',
                datefmt: 'd.m.Y H:i:s'
            },
            {
                name: 'ModifyDate',
                label: 'Дата изменения',
                align: 'center',
                width: 130,
                editable: true,
                formatoptions: {
                    newformat: 'd.m.Y H:i:s'
                },
                sorttype: 'date',
                formatter: 'date',
                datefmt: 'd.m.Y H:i:s',
                //defaultValue: ''
            }
        ],
        rowNum: 10,
        rowList: [10, 20, 50, 100],
        height: 'auto',
        pager: '#users-table-pagination',
       // autowidth: true,
        width: '100%',
        sortname: 'Id',
        sortorder: 'desc',
        caption: 'Пользователи',
        viewrecords: true,
        prmNames: {
            page: 'Page',
            rows: 'Size',
            sort: 'SortingColumn',
            order: 'SortingOrder',
            search: 'IsSearch',
            nd: 'Timestamp',
            id: 'Id',
            oper: 'oper',
            editoper: 'edit',
            addoper: 'add',
            deloper: 'del',
            subgridid: 'id',
            npage: null,
            totalrows: 'totalrows'
        },
        jsonReader: {
            root: 'Items',
            page: 'Page',
            total: 'PageCount',
            records: 'Size',
            repeatitems: true,
            id: 'Id'
        }
    });

    ui.usersTable.jqGrid('filterToolbar',
        {
            operands: {
                "eq": '==',
                "ne": '!',
                "lt": '<',
                "le": '<=',
                "gt": '>',
                "ge": '>=',
                "bw": '^',
                "bn": '!^',
                "in": '=',
                "ni": '!=',
                "ew": '\|',
                "en": '!@',
                "cn": '~',
                "nc": '!~',
                "nu": '#',
                "nn": '!#',
                "bt": '...'
            }
        });

    ui.usersTable.jqGrid('navGrid',
        '#users-table-pagination',
        {
            refresh: false,
            search: false,
            add: true,
            del: true,
            edit: true,
            addtext: 'Добавить',
            edittext: 'Изменить',
            deltext: 'Удалить'
        },
        update('edit'), // обновление
        update('add'), // добавление
        update('del') // удаление
    );


    function update(act) {
        return {
            closeAfterAdd: true, // закрыть после добавления
            height: 250,
            width: 400,
            closeAfterEdit: true, // закрыть после редактирования
            reloadAfterSubmit: true, // обновление
            drag: true,
            onclickSubmit: function(params) {
                const list = $('#jqg');
                const selectedRow = list.getGridParam('selrow');
                rowData = list.getRowData(selectedRow);
                if (act === 'add')
                    params.url = '@Url.Action("Create")';
                else if (act === 'del')
                    params.url = '@Url.Action("Delete")';
                else if (act === 'edit')
                    params.url = '@Url.Action("Edit")';
            },
            afterSubmit: function(response, postdata) {
                // обновление грида
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                return [true, '', 0];
            }
        };
    };
});