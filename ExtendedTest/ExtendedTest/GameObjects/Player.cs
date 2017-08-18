﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using ExtendedTest.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace ExtendedTest
{
    public class Player : AnimatedSprite
    {
        private int startHP;

        Sword sword;

        public int _HP { get; private set; }
        public int _Defense { get; private set; }
        public int _Attack { get; private set; }
        public int _AttackCD { get; private set; }
        public int _AttackSpeed { get; private set; }
        public float _Speed { get; private set; }
        public bool _PlayerAttacking { get; set; }

        private bool playerAttacking;

        public Managers.PlayerManager.PlayerState _MyState = Managers.PlayerManager.PlayerState.kStateIdle;
        
        public Vector2 _SwordAnchor
        {
            get
            {
                float x = _Position.X;
                float y = _Position.Y;
                Vector2 newPos;

                if(comboNum != 5)
                {
                    if(_Direction == Direction.kDirectionDown)
                    {
                        newPos = _Position;
                        newPos.Y += frameHeight;
                    }
                    else if(_Direction == Direction.kDirectionLeft)
                    {
                        newPos = _Position;
                        newPos.Y += frameHeight / 2;
                        newPos.X -= frameWidth / 2;
                    }
                    else if(_Direction == Direction.kDirectionRight)
                    {
                        newPos = _Position;
                        newPos.Y += frameHeight / 2;
                        newPos.X += frameWidth / 2;
                    }
                    else
                    {
                        newPos = _Position;
                    }
                }
                else
                {
                    newPos = _Position;

                }
                
                x = newPos.X;
                y = newPos.Y;
                int radias = frameWidth/2;
                double mathSin = Math.Sin(_Rotation);
                double mathCos = Math.Cos(_Rotation);
                newPos.X = (float)(x + (radias * mathSin));
                newPos.Y = (float)(y - (radias * mathCos));

                return newPos;
            }
        }


        int comboNum = 1;
        double comboCD = 0f;
        double comboAdvanceCD = 0.1;
        public Sprite swordTip;
        public Player()
        {
            this.startHP = 10;
            _HP = 10;
            _Defense = 5;
            _Attack = 6;
            _AttackCD = 2;
            _AttackSpeed = 2;
            _Speed = 4f;
            _Direction = Direction.kDirectionDown;
            sword = new Sword(this);
            sword.Deactivate();
            _Tag = SpriteType.kPlayerType;
            AddChild(sword);
            swordTip = new Sprite();
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            SetupAnimation(2, 10, 3, true);
            sword.LoadContent("Art/Sword", content);
            //sword.ToggleCorners();
            swordTip.LoadContent("Art/Collision", content);
            swordTip.frameHeight = 16;
            swordTip.frameWidth = 16;
        }

        public override void UpdateActive(GameTime gameTime)
        {
            if(comboCD > 0f)
            {
                comboCD -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                comboNum = 1;
                _Rotation = 0;
                _PlayerAttacking = false;
            }
            if(comboNum == 5)
            {
                this._Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 30;
            }
            base.UpdateActive(gameTime);

            if(sword._CurrentState == SpriteState.kStateActive)
            {
                swordTip._CurrentState = SpriteState.kStateActive;
                swordTip._Position = sword._SwordTip;
            }
            else
            {
                swordTip._CurrentState = SpriteState.kStateInActive;
            }

            if(comboAdvanceCD > 0)
            {
                comboAdvanceCD -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

        }

        public void Attack()
        {
            if (comboAdvanceCD > 0) return;
            _PlayerAttacking = true;
            Vector2 swordPos;
            if(comboCD > 0)
            {
                comboNum++;
                if (comboNum == 6)
                {
                    comboNum = 1;
                }
            }
            else
            {
                comboNum = 1;
            }
            comboCD = 0.42;
            comboAdvanceCD = 0.2;
            if(comboNum == 1)
            {
                _Rotation = 0f;
                if (_Direction == Direction.kDirectionUp)
                {
                    sword.Attack1(Sword.SwordPoint.kNorth);
                }
                else if (_Direction == Direction.kDirectionDown)
                {
                    sword.Attack1(Sword.SwordPoint.kSouth);
                }
                else if (_Direction == Direction.kDirectionLeft)
                {
                    sword.Attack1(Sword.SwordPoint.kWest);
                }
                else if (_Direction == Direction.kDirectionRight)
                {
                    sword.Attack1(Sword.SwordPoint.kEast);
                }
                else
                {
                    Console.WriteLine("Sword Error!");
                }

            }
            else if(comboNum == 2)
            {
                _Rotation = 0f;
                if (_Direction == Direction.kDirectionUp)
                {
                    sword.Attack2(Sword.SwordPoint.kNorth);
                }
                else if (_Direction == Direction.kDirectionDown)
                {
                    sword.Attack2(Sword.SwordPoint.kSouth);
                }
                else if (_Direction == Direction.kDirectionLeft)
                {
                    sword.Attack2(Sword.SwordPoint.kWest);
                }
                else if (_Direction == Direction.kDirectionRight)
                {
                    sword.Attack2(Sword.SwordPoint.kEast);
                }
                else
                {
                    Console.WriteLine("Sword Error!");
                }
            }
            else if (comboNum == 3)
            {
                _Rotation = 0f;
                if (_Direction == Direction.kDirectionUp)
                {
                    sword.Attack1(Sword.SwordPoint.kNorth);
                }
                else if (_Direction == Direction.kDirectionDown)
                {
                    sword.Attack1(Sword.SwordPoint.kSouth);
                }
                else if (_Direction == Direction.kDirectionLeft)
                {
                    sword.Attack1(Sword.SwordPoint.kWest);
                }
                else if (_Direction == Direction.kDirectionRight)
                {
                    sword.Attack1(Sword.SwordPoint.kEast);
                }
                else
                {
                    Console.WriteLine("Sword Error!");
                }

            }
            else if (comboNum == 4)
            {
                _Rotation = 0f;
                if (_Direction == Direction.kDirectionUp)
                {
                    sword.Attack2(Sword.SwordPoint.kNorth);
                }
                else if (_Direction == Direction.kDirectionDown)
                {
                    sword.Attack2(Sword.SwordPoint.kSouth);
                }
                else if (_Direction == Direction.kDirectionLeft)
                {
                    sword.Attack2(Sword.SwordPoint.kWest);
                }
                else if (_Direction == Direction.kDirectionRight)
                {
                    sword.Attack2(Sword.SwordPoint.kEast);
                }
                else
                {
                    Console.WriteLine("Sword Error!");
                }

            }
            else if(comboNum == 5)
            {
                sword.Attack3();
            }
        }

        //    //private Sprite collisionCheck(List<Character> gameObjectList)
        //    //{
        //    //    foreach (Character sprite in gameObjectList)
        //    //    {
        //    //        if (sprite._CurrentState == SpriteState.kStateActive)
        //    //        {
        //    //            if (_BoundingBox.Intersects(sprite._BoundingBox))
        //    //            {
        //    //                if(movingX)
        //    //                {
        //    //                    if((_BoundingBox.Right > sprite._BoundingBox.Left) && (_BoundingBox.Left < sprite._BoundingBox.Left))
        //    //                    {
        //    //                        Console.WriteLine("On player right?");
        //    //                    }
        //    //                    else if ((_BoundingBox.Left < sprite._BoundingBox.Right) && (_BoundingBox.Right > sprite._BoundingBox.Right))
        //    //                    {
        //    //                        Console.WriteLine("On player left?");
        //    //                    }
        //    //                }

        //    //                if(movingY)
        //    //                {
        //    //                    if ((_BoundingBox.Bottom > sprite._BoundingBox.Top) && (_BoundingBox.Top < sprite._BoundingBox.Top))
        //    //                    {
        //    //                        Console.WriteLine("On player bottom?");
        //    //                    }
        //    //                    else if ((_BoundingBox.Top < sprite._BoundingBox.Bottom) && (_BoundingBox.Bottom > sprite._BoundingBox.Bottom))
        //    //                    {
        //    //                        Console.WriteLine("On player top?");
        //    //                    }
        //    //                }

        //    //                return sprite;
        //    //            }
        //    //        }
        //    //    }
        //    //    return null;
        //    //}

        //    private void Gather(GameObjects.Objects.Gatherables.Gatherable thing)
        //    {
        //        Item.ItemType itemType = thing.GetGathered();
        //        if (itemType != Item.ItemType.kItemNone && itemType != Item.ItemType.kItemError)
        //        {
        //            invenManager.AddItem(itemType);
        //            this.ChangeState(CurrentState.kStateWC);
        //            if (this._Target._CurrentState == SpriteState.kStateInActive)
        //            {
        //                this._Target = null;
        //            }
        //        }
        //    }

        //    public void stopAction()
        //    {
        //        this.ChangeState(CurrentState.kStateIdle);
        //    }

        //    public void SetTarget(Sprite target)
        //    {
        //        this._Target = target;
        //    }

        public void ChangeState(Managers.PlayerManager.PlayerState action)
        {
            if (_MyState != action)
            {
                _MyState = action;
                ChangeAnimation((int)_MyState);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //swordTip.Draw(spriteBatch);
        }
    }
}
