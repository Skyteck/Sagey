﻿using Microsoft.Xna.Framework;
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
        int minWidth = 15;
        int adJustedWidth;
        int minHeight = 15;
        int adjustedHeight;
        public Vector2 _InitialPos = Vector2.Zero;
        public bool _Showing = false;
        private bool TrackMouse = false;
        private bool xTracked = false;
        private bool yTracked = false;

        private MouseState prevMousePos;
        Vector2 prevScale;

        Texture2D edgeTex;

        public Vector2 _Center
        {
            get
            {
                return new Vector2(adJustedWidth / 2, adjustedHeight / 2);
            }
        }

        //new private Vector2 _TopLeft
        //{
        //    get
        //    {

        //    }
        //}

        private Rectangle _TopEdge
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

        private Rectangle _LeftEdge
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
            adjustedHeight = frameHeight;
            adJustedWidth = frameWidth;
        }

        protected override void UpdateActive(GameTime gt)
        {
            base.UpdateActive(gt);
            if(xTracked || yTracked)
            {
                MouseState mState = Mouse.GetState();

                if (prevMousePos.LeftButton == ButtonState.Pressed && mState.LeftButton == ButtonState.Released)
                {
                    xTracked = false;
                    yTracked = false;

                }
                else
                {
                    Vector2 currentPos = HelperFunctions.PointToVector(mState.Position);
                    Vector2 prevPos = HelperFunctions.PointToVector(prevMousePos.Position);
                    if(xTracked) adJustedWidth += (int)(currentPos.X - prevPos.X);
                    if (adJustedWidth < minWidth) adJustedWidth = minWidth;
                    if(yTracked) adjustedHeight += (int)(currentPos.Y - prevPos.Y);
                    if (adjustedHeight < minHeight) adjustedHeight = minHeight;
                    float yDiff = currentPos.Y - prevPos.Y;

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
                yTracked = true;
                track = true;
            }
            else if (_BottomEdge.Contains(pos))
            {

                yTracked = true;
                track = true;
            }
        

            if (_LeftEdge.Contains(pos))
            {
                xTracked = true;
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
                spriteBatch.Draw(edgeTex, _TopEdge, Color.White);
                spriteBatch.Draw(edgeTex, _BottomEdge, Color.White);
                spriteBatch.Draw(edgeTex, _LeftEdge, Color.White);
                spriteBatch.Draw(edgeTex, _RightEdge, Color.White);
            }
            //if (_Draw)
            //{
            //    //Rectangle sr = new Rectangle((frameWidth * frameNum), (frameHeight * StateNum), frameWidth, frameHeight);
            //    Rectangle sr = new Rectangle(0, 0, adJustedWidth, adjustedHeight);
            //    if (!_FlipX && !_FlipY)
            //    {
            //        spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), _Rotation, _Center, 1, SpriteEffects.None, 0f);
            //    }
            //    else if (_FlipX)
            //    {
            //        spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), _Rotation, _Center, 1, SpriteEffects.FlipHorizontally, 0f);
            //    }
            //    else if (_FlipY)
            //    {
            //        spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), _Rotation, _Center, 1, SpriteEffects.FlipVertically, 0f);
            //    }
            //    else if (_FlipX && _FlipY)
            //    {
            //        spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), (_Rotation + (float)Math.PI), _Center, 1, SpriteEffects.None, 0f);
            //    }

            //    if (_ChildrenList != null)
            //    {
            //        if (_ChildrenList.Count >= 1)
            //        {
            //            foreach (Sprite child in _ChildrenList)
            //            {
            //                child.Draw(spriteBatch);
            //            }
            //        }
            //    }
            //}
        }

        internal void MarkToTrack(MouseState mState)
        {
            TrackMouse = true;
            prevMousePos = mState;
        }
    }
}
