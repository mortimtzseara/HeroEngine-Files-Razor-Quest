using HeroEngine.UI;

namespace HeroEngine.Core.Models.Combatants
{
    /// <summary>
    /// Represents an abstract combatant for use in combat scenarios.
    /// </summary>
    public abstract class ACombatant
    {
        public string Name { get; init; }
        public int MaxHP { get; protected set; }
        protected int BaseDamage { get; set; }
        public int DamageModifier { get; set; } = 0;
        public int ArmorModifier { get; set; } = 0;
        public virtual int TotalArmor => ArmorModifier;
        public int TotalDamage => BaseDamage + DamageModifier;
        private int _currentHP;
        public int CurrentHP
        {
            get => _currentHP;
            protected set => _currentHP = Math.Clamp(value, 0, MaxHP);
        }
        public bool IsDefeated => CurrentHP <= 0;
        public virtual int Initiative => UIConfig.Numeric.BaseInitiative;
        public int LastDamageDealt { get; protected set; } = 0;
        protected ACombatant(string name, int maxHP, int baseDamage)
        {
            Name = name;
            MaxHP = maxHP;
            CurrentHP = maxHP;
            BaseDamage = baseDamage;
        }
        /// <summary>
        /// Reset the temporary buff modifier.
        /// </summary>
        public virtual void ResetStats()
        {
            ArmorModifier = 0;
            DamageModifier = 0;
            CurrentHP = MaxHP;
            LastDamageDealt = 0;
        }
        /// <summary>
        /// Heals a combatant.
        /// </summary>
        /// <param name="amount">Amount of HP healed.</param>
        public void Heal(int amount) => CurrentHP += amount;
        /// <summary>
        /// Calculate damage of a basic attack.
        /// </summary>
        /// <returns>Damage of the attack</returns>
        public abstract int Attack();
        /// <summary>
        /// Applies damage to the object.
        /// </summary>
        /// <param name="damage">The amount of damage to apply.</param>
        public virtual int TakeDamage(int damage) 
        {
            if (IsDefeated) return 0;
            int netDamage = Math.Max(0, damage - TotalArmor);
            CurrentHP -= netDamage;
            return netDamage;
        }
        /// <summary>
        /// Executes an attack against the specified target and applies damage.
        /// </summary>
        /// <param name="target">The combatant to receive the attack.</param>
        public virtual string PerformTurn(ACombatant target)
        {
            LastDamageDealt = Attack();

            int damage = target.TakeDamage(LastDamageDealt);

            return string.Format(UIConfig.Messages.MessageAttack, Name, target.Name, damage);
        }
        public override string ToString() => $"[{GetType().Name}] --- [NAME]: {Name} [HP]: {CurrentHP}/{MaxHP}";

    }
}
