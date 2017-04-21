using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest
{
    class InventoryManager
    {
        List<Texture2D> textureList;

        public InventoryManager(ContentManager content)
        {
            Texture2D newLogTex = content.Load<Texture2D>("Art/log");
            textureList.Add(newLogTex);
        }

        //public Texture2D getText(Item.ItemType itemType)
        //{

        //}
    }
}
