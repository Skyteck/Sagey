using System.Collections.Generic;

namespace Sagey.Quests
{
    public class QuestObjective
    {
        public string Name;
        public bool Completed;
        public Sagey.Managers.EventInfo ObjectiveInfo; 
        public Enums.EventTypes lfEventType;
        public string EventText;
        public string EventID;
        public int Amount = 1;
        public bool Active = true;
        public int currentProgress = 0;
    }

}
