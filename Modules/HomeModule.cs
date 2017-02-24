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


        }
    }
}
