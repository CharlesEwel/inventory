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
    public void Dispose()
    {
      InventoryItem.DeleteAll();
    }
  }
}
