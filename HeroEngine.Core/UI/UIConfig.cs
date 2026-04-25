using HeroEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HeroEngine.UI
{
    public static class UIConfig
    {
        public static void ShowMenu()
        {
            Console.WriteLine("=== HERO ENGINE MENU ===");
            Console.WriteLine("1 - Create Character");
            Console.WriteLine("2 - Character details");
            Console.WriteLine("3 - Add Spells");
            Console.WriteLine("4 - Combat");
            Console.WriteLine("5 - Exit");
        }
        public static class Menus
        {
            public const string ChooseOp = "Choose an option: ";
            public const string MenuHeroType = "Choose a Hero Type:";
            public const string ChooseMage = "Choose a Mage: ";
            public const string ChooseSpell = "Choose a spell: ";
            public const string HeroTypeOptions = "\n1 - Warrior\t2 - Mage\t3 - Rogue";
            public const string InvalidInput = "Invalid input, try again.";
            public const string NameInput = "Name:";
            public const string LevelInput = "Level (min 1, max 100):";
            public const string HeroCreated = "Hero created correctly! Press a key to continue...";
            public const string EmptyHeroes = "No heroes created yet.";
            public const string PressAKey = "Press a key to continue...";
            public const string EmptyMages = "You have no mages created yet.";
            public const string AddEnemy = "Add an enemy:";
            public const string EnemyOp = "1 - Minion\n2 - Elite\n3 - Boss\n0 - START BATTLE!";
            public const string EnemyCount = "Enemies: {0}";
            public const string EmptyEnemy = "You need at least one enemy.";

        }
        public static class Messages
        {
            public const string MessageSpellLearned = "{0} learned the spell {1}.";
            public const string MessageSpellExists = "{0} already knows this spell.";
            public const string MessageGetDamage = "{0} took {1} damage | HP: {2}/{3}";
            public const string MessageGetDamageWarrior = "{0} took {1} damage -> absorbed {2} by armor -> net damage: {3} | HP: {4}/{5}";
            public const string MessageCannotUseAbility = "You can't use this ability";
            public const string MessageAttack = "{0} attacks {1}! Takes {2} damage.";
            public const string MessageNotEnoughMana = "You don't have enough mana.";
            public const string DefaultBattleCry = "For Bytecroft! My code compiles on the first try!";
            public const string MessageAttackSpell = "{0} casts {1}! {2} takes {3} damage.";
            public const string MessageDefenseSpell = "{0} casts {1}! Armor increased by {2}.";
            public const string MessageHealingSpell = "{0} used {1} and healed {2} HP";
            public const string MessageSupportSpell = "{0} casts {1}! Attack increased by {2}.";
            public const string MessageEmptySpellBook = "The spell book is empty... Try learning a new spell!!";
            public const string MessageDefeated = "{0} is DEFEATED!";

            public const string MessageVictory = "VICTORY!";
            public const string MessageDefeat = "DEFEAT";

            public const string CombatSummaryTemplate = """

                    --- COMBAT STATISTICS ---
                    Total Damage Dealt: {0}
                    Most Effective Combatant: {1} ({2} dmg)
                    Fastest Defeat: {3} (in {4} turns)
                    -------------------------
        """;
            public const string LogHeader = "=== COMBAT LOG {0} ===\n";
        }
        public static class Numeric
        {
            public const int HpGainPerLvl = 10;
            public const int ArmorGainPerLvl = 1;
            public const int ArcaneGainPerLvl = 1;
            public const int ManaGainPerLvl = 5;
            public const int DamageGainPerLvl = 1;
            public const float SneakyDamageGainPerLevel = 0.1f;

            public const int BaseHP = 100;
            public const int BaseMana = 25;
            public const int BaseArcane = 2;
            public const int BaseArmor = 5;
            public const int BaseDamage = 5;
            public const int BaseSneakyDamage = 2;
            public const int BaseInitiative = 10;

            public const int MinionBaseHP = 30;
            public const int MinionBaseDamage = 5;
            public const int MinionBaseInitiative = 2;

            public const int EliteBaseHP = 80;
            public const int EliteBaseDamage = 15;
            public const int EliteBaseInitiative = 0;

            public const int BossBaseHP = 250;
            public const int BossBaseDamage = 40;
            public const int BossBaseInitiative = 5;
        }
        public static class Decoration
        {
            public const string SpellBookTitle = "--- {0}'S SPELL BOOK ---";
            public const string BattleTitle = """
            ==================================================
                      BATTLE START: HEROES VS BUGS
            ==================================================
            """;
            public const string PrepareForBattle = "--- PREPARE FOR BATTLE ---";
        }
        public static class Path
        {
            public const string TxtPath = "Data/combat_log.txt";
            public const string CsvPath = "Data/combat_stats.csv";
        }
        public static class Exceptions
        {
            public const string KeyNotFoundException = "Cannot find the hero with the name '{0}' to delete.";
            public const string EmptyPathException = "The file path cannot be empty.";
            public const string ErrorReadingJson = "Error reading JSON: {0}";
            public const string HeroAlreadyExists = "A hero with the name '{0}' already exists.";
            public const string ErrorEmptyName = "Name cannot be empty.";
            public const string ErrorLengthName = "Name must be between 3 and 15 characters.";
            public const string ErrorEmptyHeroClass = "Hero class must be selected.";
        }
    }
}
