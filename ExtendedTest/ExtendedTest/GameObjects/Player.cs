﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest
{
    public class Player : Character
    {
        Vector2 Destination = Vector2.Zero;
        InventoryManager invenManager;
        Sprite _Target;
        
        public enum CurrentState
        {
            kStateIdle = 0,
            kStateWalk,
            kStateWC,
            kStateRun,
            kStateFish,
            kStateMine,
            kStateAttack,
            kStateDefend
        }

        private CurrentState action = CurrentState.kStateIdle;
        

        public Player(InventoryManager manager, CombatManager cbManager) : base(cbManager)
        {
            this.startHP = 10;
            _HP = 10;
            invenManager = manager;
            defense = 5;
            attack = 6;
            attackCD = 2;
            attackSpeed = 2;
            _Speed = 1f;
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            SetupAnimation(2, 30, 3, true);
        }

        public override void UpdateActive(GameTime gameTime)
        {
            Vector2 originalPos = _Position;
            movingX = false;
            movingY = false;
            handleInput(gameTime);

            if(_Direction == Direction.kDirectionNone)
            {
                this.ChangeState(CurrentState.kStateIdle);
            }
            else
            {
                this.ChangeState(CurrentState.kStateWalk);
            }

            if(_Target != null)
            {
                
            }

            base.UpdateActive(gameTime);
        }

        private void handleInput(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if(!atDestination)
            //{
            //    base.findPath();
            //}

            #region Keyboard State
            KeyboardState state = Keyboard.GetState();
            bool moving = false;
            _Direction = Direction.kDirectionNone;
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                //_Position.X -= _Speed;
                _Direction = Direction.kDirectionLeft;
            }
            else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                //_Position.X += _Speed;
                _Direction = Direction.kDirectionRight;
            }
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                //_Position.Y -= _Speed;
                _Direction = Direction.kDirectionUp;
            }
            else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                //_Position.Y += _Speed;
                _Direction = Direction.kDirectionDown;
            }
            #endregion
            #region Gamepad state
            /* GamePad Stuff
            GamePadCapabilities cap = GamePad.GetCapabilities(PlayerIndex.One);

            if (cap.IsConnected && cap.HasLeftXThumbStick && cap.HasLeftYThumbStick && cap.HasRightXThumbStick && cap.HasRightYThumbStick)
            {
                GamePadState gpState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
                _Position.X += (maxSpeed * gpState.ThumbSticks.Left.X);
                _Position.Y += (maxSpeed * -gpState.ThumbSticks.Left.Y);
                if (gpState.ThumbSticks.Right.X == 0 && gpState.ThumbSticks.Right.Y == 0)
                {
                }
                
            } 
            */
            #endregion

            //LockInBounds();
        }

        public override void setDestination(Vector2 dest)
        {
            base.setDestination(dest);
            _Target = null;
            this.ChangeState(CurrentState.kStateWalk);
        }

        //private Sprite collisionCheck(List<Character> gameObjectList)
        //{
        //    foreach (Character sprite in gameObjectList)
        //    {
        //        if (sprite._CurrentState == SpriteState.kStateActive)
        //        {
        //            if (_BoundingBox.Intersects(sprite._BoundingBox))
        //            {
        //                if(movingX)
        //                {
        //                    if((_BoundingBox.Right > sprite._BoundingBox.Left) && (_BoundingBox.Left < sprite._BoundingBox.Left))
        //                    {
        //                        Console.WriteLine("On player right?");
        //                    }
        //                    else if ((_BoundingBox.Left < sprite._BoundingBox.Right) && (_BoundingBox.Right > sprite._BoundingBox.Right))
        //                    {
        //                        Console.WriteLine("On player left?");
        //                    }
        //                }

        //                if(movingY)
        //                {
        //                    if ((_BoundingBox.Bottom > sprite._BoundingBox.Top) && (_BoundingBox.Top < sprite._BoundingBox.Top))
        //                    {
        //                        Console.WriteLine("On player bottom?");
        //                    }
        //                    else if ((_BoundingBox.Top < sprite._BoundingBox.Bottom) && (_BoundingBox.Bottom > sprite._BoundingBox.Bottom))
        //                    {
        //                        Console.WriteLine("On player top?");
        //                    }
        //                }

        //                return sprite;
        //            }
        //        }
        //    }
        //    return null;
        //}

        private void Chop(Tree tree)
        {
            Item item = tree.getChopped();
            if (item != null)
            {
                invenManager.AddItem(item);
                this.ChangeState(CurrentState.kStateWC);
                if (this._Target._CurrentState == SpriteState.kStateInActive)
                {
                    this._Target = null;
                }
            }
        }
        private void Mine(Rock rock)
        {
            Item item = rock.getChopped();
            if (item != null)
            {
                invenManager.AddItem(item);
                this.ChangeState(CurrentState.kStateWC);
                if (this._Target._CurrentState == SpriteState.kStateInActive)
                {
                    this._Target = null;
                }
            }
        }

        public void stopAction()
        {
            this.ChangeState(CurrentState.kStateIdle);
        }

        public void SetTarget(Sprite target)
        {
            this._Target = target;
        }

        public void ChangeState(CurrentState action)
        {
            if(this.action != action)
            {
                this.action = action;
                ChangeAnimation((int)this.action);
            }
        }
    }
}
