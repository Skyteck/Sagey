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

    public class Sprite
    {
        public Texture2D _Texture;
        public Vector2 _Position;
        public bool _Draw = true;
        public bool _LockInScreen = false;
        //for inheritance
        public Sprite parent = null;
        public List<Sprite> _ChildrenList;

        //for animation
        public int frameWidth;
        public int frameHeight;
        public bool _FlipX = false;
        public bool _FlipY = false;
        public float _zOrder;
        public Vector2 _Scale = new Vector2(1,1);
        public Color _MyColor = Color.White;
        public float _Rotation = 0.0f;
        public float _Opacity = 1.0f;
        public string Name;
        public bool _IsMoving = false;

        ////Collision helper:
        //List<Sprite> corners;
        //bool showCorners = false;
        //Texture2D cornerTex;
        public enum SpriteState
        {
            kStateActive,
            kStateInActive
        }

        public SpriteState _CurrentState = SpriteState.kStateActive;

        public enum SpriteType
        {
            kPlayerType,
            kTreeType,
            kRockType,
            kNoneType,
            kMonsterType,
            kFishingType,
            kFireType,
            kNPCType
        }

        public enum Direction
        {
            kDirectionUp,
            kDirectionDown,
            kDirectionLeft,
            kDirectionRight,
            kDirectionNone
        }

        public Direction _Direction = Direction.kDirectionNone;

        private SpriteType tag = SpriteType.kNoneType;

        public virtual Vector2 _Center
        {
            get
            {
                return new Vector2(frameWidth / 2, frameHeight / 2);
            }
        }

        public virtual Vector2 _TopLeft
        {
            get
            {
                return new Vector2((int)_Position.X - (frameWidth / 2), ((int)_Position.Y - (frameHeight / 2)));
            }
        }

        public virtual Rectangle _BoundingBox
        {
            get
            {
                return new Rectangle((int)_TopLeft.X, ((int)_TopLeft.Y), frameWidth, frameHeight);
            }
        }

        public Rectangle _WorldBoundingBox
        {
            get
            {
                return new Rectangle((int)_TopLeft.X + 6, ((int)_TopLeft.Y + 6), frameWidth - 12, frameHeight - 12);
            }
        }



        public SpriteType _Tag { get => tag; set => tag = value; }



        public Sprite()
        {
            _ChildrenList = new List<Sprite>();
            //corners = new List<Sprite>();
        }

        public virtual void LoadContent(string path, ContentManager content)
        {
            _Texture = content.Load<Texture2D>(path);
            frameHeight = _Texture.Height;
            frameWidth = _Texture.Width;
            //cornerTex = content.Load<Texture2D>("Art/Corner");


            //List<Vector2> playerCorners = HelperFunctions.RotatedRectList(this._BoundingBox, _Rotation);
            //Sprite topLeft = new Sprite();
            //topLeft._Position = new Vector2(playerCorners[0].X, playerCorners[0].Y);
            //topLeft.LoadContent(cornerTex);
            //corners.Add(topLeft);
            //Sprite topRight = new Sprite();
            //topRight._Position = new Vector2(playerCorners[1].X, playerCorners[1].Y);
            //topRight.LoadContent(cornerTex);
            //corners.Add(topRight);
            //Sprite bottomLeft = new Sprite();
            //bottomLeft._Position = new Vector2(playerCorners[2].X, playerCorners[2].Y);
            //bottomLeft.LoadContent(cornerTex);
            //corners.Add(bottomLeft);
            //Sprite bottomRight = new Sprite();
            //bottomRight._Position = new Vector2(playerCorners[3].X, playerCorners[3].Y);
            //bottomRight.LoadContent(cornerTex);
            //corners.Add(bottomRight);

        }
        public virtual void LoadContent(Texture2D tex)
        {
            _Texture = tex;
            frameHeight = _Texture.Height;
            frameWidth = _Texture.Width;
        }

        public virtual void Update(GameTime gt)
        {
            if(_CurrentState == SpriteState.kStateActive)
            {
                UpdateActive(gt);
            }
            else
            {
                UpdateDead(gt);
            }
        }


        public virtual void UpdateActive(GameTime gameTime)
        {
            if (_CurrentState == SpriteState.kStateActive)
            {
                if (_ChildrenList != null)
                {
                    if (_ChildrenList.Count >= 1)
                    {
                        foreach (Sprite child in _ChildrenList)
                        {
                            child.Update(gameTime);
                        }
                    }
                }

                if (_LockInScreen)
                {
                    LockInBounds();
                }


                //if (showCorners)
                //{
                //    UpdateCorners();
                //}
            }
        }

        public virtual void UpdateDead(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_Draw)
            {
                //Rectangle sr = new Rectangle((frameWidth * frameNum), (frameHeight * StateNum), frameWidth, frameHeight);
                Rectangle sr = new Rectangle(0, 0, frameWidth, frameHeight);
                Draw(spriteBatch, sr);
            }
        }


        public virtual void Draw(SpriteBatch spriteBatch, Rectangle sr)
        {
            if (_Draw)
            {
                //Rectangle sr = new Rectangle((frameWidth * frameNum), (frameHeight * StateNum), frameWidth, frameHeight);
                if (!_FlipX && !_FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), _Rotation, _Center, _Scale, SpriteEffects.None, 0f);
                }
                else if (_FlipX)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), _Rotation, _Center, _Scale, SpriteEffects.FlipHorizontally, 0f);
                }
                else if (_FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), _Rotation, _Center, _Scale, SpriteEffects.FlipVertically, 0f);
                }
                else if (_FlipX && _FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, new Color(_MyColor, _Opacity), (_Rotation + (float)Math.PI), _Center, _Scale, SpriteEffects.None, 0f);
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
                
                //if (showCorners)
                //{
                //    foreach (Sprite sprite in corners)
                //    {
                //        sprite.Draw(spriteBatch);
                //    }
                //}
            }
        }

        public void AddChild(Sprite child)
        {
            child.parent = this;
            _ChildrenList.Add(child);
        }

        public virtual void LockInBounds()
        {
            if ((_Position.X - (frameWidth / 2)) <= 0)
            {
                _Position.X = frameWidth / 2;
            }
            if ((_Position.X + (frameWidth / 2)) > 320)
            {
                _Position.X = 320 - (frameWidth / 2);
            }
        }
        public void ChangeColor(Color searchColor, Color toColor)
        {
            Color[] data = new Color[_Texture.Width * _Texture.Height];
            _Texture.GetData(data);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == searchColor)
                {
                    data[i] = toColor;
                }
            }

            _Texture.SetData(data);
        }

        public virtual void ResetSelf()
        {
            _Texture = null;
            _Position = Vector2.Zero;
            _Draw = true;
            _CurrentState = SpriteState.kStateActive;
            _Tag = SpriteType.kNoneType;
            _Rotation = 0.0f;
            _Scale = new Vector2(1, 1);
            _FlipX = false;
            _FlipY = false;
            _LockInScreen = false;
            if (_ChildrenList != null)
            { 
                _ChildrenList.Clear();
            }
            _MyColor = Color.White;
            parent = null;
            Setup();
        }

        public virtual void Setup()
        {

        }

        public virtual void Activate()
        {
            _CurrentState = SpriteState.kStateActive;
            _Draw = true;
        }

        public virtual void Activate(Vector2 pos)
        {
            _Position = pos;
            Activate();
        }

        public virtual void Deactivate()
        {
            _CurrentState = SpriteState.kStateInActive;
            _Draw = false;
        }
        

        //public void ToggleCorners()
        //{
        //    if(showCorners)
        //    {
        //        foreach(Sprite sprite in corners)
        //        {
        //            sprite.Deactivate();
        //        }
        //        showCorners = false;
        //    }
        //    else
        //    {
        //        UpdateCorners();
        //        showCorners = true;
        //    }
        //}


        //private void UpdateCorners()
        //{
        //    List<Vector2> myCorners = HelperFunctions.RotatedRectList(_BoundingBox, _Rotation);
        //    for (int i = 0; i < corners.Count; i++)
        //    {
        //        corners[i].Activate(new Vector2(myCorners[i].X, myCorners[i].Y));
        //    }
        //}
    }
}
