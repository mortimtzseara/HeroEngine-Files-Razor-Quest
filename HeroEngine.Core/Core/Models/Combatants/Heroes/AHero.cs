using HeroEngine.Core.Models.Combatants;
using HeroEngine.UI;
using System.Text;

namespace HeroEngine.Core.Models.Combatants.Heroes
{
    /// <summary>
    /// Represents an abstract base class for hero combatants with level-based attributes.
    /// </summary>
    public abstract class AHero: ACombatant
    {
        public int Level { get; protected set; }
        public override int Initiative => base.Initiative + Level;
        protected AHero(string name, int level, int baseHP, int baseDamage) : base(name, baseHP + (level - 1) * UIConfig.Numeric.HpGainPerLvl, baseDamage + (level - 1) * UIConfig.Numeric.DamageGainPerLvl)
        {
            Level = level;
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{base.ToString()} [LEVEL]: {Level}");
            return stringBuilder.ToString();
        }
    }
}
