using HeroEngine.Core.enums;
using HeroEngine.Core.Interfaces;
using HeroEngine.Core.Models;
using HeroEngine.Core.Models.Combatants.Enemies;
using HeroEngine.Core.Models.Combatants.Heroes;
using HeroEngine.Core.Models.Spells;
namespace HeroEngine.UI
{
    public class Program
    {      

        static void Main(string[] args)
        {
            List<AHero> myHeroes = new List<AHero>();
            List<IAbility> availableSpells = new List<IAbility>()
            {
                new AttackSpell("Thunder Smash", TypeRarity.Legendary, 40, 95),
                new DefenseSpell("Iron Fortress", TypeRarity.Epic, 25, 10),
                new SupportSpell("War Taunt", TypeRarity.Common, 5, 2),
                new HealingSpell("Second Wind", TypeRarity.Rare, 15, 25)
            };

            bool running = true;

            while (running)
            {
                Console.Clear();
                UIConfig.ShowMenu();

                switch (Tools.Utils.ReadInt(UIConfig.Menus.ChooseOp, UIConfig.Menus.InvalidInput))
                {
                    case 1: MenuHandler.CreateHero(myHeroes); break;
                    case 2: MenuHandler.ShowHeroDetails(myHeroes); break;
                    case 3: MenuHandler.AddSpellToMage(myHeroes, availableSpells); break;
                    case 4: MenuHandler.StartCombat(myHeroes); break;
                    case 5: running = false; break;
                    default: Console.WriteLine(UIConfig.Menus.InvalidInput); break;
                }
            }
        }
    }
}