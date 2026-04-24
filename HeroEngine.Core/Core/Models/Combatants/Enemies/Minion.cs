using HeroEngine.Core.Models.Combatants.Enemies;
using HeroEngine.UI;

namespace HeroEngine.Core.Models
{
    /// <summary>
    /// Weak and fast enemy.
    /// </summary>
    public class Minion : AEnemy
    {
        public Minion(string name) : base(name, UIConfig.Numeric.MinionBaseHP, UIConfig.Numeric.MinionBaseDamage, UIConfig.Numeric.MinionBaseInitiative) { }
    }
}