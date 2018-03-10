using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.ClientAttributeLink;
using Crm.Exceptions;
using Crm.Mappers.User.ClientAttributeLink;
using Crm.Models;
using Crm.Models.User.ClientAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("ClientAttributeLinks")]
    public class ClientAttributeLinksController : BaseController
    {
        private readonly IClientAttributeLinkDao _dao;

        public ClientAttributeLinksController(IClientAttributeLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientAttributeLinkModel>> GetList(ClientAttributeLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(ClientAttributeLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ClientAttributeLinkModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(int id)
        {
            var result = await _dao.GetAsync(id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            result.IsDeleted = !result.IsDeleted;
            await _dao.UpdateAsync(result).ConfigureAwait(false);

            return result.IsDeleted;
        }
    }
}