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
            QO.ObjectiveInfo = new EventInfo { EventType = Enums.EventTypes.kEventNPCInteract, EventTitle = "CowMilk" };
            QO.lfEventType = Enums.EventTypes.kEventNPCInteract;
            QO.EventID = "CowMilk";

            QuestObjective QO2 = new QuestObjective();
            QO2.Name = "Kill 5 Slimes";
            QO2.ObjectiveInfo = new EventInfo { EventType = Enums.EventTypes.kEventNPCDying, EventTitle = "SLIME" };
            QO2.lfEventType = Enums.EventTypes.kEventNPCDying;
            QO2.EventID = "SLIMEKilled";
            QO2.Amount = 5;

            nq.Objectives.Add(QO2);
            nq.Objectives.Add(QO);
            nq.Active = true;
            Quests.Add(nq);
            
        }

        internal void CheckEvent(EventInfo eI)
        {
            List<Quest> ActiveQuests = Quests.FindAll(x => x.Active = true);
            foreach(Quest q in ActiveQuests)
            {
                List<QuestObjective> currentObjectives = q.Objectives.FindAll(x => x.Active == true);
                foreach(QuestObjective qo in currentObjectives)
                {
                    if(eI.EventType == qo.ObjectiveInfo.EventType)
                    {
                        if(eI.EventTitle == qo.ObjectiveInfo.EventTitle)
                        {

                            qo.currentProgress++;
                            Console.WriteLine("Objective: " + qo.Name + " Progressed.");
                            Console.WriteLine("Objective: " + qo.Name + " " + (qo.Amount - qo.currentProgress).ToString() + " to go.");
                            if (qo.currentProgress >= qo.Amount)
                            {
                                qo.Completed = true;
                                Console.WriteLine("Objective: " + qo.Name + " Completed");
                            }
                        }
                    }
                }
            }
        }

        public List<Quest> GetActiveQuests()
        {
            return Quests.FindAll(x => x.Active);
        }
    }
}
