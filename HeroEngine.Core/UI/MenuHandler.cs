using HeroEngine.Core.Interfaces;
using HeroEngine.Core.Logic;
using HeroEngine.Core.Models;
using HeroEngine.Core.Models.Combatants;
using HeroEngine.Core.Models.Combatants.Enemies;
using HeroEngine.Core.Models.Combatants.Heroes;
using HeroEngine.Core.Models.Spells;
using HeroEngine.Tools;

namespace HeroEngine.UI
{
    public static class MenuHandler
    {
        public static void CreateHero(List<AHero> heroes)
        {
            Console.Clear();
            int heroOp;
            bool isValid;
            int lvl;

            do
            {
                heroOp = Tools.Utils.ReadInt(UIConfig.Menus.MenuHeroType + UIConfig.Menus.HeroTypeOptions, UIConfig.Menus.InvalidInput);

                isValid = (heroOp >= 1 && heroOp <= 3);

                if (!isValid)
                {
                    Console.WriteLine(UIConfig.Menus.InvalidInput);
                }

            } while (!isValid);

            string name = Tools.Utils.ReadString(UIConfig.Menus.NameInput, UIConfig.Menus.InvalidInput, 3);

            do
            {
                lvl = Tools.Utils.ReadInt(UIConfig.Menus.LevelInput, UIConfig.Menus.InvalidInput);
                isValid = (lvl >= 1 && lvl <= 100);

            } while (!isValid);

            switch (heroOp)
            {
                case 1:
                    heroes.Add(new Warrior(name, lvl));
                    break;
                case 2:
                    heroes.Add(new Mage(name, lvl));
                    break;
                case 3:
                    heroes.Add(new Rogue(name, lvl));
                    break;
            }

            Console.WriteLine(UIConfig.Menus.HeroCreated);
            Console.ReadKey();
        }

        public static void ShowHeroDetails(List<AHero> heroes)
        {
            Console.Clear();
            if (!heroes.Any()) { Console.WriteLine(UIConfig.Menus.EmptyHeroes); Console.ReadKey(); return; }

            heroes.ForEach(hero => Console.WriteLine(hero.ToString()));

            Console.WriteLine();
            Console.WriteLine(UIConfig.Menus.PressAKey);
            Console.ReadKey();
        }

        public static void AddSpellToMage(List<AHero> heroes, List<IAbility> spells)
        {
            var mages = heroes.OfType<Mage>().ToList();
            bool isValid;
            int mageOp;
            int spellOp;
            bool addingSpells = true;

            if (!mages.Any()) 
            { 
                Console.WriteLine(UIConfig.Menus.EmptyMages);
                Console.WriteLine(UIConfig.Menus.PressAKey);
                Console.ReadKey();
                return; 
            }
            while (addingSpells)
            {
                do
                {
                    mages.ForEach(mage => Console.WriteLine($"{mages.IndexOf(mage) + 1} - {mage.Name}"));
                    mageOp = Tools.Utils.ReadInt(UIConfig.Menus.ChooseMage, UIConfig.Menus.InvalidInput);
                    isValid = (mageOp >= 1 && mageOp <= mages.Count);

                } while (!isValid);

                mages[mageOp - 1].ListSpellBook();
                Console.WriteLine();

                do
                {
                    spells.ForEach(spell => Console.WriteLine($"{spells.IndexOf(spell) + 1} - {spell.ToString()}"));
                    spellOp = Tools.Utils.ReadInt(UIConfig.Menus.ChooseSpell, UIConfig.Menus.InvalidInput);
                    isValid = (spellOp >= 1 && spellOp <= spells.Count);

                } while (!isValid);

                mages[mageOp - 1].LearnSpell(spells[spellOp - 1]);
                mages[mageOp - 1].ListSpellBook();
            }
            Console.WriteLine(UIConfig.Menus.PressAKey);
            Console.ReadKey();

        }

        public static void StartCombat(List<AHero> heroes)
        {
            int enemyOp;

            if (!heroes.Any()) 
            { 
                Console.WriteLine(UIConfig.Menus.EmptyHeroes);
                Console.WriteLine(UIConfig.Menus.PressAKey);
                Console.ReadKey();
                return;
            }

            List<ACombatant> enemies = new List<ACombatant>();
            bool addingEnemies = true;
           
            while (addingEnemies)
            {
                Console.Clear();
                Console.WriteLine(UIConfig.Decoration.PrepareForBattle);
                Console.WriteLine(string.Format(UIConfig.Menus.EnemyCount, enemies.Count));
                enemies.ForEach(enemy => Console.WriteLine(enemy.ToString()));

                enemyOp = Tools.Utils.ReadInt(UIConfig.Menus.EnemyOp, UIConfig.Menus.InvalidInput);

                switch (enemyOp)
                {
                    case 1:
                        enemies.Add(new Minion("Minion"));
                        break;
                    case 2:
                        enemies.Add(new Elite("Elite"));
                        break;
                    case 3:
                        enemies.Add(new Boss("Boss"));
                        break;
                    case 0:
                        if (enemies.Any()){ addingEnemies = false; Thread.Sleep(500); }
                        else Console.WriteLine(UIConfig.Menus.EmptyEnemy);
                        break;
                    default:
                        Console.WriteLine(UIConfig.Menus.InvalidInput);
                        break;
                }
            }

            if (enemies.Any())
            {
                var engine = new CombatEngine(heroes.Cast<ACombatant>().ToList(), enemies);
                Console.Clear();
                engine.RunBattle();
            }
            Console.ReadKey();
        }
    }
}
