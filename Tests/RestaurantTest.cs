using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Empty_DBIsEmpty()
    {
      //Arrange//Act
      int result = Restaurant.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_SavesRestaurantToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Chipotle", 1, new DateTime(1984, 9, 3));
      testRestaurant.Save();

      //Act
      List<Restaurant> restaurantList = Restaurant.GetAll();
      List<Restaurant> tempList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testRestaurant,tempList);
    }
    public void Dispose()
    {
      // Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }
  }
}
