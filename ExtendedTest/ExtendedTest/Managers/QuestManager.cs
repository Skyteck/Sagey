using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagey.Quests;

namespace Sagey.Managers
{
    public class QuestManager
    {
        List<Quest> Quests;

        public QuestManager()
        {
            Quests = new List<Quest>();
        }


        public void GenerateQuest()
        {
            Quest nq = new Quest();
            nq.QuestName = "Cow milking quest";
            nq.QuestID = "CowMilkQuest";
            QuestObjective QO = new QuestObjective();
            QO.Name = "Milk Cow";
            QO.lfEvent = Enums.EventTypes.kEventNPCInteract;
            QO.EventID = "CowMilk";
            nq.Objectives.Add(QO);
            nq.Active = true;
            Quests.Add(nq);
            
        }

        public List<Quest> GetActiveQuests()
        {
            return Quests.FindAll(x => x.Active);
        }
    }
}
