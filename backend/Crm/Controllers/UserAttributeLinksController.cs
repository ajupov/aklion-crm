using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.UserAttributeLink;
using Crm.Exceptions;
using Crm.Mappers.User.UserAttributeLink;
using Crm.Models;
using Crm.Models.User.UserAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("UserAttributeLinks")]
    public class UserAttributeLinksController : BaseController
    {
        private readonly IUserAttributeLinkDao _dao;

        public UserAttributeLinksController(IUserAttributeLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<UserAttributeLinkModel>> GetList(UserAttributeLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(UserAttributeLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(UserAttributeLinkModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            await _dao.UpdateAsync(result.MapFrom(model, UserContext.StoreId)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var result = await _dao.GetAsync(id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            result.IsDeleted = !result.IsDeleted;
            await _dao.UpdateAsync(result).ConfigureAwait(false);
        }
    }
}