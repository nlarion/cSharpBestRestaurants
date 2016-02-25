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
    public int GetId()
    {
      return _id;
    }
    public void SetId(int id)
    {
      _id = id;
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
    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant WHERE id = @RestaurantId;", conn);
      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(restaurantIdParameter);
      rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantName= null;
      int foundCuisineId = 0;
      DateTime foundDateTime = new DateTime(2016,1,1);
      string foundLocation = null;

      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantName = rdr.GetString(1);
        foundCuisineId = rdr.GetInt32(2);
        foundDateTime = rdr.GetDateTime(3);
        foundLocation = rdr.GetString(4);
      }
      Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundCuisineId, foundDateTime, foundLocation);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundRestaurant;
    }
    public static List<Restaurant> FindByCuisineId(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      List<Restaurant> myListRestaurant = new List<Restaurant>{};

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant WHERE cuisineId = @CuisisneId;", conn);
      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@CuisisneId";
      restaurantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(restaurantIdParameter);
      rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantName= null;
      int foundCuisineId = 0;
      DateTime foundDateTime = new DateTime(2016,1,1);
      string foundLocation = null;

      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantName = rdr.GetString(1);
        foundCuisineId = rdr.GetInt32(2);
        foundDateTime = rdr.GetDateTime(3);
        foundLocation = rdr.GetString(4);
        Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundCuisineId, foundDateTime, foundLocation);
        myListRestaurant.Add(foundRestaurant);
      }


      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return myListRestaurant;
    }
    public void Update(string newName, int newCuisineId, DateTime newDateTime, string newLocation)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurant SET name = @Name, cuisineId = @CuisineId, date = @Date, location = @Location OUTPUT INSERTED.name, INSERTED.cuisineId, INSERTED.date, INSERTED.location WHERE id = @CuisineId;", conn);
      SqlParameter restaurantNameParameter = new SqlParameter();
      restaurantNameParameter.ParameterName = "@Name";
      restaurantNameParameter.Value = newName;
      cmd.Parameters.Add(restaurantNameParameter);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@CuisineId";
      restaurantIdParameter.Value = newCuisineId;
      cmd.Parameters.Add(restaurantIdParameter);

      SqlParameter restaurantDateParameter = new SqlParameter();
      restaurantDateParameter.ParameterName = "@Date";
      restaurantDateParameter.Value = newDateTime;
      cmd.Parameters.Add(restaurantDateParameter);

      SqlParameter restaurantLocationParameter = new SqlParameter();
      restaurantLocationParameter.ParameterName = "@Location";
      restaurantLocationParameter.Value = newLocation;
      cmd.Parameters.Add(restaurantLocationParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
        this._cuisineId = rdr.GetInt32(1);
        this._date = rdr.GetDateTime(2);
        this._location = rdr.GetString(3);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }

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
