using Microsoft.Xna.Framework;
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

        public Sword sword;

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

        Texture2D arrowTex;
        Texture2D mageTex;
        public List<Projectile> _ActiveArrows;
        List<Projectile> _DeadArrows;
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
            _ActiveArrows = new List<Projectile>();
            _DeadArrows = new List<Projectile>();
            for(int i = 0; i < 50; i++)
            {
                Projectile arrow = new Projectile();
                arrow._CurrentState = SpriteState.kStateInActive;
                _DeadArrows.Add(arrow);
                AddChild(arrow);
            }
            Animation idle = new Animation("Idle", 64, 64, 2, 2);
            AddAnimation(idle);
            Animation walk = new Animation("Walk", 64, 64, 2, 2);
            AddAnimation(walk);
            Animation WC = new Animation("WC", 64, 64, 2, 2);
            AddAnimation(WC);

        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            arrowTex = content.Load<Texture2D>("Art/Sword");
            SetupAnimation(2, 10, 3, true);
            sword.LoadContent("Art/Sword", content);
            //sword.ToggleCorners();
            swordTip.LoadContent("Art/Collision", content);
            swordTip.frameHeight = 16;
            swordTip.frameWidth = 16;
            foreach(Projectile arrow in _DeadArrows)
            {
                arrow.LoadContent("Art/Sword", content);
            }
            
        }

        protected override void UpdateActive(GameTime gameTime)
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

            List<Projectile> allArrows = new List<Projectile>();
            allArrows.AddRange(_ActiveArrows);
            allArrows.AddRange(_DeadArrows);
            _ActiveArrows.Clear();
            _DeadArrows.Clear();
            _ActiveArrows = allArrows.FindAll(x => x._CurrentState == SpriteState.kStateActive);
            _DeadArrows = allArrows.FindAll(x => x._CurrentState == SpriteState.kStateInActive);

            base.UpdateActive(gameTime);
            CheckAnimations();
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

        internal void RangedAttack()
        {
            if (comboAdvanceCD > 0) return;
            _PlayerAttacking = true;
            if (comboCD > 0)
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
            if (comboNum == 1)
            {
                //loop through dead arrows and activate one.
                foreach (Projectile arrow in _DeadArrows)
                {
                    if (arrow._CurrentState == SpriteState.kStateActive) continue;
                    arrow.Activate(this._Position);
                    float rad = 0;

                    if (this._Direction == Direction.kDirectionRight)
                    {
                        arrow.setDirection(new Vector2(1, 0));
                    }
                    else if (this._Direction == Direction.kDirectionLeft)
                    {
                        arrow.setDirection(new Vector2(-1, 0));
                    }
                    else if (this._Direction == Direction.kDirectionDown)
                    {
                        arrow.setDirection(new Vector2(0, 1));
                    }
                    else if (this._Direction == Direction.kDirectionUp)
                    {
                        arrow.setDirection(new Vector2(0, -1));
                    }
                    break;
                }

            }
            else if(comboNum == 2)
            {
                int arrowsShot = 0;
                //loop through dead arrows and activate one.
                foreach (Projectile arrow in _DeadArrows)
                {
                    if (arrow._CurrentState == SpriteState.kStateActive)
                    {
                        continue;
                    }
                    arrow.Activate(this._Position);
                    float rad = 0;

                    if(arrowsShot == 0)
                    {
                        if (this._Direction == Direction.kDirectionRight)
                        {
                            arrow.setDirection(new Vector2(1, 0));
                        }
                        else if (this._Direction == Direction.kDirectionLeft)
                        {
                            arrow.setDirection(new Vector2(-1, 0));
                        }
                        else if (this._Direction == Direction.kDirectionDown)
                        {
                            arrow.setDirection(new Vector2(0, 1));
                        }
                        else if (this._Direction == Direction.kDirectionUp)
                        {
                            arrow.setDirection(new Vector2(0, -1));
                        }
                        arrowsShot++;
                    }
                    else if(arrowsShot == 1)
                    {
                        if (this._Direction == Direction.kDirectionRight)
                        {
                            arrow.setDirection(new Vector2(1, -1));
                        }
                        else if (this._Direction == Direction.kDirectionLeft)
                        {
                            arrow.setDirection(new Vector2(-1, -1));
                        }
                        else if (this._Direction == Direction.kDirectionDown)
                        {
                            arrow.setDirection(new Vector2(-1, 1));
                        }
                        else if (this._Direction == Direction.kDirectionUp)
                        {
                            arrow.setDirection(new Vector2(-1, -1));
                        }
                        arrowsShot++;
                    }
                    else if (arrowsShot == 2)
                    {
                        if (this._Direction == Direction.kDirectionRight)
                        {
                            arrow.setDirection(new Vector2(1, 1));
                        }
                        else if (this._Direction == Direction.kDirectionLeft)
                        {
                            arrow.setDirection(new Vector2(-1, 1));
                        }
                        else if (this._Direction == Direction.kDirectionDown)
                        {
                            arrow.setDirection(new Vector2(1, 1));
                        }
                        else if (this._Direction == Direction.kDirectionUp)
                        {
                            arrow.setDirection(new Vector2(1, -1));
                        }
                        arrowsShot++;
                    }
                    if(arrowsShot == 3)
                    {
                        break;
                    }
                }
            }
            else if(comboNum == 3)
            {
                int arrowsShot = 0;
                //loop through dead arrows and activate one.
                foreach (Projectile arrow in _DeadArrows)
                {
                    if (arrow._CurrentState == SpriteState.kStateActive)
                    {
                        continue;
                    }
                    arrow.Activate(this._Position);
                    float rad = 0;

                    if (arrowsShot == 0)
                    {
                        if (this._Direction == Direction.kDirectionRight)
                        {
                            arrow.setDirection(new Vector2(1, 0));
                        }
                        else if (this._Direction == Direction.kDirectionLeft)
                        {
                            arrow.setDirection(new Vector2(-1, 0));
                        }
                        else if (this._Direction == Direction.kDirectionDown)
                        {
                            arrow.setDirection(new Vector2(0, 1));
                        }
                        else if (this._Direction == Direction.kDirectionUp)
                        {
                            arrow.setDirection(new Vector2(0, -1));
                        }
                        arrowsShot++;
                    }
                    else if (arrowsShot == 1)
                    {
                        if (this._Direction == Direction.kDirectionRight)
                        {
                            arrow.setDirection(new Vector2(1, -1));
                        }
                        else if (this._Direction == Direction.kDirectionLeft)
                        {
                            arrow.setDirection(new Vector2(-1, -1));
                        }
                        else if (this._Direction == Direction.kDirectionDown)
                        {
                            arrow.setDirection(new Vector2(-1, 1));
                        }
                        else if (this._Direction == Direction.kDirectionUp)
                        {
                            arrow.setDirection(new Vector2(-1, -1));
                        }
                        arrowsShot++;
                    }
                    else if (arrowsShot == 2)
                    {
                        if (this._Direction == Direction.kDirectionRight)
                        {
                            arrow.setDirection(new Vector2(1, 1));
                        }
                        else if (this._Direction == Direction.kDirectionLeft)
                        {
                            arrow.setDirection(new Vector2(-1, 1));
                        }
                        else if (this._Direction == Direction.kDirectionDown)
                        {
                            arrow.setDirection(new Vector2(1, 1));
                        }
                        else if (this._Direction == Direction.kDirectionUp)
                        {
                            arrow.setDirection(new Vector2(1, -1));
                        }
                        arrowsShot++;
                    }
                    else if (arrowsShot == 3)
                    {
                        if (this._Direction == Direction.kDirectionRight)
                        {
                            arrow.setDirection(new Vector2(1, 0.5f));
                        }
                        else if (this._Direction == Direction.kDirectionLeft)
                        {
                            arrow.setDirection(new Vector2(-1, 0.5f));
                        }
                        else if (this._Direction == Direction.kDirectionDown)
                        {
                            arrow.setDirection(new Vector2(0.5f, 1));
                        }
                        else if (this._Direction == Direction.kDirectionUp)
                        {
                            arrow.setDirection(new Vector2(0.5f, -1));
                        }
                        arrowsShot++;
                    }
                    else if (arrowsShot == 4)
                    {
                        if (this._Direction == Direction.kDirectionRight)
                        {
                            arrow.setDirection(new Vector2(1, -0.5f));
                        }
                        else if (this._Direction == Direction.kDirectionLeft)
                        {
                            arrow.setDirection(new Vector2(-1, -0.5f));
                        }
                        else if (this._Direction == Direction.kDirectionDown)
                        {
                            arrow.setDirection(new Vector2(-0.5f, 1));
                        }
                        else if (this._Direction == Direction.kDirectionUp)
                        {
                            arrow.setDirection(new Vector2(-0.5f, -1));
                        }
                        arrowsShot++;
                    }
                    if (arrowsShot == 5)
                    {
                        break;
                    }
                }
            }
            //set it's ehading to correct direction
        }
        
        public void CheckAnimations()
        {
            switch(_MyState)
            {
                case Managers.PlayerManager.PlayerState.kStateIdle:
                    ChangeAnimation("Idle");
                    break;
                case Managers.PlayerManager.PlayerState.kStateWalk:
                    ChangeAnimation("Walk");
                    break;
                case Managers.PlayerManager.PlayerState.kStateWC:
                    ChangeAnimation("WC");
                    break;
                default:
                    ChangeAnimation("Idle");
                    break;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //swordTip.Draw(spriteBatch);
        }
    }
}
