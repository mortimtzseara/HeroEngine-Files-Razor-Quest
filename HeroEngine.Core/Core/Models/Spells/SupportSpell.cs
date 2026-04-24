using HeroEngine.Core.enums;
using HeroEngine.Core.Models.Combatants;
using HeroEngine.Core.Models.Combatants.Heroes;
using HeroEngine.UI;

namespace HeroEngine.Core.Models.Spells
{
    /// <summary>
    /// Represents a support spell that increases a target's damage for their next attack.
    /// </summary>
    public class SupportSpell : ASpell
    {
        public int DamageBonus { get; protected set; }

        public SupportSpell(string name, TypeRarity rarity, int manaCost, int damageBonus) : base(name, TypeAbility.Support, rarity, manaCost)
        {
            DamageBonus = damageBonus;
        }
        /// <summary>
        /// Casts the ability, applying a damage bonus to the target if the caster has sufficient mana.
        /// </summary>
        /// <param name="caster">The combatant attempting to cast the ability.</param>
        /// <param name="target">The combatant receiving the effects of the ability.</param>
        /// <returns>A message indicating the result of the ability cast.</returns>
        public override string CastAbility(ACombatant caster, ACombatant target)
        {
            if (caster is Mage mage && mage.ConsumeMana(ManaCost))
            {
                target.DamageModifier += DamageBonus;

                return string.Format(UIConfig.Messages.MessageSupportSpell, caster.Name, Name, DamageBonus);
            }
            else
            {
                return UIConfig.Messages.MessageNotEnoughMana;
            }
        }
    }
}