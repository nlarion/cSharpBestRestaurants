using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class Cruisine
  {
    private int _id;
    private string _name;

    public Cruisine(string name, int id=0)
    {
      _id = id;
      _name = name;
    }
    public override bool Equals(System.Object otherCruisine)
    {
      if(!(Cruisine in otherCruisine))
      {
        return false;
      }
      else
      {
        Cruisine newCruisine = (Crusine) otherCruisine;
        bool nameEquals = this.GetName() == newCruisine.GetName();
        bool idEquals = this.GetId() == newCruisine.GetId();
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
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("Insert INTO cruisine (name) OUTPUT INSERTED.id VALUES (@CruisineName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@CruisineName";
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
  }
}
