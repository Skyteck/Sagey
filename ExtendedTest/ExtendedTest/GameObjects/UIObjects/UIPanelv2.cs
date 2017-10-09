using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.GameObjects.UIObjects
{
    class UIPanelv2
    {
        Texture2D bgTex;
        Texture2D edgeTex;
        public Vector2 _Position;
        public Vector2 _InitialPos = Vector2.Zero;
        public String _PanelName = "None";
        private int adjustedHeight;

        public UIPanelv2()
        {

        }

        public void LoadContent(String Path, ContentManager content)
        {
            bgTex = content.Load<Texture2D>("Art/inventoryBG");
            edgeTex = content.Load<Texture2D>("Art/Whitetexture");
        }

        //public void Draw(SpriteBatch spriteBatch)
        //{

        //    Rectangle sr = new Rectangle((int)_Position.X, (int)_Position.Y, adjustedHeight, adjustedHeight);
        //    spriteBatch.Draw(_Texture, sr, null, Color.White, 0f, _Center, SpriteEffects.None, 0f);
        //    spriteBatch.Draw(edgeTex, _TopEdge, Color.White);
        //    spriteBatch.Draw(edgeTex, _BottomEdge, Color.White);
        //    spriteBatch.Draw(edgeTex, _LeftEdge, Color.White);
        //    spriteBatch.Draw(edgeTex, _RightEdge, Color.White);
            
        //}
    }
}
