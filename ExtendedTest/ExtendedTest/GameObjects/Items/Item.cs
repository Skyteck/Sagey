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
        public bool _Stackable = false;
        public int Uses = 1;

        public enum ItemType
        {
            kItemLog = 0,
            kItemNone,
            kItemError,
            kItemOre,
            kItemFish,
            kItemFishNet,
            kItemMatches,
            kItemFishStick
        }

        public ItemType myType = ItemType.kItemNone;
        

        public void Draw(SpriteBatch spritebatch, Vector2 Pos)
        {
            spritebatch.Draw(itemtexture, Pos, Color.White);
        }
    }
}
