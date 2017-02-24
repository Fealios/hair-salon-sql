using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using HairSalonApp.Objects;
using System.Linq;

namespace HairSalonApp
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                return View["index.cshtml", Stylist.GetAll()];
            };

            Get["/add-stylist"] =_=>
            {
                return View["new-stylist-form.cshtml"];
            };

            Post["/new-stylist"] =_=>
            {
                Stylist newStylist = new Stylist(Request.Form["name"], Request.Form["phone"]);
                newStylist.Save();
                return View["success.cshtml", "stylist"];
            };

            Get["/add-client/{id}"] = parameter =>
            {
                int stylistId = parameter.id;
                return View["add-client-form.cshtml", stylistId];
            };

            Post["/new-client"] =_=>
            {
                Client newClient = new Client(Request.Form["name"], Request.Form["id"]);
                newClient.Save();
                return View["success.cshtml", "client"];
            };
        }
    }
}
