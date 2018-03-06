using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Mappers.Administration.User;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.User;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.User
{
    [AjaxErrorHandle]
    [Route("Administration/Users")]
    public class AdministrationUserController : BaseController
    {
        private readonly IUserDao _dao;

        public AdministrationUserController(IUserDao dao)
        {
            _dao = dao;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/User/Index.cshtml");
        }

        [HttpGet("GetList")]
        public async Task<PagingModel<UserModel>> GetList(UserParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet("GetAutocomplete")]
        public Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew());
        }

        [HttpPost("Update")]
        public async Task Update(UserModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost("Delete")]
        public Task Delete(int id)
        {
            return _dao.DeleteAsync(id);
        }
    }
}