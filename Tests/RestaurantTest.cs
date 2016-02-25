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
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Chipotle", 1, new DateTime(1984, 9, 3), "In da hood");
      Restaurant secondRestaurant = new Restaurant("Chipotle", 1, new DateTime(1984, 9, 3), "In da hood");
      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }
    [Fact]
    public void Test_SavesRestaurantToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Chipotle", 1, new DateTime(1984, 9, 3), "In da hood");
      testRestaurant.Save();
      //Act
      List<Restaurant> restaurantList = Restaurant.GetAll();
      List<Restaurant> tempList = new List<Restaurant>{testRestaurant};
      Assert.Equal(restaurantList,tempList);
    }
    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Chipotle", 1, new DateTime(1984, 9, 3), "In da hood");
      testRestaurant.Save();
      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetCuisineId());
      //Assert
      Assert.Equal(testRestaurant, foundRestaurant);
    }

    [Fact]
    public void Test_Update_UpdateRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Chipotle", 1, new DateTime(1984, 9, 3), "In da hood");
      testRestaurant.Save();
      //Act
      testRestaurant.Update("Starbucks", 1, new DateTime(3030, 9, 3), "Portland");
      Restaurant otherRestaurant = new Restaurant("Starbucks", 1, new DateTime(3030, 9, 3), "Portland");
      //Assert
      Assert.Equal(testRestaurant, otherRestaurant);
    }
    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }
  }
}
