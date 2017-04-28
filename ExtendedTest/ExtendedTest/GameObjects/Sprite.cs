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
        public float speed = 0f;
        //for inheritance
        public Sprite parent = null;
        public List<Sprite> _ChildrenList;
        public bool enemy = false;
        //for animation
        public int frameWidth;
        public int frameHeight;
        public bool _FlipX = false;
        public bool _FlipY = false;
        public float _zOrder;
        public float _Scale = 1.0f;
        public Color _MyColor = Color.White;
        public float _Rotation = 0.0f;
        public List<Sprite> parentList;

        public enum SpriteState
        {
            kStateActive,
            kStateInActive
        }

        public SpriteState _CurrentState = SpriteState.kStateInActive;

        public enum SpriteType
        {
            kPlayerType,
            kTreeType,
            kRockType,
            kNoneType,
            kSlimeType
        }

        public SpriteType _Tag = SpriteType.kNoneType;

        public Vector2 _Center
        {
            get
            {
                return new Vector2(frameWidth / 2, frameHeight / 2);
            }
        }

        public Rectangle _BoundingBox
        {
            get
            {
                return new Rectangle((int)_Position.X, (int)_Position.Y, frameWidth, frameHeight);
            }
        }

        public virtual void LoadContent(string path, ContentManager content)
        {
            _Texture = content.Load<Texture2D>(path);
            frameHeight = _Texture.Height;
            frameWidth = _Texture.Width;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> gameObjectList)
        {
            if (_CurrentState == SpriteState.kStateActive)
            {
                if (_ChildrenList != null)
                {
                    if (_ChildrenList.Count >= 1)
                    {
                        foreach (Sprite child in _ChildrenList)
                        {
                            child.Update(gameTime, gameObjectList);
                        }
                    }
                }

                if (_LockInScreen)
                {
                    LockInBounds();
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_Draw)
            {
                //Rectangle sr = new Rectangle((frameWidth * frameNum), (frameHeight * StateNum), frameWidth, frameHeight);
                Rectangle sr = new Rectangle(0, 0, frameWidth, frameHeight);
                if (!_FlipX && !_FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, _Rotation, _Center, _Scale, SpriteEffects.None, 0f);
                }
                else if (_FlipX)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, _Rotation, _Center, _Scale, SpriteEffects.FlipHorizontally, 0f);
                }
                else if (_FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, _Rotation, _Center, _Scale, SpriteEffects.FlipVertically, 0f);
                }
                else if (_FlipX && _FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, (_Rotation + (float)Math.PI), _Center, _Scale, SpriteEffects.None, 0f);
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
            _Scale = 1.0f;
            _FlipX = false;
            _FlipY = false;
            _LockInScreen = false;
            if (_ChildrenList != null)
            { 
                _ChildrenList.Clear();
            }
            _MyColor = Color.White;
            parent = null;
            speed = 0f;
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
            _CurrentState = SpriteState.kStateActive;
            _Draw = true;

            _Position = pos;
        }
    }
}
