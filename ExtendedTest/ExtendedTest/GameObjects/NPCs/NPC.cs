using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sagey
{
    /// <summary>
    /// A NPC is any object in the game world that can move around using its own logic. FindPath is provided to get the character where it's going.
    /// For example the player's destination is set by clicking on the world. NPCs use their logic to determien their destination
    /// findPath gets things where they need to be.
    /// </summary>
    public class NPC : AnimatedSprite
    {
        public List<NPC> parentList;

        protected Managers.NPCManager _NPCManager;
        double currentMoveTimer = 6f;
        Vector2 Destination;
        public bool atDestination = true;
        public bool movingX = false;
        public bool movingY = false;
        public bool moving = false;

        public float _StartSpeed = 5f;
        public float _Speed = 5f;

        List<Tile> myPath;

        protected List<string> myDialog;
        public bool _Interactable = false;
        public string InteractText = string.Empty;
        public double LeftBoundary { get; private set; }
        public double RightBoundary { get; private set; }
        public double BottomBoundary { get; private set; }
        public double TopBoundary { get; private set; }

        public Tile NextTileInPath
        {
            get
            {
                if(myPath.Count>0)
                {
                    return myPath[0];
                }
                else
                {
                    return null;
                }
            }
        }

        //Combat stats
        public bool _CanFight = false;
        public bool _KnockedBack = false;
        public bool _Stunned = false;
        public bool _Invuln = false;
        public double attackSpeed = 3.0;
        public double attackCD = 0;
        public double _StunTime = 0;
        public double respawnTimerStart = 15;
        public double timeDead = 0d;
        public int startHP = 2;
        public int _HP = 2;
        public int defense;
        public int attack;
        public float attackRange = 64f; // tileWidth

        Rectangle huntZone;

        public Projectile myShot;


        bool Agressive = false;
        List<Sprite> _TargetList;


        private Texture2D effectTex;
        private Enums.EffectTypes _CurrentEffect = Enums.EffectTypes.kEffectNone;

#region collision rects
        public Rectangle _TopLeftRect
        {
            get
            {
                return new Rectangle((int)_TopLeft.X, (int)_TopLeft.Y, _ActiveAnim._FrameWidth / 2, _ActiveAnim._FrameHeight / 2);
            }
        }

        public Rectangle _TopRightRect
        {
            get
            {
                return new Rectangle((int)_Position.X, (int)_TopLeft.Y, _ActiveAnim._FrameWidth / 2, _ActiveAnim._FrameHeight / 2);
            }
        }

        public Rectangle _BottomLeftRect
        {
            get
            {
                return new Rectangle((int)_TopLeft.X, (int)_Position.Y, _ActiveAnim._FrameWidth / 2, _ActiveAnim._FrameHeight / 2);
            }
        }
        
        public Rectangle _BottomRightRect
        {
            get
            {
                return new Rectangle((int)_Position.X, (int)_Position.Y, _ActiveAnim._FrameWidth / 2, _ActiveAnim._FrameHeight / 2);
            }
        }
#endregion

        public enum AttackStyle
        {
            kMeleeStyle,
            kRangeStyle,
            kMagicStyle,
            kNoneStyle
        }

        public AttackStyle _AttackStyle = AttackStyle.kMeleeStyle;

        public NPC(Managers.NPCManager nm)
        {
            _NPCManager = nm;
            myPath = new List<Tile>();
            _TargetList = new List<Sprite>();
            Animation idle = new Animation("Idle", 64, 64, 1, 1);
            myDialog = new List<string>();
            //AddAnimation(idle);
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            //SetupAnimation(1, 1, 1, false);
            effectTex = content.Load<Texture2D>("Art/Effect");
        }

        protected override void UpdateActive(GameTime gameTime)
        {
            if(_StunTime > 0)
            {
                _StunTime -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                _KnockedBack = false;
            }
            if(_KnockedBack)
            {
                _Speed = 20f;
            }
            else
            {
                _Speed = _StartSpeed;
            }
            if (!atDestination)
            {
                FindPath();
            }
            if(_CanFight)
            {
                //UpdateCombat(gameTime);
            }

            base.UpdateActive(gameTime);
        }

        protected override void UpdateDead(GameTime gameTime)
        {
            if (_CurrentState == SpriteState.kStateInActive)
            {
                timeDead += gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timeDead >= respawnTimerStart)
            {
                Activate();
                timeDead = 0;
                _HP = startHP;
                ClearPath();
            }
            base.UpdateDead(gameTime);
        }

        public virtual void Interact()
        {

        }

        public void AddMessages(string MsgID)
        {
            this.myDialog.Add(MsgID);
        }

        #region movement stuff
        public void AddPath(List<Tile> path)
        {
            myPath.AddRange(path);
            if(myPath!= null)
            {
                SetDestination(myPath[0].tileCenter);
            }
        }

        public void AddToPath(Tile tile)
        {
            myPath.Add(tile);
            if (myPath != null)
            {
                SetDestination(myPath[0].tileCenter);
            }
        }

        public virtual void Roam(GameTime gameTime)
        {
            //the way I may want to do this now is checking if a random tile N,S,E,W of the 
            // NPC is open and in boundary and then moving to it
            currentMoveTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (currentMoveTimer <= 0)
            {
                Random num = new Random((int)this._Position.X);
                bool move = (num.Next() % 2 == 0) ? true : false;
                if (move)
                {
                    float newX = num.Next((int)this.LeftBoundary + this._Texture.Width / 2, (int)this.RightBoundary - this._Texture.Width / 2);
                    float newY = num.Next((int)this.TopBoundary + this._Texture.Height / 2, (int)this.BottomBoundary - this._Texture.Height / 2);
                    Tile tileGoal = _NPCManager._TilemapManager.findTile(new Vector2(newX, newY));
                    this.SetDestination(tileGoal.tileCenter);
                }

                currentMoveTimer += num.Next(0, 3);
            }
        }

        public void FindPath()
        {
            if (Math.Abs(Destination.X - _Position.X) > _Speed)
            {
                if (Destination.X > _Position.X)
                {
                    _Position.X += _Speed;
                    movingX = true;
                }
                else if (Destination.X < _Position.X)
                {
                    _Position.X -= _Speed;
                    movingX = true;
                }

            }

            if (Math.Abs(Destination.Y - _Position.Y) > _Speed)
            {
                if (Destination.Y > _Position.Y)
                {
                    _Position.Y += _Speed;
                    movingY = true;
                }
                else if (Destination.Y < _Position.Y)
                {
                    _Position.Y -= _Speed;
                    movingY = true;
                }
            }

            float distance = Vector2.Distance(Destination, _Position);
            if (distance <= _Speed + 1)
            {
                _Position = Destination;
                atDestination = true;
                if(myPath!= null)
                {
                    if (myPath.Count > 0)
                    {
                        SetDestination(myPath[0].tileCenter);
                        myPath.RemoveAt(0);
                    }
                }

            }
        }

        public void ClearPath()
        {
            myPath.Clear();
        }

        public virtual void SetDestination(Vector2 dest)
        {
            Destination = dest;
            atDestination = false;
        }

        public virtual void SetBoundaries(double lX, double rx, double by, double ty)
        {
            LeftBoundary = lX;
            RightBoundary = rx + lX;
            TopBoundary = ty;
            BottomBoundary = by + ty;
        }
#endregion

        #region Combat Stuff

        private void UpdateCombat(GameTime gameTime)
        {
            bool targetFound = false;
            if (_StunTime <= 0)
            {
                if (this.Agressive)
                {
                    foreach (Sprite target in _TargetList)
                    {
                        if (target._BoundingBox.Intersects(huntZone))
                        {
                            targetFound = true;
                            if (this._AttackStyle == AttackStyle.kMeleeStyle)
                            {
                                if(Vector2.Distance(this._Position, target._Position) > attackRange)
                                {
                                    this.SetDestination(target._Position);
                                }
                            }
                            if (Vector2.Distance(this._Position, target._Position) <= attackRange)
                            {
                                if (this.attackCD <= 0)
                                {
                                    if (this._AttackStyle == AttackStyle.kMeleeStyle)
                                    {
                                        _MyColor = Color.Blue;
                                    }
                                    else
                                    {
                                        //myShot.SetTarget(target);
                                    }
                                    attackCD = attackSpeed;
                                }
                            }
                        }
                        if (targetFound) break;
                    }
                }
                if (!targetFound)
                {
                    Roam(gameTime);
                }
                base.UpdateActive(gameTime);
            }


            if (attackCD > 0)
            {
                attackCD -= gameTime.ElapsedGameTime.TotalSeconds;
            }


            if (_StunTime > 0)
            {
                _StunTime -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                _Invuln = false;
                _Stunned = false;
                _MyColor = Color.White;
            }
        }

        public void AddTarget(Sprite target)
        {
            _TargetList.Add(target);
            this.Agressive = true;
            huntZone = new Rectangle((int)this.LeftBoundary, (int)this.TopBoundary, (int)(this.RightBoundary - this.LeftBoundary), (int)(this.BottomBoundary - this.TopBoundary));
        }

        public void AddTarget(List<Sprite> targetList)
        {
            _TargetList.AddRange(targetList);
            this.Agressive = true;
            huntZone = new Rectangle((int)this.LeftBoundary, (int)this.TopBoundary, (int)(this.RightBoundary - this.LeftBoundary), (int)(this.BottomBoundary - this.TopBoundary));
        }

        public virtual void ReceiveDamage(int amt)
        {

            if (_Invuln) return;
            int prevHP = _HP;
            _HP -= amt;
            _StunTime = 0.3;
            //_Invuln = true;
            //_Stunned = true;
            //_MyColor = Color.Red;
            if (_HP <= 0)
            {
                Die();
            }
        }

        public virtual void ReceiveDamage(int amt, Vector2 recoilVect)
        {
            ReceiveDamage(amt);

            _KnockedBack = true;
            _StunTime = 0.5f;
            ClearPath();
            _Speed = 20f;
            float x = (float)(recoilVect.X * 32);
            float y = (float)(recoilVect.Y * 32);
            Vector2 newDest = new Vector2(this._Position.X + x, this._Position.Y + y);
            SetDestination(newDest);
        }

        private void Knockback()
        {
            //this._Position.X += (float)(_Heading.X * (_Speed * gameTime.ElapsedGameTime.TotalSeconds));
            //this._Position.Y += (float)(_Heading.Y * (_Speed * gameTime.ElapsedGameTime.TotalSeconds));
        }

        private void Die()
        {
            _CurrentState = SpriteState.kStateInActive;
            _StunTime = 0;
            _Invuln = false;
            _Stunned = false;
            _Draw = false;
        }


        public void Tame()
        {
            _TargetList.Clear();
            this.Agressive = false;
        }

        public void SetStatus(Enums.EffectTypes effectType)
        {
            _CurrentEffect = effectType;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if(_CurrentEffect != Enums.EffectTypes.kEffectNone)
            //{
            //    switch (_CurrentEffect)
            //    {
            //        case Enums.EffectTypes.kEffectPoison:
            //            {
            //                _MyColor = Color.Green;
            //                break;
            //            }
            //        case Enums.EffectTypes.kEffectBurn:
            //            {
            //                _MyColor = Color.OrangeRed;
            //                break;
            //            }
            //        case Enums.EffectTypes.kEffectFreeze:
            //            {
            //                _MyColor = Color.AliceBlue;
            //                break;
            //            }
            //        case Enums.EffectTypes.kEffectStun:
            //            {
            //                _MyColor = Color.Yellow;
            //                break;
            //            }
            //    }
            //}

                base.Draw(spriteBatch);

            // draw the effectTex to show status effect
            if (_CurrentEffect != Enums.EffectTypes.kEffectNone)
            {
                Rectangle eR = new Rectangle((int)_TopLeft.X, (int)_TopLeft.Y, frameWidth, frameHeight);
                switch (_CurrentEffect)
                {
                    case Enums.EffectTypes.kEffectPoison:
                        {
                            spriteBatch.Draw(effectTex, eR, Color.Green);
                            break;
                        }
                    case Enums.EffectTypes.kEffectBurn:
                        {
                            spriteBatch.Draw(effectTex, eR, Color.OrangeRed);
                            break;
                        }
                    case Enums.EffectTypes.kEffectFreeze:
                        {
                            spriteBatch.Draw(effectTex, eR, Color.AliceBlue);
                            break;
                        }
                    case Enums.EffectTypes.kEffectStun:
                        {
                            spriteBatch.Draw(effectTex, eR, Color.Yellow);
                            break;
                        }
                }
            }
        }
    }
#endregion
}
