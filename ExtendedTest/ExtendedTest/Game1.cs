using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using TiledSharp;

namespace ExtendedTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Player player;
        //List<Sprite> gameObjectList;
        MouseState previousMouseState;
        KeyboardState previousKBState;
        Sprite mouseCursor;
        public Camera  camera;
        
        List<TileMap> mapList;

        SpriteFont font;
        bool typingMode = false;
        public KbHandler kbHandler;

        //Managers
        InventoryManager _InvenManager;
        TilemapManager _MapManager;
        public NpcManager _NPCManager;
        WorldObjectManager _GameObjectManager;
        CombatManager _CBManager;
        UIManager _UIManager;

        //UI
        
        UIElement UIClicked;
        Vector2 mouseClickPos;
        UIElement inventoryBG;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _InvenManager = new InventoryManager(Content);
            _CBManager = new CombatManager();
            player = new Player(_InvenManager, _CBManager);
            _MapManager = new TilemapManager(_NPCManager, _GameObjectManager);
            _NPCManager = new NpcManager(_MapManager, _CBManager,  Content, player);
            _GameObjectManager = new WorldObjectManager(_MapManager, _InvenManager, Content, player);
            _UIManager = new UIManager();
            mapList = new List<TileMap>();
            camera = new Camera(GraphicsDevice);
            kbHandler = new KbHandler();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadPlayerContent();
            LoadGUI();

            _MapManager.LoadMap("0-0", Content);
            LoadMapNPCs(_MapManager.findMapByName("0-0"));
            LoadMapObjects(_MapManager.findMapByName("0-0"));
            _MapManager.LoadMap("0-1", Content);
            font = Content.Load<SpriteFont>("Fonts/Fipps");

            //XDocument xmlTest = XDocument.Load("Content/Items.xml");
            //IEnumerable<XElement> itemList = xmlTest.Elements("Items");
            //IEnumerable<XElement> LogList = itemList.Elements("Logs");
            //foreach(XElement item in LogList.Elements("Item"))
            //{
            //    String test = item.Element("Name").Value;
            //    String test2 = item.Element("SaleValue").Value;
            //    String test3 = item.Element("Weight").Value;
            //}
            //IEnumerable<XElement> oreList = itemList.Elements("Ores");
            //foreach (XElement item in oreList.Elements("Item"))
            //{
            //    String test = item.Element("Name").Value;
            //    String test2 = item.Element("SaleValue").Value;
            //    String test3 = item.Element("Weight").Value;
            //}

            //List<Sprite> test = new List<Sprite>();
            //test = gameObjectList.FindAll(x => x._Tag == Sprite.SpriteType.kTreeType);

            // TODO: use this.Content to load your game content here
        }

        private void LoadPlayerContent()
        {
            player.LoadContent("Art/Player", Content);
            player._Position = new Vector2(0, 0);
            _InvenManager.AddItem(new Log());
        }

        private void LoadGUI()
        {
            UIElement inventoryBG = new UIElement();
            inventoryBG.LoadContent("Art/inventoryBG", Content);
            inventoryBG._Position = new Vector2(450, 450);
            inventoryBG.Name = "InventoryBG";
            _UIManager.UIElements.Add(inventoryBG);

            mouseCursor = new Sprite();
            mouseCursor.LoadContent("Art/log", Content);
            mouseCursor.Name = "Cursor";
        }

        public void LoadMapNPCs(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.findNPCs();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    int adjustThingX = (int)(thing.X + (testMap._Postion.X + (thing.Width / 2)));
                    int adjustThingY = (int)(thing.Y + (testMap._Postion.Y + (thing.Height / 2)));
                    _NPCManager.CreateMonster(thing, new Vector2(adjustThingX, adjustThingY));
                }
            }
        }

        public void LoadMapObjects(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.findObjects();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    Vector2 newPos = new Vector2((int)thing.X + testMap._Postion.X, (int)thing.Y + testMap._Postion.Y);
                    _GameObjectManager.CreateObject(thing, newPos);
                }
            }
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(this.IsActive)
            {
                // TODO: Add your update logic here
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();


                ProcessMouse(gameTime);


                kbHandler.Update();
                //if (typingMode && !kbHandler.typingMode) //ugly, but should show that input mode ended...?
                //{
                //    processor.Parsetext(kbHandler.Input);
                //    if (processor.currentError != string.Empty) kbHandler.Input = processor.currentError;
                //    //kbHandler.Input = string.Empty;
                //}
                player.UpdateActive(gameTime);
                _NPCManager.UpdateNPCs(gameTime);
                _GameObjectManager.Update(gameTime);

                if(!kbHandler.typingMode)
                {
                    //processor.currentError = string.Empty;
                    ProcessCamera(gameTime);

                }

                foreach(UIElement element in _UIManager.UIElements)
                {
                    if(element.Name == "InventoryBG")
                    {
                        element._Position = camera.ToWorld(new Vector2(400, 400));
                    }
                    element.UpdateActive(gameTime);
                }

                base.Update(gameTime);
                //Show FPS
                if((1 / gameTime.ElapsedGameTime.TotalSeconds) <= 59)
                {
                    Console.WriteLine("BAD FPS!!!!!!!!!!");
                }
                typingMode = kbHandler.typingMode;
            }
        }

        private void ProcessMouse(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if(UIClicked == null)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    mouseCursor._Position = camera.ToWorld(new Vector2(mouseState.Position.X, mouseState.Position.Y));
                    //check if UI clicked first
                    foreach (UIElement sprite in _UIManager.UIElements)
                    {
                        Vector2 test1 = camera.ToWorld(new Vector2(mouseState.Position.X, mouseState.Position.Y));
                        if (sprite._BoundingBox.Contains(test1))
                        {
                            UIClicked = sprite;
                            mouseClickPos = mouseCursor._Position;
                        }
                    }
                    if(UIClicked == null)
                    {
                        List<Sprite> gameObjects = new List<Sprite>();
                        gameObjects.AddRange(_NPCManager._SpriteListActive);
                        gameObjects.AddRange(_GameObjectManager.ObjectList);

                        bool targetFound = false;
                        Sprite clickedSprite = new Sprite();
                        foreach (Sprite sprite in gameObjects)
                        {
                            if (sprite._BoundingBox.Intersects(mouseCursor._BoundingBox))
                            {
                                clickedSprite = sprite;
                                targetFound = true;
                            }
                        }
                        if (!targetFound)
                        {
                            if (mouseCursor._Position.X >= 0 && mouseCursor._Position.Y > 0)
                            {
                                Tile clickedTile = _MapManager.findTile(mouseCursor._Position);
                                List<Tile> test = _MapManager.CalculatePath(clickedTile.tileCenter, player._Position);
                                if (clickedTile != null)
                                {
                                    // player.setDestination(clickedTile.tileCenter);
                                    player.SetPath(test);
                                }
                            }
                        }
                        else
                        {
                            Tile closestTile = _MapManager.findClosestTile(clickedSprite._Position, player._Position);
                            List<Tile> test = _MapManager.CalculatePath(clickedSprite._Position, player._Position, true);
                            if (closestTile.walkable)
                            {
                                player.setDestination(closestTile.tileCenter);
                                player.SetTarget(clickedSprite);
                            }
                        }
                    }
                }
            }
            else if(UIClicked != null)
            {
                Vector2 stateToworld = camera.ToWorld(mouseState.X, mouseState.Y);
                float clickPosX = mouseClickPos.X;
                float letGoPosX = stateToworld.X;
                float differenceX = letGoPosX - clickPosX;
                mouseClickPos.X = stateToworld.X;
                differenceX /= 100f;
                UIClicked._Scale.X += differenceX;

                float clickPosY = mouseClickPos.Y;
                float letGoPosY = stateToworld.Y;
                float differenceY = letGoPosY - clickPosY;
                mouseClickPos.Y = stateToworld.Y;
                differenceY /= 100f;
                UIClicked._Scale.Y -= differenceY;

                UIClicked.Resize(new Vector2(differenceX, differenceY));


                if (mouseState.LeftButton == ButtonState.Released)
                {
                    UIClicked = null;
                }
            }


            if (mouseState.ScrollWheelValue > previousMouseState.ScrollWheelValue)
            {
                camera.Scale += 0.2f;
                if (camera.Scale > 2f)
                {
                    camera.Scale = 2f;
                }
            }
            else if (mouseState.ScrollWheelValue < previousMouseState.ScrollWheelValue)
            {
                camera.Scale -= 0.25f;
                if (camera.Scale < 0.6f)
                {
                    camera.Scale = 0.6f;
                }
            }

            previousMouseState = mouseState;

        }

        private void ProcessCamera(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                this.camera.Position = new Vector2(this.camera.Position.X - 5, this.camera.Position.Y);
            }
            else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                this.camera.Position = new Vector2(this.camera.Position.X + 5, this.camera.Position.Y);
            }
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                this.camera.Position = new Vector2(this.camera.Position.X, this.camera.Position.Y - 5);
            }
            else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                this.camera.Position = new Vector2(this.camera.Position.X, this.camera.Position.Y + 5);
            }


            if(state.IsKeyDown(Keys.I))
            {
                if(inventoryBG._Scale.Y <= 0f)
                {
                    inventoryBG._Scale.Y += 0.05f;
                }
                inventoryBG._Scale.Y -= 0.05f;
            }
            else if (state.IsKeyDown(Keys.K))
            {
                inventoryBG._Scale.Y += 0.05f;
            }

            if (state.IsKeyDown(Keys.J))
            {
                if (inventoryBG._Scale.X <= 0f)
                {
                    inventoryBG._Scale.X += 0.05f;
                }
                inventoryBG._Scale.X -= 0.05f;
            }
            else if (state.IsKeyDown(Keys.L))
            {
                inventoryBG._Scale.X += 0.05f;
            }

            camera.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(camera);

            _MapManager.Draw(spriteBatch);

            player.Draw(spriteBatch);

            _NPCManager.DrawNPCs(spriteBatch);

            _GameObjectManager.Draw(spriteBatch);

            _UIManager.Draw(spriteBatch);
            Vector2 invenBgpos = camera.ToWorld(new Vector2(20,20));
            _InvenManager.Draw(spriteBatch, invenBgpos);

            mouseCursor.Draw(spriteBatch);

            spriteBatch.DrawString(font, kbHandler.Input, camera.ToWorld(new Vector2(100, 100)), Color.Black);
            spriteBatch.DrawString(font, player._HP.ToString(), camera.ToWorld(new Vector2(200, 200)), Color.White);

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
