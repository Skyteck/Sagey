using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{ 
    public class UIManager
    {
        public List<UIPanel> UIPanels;
        InventoryManager _invenManager;

        public UIManager(InventoryManager invenManager)
        {
            UIPanels = new List<UIPanel>();
            _invenManager = invenManager;
        }

        public void Update(GameTime gameTime)
        {
            foreach(UIPanel element in UIPanels)
            {
                element.UpdateActive(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(UIPanel panel in UIPanels)
            {
                panel.Draw(spriteBatch);
            }
        }

        public UIPanel getUIElement(String UIName)
        {
            UIPanel element = UIPanels.Find(x => x.Name == UIName);
            if(element == null)
            {
                Console.WriteLine("UI Element: " + UIName + " not found.");
                return null;
            }
            return element;
        }
    }
}
