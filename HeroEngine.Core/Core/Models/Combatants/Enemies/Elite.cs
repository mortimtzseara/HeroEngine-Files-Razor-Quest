using HeroEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroEngine.Core.Models.Combatants.Enemies
{
    /// <summary>
    /// Tougher enemy with more damage.
    /// </summary>
    public class Elite : AEnemy
    {
        public Elite(string name) : base(name, UIConfig.Numeric.EliteBaseHP, UIConfig.Numeric.EliteBaseDamage, UIConfig.Numeric.EliteBaseInitiative) { }
    }
}
