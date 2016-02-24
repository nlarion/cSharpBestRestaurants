using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class Cuisine
  {
    private int _id;
    private string _name;

    public Cuisine(string name, int id=0)
    {
      _id = id;
      _name = name;
    }
    public override bool Equals(System.Object otherCuisine)
    {
      if(!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool nameEquals = this.GetName() == newCuisine.GetName();
        bool idEquals = this.GetId() == newCuisine.GetId();
        return (nameEquals && idEquals);
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
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("Insert INTO cuisine (name) OUTPUT INSERTED.id VALUES (@CuisineName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@CuisineName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
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

    public static List<Cuisine> GetAll()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      List<Cuisine> myListCuisine = new List<Cuisine>{};

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(name, id);
        myListCuisine.Add(newCuisine);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return myListCuisine;
    }

    public static Cuisine Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisine;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
