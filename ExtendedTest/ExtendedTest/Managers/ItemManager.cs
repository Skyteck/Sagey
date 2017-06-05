using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{
    public class ItemManager
    {
        ContentManager _Content;
        List<Item> ItemList;
        public ItemManager()
        {
            ItemList = new List<Item>();
            ItemList.Add(new Log());
            ItemList.Add(new FishStick());
            ItemList.Add(new Fish());
            ItemList.Add(new CookedFish());
            ItemList.Add(new Ore());
            ItemList.Add(new Matches());
        }

        public void LoadItems(String path)
        {

        }
    }
}
