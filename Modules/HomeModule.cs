using Nancy;
using Inventory.Objects;
using System.Collections.Generic;

namespace Inventory
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/allItemsList"] = _ => {
        List<InventoryItem> allInventoryItems = InventoryItem.GetAll();
        return View["catalog.cshtml", allInventoryItems];
      };
      Post["/addItemEntry"] = _ => {
        if(InventoryItem.Search(Request.Form["name"]) == -1)
        {
          InventoryItem newEntry = new InventoryItem(Request.Form["name"],Request.Form["description"]);
          newEntry.Save();
          List<InventoryItem> allInventoryItems = InventoryItem.GetAll();
          return View["catalog.cshtml", allInventoryItems];
        }
        else
        {
          return View["error.cshtml"];
        }
      };
      Get["/deleteAll"] = _ => {
        InventoryItem.DeleteAll();
        List<InventoryItem> allInventoryItems = InventoryItem.GetAll();
        return View["catalog.cshtml", allInventoryItems];
      };
    }
  }
}
