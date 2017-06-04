using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest
{
    public class UIPanel : Sprite
    {
        int minWidth = 15;
        int adJustedWidth;
        int minHeight = 15;
        int adjustedHeight;
        Vector2 prevScale;

        public Vector2 _Center
        {
            get
            {
                return new Vector2(frameWidth / 2, frameHeight / 2);
            }
        }

        //List<InventorySlot> slots;
        public UIPanel()
        {
            //slots = new List<InventorySlot>();
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            adjustedHeight = frameHeight;
            adJustedWidth = frameWidth;
        }

        public void Resize(Vector2 difference)
        {
            Console.WriteLine(this._Texture.Width);
            adJustedWidth += (int)difference.X;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_Draw)
            {
                //Rectangle sr = new Rectangle((frameWidth * frameNum), (frameHeight * StateNum), frameWidth, frameHeight);
                Rectangle sr = new Rectangle(0, 0, adJustedWidth, adjustedHeight);
                if (!_FlipX && !_FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), _Rotation, _Center, 1, SpriteEffects.None, 0f);
                }
                else if (_FlipX)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), _Rotation, _Center, 1, SpriteEffects.FlipHorizontally, 0f);
                }
                else if (_FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), _Rotation, _Center, 1, SpriteEffects.FlipVertically, 0f);
                }
                else if (_FlipX && _FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), (_Rotation + (float)Math.PI), _Center, 1, SpriteEffects.None, 0f);
                }

                if (_ChildrenList != null)
                {
                    if (_ChildrenList.Count >= 1)
                    {
                        foreach (Sprite child in _ChildrenList)
                        {
                            child.Draw(spriteBatch);
                        }
                    }
                }
            }
        }
    }
}
