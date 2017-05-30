using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{
    public class CombatManager
    {

        public CombatManager()
        {

        }

        public void PerformAttack(Character attacker, Character receiver)
        {
            int amt = attacker.attack - receiver.defense;
            //attacker attack higher than defense
            if(amt>= 0)
            {
                receiver.ReceiveDamage(amt);
                attacker.attackCD = attacker.attackSpeed;
            }
        }
    }
}
