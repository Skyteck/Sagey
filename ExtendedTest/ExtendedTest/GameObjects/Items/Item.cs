using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExtendedTest
{
    public class Item
    {
        public  String _Name;
        public double _Weight;
        public String _Image;
        public int _SaleValue;
        public Texture2D itemtexture;
        public int ID;
        public bool _Stackable = false;
        public enum ItemType
        {
            ItemLog, ItemNone, ItemError, ItemOre
        }

        public ItemType myType = ItemType.ItemNone;
        

        public void Draw(SpriteBatch spritebatch, Vector2 Pos)
        {
            spritebatch.Draw(itemtexture, Pos, Color.White);
        }
    }
}
