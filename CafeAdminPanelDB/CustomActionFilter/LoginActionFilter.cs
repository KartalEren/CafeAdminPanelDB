using CafeAdminPanelDB.Data;
using CafeAdminPanelDB.Models;
using CafeAdminPanelDB.ViewModel.UserVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CafeAdminPanelDB.CustomActionFilter
{
    public class LoginActionFilter: ActionFilterAttribute, IActionFilter
    {

        //private readonly AdminPanelDbContext _db;
        public LoginActionFilter(/*AdminPanelDbContext db*/)
        {
            //_db = db;
        }


        public override void OnActionExecuted(ActionExecutedContext context)//action çıkışında çalışır
        {
           
            string user = context.HttpContext.Session.GetString("loginUser");

            LoginVM loginVM=JsonConvert.DeserializeObject<LoginVM>(user);
            //User user=_db..FirstOrDefault(x => x.UserName == userVm.UserName && x.Password == userVm.Password);

            if (loginVM.UserName == null || loginVM.Password==null)
            {
                context.HttpContext.Response.Redirect("http://localhost:12538");
            }
            

           

        }
        public override void OnActionExecuting(ActionExecutingContext context)//action girişinde çalışır
        {
      
            
        }

    }
}
