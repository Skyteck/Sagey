using Sagey.Managers;
using Microsoft.Xna.Framework.Content;



namespace Sagey.GameObjects.NPCs
{
    public class QuestSlime : NPC
    {
        public QuestSlime(NPCManager nm) : base(nm)
        {
            AddMessages("OpenBank");
            _Interactable = true;
        }
        

        public override void Interact()
        {
            base.Interact();
            _NPCManager.PlayDialogue("TestQuest");
        }
    }
}