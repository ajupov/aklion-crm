using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Dao;
using Aklion.Crm.Dao.Store.Models;
using Aklion.Crm.Models.JqGrid;

namespace Aklion.Crm.Helpers
{
    public class JqGridHelper : IJqGridHelper
    {
        private readonly IDao _dao;

        public JqGridHelper(IDao dao)
        {
            _dao = dao;
        }

        public async Task<JqGridDataModel> GetData<TModel>(JqGridGetModel model)
        {
            var count = await _dao.GetCount<TModel>(model).ConfigureAwait(false);
            var rows = await _dao.GetList<TModel>(model).ConfigureAwait(false);
            return new JqGridDataModel((IEnumerable<object>)rows, count, model.page, model.rows);
        }

        public async Task Edit<TModel>(JqGridEditModel model) where TModel : new()
        {
            switch (model.oper)
            {
                case "add":
                {
                    var domain = new TModel();

                    foreach (var property in model.GetType().GetProperties())
                    {
                        var domainProperty = domain.GetType().GetProperty(property.Name);
                        if (domainProperty == null)
                        {
                            continue;
                        }

                        if (domainProperty.PropertyType != property.PropertyType)
                        {
                            continue;
                        }

                        var value = property.GetValue(model);
                        domainProperty.SetValue(domain, value);
                    }

                    var createDateProperty = domain.GetType().GetProperty("CreateDate");
                    if (createDateProperty != null && createDateProperty.PropertyType == typeof(DateTime))
                    {
                        createDateProperty.SetValue(domain, DateTime.Now);
                    }

                    await _dao.Create(domain).ConfigureAwait(false);
                    break;
                }
                case "edit":
                {
                    var id = GetId(model);
                    if (id <= 0)
                    {
                        return;
                    }

                    var domain = await _dao.Get<TModel>(id).ConfigureAwait(false);
                    if (domain == null)
                    {
                        return;
                    }

                    foreach (var property in model.GetType().GetProperties())
                    {
                        var domainProperty = domain.GetType().GetProperty(property.Name);
                        if (domainProperty == null)
                        {
                            continue;
                        }

                        if (domainProperty.PropertyType != property.PropertyType)
                        {
                            continue;
                        }

                        var value = property.GetValue(model);
                        domainProperty.SetValue(domain, value);
                    }

                    var modifyDateProperty = domain.GetType().GetProperty("ModifyDate");
                    if (modifyDateProperty != null && modifyDateProperty.PropertyType == typeof(DateTime))
                    {
                        modifyDateProperty.SetValue(domain, DateTime.Now);
                    }

                    await _dao.Update(domain).ConfigureAwait(false);
                    break;
                }
                case "del":
                {
                    var id = GetId(model);
                    if (id <= 0)
                    {
                        return;
                    }

                    await _dao.Delete<Store>(id).ConfigureAwait(false);
                    break;
                }
            }
        }

        private static int GetId(object model)
        {
            var idProperty = model.GetType().GetProperty("Id");
            if (idProperty == null)
            {
                return 0;
            }

            var idObject = idProperty.GetValue(model);
            if (idObject == null)
            {
                return 0;
            }

            if (!int.TryParse(idObject.ToString(), out var id))
            {
                return 0;
            }

            return id;
        }
    }
}