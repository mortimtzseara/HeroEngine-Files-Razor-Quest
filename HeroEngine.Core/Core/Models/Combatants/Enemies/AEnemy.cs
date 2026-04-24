using HeroEngine.UI;

namespace HeroEngine.Core.Models.Combatants.Enemies
{
    /// <summary>
    /// Represents a generic enemy from the Bug Primordial forces.
    /// </summary>
    public abstract class AEnemy : ACombatant
    {
        private readonly int InitiativeModifier;

        public override int Initiative => UIConfig.Numeric.BaseInitiative + InitiativeModifier;

        protected AEnemy(string name, int maxHP, int baseDamage, int initiativeModifier) : base(name, maxHP, baseDamage)
        {
            InitiativeModifier = initiativeModifier;
        }
        public override int Attack() => TotalDamage;
    }
}

