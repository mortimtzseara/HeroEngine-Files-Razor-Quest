using HeroEngine.Core.Models.Combatants;
using HeroEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace HeroEngine.Core.Logic
{
    /// <summary>
    /// Engine that manages polymorphic combat between heroes and enemies.
    /// </summary>
    public class CombatEngine
    {
        private readonly List<ACombatant> Heroes;
        private readonly List<ACombatant> Enemies;
        private readonly StringBuilder BattleLog;
        private readonly List<ACombatant> Queue;
        private readonly CombatStats Stats;

        public CombatEngine(List<ACombatant> heroes, List<ACombatant> enemies)
        {
            Heroes = heroes;
            Enemies = enemies;
            BattleLog = new StringBuilder();
            Queue = Heroes.Concat(Enemies).OrderByDescending(combatant => combatant.Initiative).ToList();
            Stats = new CombatStats(Queue.Count);
        }

        /// <summary>
        /// Runs the automatic combat loop until one side is defeated.
        /// </summary>
        public void RunBattle()
        {
            BattleLog.AppendLine(UIConfig.Decoration.BattleTitle);
            Console.WriteLine(UIConfig.Decoration.BattleTitle);

            while (Heroes.Any(h => !h.IsDefeated) && Enemies.Any(e => !e.IsDefeated))
            {
                ACombatant participant = Queue[0];
                Queue.RemoveAt(0);

                if (!participant.IsDefeated)
                {
                    ACombatant? target = GetTarget(participant);

                    if (target != null)
                    {
                        string turnResult = participant.PerformTurn(target);
                        int damageDealt = participant.LastDamageDealt;

                        Stats.RegisterAction(participant, target, damageDealt);

                        Console.WriteLine(turnResult);
                        BattleLog.AppendLine(turnResult);

                        if (target.IsDefeated)
                        {
                            string defeatedMessage = string.Format(UIConfig.Messages.MessageDefeated, target.Name);
                            Console.WriteLine(defeatedMessage);
                            BattleLog.AppendLine(defeatedMessage);
                        }

                        Queue.Add(participant);
                        Thread.Sleep(500);

                    }
                }
            }
            string finalStats = Stats.GetSummary();

            Console.WriteLine(finalStats);
            BattleLog.AppendLine(finalStats);

            FinalizeBattle();
        }
        /// <summary>
        /// Completes the battle by determining the outcome, displaying and logging the result, resetting participant
        /// stats, and saving the battle log to a file.
        /// </summary>
        private void FinalizeBattle()
        {
            bool heroesWon = Heroes.Any(h => !h.IsDefeated);
            string result = heroesWon ? UIConfig.Messages.MessageVictory : UIConfig.Messages.MessageDefeat;

            Console.WriteLine(result);
            BattleLog.AppendLine(result);
            foreach (var p in Heroes.Concat(Enemies))
            {
                p.ResetStats();
            }
            File.WriteAllText(UIConfig.Path.LogPath, BattleLog.ToString());
        }
        /// <summary>
        /// Finds the first undefeated enemy combatant in the queue relative to the specified participant.
        /// </summary>
        /// <param name="participant">The combatant for whom to find an enemy target.</param>
        /// <returns>The first undefeated enemy combatant, or null if none are found.</returns>
        private ACombatant? GetTarget(ACombatant participant)
        {
            bool isHero = Heroes.Contains(participant);

            Predicate<ACombatant> isEnemy = c => isHero ? Enemies.Contains(c) : Heroes.Contains(c);

            return Queue.FirstOrDefault(target => isEnemy(target) && !target.IsDefeated);
        }
    }
}
