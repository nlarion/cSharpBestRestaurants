using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Empty_DBIsEmpty()
    {
      //Arrange//Act
      int result = Cuisine.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Cuisine firstCuisine = new Cuisine("Burritos");
      Cuisine secondCuisine = new Cuisine("Burritos");

      //Assert
      Assert.Equal(firstCuisine, secondCuisine);
    }

    [Fact]
    public void Test_Save_SavesCategoryToDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Burritos");
      testCuisine.Save();

      //Act
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      //Assert
      Assert.Equal(testList, result);
    }

    public void Dispose()
    {
      // Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }
  }
}
