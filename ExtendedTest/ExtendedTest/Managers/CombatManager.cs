using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{
    class CombatManager
    {

        public CombatManager()
        {

        }

        public void PerformAttack(Character attacker, Character receiver)
        {
            int amt = receiver.defense - attacker.attack;
            //attacker attack higher than defense
            if(amt<= 0)
            {
                receiver.ReceiveDamage(amt);
                attacker.attackCD = attacker.attackSpeed;
            }
        }
    }
}
