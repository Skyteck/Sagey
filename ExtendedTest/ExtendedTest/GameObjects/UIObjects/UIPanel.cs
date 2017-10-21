using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ExtendedTest
{
    public class UIPanel : Sprite
    {
        int minWidth = 50;
        protected int adJustedWidth = 200;
        int minHeight = 60;
        protected int adjustedHeight = 200;
        public Vector2 _InitialPos = Vector2.Zero;
        public bool _Showing = false;
        private bool TrackMouse = false;
        private bool xTracked = false;
        private bool yTracked = false;
        bool leftTracked = false;
        bool topTracked = false;
        public bool _Resizable = true;
        protected int scrollPos = 0;
        public Managers.UIManager parentManager;
        private MouseState prevMousePos;

        Texture2D edgeTex;
        protected Texture2D extraTex;


        protected SpriteFont count;

        public Vector2 _Center
        {
            get
            {
                return new Vector2(adJustedWidth / 2, adjustedHeight / 2);
            }
        }

        public override Rectangle _BoundingBox 
        {
            get
            {
                return new Rectangle((int)_Position.X, (int)_Position.Y, adJustedWidth, adjustedHeight);
            }
        }

        protected Rectangle _TopEdge
        {
            get
            {
                return new Rectangle((int)_Position.X, (int)_Position.Y, adJustedWidth, 5);
            }
        }

        private Rectangle _BottomEdge
        {
            get
            {
                return new Rectangle((int)_Position.X, (int)_Position.Y - 5 + adjustedHeight, adJustedWidth, 5);
            }
        }

        protected Rectangle _LeftEdge
        {
            get
            {
                return new Rectangle((int)_Position.X, (int)_Position.Y, 5, adjustedHeight);
            }
        }

        private Rectangle _RightEdge
        {
            get
            {
                return new Rectangle((int)_Position.X + adJustedWidth - 5, (int)_Position.Y, 5, adjustedHeight);
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
            edgeTex = content.Load<Texture2D>("Art/Whitetexture");
            count = content.Load<SpriteFont>("Fonts/Fipps");
            extraTex = content.Load<Texture2D>("Art/YellowTexture");
        }

        protected override void UpdateActive(GameTime gt)
        {
            base.UpdateActive(gt);

            if (!_Resizable) return;

            if(xTracked || yTracked || topTracked || leftTracked)
            {
                MouseState mState = Mouse.GetState();
                if(InputHelper.LeftButtonReleased)
                {

                    xTracked = false;
                    yTracked = false;
                    leftTracked = false;
                    topTracked = false;
                }
                else
                {
                    scrollPos = 0;
                    Vector2 currentPos = InputHelper.MouseScreenPos;
                    Vector2 prevPos = InputHelper.PrevMouseScreenPos;

                    int xDiff = (int)(currentPos.X - prevPos.X);
                    int yDiff = (int)(currentPos.Y - prevPos.Y);
                    Vector2 initPos = _InitialPos;
                    if (topTracked)
                    {
                        adjustedHeight -= yDiff;
                        _InitialPos.Y += yDiff;
                    }

                    if(leftTracked)
                    {
                        adJustedWidth -= xDiff;
                        _InitialPos.X += xDiff;
                    }

                    if (xTracked) adJustedWidth += xDiff;
                    if (yTracked) adjustedHeight += yDiff;

                    if (adjustedHeight < minHeight)
                    {
                        adjustedHeight = minHeight;
                        _InitialPos.Y = initPos.Y;
                    }
                    if (adJustedWidth < minWidth)
                    {
                        adJustedWidth = minWidth;
                        _InitialPos.X = initPos.X;
                    }

                    prevMousePos = mState;
                }

                    
            }
        }


        public void Resize(Vector2 difference)
        {
            Console.WriteLine(this._Texture.Width);
            adJustedWidth += (int)difference.X;
        }

        public virtual void ProcessClick(Vector2 pos)
        {
        }

        public bool CheckForEdgeClicked(Vector2 pos)
        {
            bool track = false;
            if (_TopEdge.Contains(pos))
            {
                topTracked = true;
                track = true;
            }
            else if (_BottomEdge.Contains(pos))
            {

                yTracked = true;
                track = true;
            }
        

            if (_LeftEdge.Contains(pos))
            {
                leftTracked = true;
                track = true;
            }
            else if (_RightEdge.Contains(pos))
            {
                xTracked = true;
                track = true;
            }
            return track;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(_Draw)
            {
                Rectangle sr = new Rectangle((int)_Position.X, (int)_Position.Y, adJustedWidth, adjustedHeight);
                spriteBatch.Draw(_Texture, sr, Color.White);

                if(_Resizable)
                {
                    spriteBatch.Draw(edgeTex, _TopEdge, Color.White);
                    spriteBatch.Draw(edgeTex, _BottomEdge, Color.White);
                    spriteBatch.Draw(edgeTex, _LeftEdge, Color.White);
                    spriteBatch.Draw(edgeTex, _RightEdge, Color.White);
                }
                
            }
        }

        internal void MarkToTrack(MouseState mState)
        {
            TrackMouse = true;
            prevMousePos = mState;
        }
    }
}
