using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Inventory.Objects
{
  public class InventoryItem
  {
    private int _id;
    private string _name;
    private string _description;

    public InventoryItem(string name, string description, int Id = 0)
    {
      _id = Id;
      SetName(name);
      SetDescription(description);
    }

    public void SetName(string name)
    {
      _name = name;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetDescription(string description)
    {
      _description = description;
    }

    public string GetDescription()
    {
      return _description;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM InventoryItems;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
