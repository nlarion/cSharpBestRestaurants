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
    public override bool Equals(System.Object otherRestaurant)
    {
      if(!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool nameEquals = this.GetName() == newRestaurant.GetName();
        bool idEquals = this.GetCuisineId() == newRestaurant.GetCuisineId();
        bool dateEquals = this.GetDateTime() == newRestaurant.GetDateTime();
        bool locationEquals = this.GetLocation() == newRestaurant.GetLocation();
        return (nameEquals && idEquals && dateEquals && locationEquals);
      }
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
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("Insert INTO restaurant (name, cuisineId, date, location) OUTPUT INSERTED.id VALUES (@RestaurantName, @CuisineId, @Date, @Location);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@RestaurantName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter cuisineId = new SqlParameter();
      cuisineId.ParameterName = "@CuisineId";
      cuisineId.Value = this.GetCuisineId();
      cmd.Parameters.Add(cuisineId);

      SqlParameter dateParameter = new SqlParameter();
      dateParameter.ParameterName = "@Date";
      dateParameter.Value = this.GetDateTime();
      cmd.Parameters.Add(dateParameter);

      SqlParameter locationParameter = new SqlParameter();
      locationParameter.ParameterName = "@Location";
      locationParameter.Value = this.GetLocation();
      cmd.Parameters.Add(locationParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static List<Restaurant> GetAll()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      List<Restaurant> myListRestaurant = new List<Restaurant>{};

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int cuisineId = rdr.GetInt32(2);
        DateTime dateTime = rdr.GetDateTime(3);
        string location = rdr.GetString(4);
        Restaurant restaurant = new Restaurant(name, cuisineId, dateTime, location);
        myListRestaurant.Add(restaurant);

      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return myListRestaurant;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurant;", conn);
      cmd.ExecuteNonQuery();

    }
  }
}
