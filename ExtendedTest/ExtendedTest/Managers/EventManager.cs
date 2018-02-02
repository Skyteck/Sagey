using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagey.Enums;

namespace Sagey.Managers
{
    public class EventManager
    {
        List<EventInfo> CurrentEvents;
        List<EventInfo> PastEvents;

        private QuestManager _QuestManager;

        public EventManager(QuestManager questManager)
        {
            _QuestManager = questManager;
            CurrentEvents = new List<EventInfo>();
            PastEvents = new List<EventInfo>();
            
        }

        public void ProcessEvents()
        {
            foreach(EventInfo EI in CurrentEvents)
            {
                Console.WriteLine(EI.EventTitle);
                _QuestManager.CheckEvent(EI);
            }

            PastEvents.AddRange(CurrentEvents);
            CurrentEvents.Clear();
        }

        public void HandleEvent(EventTypes eventType, string eventID)
        {
            CurrentEvents.Add(new EventInfo { EventType = eventType, EventTitle = eventID });
        }

    }

    public class EventInfo
    {
        public Enums.EventTypes EventType;
        public string EventTitle;
    }
}
