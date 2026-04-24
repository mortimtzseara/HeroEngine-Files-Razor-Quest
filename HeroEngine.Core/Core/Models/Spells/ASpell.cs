using HeroEngine.Core.enums;
using HeroEngine.Core.Interfaces;
using HeroEngine.Core.Models.Combatants;

namespace HeroEngine.Core.Models.Spells
{
    /// <summary>
    /// Represents an abstract magical ability with a name, type, rarity, and mana cost.
    /// </summary>
    public abstract class ASpell : IAbility
    {
        public string Name { get; protected set; }
        public TypeAbility Type { get; protected set; }
        public TypeRarity Rarity { get; protected set; }
        public int ManaCost { get; protected set; }
        protected ASpell(string name, TypeAbility type, TypeRarity rarity, int manaCost)
        {
            Name = name;
            Type = type;
            Rarity = rarity;
            ManaCost = manaCost;
        }
        /// <summary>
        /// Executes an ability from the caster against the target and returns the result as a string.
        /// </summary>
        /// <param name="caster">The combatant performing the ability.</param>
        /// <param name="target">The combatant receiving the ability.</param>
        /// <returns>A string describing the outcome of the ability.</returns>
        public abstract string CastAbility(ACombatant caster, ACombatant target);
        public override string ToString() => $"[{Rarity}]\t{Name}\tType: {Type}\t Cost:\t{ManaCost} mana";
    }
}
