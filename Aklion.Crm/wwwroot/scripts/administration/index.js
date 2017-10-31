'use strict';

const urls = {
    usersGetList: '/Administration/Users/GetList',
    usersCreate: '/Administration/Users/Create',
    usersUpdate: '/Administration/Users/Update',
    usersDelete: '/Administration/Users/Delete'
}

const ui = {
    usersTable: $('#users-table')
};

$(document).ready(() => {

    var colModel = [
        {
            name: 'Id',
            label: '№',
            width: 70,
            formatter: 'integer',
            key: true
        },
        {
            name: 'Email',
            label: 'Email',
            width: 150,
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
            width: 80,
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
            width: 140,
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
            width: 80,
            editable: true,
            stype: 'select',
            align: 'center',
            formatter: 'select',
            edittype: 'select',
            editoptions: {
                value: '0:;1:Мужской;2:Женский'
            },
            searchoptions: {
                value: '0:;1:Мужской;2:Женский'
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
            },
            editoptions: {
                dataInit: e => {
                    $(e).datepicker();
                }
            }
        },
        {
            name: 'IsEmailConfirmed',
            label: 'Email подтвержден',
            align: 'center',
            edittype: 'checkbox',
            editoptions: {
                value: 'true:false'
            },
            editable: true,
            hidden: true,
            editrules: {
                edithidden: true
            }
        },
        {
            name: 'IsPhoneConfirmed',
            label: 'Телефон подтвержден',
            formatter: 'checkbox',
            align: 'center',
            edittype: 'checkbox',
            editoptions: {
                value: 'true:false'
            },
            editable: true,
            hidden: true,
            editrules: {
                edithidden: true
            }
        },
        {
            name: 'IsLocked',
            label: 'Заблокирован',
            formatter: 'checkbox',
            align: 'center',
            width: 110,
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
            formatoptions: {
                srcformat: 'Y-m-dTh:i:s.u',
                newformat: 'd.m.Y H:i:s'
            },

            sorttype: 'date',
            datefmt: 'd.m.Y H:i:s',
            searchoptions: {
                dataInit: e => {
                    $(e).datepicker();
                },
                attr: {
                    title: 'Select Date'
                }
            }
        },
        {
            name: 'ModifyDate',
            label: 'Дата изменения',
            align: 'center',
            width: 130,
            formatoptions: {
                newformat: 'd.m.Y H:i:s'
            },
            sorttype: 'date',
            formatter: 'date',
            datefmt: 'd.m.Y H:i:s',
            hidden: true,
            editrules: {
                edithidden: true
            }
        }
    ];

    var options = {
        Element: ui.usersTable,
        Title: 'Пользователи',
        DataUrl: urls.usersGetList,
        CreateUrl: urls.usersCreate,
        UpdateUrl: urls.usersUpdate,
        DeleteUrl: urls.usersDelete,
        Columns: colModel,
        Pager: '#users-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true
    }

    createTable(options);
});