using HeroEngine.Core.enums;
using HeroEngine.Core.Models.Combatants;
using HeroEngine.Core.Models.Combatants.Heroes;
using HeroEngine.UI;

namespace HeroEngine.Core.Models.Spells
{
    /// <summary>
    /// Represents a spell that inflicts damage on a target combatant.
    /// </summary>
    public class AttackSpell : ASpell
    {
        public int Damage { get; protected set; }
        public AttackSpell(string name, TypeRarity rarity, int manaCost, int damage) : base(name, TypeAbility.Attack, rarity, manaCost)
        {
            Damage = damage;
        }
        /// <summary>
        /// Casts the ability from the specified caster to the target, applying damage if the caster has sufficient
        /// mana.
        /// </summary>
        /// <param name="caster">The combatant attempting to cast the ability.</param>
        /// <param name="target">The combatant targeted by the ability.</param>
        /// <returns>A message indicating the result of the ability cast.</returns>
        public override string CastAbility(ACombatant caster, ACombatant target)
        {
            if (caster is Mage mage)
            {
                if (mage.ConsumeMana(ManaCost))
                {
                    int damage = target.TakeDamage(Damage);
                    return string.Format(UIConfig.Messages.MessageAttackSpell, caster.Name, Name, target.Name, damage);
                }
                else
                {
                    return UIConfig.Messages.MessageNotEnoughMana;
                }
            }
            return UIConfig.Messages.MessageCannotUseAbility;
        }
    }
}
