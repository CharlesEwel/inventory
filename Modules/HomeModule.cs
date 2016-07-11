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
        InventoryItem newEntry = new InventoryItem(Request.Form["name"],Request.Form["description"]);
        newEntry.Save();
        List<InventoryItem> allInventoryItems = InventoryItem.GetAll();
        return View["catalog.cshtml", allInventoryItems];
      };
    }
  }
}
