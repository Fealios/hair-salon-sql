using Nancy;
using System.Collections.Generic;
using HairSalonApp.Objects;

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

            Get["/new-client/{id}"] = parameter =>
            {
                int stylistId = parameter.id;
                return View["add-client-form.cshtml", stylistId];
            };

            Post["/add-client"] = _ =>
            {
                Client newClient = new Client(Request.Form["name"], Request.Form["stylistId"]);
                newClient.Save();
                return View["success.cshtml", "client"];
            };

            Get["/unique-client/{id}"] = parameter =>
            {
                Client tempClient = Client.Find(parameter.id);
                return View["client.cshtml", tempClient];
            };

            Get["/delete-confirm/{id}"] = parameter =>
            {
                return View["confirm-delete.cshtml", Client.Find(parameter.id)];
            };

            Delete["/delete-client/{id}"] = parameter =>
            {
                Client tempClient = Client.Find(parameter.id);
                // Console.WriteLine(tempClient.GetId());
                tempClient.Delete();
                return View["index.cshtml", Stylist.GetAll()];
            };

            Get["/modify-client/{id}"] = parameter =>
            {
                Client tempClient = Client.Find(parameter.id);
                return View["modify-form.cshtml", tempClient];
            };

            Patch["/client/update/{id}"] = parameter =>
            {
                Client tempClient = Client.Find(parameter.id);
                tempClient.Update(Request.Form["update"]);
                Client outputClient = Client.Find(parameter.id);
                return View["client.cshtml", outputClient];
            };
        }
    }
}
