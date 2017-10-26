using ExtendedTest.Managers;
using Microsoft.Xna.Framework.Content;



namespace ExtendedTest.GameObjects.NPCs
{
    public class Banker : NPC
    {
        public Banker(NPCManager nm) : base(nm)
        {
            AddMessages("OpenBank");
            _Interactable = true;
        }

        public override void Interact()
        {
            base.Interact();
            ParentManager.PlayDialogue("OpenBank");
        }
    }
}