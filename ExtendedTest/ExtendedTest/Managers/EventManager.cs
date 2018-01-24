using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.Managers
{
    public class EventManager
    {
        private QuestManager _QuestManager;

        public EventManager(QuestManager questManager)
        {
            _QuestManager = questManager;
        }

        public void ProcessEvent(Enums.EventTypes TheEvent , String eventName)
        {
            Console.WriteLine(eventName);
        }
    }
}
