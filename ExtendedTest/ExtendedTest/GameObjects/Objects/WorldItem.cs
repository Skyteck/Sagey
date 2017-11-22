using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.GameObjects.Objects
{
    class WorldItem : WorldObject
    {
        public Item MyItem;
        
        public WorldItem(Item it, Vector2 pos)
        {
            MyItem = it;
            _Position = pos;
            this.LoadContent(it.itemtexture);
        }
    }
}
