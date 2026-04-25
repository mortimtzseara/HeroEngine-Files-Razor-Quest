using HeroEngine.Core.enums;
using HeroEngine.Core.Interfaces;
using HeroEngine.Core.Models.Spells;
using HeroEngine.UI;
using System.Text;
using System.Text.Json.Serialization;

namespace HeroEngine.Core.Models.Combatants.Heroes
{
    /// <summary>
    /// Represents a mage hero with arcane abilities and mana management.
    /// </summary>
    public class Mage : AHero
    {
        public int Arcane => UIConfig.Numeric.BaseArcane + UIConfig.Numeric.ArcaneGainPerLvl * (Level - 1);
        public override int Initiative => base.Initiative + Arcane / 2;
        public int MaxMana => UIConfig.Numeric.BaseMana + UIConfig.Numeric.ManaGainPerLvl * (Level - 1);
        private int _mana;
        public int Mana
        {
            get => _mana;
            protected set => _mana = Math.Clamp(value, 0, MaxMana);
        }
        [JsonInclude]
        public List<ASpell> SpellBook { get; private set; } = new List<ASpell>();
        public Mage(string name, int level): base(name, level, UIConfig.Numeric.BaseHP, UIConfig.Numeric.BaseDamage)
        {
            _mana = MaxMana;
        }
        public override void ResetStats()
        {
            base.ResetStats();
            Mana = MaxMana;
        }
        public override int Attack() => TotalDamage + Arcane;
        /// <summary>
        /// Consume mana
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>Returns true if you have enough mana</returns>
        public bool ConsumeMana(int amount)
        {
            if (Mana >= amount)
            {
                Mana -= amount;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Adds a new spell to the spellbook if it has not already been learned.
        /// </summary>
        /// <param name="newSpell">The spell to learn.</param>
        public void LearnSpell(ASpell newSpell)
        {
            if(SpellBook.Any(spell => spell.Name.Equals(newSpell.Name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(UIConfig.Messages.MessageSpellExists, Name);
            }
            else
            {
                SpellBook.Add(newSpell);
                Console.WriteLine(UIConfig.Messages.MessageSpellLearned, Name, newSpell.Name);
            }
        }
        /// <summary>
        /// Displays the spell book, grouping spells by rarity and listing them in descending order. Shows a message if
        /// the spell book is empty.
        /// </summary>
        public void ListSpellBook()
        {
            Console.WriteLine(UIConfig.Decoration.SpellBookTitle, Name.ToUpper());

            if (!SpellBook.Any())
            {
                Console.WriteLine(UIConfig.Messages.MessageEmptySpellBook);
            }
            else
            {
                var sortedSpells = SpellBook.GroupBy(s => s.Rarity).OrderByDescending(g => g.Key);

                foreach (var group in sortedSpells)
                {
                    foreach (var spell in group)
                    {
                        Console.WriteLine(spell.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// Executes the combatant's turn by selecting and casting the most powerful available spell or performing a
        /// basic attack if no suitable spell is available.
        /// </summary>
        /// <param name="defaultEnemy">The default enemy combatant targeted during the turn.</param>
        public override string PerformTurn(ACombatant defaultEnemy)
        {
            var spell = SpellBook
                .OfType<ASpell>()
                .OrderByDescending(s => s.Rarity)
                .FirstOrDefault(s => s.ManaCost <= Mana);
            string attackResult;

            if (spell != null)
            {
                if (spell is AttackSpell attackSpell)
                {
                    LastDamageDealt = attackSpell.Damage;
                    attackResult = spell.CastAbility(this, defaultEnemy);
                }
                else
                {
                    LastDamageDealt = 0;
                    attackResult = spell.CastAbility(this, this);
                }
            }
            else
            {
                int damage = Attack();
                defaultEnemy.TakeDamage(damage);
                attackResult = string.Format(UIConfig.Messages.MessageAttack, Name, defaultEnemy.Name, damage);
            }
            return attackResult;
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{base.ToString()} [MANA]: {Mana} [ARCANE]: {Arcane}");
            return stringBuilder.ToString();
        }
    }
}
