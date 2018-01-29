using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.Managers
{
    public class EventManager
    {
        List<EventInfo> CurrentEvents;
        private QuestManager _QuestManager;
        private NPCManager _NPCManager;

        public EventManager(QuestManager questManager, NPCManager nm)
        {
            _QuestManager = questManager;
            _NPCManager = nm;
            CurrentEvents = new List<EventInfo>();

            _NPCManager.NPCDyingEvent += HandleNPCDying;
        }

        public void ProcessEvents()
        {
            foreach(EventInfo EI in CurrentEvents)
            {
                bool eventProcessed = false;
                _QuestManager.CheckEvent(EI);
            }

            CurrentEvents.Clear();
        }

        private void HandleNPCDying(NPC theNPC)
        {
            CurrentEvents.Add(new EventInfo { EventType = Enums.EventTypes.kEventNPCDying, EventTitle = theNPC.Name });
        }
    }

    public class EventInfo
    {
        public Enums.EventTypes EventType;
        public string EventTitle;
    }
}
