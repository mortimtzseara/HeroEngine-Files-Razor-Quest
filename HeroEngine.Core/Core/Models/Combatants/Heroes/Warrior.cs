using HeroEngine.UI;
using System.Text;

namespace HeroEngine.Core.Models.Combatants.Heroes
{
    public class Warrior : AHero
    {
        public int Armor => UIConfig.Numeric.BaseArmor + UIConfig.Numeric.ArmorGainPerLvl * (Level - 1);
        public override int TotalArmor => Armor + ArmorModifier;
        public override int Initiative => base.Initiative - Armor / 5;
        public string BattleCry { get; set; }
        public Warrior(string name, int level, string battleCry = UIConfig.Messages.DefaultBattleCry) : base(name, level, UIConfig.Numeric.BaseHP, UIConfig.Numeric.BaseDamage)
        {
            BattleCry = battleCry;
        }
        public override int Attack() => TotalDamage * 3;
        public override string ToString() 
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{base.ToString()} [ARMOR]: {Armor} [BATTLE CRY]: {BattleCry}");
            return stringBuilder.ToString();
        }
    }
}
