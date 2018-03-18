using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.Client;
using Infrastructure.Dao;
using Infrastructure.Dao.Enums;
using Infrastructure.Dao.Models;
using Infrastructure.DataBaseExecutor;

namespace Crm.Dao.Client
{
    public class ClientDao : IClientDao
    {
        private readonly IDao _dao;
        private readonly IDataBaseExecutor _executor;

        public ClientDao(IDao dao, IDataBaseExecutor executor)
        {
            _dao = dao;
            _executor = executor;
        }

        public Task<(int TotalCount, List<ClientModel> List)> GetPagedListAsync(ClientParameterModel parameter)
        {
            return _dao.GetPagedListAsync<ClientModel, ClientParameterModel>(parameter);
        }

        //public Task<(int TotalCount, List<ShortClientModel> List)> GetPagedListAsync(UserClientParameterModel parameter)
        //{
        //    var sql = "select Id, Name, IsDeleted, CreateDate, ModifyDate from dbo.Client where StoreId = @storeId";

        //    var sorting = "";
        //    foreach (var field in parameter.GetType().GetProperties())
        //    {
        //        if (field.PropertyType == typeof(FilterModel))
        //        {
        //            var value = field.GetValue(parameter) as FilterModel;
        //            if (value == null)
        //            {
        //                continue;
        //            }

        //            columns.Add(field.Name);

        //            switch (value.Type)
        //            {
        //                case FilterType.Equal:
        //                    filters.Add(field.Name);
        //                    break;
        //                case FilterType.NotEqual:
        //                    break;
        //                case FilterType.Above:
        //                    break;
        //                case FilterType.AboveOrEqual:
        //                    break;
        //                case FilterType.Less:
        //                    break;
        //                case FilterType.LessOrEqual:
        //                    break;
        //                case FilterType.Begins:
        //                    break;
        //                case FilterType.NotBegins:
        //                    break;
        //                case FilterType.Ends:
        //                    break;
        //                case FilterType.NotEnds:
        //                    break;
        //                case FilterType.NotContains:
        //                    break;
        //                case FilterType.Inlist:
        //                    break;
        //            }
        //        }
        //        else if (field.PropertyType == typeof(SortingModel))
        //        {
        //            var value = field.GetValue(parameter) as SortingModel;
        //            if (value == null)
        //            {
        //                continue;
        //            }

        //            sorting = !string.IsNullOrWhiteSpace(value.Name) 
        //                ? "order by " + value.Name + (value.Type == SortingType.Descending ? " desc" : " asc")
        //                : string.Empty;
        //        }
        //        else if (field.PropertyType == typeof(PagingModel))
        //        {
        //            var value = field.GetValue(model) as PagingModel;
        //            if (value == null)
        //            {
        //                continue;
        //            }
        //        }


        //        if (filter == null)
        //        {
        //            continue;
        //        }


        //    }


        //    throw new System.NotImplementedException();
        //}

        public Task<Dictionary<string, int>> GetAutocompleteAsync(ClientAutocompleteParameterModel parameter)
        {
            return _dao.GetForAutoCompleteAsync<ClientModel, ClientAutocompleteParameterModel>(parameter);
        }

        public Task<ClientModel> GetAsync(int id)
        {
            return _dao.GetAsync<ClientModel>(id);
        }

        public Task<int> CreateAsync(ClientModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(ClientModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<ClientModel>(id);
        }
    }
}