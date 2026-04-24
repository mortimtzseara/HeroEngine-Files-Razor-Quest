using HeroEngine.Core.enums;
using HeroEngine.Core.Models.Combatants;
using HeroEngine.Core.Models.Combatants.Heroes;
using HeroEngine.UI;

namespace HeroEngine.Core.Models.Spells
{
    /// <summary>
    /// Represents a defensive spell that increases a target's armor when cast.
    /// </summary>
    public class DefenseSpell : ASpell
    {
        public int ArmorBonus { get; protected set; }
        public DefenseSpell(string name, TypeRarity rarity, int manaCost, int armorBonus) : base(name, TypeAbility.Defense, rarity, manaCost)
        {
            ArmorBonus = armorBonus;
        }
        /// <summary>
        /// Casts the ability, applying an armor bonus to the target if the caster is a mage with sufficient mana.
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
                    target.ArmorModifier += ArmorBonus;

                    return string.Format(UIConfig.Messages.MessageDefenseSpell, caster.Name, Name, ArmorBonus);
                }
                else
                {
                    return UIConfig.Messages.MessageNotEnoughMana;
                }
            }
            else
            {
                return UIConfig.Messages.MessageCannotUseAbility;
            }
        }
    }
}
