using System;
using System.Linq;
using Aklion.Crm.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Aklion.Crm.Attributes
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute(params Role[] roles)
        {
            Roles = !roles.Any() ? string.Empty : string.Join(",", roles.Select(r => Enum.GetName(typeof(Role), r)));
        }
    }
}