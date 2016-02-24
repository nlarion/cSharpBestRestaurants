using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private int _cuisineId;
    private DateTime _date;
    private string _location;

    public Restaurant(string name, int cuisineId, DateTime date, string location ,int id=0)
    {
      _id = id;
      _name = name;
      _cuisineId = cuisineId;
      _date = date;
      _location = location;
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string name)
    {
      _name = name;
    }
    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public void SetCusineId(int cuisineId)
    {
      _cuisineId = cuisineId;
    }
    public DateTime GetDateTime()
    {
      return _date;
    }
    public void SetDateTime(DateTime date)
    {
      _date = date;
    }
    public string GetLocation()
    {
      return _location;
    }
    public void SetLocation(string location)
    {
      _location = location;
    }
    public static List<Restaurant> GetAll()
    {
      return new List<Restaurant>{};
    }
  }
}
