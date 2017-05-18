using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{ 
    public class UIManager
    {
        public List<UIElement> UIElements;
        InventoryManager _invenManager;

        public UIManager(InventoryManager invenManager)
        {
            UIElements = new List<UIElement>();
            _invenManager = invenManager;
        }

        public void Update(GameTime gameTime)
        {
            foreach(UIElement element in UIElements)
            {
                element.UpdateActive(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(UIElement element in UIElements)
            {
                element.Draw(spriteBatch);
            }
        }

        public UIElement getUIElement(String UIName)
        {
            UIElement element = UIElements.Find(x => x.Name == UIName);
            if(element == null)
            {
                Console.WriteLine("UI Element: " + UIName + " not found.");
                return null;
            }
            return element;
        }
    }
}
