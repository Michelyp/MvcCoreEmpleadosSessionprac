using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace MvcCoreEmpleadosSession.Filter
{
        public class AuthorizeUsersAttribute : AuthorizeAttribute,
        IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                string controller =
                    context.RouteData.Values["controller"].ToString();
                string action =
                    context.RouteData.Values["action"].ToString();

                ITempDataProvider provider =
                    context.HttpContext.RequestServices
                    .GetService<ITempDataProvider>();

                var TempData = provider.LoadTempData(context.HttpContext);

                TempData["controller"] = controller;
                TempData["action"] = action;

                provider.SaveTempData(context.HttpContext, TempData);
                var user = context.HttpContext.User;
                if (user.Identity.IsAuthenticated == false)
                {
                    RouteValueDictionary rutaLogin =
                        new RouteValueDictionary(
                                new { controller = "Libros", action = "Login" }
                            );
                    context.Result =
                        new RedirectToRouteResult(rutaLogin);
                }
            }
        }
    }