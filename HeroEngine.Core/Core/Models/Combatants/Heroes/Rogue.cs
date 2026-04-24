using HeroEngine.UI;
using System.Text;

namespace HeroEngine.Core.Models.Combatants.Heroes
{
    /// <summary>
    /// Represents a rogue hero with specialized sneaky damage and hidden daggers.
    /// </summary>
    public class Rogue : AHero
    {
        public float SneakyDamage => UIConfig.Numeric.BaseSneakyDamage + UIConfig.Numeric.SneakyDamageGainPerLevel * (Level - 1);
        public int HiddenDaggers { get; set; }
        public override int Initiative => base.Initiative + Level * 2 + HiddenDaggers;
        public Rogue(string name, int level, int hiddenDaggers = 1): base(name, level, UIConfig.Numeric.BaseHP, UIConfig.Numeric.BaseDamage)
        {
            HiddenDaggers = hiddenDaggers;
        }
        public override int Attack() => (int)(SneakyDamage * TotalDamage);
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{base.ToString()} [SNEAKY DAMAGE]: {SneakyDamage} [HIDDEN DAGGERS]: {HiddenDaggers}");
            return stringBuilder.ToString();
        }
    }
}
