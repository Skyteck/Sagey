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
        public Camera  camera;

        SpriteFont font;
        bool typingMode = false;
        public KbHandler kbHandler;

        //Managers
        Managers.InventoryManager _InvenManager;
        Managers.TilemapManager _MapManager;
        public Managers.NPCManager _NPCManager;
        Managers.WorldObjectManager _WorldObjectManager;
        Managers.UIManager _UIManager;
        Managers.ChemistryManager _ChemistryManager;
        Managers.ItemManager _ItemManager;
        Managers.PlayerManager _PlayerManager;
        Managers.BankManager _BankManager;
        Managers.GatherableManager _GatherableManager;


        //UI        
        Vector2 mouseClickPos;
        Sprite mouseCursor;

        bool BankMode = false;
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
            player = new Player();
            _ItemManager = new Managers.ItemManager(Content);
            _InvenManager = new Managers.InventoryManager(_ItemManager);
            _BankManager = new Managers.BankManager(_ItemManager);
            _MapManager = new Managers.TilemapManager(_NPCManager, _WorldObjectManager);
            _NPCManager = new Managers.NPCManager(_MapManager,  Content, player);
            _WorldObjectManager = new Managers.WorldObjectManager(_MapManager, _InvenManager, Content, player);
            _GatherableManager = new Managers.GatherableManager(_MapManager, _InvenManager, Content, player);
            _UIManager = new Managers.UIManager(_InvenManager);
            _ChemistryManager = new Managers.ChemistryManager(_InvenManager, _WorldObjectManager, _NPCManager, Content, _ItemManager);

            _PlayerManager = new Managers.PlayerManager(player, _InvenManager, _WorldObjectManager, _NPCManager, _MapManager, _GatherableManager);
            _WorldObjectManager.SetGatherManager(_GatherableManager);
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
            _ItemManager.LoadItems("Content/JSON/Items.json");
            _ChemistryManager.LoadIcons();
            _GatherableManager.LoadContent(Content);
            _WorldObjectManager.PlantAll();
            //Effect PixelShader = Content.Load<Effect>("Effects/PXS");
            //check if save exists

            //else start a new save
            bool saveExist = false;
            string path = Content.RootDirectory + @"\Save\save.txt";
            if (System.IO.File.Exists(path))
            {
                saveExist = true;
            }
            if (saveExist)
            {
                //load
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                string line = file.ReadLine();
                if (line != null)
                {
                    string[] playerPos = line.Split(' ');
                    float.TryParse(playerPos[0], out var x);
                    float.TryParse(playerPos[1], out var y);
                    _PlayerManager.SetPosition(x, y);
                }
                bool bankMode = false;
                bool inventoryMode = false;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Equals("B"))
                    {
                        bankMode = true;
                        continue;
                    }
                    else if (line.Equals("BEnd"))
                    {
                        bankMode = false;
                        continue;
                    }
                    if (line.Equals("I"))
                    {
                        inventoryMode = true;
                        continue;
                    }
                    else if (line.Equals("IEnd"))
                    {
                        inventoryMode = false;
                        continue;
                    }
                    if (bankMode || inventoryMode)
                    {
                        string[] items = line.Split(' ');
                        Int32.TryParse(items[0], out int itemType);
                        Item.ItemType type = (Item.ItemType)itemType;
                        Int32.TryParse(items[1], out int amt);
                        if (bankMode)
                        {
                            _BankManager.AddItem(type, amt);
                        }
                        else if (inventoryMode)
                        {
                            _InvenManager.AddItem(type, amt);
                        }
                    }
                    
                }

                file.Close();
            }
            else
            {
                player._Position = new Vector2(32, 32);

                _InvenManager.AddItem(Item.ItemType.kItemLog, 5);
                _InvenManager.AddItem(Item.ItemType.kItemMatches);

                _BankManager.AddItem(Item.ItemType.kItemLog, 10);
                _BankManager.AddItem(Item.ItemType.kItemFish, 3);
            }


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
        }

        private void LoadGUI()
        {
            GameObjects.UIObjects.InventoryPanel inventoryBG = new GameObjects.UIObjects.InventoryPanel(_InvenManager);
            inventoryBG.LoadContent("Art/inventoryBG", Content);
            inventoryBG._Position = new Vector2(250, 450);
            inventoryBG.Name = "Inventory";
            inventoryBG._Opacity = 0.6f;
            _UIManager.UIPanels.Add(inventoryBG);

            GameObjects.UIObjects.CraftingPanel craftingPanel = new GameObjects.UIObjects.CraftingPanel(_ChemistryManager);
            craftingPanel.LoadContent("Art/inventoryBG", Content);
            craftingPanel._Position = new Vector2(450, 450);
            craftingPanel.Name = "Crafting";
            craftingPanel._Opacity = 0.9f;
            _UIManager.UIPanels.Add(craftingPanel);

            GameObjects.UIObjects.BankPanel bankPanel = new GameObjects.UIObjects.BankPanel(_BankManager);
            bankPanel.LoadContent("Art/inventoryBG", Content);
            bankPanel._Position = new Vector2(0, 0);
            bankPanel.Name = "Bank";
            bankPanel._Opacity = 1f;
            _UIManager.UIPanels.Add(bankPanel);

            mouseCursor = new Sprite();
            mouseCursor.LoadContent("Art/log", Content);
            mouseCursor.Name = "Cursor";
        }

        public void LoadMapNPCs(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.FindNPCs();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    int adjustThingX = (int)(thing.X + (testMap._Postion.X + (thing.Width / 2)));
                    int adjustThingY = (int)(thing.Y + (testMap._Postion.Y + (thing.Height / 2)));
                    _NPCManager.CreateNPC(thing, new Vector2(adjustThingX, adjustThingY));
                }
            }
        }

        public void LoadMapObjects(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.FindObjects();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    Vector2 newPos = new Vector2((int)thing.X + testMap._Postion.X, (int)thing.Y + testMap._Postion.Y);
                    //_WorldObjectManager.CreateObject(thing, newPos);
                    if(thing.Type == "Dirt")
                    {
                        _WorldObjectManager.CreateObject(thing, newPos);
                    }
                    else
                    {
                        _GatherableManager.CreateGatherable(thing, newPos);

                    }
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
                {
                    SaveGame();
                    Exit();
                }


                ProcessMouse(gameTime);
                ProcessKeyboard(gameTime);

                kbHandler.Update();
                //if (typingMode && !kbHandler.typingMode) //ugly, but should show that input mode ended...?
                //{
                //    processor.Parsetext(kbHandler.Input);
                //    if (processor.currentError != string.Empty) kbHandler.Input = processor.currentError;
                //    //kbHandler.Input = string.Empty;
                //}
                _PlayerManager.Update(gameTime);

                if(_PlayerManager._BankerGo)
                {
                    _UIManager.ShowPanel("Bank");
                    BankMode = true;
                }
                else
                {
                    _UIManager.HidePanel("Bank");
                    BankMode = false;
                }

                _NPCManager.UpdateNPCs(gameTime);
                _WorldObjectManager.Update(gameTime);
                _GatherableManager.Update(gameTime);

                if(!kbHandler.typingMode)
                {
                    //processor.currentError = string.Empty;
                    ProcessCamera(gameTime);

                }

                _UIManager.getUIElement("Inventory")._Position = camera.ToWorld(400, 400);
                _UIManager.getUIElement("Crafting")._Position = camera.ToWorld(600, 400);
                _UIManager.getUIElement("Bank")._Position = camera.ToWorld(200, 400);

                foreach (UIPanel element in _UIManager.UIPanels)
                {
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

        private void ProcessKeyboard(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();

            if(IsKeyPressed(Keys.E))
            {
                _ChemistryManager.CheckRecipes();
                _UIManager.TogglePanel("Crafting");
            }
            if (IsKeyPressed(Keys.I))
            {
                _UIManager.TogglePanel("Inventory");
            }
            if(IsKeyPressed(Keys.P))
            {
                SaveGame();
            }
            previousKBState = kbState;
        }

        private bool IsKeyPressed(Keys key)
        {
            KeyboardState kbState = Keyboard.GetState();
            if(kbState.IsKeyDown(key) && previousKBState.IsKeyUp(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsKeyReleased(Keys key)
        {
            KeyboardState kbState = Keyboard.GetState();
            if (kbState.IsKeyUp(key) && previousKBState.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsKeyHeld(Keys key)
        {
            KeyboardState kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(key) && previousKBState.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ProcessMouse(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            String command = string.Empty;
            if(mouseState.LeftButton==ButtonState.Pressed && previousMouseState.LeftButton==ButtonState.Released)
            {
                Vector2 mouseClickpos = camera.ToWorld(new Vector2(mouseState.Position.X, mouseState.Position.Y));

                //check if a click was in the crafting panel
                foreach(UIPanel panel in _UIManager.ActivePanels)
                {
                    if(BankMode)
                    {
                        if (panel.Name.Equals("Bank"))
                        {
                            Item.ItemType item = (panel as GameObjects.UIObjects.BankPanel).ProcessClick(mouseClickpos);
                            if (item != Item.ItemType.kItemNone)
                            {
                                _BankManager.RemoveItem(item);
                                _InvenManager.AddItem(item);
                            }
                        }
                        if (panel.Name.Equals("Inventory"))
                        {
                            Item.ItemType item = (panel as GameObjects.UIObjects.InventoryPanel).ProcessClick(mouseClickpos);
                            if (item != Item.ItemType.kItemNone)
                            {
                                _InvenManager.RemoveItem(item);
                                _BankManager.AddItem(item);
                            }
                        }
                    }
                    panel.ProcessClick(mouseClickpos);
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

            this.camera.Position = _PlayerManager._PlayerPos;

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

            _PlayerManager.Draw(spriteBatch);

            _NPCManager.DrawNPCs(spriteBatch);

            _WorldObjectManager.Draw(spriteBatch);
            _GatherableManager.Draw(spriteBatch);
            _UIManager.Draw(spriteBatch);

            //Vector2 invenBgpos = _UIManager.getUIElement("Inventory")._TopLeft;
            //_InvenManager.Draw(spriteBatch, invenBgpos);

            mouseCursor.Draw(spriteBatch);

            spriteBatch.DrawString(font, kbHandler.Input, camera.ToWorld(new Vector2(100, 100)), Color.Black);
            //spriteBatch.DrawString(font, player._HP.ToString(), camera.ToWorld(new Vector2(200, 200)), Color.White);

            base.Draw(gameTime);
            spriteBatch.End();
        }

        private void CheckPlayerHit()
        {

        }

        private void SaveGame()
        {
            string playerPos = string.Format("{0} {1}", _PlayerManager._PlayerPos.X, _PlayerManager._PlayerPos.Y);

            List<string> bankStuff = _BankManager.getList();
            List<string> inventoryStuff = _InvenManager.getList();
            string test = Content.RootDirectory + @"\Save\save.txt";
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(test))
            {
                file.WriteLine(playerPos);
                file.WriteLine("B");
                foreach(string line in bankStuff)
                {
                    file.WriteLine(line);
                }
                file.WriteLine("BEnd");
                file.WriteLine("I");
                foreach (string line in inventoryStuff)
                {
                    file.WriteLine(line);
                }
                file.WriteLine("IEnd");
            }
        }
    }
}
