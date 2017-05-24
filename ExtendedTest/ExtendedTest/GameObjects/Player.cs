using Microsoft.Xna.Framework;
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
        
        private Rectangle checkRect
        {
            get
            {
                if(_Direction==Direction.kDirectionDown)
                {
                    return new Rectangle((int)this._Position.X - 32, (int)this._Position.Y + 32, 64, 32);
                }
                else if(_Direction==Direction.kDirectionUp)
                {
                    return new Rectangle((int)this._Position.X - 32, (int)this._Position.Y - 64, 64, 32);
                }
                else if (_Direction == Direction.kDirectionLeft)
                {
                    return new Rectangle((int)this._Position.X - 64, (int)this._Position.Y - 32, 32, 642);
                }
                else if (_Direction == Direction.kDirectionRight)
                {
                    return new Rectangle((int)this._Position.X + 32, (int)this._Position.Y - 32, 32, 64);
                }
                return new Rectangle();
            }
        }

        public Player(InventoryManager manager, CombatManager cbManager) : base(cbManager)
        {
            this.startHP = 10;
            _HP = 10;
            invenManager = manager;
            defense = 5;
            attack = 6;
            attackCD = 2;
            attackSpeed = 2;
            _Speed = 4f;
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

            if(moving)
            {
                this.ChangeState(CurrentState.kStateIdle);
            }
            else
            {
                this.ChangeState(CurrentState.kStateWalk);
            }

            if(_Target != null)
            {

                if (_Target._Tag == SpriteType.kMonsterType)
                {
                    if (Vector2.Distance(_Target._Position, this._Position) <= attackRange)
                    {
                        _CBManager.PerformAttack(this, _Target as Character);
                        atDestination = true;
                    }
                }
                if (_Target._Tag == SpriteType.kRockType)
                {
                    Mine(_Target as Rock);
                    this.ChangeState(CurrentState.kStateWC);
                }
                else if (_Target._Tag == SpriteType.kTreeType)
                {
                    Chop(_Target as Tree);
                    this.ChangeState(CurrentState.kStateWC);
                }
                else if (_Target._Tag == SpriteType.kFishingType)
                {
                    Fish(_Target as FishingHole);
                    this.ChangeState(CurrentState.kStateWC);
                }
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
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                //_Position.X -= _Speed;
                _Direction = Direction.kDirectionLeft;
                moving = true;
            }
            else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                //_Position.X += _Speed;
                _Direction = Direction.kDirectionRight;
                moving = true;
            }
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                //_Position.Y -= _Speed;
                _Direction = Direction.kDirectionUp;
                moving = true;
            }
            else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                //_Position.Y += _Speed;
                _Direction = Direction.kDirectionDown;
                moving = true;
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
                if (this._Target._CurrentState == SpriteState.kStateInActive)
                {
                    this._Target = null;
                }
            }
        }

        private void Fish(FishingHole fishHole)
        {
            Item item = fishHole.getFished();
            if (item != null)
            {
                invenManager.AddItem(item);
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

        public WorldObject CheckObjectHit(List<WorldObject> spriteList)
        {
            foreach(WorldObject sprite in spriteList)
            {
                if(checkRect.Intersects(sprite._BoundingBox))
                {
                    this.SetTarget(sprite);
                    return sprite;
                }
            }
            return null;
        }

        internal void checkCharacterHit(List<Character> spriteListActive)
        {
            foreach(NPC npc in spriteListActive)
            {
                if(checkRect.Intersects(npc._BoundingBox))
                {
                    this.SetTarget(npc);
                }
            }
        }
    }
}
