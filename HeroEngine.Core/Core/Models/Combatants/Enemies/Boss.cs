using HeroEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroEngine.Core.Models.Combatants.Enemies
{
    /// <summary>
    /// Powerful boss with high stats.
    /// </summary>
    public class Boss : AEnemy
    {
        public Boss(string name) : base(name, UIConfig.Numeric.BossBaseHP, UIConfig.Numeric.BossBaseDamage, UIConfig.Numeric.BossBaseInitiative) { }
    }
}
