using HeroEngine.Core.enums;
using HeroEngine.Core.Models.Combatants;
using HeroEngine.Core.Models.Combatants.Heroes;
using HeroEngine.UI;

namespace HeroEngine.Core.Models.Spells
{
    /// <summary>
    /// Represents a spell that restores health to a target combatant.
    /// </summary>
    public class HealingSpell : ASpell
    {
        public int HealAmount { get; protected set; }

        public HealingSpell(string name, TypeRarity rarity, int manaCost, int healAmount) : base(name, TypeAbility.Healing, rarity, manaCost)
        {
            HealAmount = healAmount;
        }
        /// <summary>
        /// Casts the ability, healing the target if the caster has sufficient mana.
        /// </summary>
        /// <param name="caster">The combatant attempting to cast the ability.</param>
        /// <param name="target">The combatant targeted by the ability.</param>
        /// <returns>A message indicating the result of the ability cast.</returns>
        public override string CastAbility(ACombatant caster, ACombatant target)
        {
            if (caster is Mage mage && mage.ConsumeMana(ManaCost))
            {
                target.Heal(HealAmount);
                return string.Format(UIConfig.Messages.MessageHealingSpell, caster.Name, Name, HealAmount);
            }
            else
            {
                return UIConfig.Messages.MessageNotEnoughMana;
            }
        }
    }
}
