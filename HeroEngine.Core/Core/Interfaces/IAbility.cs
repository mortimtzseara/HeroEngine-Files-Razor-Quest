using HeroEngine.Core.enums;
using HeroEngine.Core.Models.Combatants;

namespace HeroEngine.Core.Interfaces
{
    /// <summary>
    /// Defines a contract for abilities that can be cast by a combatant.
    /// </summary>
    public interface IAbility
    {
        public string Name { get; }
        public TypeAbility Type { get; }
        public TypeRarity Rarity { get; }
        /// <summary>
        /// Executes an ability from the caster against the target and returns the result value.
        /// </summary>
        /// <param name="caster">The combatant performing the ability.</param>
        /// <param name="target">The combatant targeted by the ability.</param>
        /// <returns>The result value of the ability cast.</returns>
        public string CastAbility(ACombatant caster, ACombatant target);
    }
}
