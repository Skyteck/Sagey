using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using ExtendedTest.GameObjects;

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

        public Managers.PlayerManager.PlayerState _MyState = Managers.PlayerManager.PlayerState.kStateIdle;

        //    Vector2 Destination = Vector2.Zero;
        //    Managers.InventoryManager invenManager;
        //    Sprite _Target;



        //    private Rectangle checkRect
        //    {
        //        get
        //        {
        //            if(_Direction==Direction.kDirectionDown)
        //            {
        //                return new Rectangle((int)this._Position.X - 32, (int)this._Position.Y + 32, 64, 32);
        //            }
        //            else if(_Direction==Direction.kDirectionUp)
        //            {
        //                return new Rectangle((int)this._Position.X - 32, (int)this._Position.Y - 64, 64, 32);
        //            }
        //            else if (_Direction == Direction.kDirectionLeft)
        //            {
        //                return new Rectangle((int)this._Position.X - 64, (int)this._Position.Y - 32, 32, 642);
        //            }
        //            else if (_Direction == Direction.kDirectionRight)
        //            {
        //                return new Rectangle((int)this._Position.X + 32, (int)this._Position.Y - 32, 32, 64);
        //            }
        //            return new Rectangle();
        //        }
        //    }

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
            sword = new Sword();
            sword.Deactivate();
            AddChild(sword);
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            SetupAnimation(2, 10, 3, true);
            sword.LoadContent("Art/Sword", content);
            
        }

        public override void UpdateActive(GameTime gameTime)
        {
            base.UpdateActive(gameTime);
        }
        
        public void Attack()
        {
            Vector2 swordPos;
            if (_Direction == Direction.kDirectionUp)
            {
                swordPos = new Vector2(_Position.X, _Position.Y - (frameHeight/2));
                sword.PointTo(swordPos, Sword.SwordPoint.kNorth);
            }
            else if (_Direction == Direction.kDirectionDown)
            {
                swordPos = new Vector2(_Position.X, _Position.Y + (frameHeight/2));
                sword.PointTo(swordPos, Sword.SwordPoint.kSouth);
            }
            else if (_Direction == Direction.kDirectionLeft)
            {
                swordPos = new Vector2(_Position.X - (frameWidth/2), _Position.Y);
                sword.PointTo(swordPos, Sword.SwordPoint.kWest);
            }
            else if (_Direction == Direction.kDirectionRight)
            {
                swordPos = new Vector2(_Position.X + (frameWidth/2), _Position.Y);
                sword.PointTo(swordPos, Sword.SwordPoint.kEast);
            }
            else
            {
                Console.WriteLine("Sword Error!");
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

        //    public WorldObject CheckObjectHit(List<WorldObject> spriteList)
        //    {
        //        foreach(WorldObject sprite in spriteList)
        //        {
        //            if(checkRect.Intersects(sprite._BoundingBox))
        //            {
        //                this.SetTarget(sprite);
        //                return sprite;
        //            }
        //        }
        //        return null;
        //    }

        //    internal void checkCharacterHit(List<Character> spriteListActive)
        //    {
        //        foreach(NPC npc in spriteListActive)
        //        {
        //            if(checkRect.Intersects(npc._BoundingBox))
        //            {
        //                this.SetTarget(npc);
        //            }
        //        }
        //    }
    }
}
