using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.UserAttributeLink;
using Aklion.Crm.Mappers.Administration.UserAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("UserAttributeLinks")]
    public class UserAttributeLinkController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IUserAttributeLinkDao _userAttributeLinkDao;

        public UserAttributeLinkController(
            IAuditLogService auditLogService,
            IUserAttributeLinkDao userAttributeLinkDao)
        {
            _auditLogService = auditLogService;
            _userAttributeLinkDao = userAttributeLinkDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<UserAttributeLinkModel>> GetList(UserAttributeLinkParameterModel model)
        {
            var result = await _userAttributeLinkDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(UserAttributeLinkModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _userAttributeLinkDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(UserAttributeLinkModel model)
        {
            var oldModel = await _userAttributeLinkDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _userAttributeLinkDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _userAttributeLinkDao.GetAsync(id).ConfigureAwait(false);

            await _userAttributeLinkDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}