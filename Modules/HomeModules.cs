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
      Get["/Cuisine/{id}"]  = parameters => {
        Cuisine newCuisine = Cuisine.Find(parameters.id);
        List<Restaurant> restaurantList = Restaurant.FindByCuisineId(newCuisine.GetId());
        Dictionary<string,object> myDictionary = new Dictionary<string,object>{};
        myDictionary.Add("cuisine",newCuisine);
        myDictionary.Add("restaurants",restaurantList);
        return View["cuisineView.cshtml",myDictionary];
      };
      Post["/Cuisine/Update/{id}"]  = parameters => {
        Cuisine newCuisine = Cuisine.Find(parameters.id);
        newCuisine.Update(Request.Form["name"]);
        return View["cuisine.cshtml",Cuisine.GetAll()];
      };
      Get["/Cuisine/Create"]  = _ => {
        return View["cuisineCreate.cshtml"];
      };
      Get["/Cuisine/Delete"] = _ => {
        Cuisine.DeleteAll();
        return View["cuisine.cshtml",  "delete"];
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
      Get["/Restaurant/Delete"] = _ => {
        Restaurant.DeleteAll();
        return View["restaurant.cshtml",  "delete"];
      };
    }
  }
}
