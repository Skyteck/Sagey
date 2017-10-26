using ExtendedTest.GameObjects.Gatherables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{
    public class PlayerManager
    {
        public event EventHandler BankOpened;
        public event EventHandler PlayerMoved;

        Player _Player;
        //Managers
        InventoryManager _InventoryManager;
        WorldObjectManager _WorldObjectManager;
        GatherableManager _GatherManager;
        NPCManager _NPCManager;
        TilemapManager _MapManager;
        public Vector2 _PlayerPos;


        //Gathering variables
        Gatherable _CurrentGatherTarget;
        double _GatherTimer = 0;

        public enum PlayerState
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

        private PlayerState playerState = PlayerState.kStateIdle;
        public PlayerState _PlayerState { get => playerState; set => playerState = value; }

        public float _PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
        private float playerSpeed = 200.0f;

        private Rectangle CheckRect
        {
            get
            {
                if (_Player._Direction == Sprite.Direction.kDirectionDown)
                {
                    return new Rectangle((int)_Player._Position.X - 26, (int)_Player._Position.Y + 32, 58, 32);
                }
                else if (_Player._Direction == Sprite.Direction.kDirectionUp)
                {
                    return new Rectangle((int)_Player._Position.X - 26, (int)_Player._Position.Y - 64, 58, 32);
                }
                else if (_Player._Direction == Sprite.Direction.kDirectionLeft)
                {
                    return new Rectangle((int)_Player._Position.X - 64, (int)_Player._Position.Y - 26, 32, 58);
                }
                else if (_Player._Direction == Sprite.Direction.kDirectionRight)
                {
                    return new Rectangle((int)_Player._Position.X + 32, (int)_Player._Position.Y - 32, 32, 58);
                }
                else
                {
                    Console.WriteLine("Error creating player check rectangle");
                    return new Rectangle();
                }
            }
        }

        public Sprite _FrontSprite;

        public PlayerManager(Player p, InventoryManager IM, WorldObjectManager WOM, NPCManager NPCM, TilemapManager tm, GatherableManager gm)
        {
            _Player = p;
            _InventoryManager = IM;
            _WorldObjectManager = WOM;
            _NPCManager = NPCM;
            _MapManager = tm;
            _GatherManager = gm;
        }

        public void Update(GameTime gt)
        {
            _PlayerPos = _Player._Position;
            _Player._MyState = PlayerState.kStateIdle;
            Vector2 currentPos = _Player._Position;
            ProcessKeyboard(gt);
            if(CheckCollision())
            {
                _Player._Position = currentPos;
            }


            _GatherTimer -= gt.ElapsedGameTime.TotalSeconds;

            if(_CurrentGatherTarget != null)
            {
                _Player._MyState = PlayerState.kStateWC;
                if(_GatherTimer <= 0)
                {
                    Gather(_CurrentGatherTarget);
                    _GatherTimer = 1;
                }
                if(_CurrentGatherTarget._CurrentState == Sprite.SpriteState.kStateInActive)
                {
                    ClearTargets();
                }
            }
            
            _Player.Update(gt);

            if (_Player._PlayerAttacking)
            {
                CheckSwordCollision();
            }

            CheckArrowCollision();
            _FrontSprite = CheckPlayerFront();
            _PlayerPos = _Player._Position;
        }

        private Sprite CheckPlayerFront()
        {
            //This is for interacting with NPCs or other objects.
            //first check if we talked to an NPC;
            Sprite spHit = null;
            spHit = _NPCManager.CheckCollisions(CheckRect);
            if (spHit != null)
            {
                return spHit;
            }
            if (spHit == null)
            {
                spHit = _GatherManager.CheckCollision(CheckRect);
                if(spHit != null)
                {
                    return spHit;
                }
            }

            spHit = _WorldObjectManager.CheckDetectors(CheckRect);
            if (spHit != null)
            {
                //go back to make sure it's a dirt patch that was hit.
                return spHit;
            }
            return spHit;
        }

        private void CheckSwordCollision()
        {
            NPC npcHit = _NPCManager.CheckAttacks(_Player.swordTip._BoundingBox);
            if(npcHit != null)
            {
                Vector2 recoilVect = Vector2.Zero;
                if( npcHit._TopLeftRect.Intersects(_Player.swordTip._BoundingBox))
                {
                    recoilVect = new Vector2(1, 1);
                }
                else if (npcHit._TopRightRect.Intersects(_Player.swordTip._BoundingBox))
                {
                    recoilVect = new Vector2(-1, 1);
                }
                else if (npcHit._BottomLeftRect.Intersects(_Player.swordTip._BoundingBox))
                {
                    recoilVect = new Vector2(1, -1);
                }
                else if (npcHit._BottomRightRect.Intersects(_Player.swordTip._BoundingBox))
                {
                    recoilVect = new Vector2(-1, -1);
                }
                _Player.sword.Deactivate();
                npcHit.ReceiveDamage(3, recoilVect);


                return;
            }
        }

        private void CheckArrowCollision()
        {
            foreach(Projectile arrow in _Player._ActiveArrows)
            {
                NPC npcHit = _NPCManager.CheckAttacks(arrow._ProjectileTip);
                if (npcHit != null)
                {
                    arrow.Deactivate();
                    npcHit.ReceiveDamage(1);

                }
            }
        }

        private bool CheckCollision()
        {
            //check collision with map
            //will check every rectangle on the current active map... not good but optimize later
            TileMap currentMap = _MapManager.findMap(_MapManager.PosToWorldTilePos(_PlayerPos));
            List<Rectangle> mapRects = currentMap.WallList;
            List<Vector2> test = HelperFunctions.RotatedRectList(_Player._WorldBoundingBox, _Player._Rotation);
            foreach (Rectangle rect in mapRects)
            {
                List<Vector2> dddd = HelperFunctions.RectToList(rect);
                bool collided = CollisionDetection2D.BoundingRectangle(test, dddd);
                if (collided)
                {
                    return true;
                }
            }
            

            //check collision with other sprites
            Sprite hit = _NPCManager.CheckCollisions(_Player._BoundingBox);
            if (hit == null)
            {
                hit = _WorldObjectManager.CheckWalkable(_Player._BoundingBox);
            }
            if(hit == null)
            {
                hit = _GatherManager.CheckWalkable(_Player._WorldBoundingBox);
            }
            if(hit != null)
            {
                return true;
            }
            return false;
        }

        public void ProcessKeyboard(GameTime gt)
        {
            bool moved = false;
            Vector2 newPos = _Player._Position;
            if (InputHelper.IsKeyDown(Keys.A) || InputHelper.IsKeyDown(Keys.Left))
            {
                //player._Position.X -= player._Speed;
                newPos.X -= (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                _Player._Direction = Sprite.Direction.kDirectionLeft;
                moved = true;
            }
            else if (InputHelper.IsKeyDown(Keys.D) || InputHelper.IsKeyDown(Keys.Right))
            {
                //player._Position.X += player._Speed;
                newPos.X += (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                _Player._Direction = Sprite.Direction.kDirectionRight;
                moved = true;
            }
            if (InputHelper.IsKeyDown(Keys.W) || InputHelper.IsKeyDown(Keys.Up))
            {
                //player._Position.Y -= player._Speed;
                newPos.Y -= (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                _Player._Direction = Sprite.Direction.kDirectionUp;
                moved = true;
            }
            else if (InputHelper.IsKeyDown(Keys.S) || InputHelper.IsKeyDown(Keys.Down))
            {
                //player._Position.Y += player._Speed;
                newPos.Y += (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                _Player._Direction = Sprite.Direction.kDirectionDown;
                moved = true;
            }

            if(InputHelper.IsKeyDown(Keys.R) )
            {
                _Player._Rotation += 0.05f;
            }
            if(InputHelper.IsKeyPressed(Keys.F))
            {
                _Player._Rotation = 0f;
            }


            _Player._Position = newPos;

            if (InputHelper.IsKeyPressed(Keys.Space))
            {
                bool gotHit = false;
                //This is for interacting with NPCs or other objects.
                //first check if we talked to an NPC;
                //NPC npcHit = _NPCManager.CheckCollisions(CheckRect);
                gotHit =  _NPCManager.AttemptInteract(CheckRect);

                if (!gotHit)
                {
                    _CurrentGatherTarget = _GatherManager.CheckCollision(CheckRect);
                }
            }

            if (InputHelper.IsKeyPressed(Keys.V))
            {
                _Player.Attack();
            }

            if (InputHelper.IsKeyPressed(Keys.B))
            {
                _Player.RangedAttack();
            }

            if(InputHelper.IsKeyPressed(Keys.L))
            {
                //check collision to see if dirt patch hit
                WorldObject woHit = _WorldObjectManager.CheckDetectors(CheckRect);
                if (woHit != null)
                {
                    //go back to make sure it's a dirt patch that was hit.
                    _GatherManager.CreatePlant(Plant.PlantType.kStrawBerryType, woHit._Position);
                }
                // if a dirt patch was hit, tell the gatherable manager to create strawberry at the dirt patch location
            }

            //if (kbState.IsKeyDown(Keys.J) && _PrevKBState.IsKeyUp(Keys.J))
            //{
            //    _Player.ToggleCorners();
            //}

            if (moved)
            {
                ClearTargets();
                _Player._MyState = PlayerState.kStateWalk;
                //OnPlayerMoved();
            }
        }

        private void ProcessWorldObject(WorldObject woHit)
        {
            if(woHit.IsGatherable)
            {
                Gather(woHit as Gatherable);
            }
        }

        private void Gather(Gatherable gatherable)
        {
            GameObjects.Items.ItemBundle itemGet = _GatherManager.GatherItem(gatherable);
            if(itemGet.output != Item.ItemType.kItemNone)
            {
                _InventoryManager.AddItem(itemGet);
            }
        }
        private void ProcessNPC(NPC npcHit)
        {
            if(npcHit._Tag == Sprite.SpriteType.kNPCType)
            {
                OnBankOpened();
            }
        }

        public void Draw(SpriteBatch SB)
        {
            _Player.Draw(SB);
        }

        public void SetPosition(float x, float y)
        {
            _Player._Position.X = x;
            _Player._Position.Y = y;
            _PlayerPos = new Vector2(x, y);
        }

        public void ClearTargets()
        {
            _CurrentGatherTarget = null;
        }


        #region Events

        public void OnBankOpened()
        {
            BankOpened?.Invoke(this, EventArgs.Empty);
        }

        public void OnBankClosed()
        {
            PlayerMoved?.Invoke(this, EventArgs.Empty);
        }
#endregion
    }
}
