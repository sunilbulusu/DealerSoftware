using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Launch.Library.Filters
{
    public class AdminFilterAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        #region IAuthorizationFilter Members

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //if (UserManager.CurrentUser.RoleId != (int)Avidity.DAL.Roles.Administrator)
            //    filterContext.Result = new HttpUnauthorizedResult();
        }

        #endregion
    }
}