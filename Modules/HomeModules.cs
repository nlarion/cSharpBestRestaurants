using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BestRestaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/Cuisine"] = _ => {
        return View["cuisine.cshtml", Cuisine.GetAll()];
      };
      Post["/Cuisine"] = _ => {
        Cuisine newCuisine = new Cuisine(Request.Form["name"]);
        newCuisine.Save();
        return View["cuisine.cshtml", Cuisine.GetAll()];
      };
      Get["/Cuisine/Create"]  = _ => {
        return View["cuisineCreate.cshtml"];
      };
      Get["/Restaurant"] = _ => {
        return View["restaurant.cshtml", Restaurant.GetAll()];
      };
      Post["/Restaurant"] =_=> {
        DateTime newDateTime = Convert.ToDateTime((string)Request.Form["date"]);
        Restaurant newRestaurant = new Restaurant(Request.Form["name"],Request.Form["cuisine"],newDateTime,Request.Form["location"]);
        newRestaurant.Save();
        return View["restaurant.cshtml",Restaurant.GetAll()];
      };
      Get["/Restaurant/Create"] = _ => {
        List<Cuisine> newCuisine = Cuisine.GetAll();
        return View["restaurantCreate.cshtml", newCuisine];
      };
    }
  }
}
