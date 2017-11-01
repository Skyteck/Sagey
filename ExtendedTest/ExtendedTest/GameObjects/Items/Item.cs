using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sagey
{
    public class Item
    {
        public String _Name { get; set; }
        public double _Weight { get; set; } = 0;
        public int _SaleValue { get; set; } = 1;
        public Texture2D itemtexture { get; set; }
        public bool _Stackable { get => stackable; set => stackable = value; }
        public int _Uses { get => uses; set => uses = value; } 
        public Enums.ItemID _ID { get => ID; set => ID = value; }

        private bool stackable = false;

        private int uses = 1;
        

        private Enums.ItemID ID = Enums.ItemID.kItemNone;


        public void Draw(SpriteBatch spritebatch, Vector2 Pos)
        {
            spritebatch.Draw(itemtexture, Pos, Color.White);
        }
    }

}
