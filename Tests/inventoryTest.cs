using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Inventory.Objects
{
  public class InventoryTest : IDisposable
  {
    public InventoryTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Inventory_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
     //Arrange, Act
     int result = InventoryItem.GetAll().Count;

     //Assert
     Assert.Equal(0, result);
    }
    [Fact]
    public void Test_DatabaseHasStuff()
    {
     //Arrange, Act
     InventoryItem newInventoryItem = new InventoryItem("cat", "it's a cat");
     newInventoryItem.Save();
     int result = InventoryItem.GetAll().Count;

     //Assert
     Assert.Equal(1, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAreTheSame()
    {
     //Arrange, Act
     InventoryItem firstInventoryItem = new InventoryItem("cat", "it's a cat");
     InventoryItem secondInventoryItem = new InventoryItem("cat", "it's a cat");

     //Assert
     Assert.Equal(firstInventoryItem, secondInventoryItem);
    }
    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      InventoryItem newInventoryItem = new InventoryItem("cat", "it's a cat");

      //Act
      newInventoryItem.Save();
      List<InventoryItem> result = InventoryItem.GetAll();
      List<InventoryItem> testList = new List<InventoryItem>{newInventoryItem};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      InventoryItem testInventoryItem = new InventoryItem("cat", "it's a cat");
      testInventoryItem.Save();

      InventoryItem foundInventoryItem = InventoryItem.Find(testInventoryItem.GetId());

      Assert.Equal(testInventoryItem, foundInventoryItem);
    }
    [Fact]
    public void Test_Search_FindsIdByName()
    {
      InventoryItem testInventoryItem = new InventoryItem("cat", "it's a cat");
      testInventoryItem.Save();

      int result = InventoryItem.Search(testInventoryItem.GetName());

      Assert.Equal(testInventoryItem.GetId(), result);
    }
    [Fact]
    public void Test_Search_DoesntFindIdForUnused()
    {
      InventoryItem testInventoryItem = new InventoryItem("cat", "it's a cat");
      testInventoryItem.Save();

      int result = InventoryItem.Search("dog");

      Assert.Equal(-1, result);
    }

    public void Dispose()
    {
      InventoryItem.DeleteAll();
    }
  }
}
