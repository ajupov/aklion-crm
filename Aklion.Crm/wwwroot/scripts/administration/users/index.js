'use strict';

$(document).ready(() => {
    createTable({
        Element: '#users-table',
        Title: 'Пользователи',
        DataUrl: '/Administration/Users/GetList',
        //CreateUrl: '/Administration/Users/Create',
        UpdateUrl: '/Administration/Users/Update',
        //DeleteUrl: '/Administration/Users/Delete',
        Pager: '#users-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: false,
        IsDeletable: false,
        IsFilterable: true,
        Columns: [
            {
                name: 'Id',
                label: '№',
                width: 70,
                formatter: 'integer',
                key: true
            },
            {
                name: 'Login',
                label: 'Логин',
                width: 100,
                editable: true,
                editoptions: {
                    maxlength: 256
                }
            },
            {
                name: 'Email',
                label: 'Email',
                width: 120,
                formatter: 'email',
                editable: true,
                editoptions: {
                    maxlength: 256
                }
            },
            {
                name: 'Phone',
                label: 'Телефон',
                align: 'center',
                width: 80,
                editable: true,
                editoptions: {
                    maxlength: 10
                }
            },
            {
                name: 'Surname',
                label: 'Фамилия',
                width: 130,
                editable: true,
                editoptions: {
                    maxlength: 256
                }
            },
            {
                name: 'Name',
                label: 'Имя',
                width: 130,
                editable: true,
                editoptions: {
                    maxlength: 256
                }
            },
            {
                name: 'Patronymic',
                label: 'Отчество',
                width: 130,
                editable: true,
                editoptions: {
                    maxlength: 256
                }
            },
            {
                name: 'Gender',
                label: 'Пол',
                width: 80,
                align: 'center',
                formatter: 'select',
                editable: true,
                edittype: 'select',
                editoptions: {
                    value: '1:Мужской;2:Женский'
                },
                stype: 'select',
                searchoptions: {
                    value: '0:;1:Мужской;2:Женский'
                }
            },
            {
                name: 'BirthDate',
                label: 'Дата рождения',
                width: 100,
                align: 'center',
                formatter: 'date',
                editable: true,
                editoptions: {
                    dataInit: e => {
                        $(e).datepicker();
                    }
                },
                sorttype: 'date',
                searchoptions: {
                    dataInit: e => {
                        $(e).datepicker({
                            changeMonth: true,
                            changeYear: true
                        });
                    }
                }
            },
            {
                name: 'IsEmailConfirmed',
                label: 'Email подтвержден',
                align: 'center',
                hidden: true,
                formatter: 'checkbox',
                edittype: 'checkbox',
                editoptions: {
                    value: 'true:false'
                },
                editable: true,
                editrules: {
                    edithidden: true
                }
            },
            {
                name: 'IsPhoneConfirmed',
                label: 'Телефон подтвержден',
                align: 'center',
                hidden: true,
                formatter: 'checkbox',
                edittype: 'checkbox',
                editoptions: {
                    value: 'true:false'
                },
                editable: true,
                editrules: {
                    edithidden: true
                }
            },
            {
                name: 'IsLocked',
                label: 'Заблокирован',
                width: 100,
                align: 'center',
                formatter: 'checkbox',
                editable: true,
                edittype: 'checkbox',
                editoptions: {
                    value: 'true:false'
                },
                stype: 'select',
                searchoptions: {
                    value: 'null:;false:Нет;true:Да'
                }
            },
            {
                name: 'IsDeleted',
                label: 'Удален',
                width: 70,
                align: 'center',
                formatter: 'checkbox',
                editable: true,
                edittype: 'checkbox',
                editoptions: {
                    value: 'true:false'
                },
                stype: 'select',
                searchoptions: {
                    value: 'null:;false:Нет;true:Да'
                }
            },
            {
                name: 'CreateDate',
                label: 'Дата создания',
                width: 120,
                align: 'center',
                datefmt: 'd.m.Y H:i:s',
                formatter: 'date',
                formatoptions: {
                    srcformat: 'Y-m-d h:i:s',
                    newformat: 'd.m.Y H:i:s'
                },
                sorttype: 'date',
                searchoptions: {
                    dataInit: e => {
                        $(e).datepicker();
                    }
                }
            },
            {
                name: 'ModifyDate',
                label: 'Дата изменения',
                align: 'center',
                datefmt: 'd.m.Y H:i:s',
                formatter: 'date',
                formatoptions: {
                    srcformat: 'Y-m-d h:i:s',
                    newformat: 'd.m.Y H:i:s'
                },
                sorttype: 'date',
                hidden: true,
                editrules: {
                    edithidden: true
                }
            }
        ]
    });
});