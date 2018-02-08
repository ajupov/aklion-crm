using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.UserAttributeLink;
using Aklion.Crm.Mappers.User.UserAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.UserAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.User
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
            var result = await _userAttributeLinkDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(UserAttributeLinkModel model)
        {
            var newModel = model.MapNew(UserContext.StoreId);

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

            var newModel = oldModel.MapFrom(model, UserContext.StoreId);

            await _userAttributeLinkDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var model = await _userAttributeLinkDao.GetAsync(id).ConfigureAwait(false);
            if (model.StoreId != UserContext.StoreId)
            {
                return;
            }

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _userAttributeLinkDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }
    }
}