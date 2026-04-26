using HeroEngine.Core.Models.Combatants;
using HeroEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroEngine.Core.Logic
{
    /// <summary>
    /// Tracks and summarizes combat statistics, including total damage dealt, damage per combatant, and defeat rounds.
    /// </summary>
    public class CombatStats
    {
        public int TotalDamageDealt { get; private set; }
        private readonly Dictionary<ACombatant, int> DamagePerCombatant = new Dictionary<ACombatant, int>();
        private readonly Dictionary<ACombatant, int> RoundsToDefeat = new Dictionary<ACombatant, int>();
        private int TotalTurns = 0;
        private readonly int CombatantsCount;
        public List<string> HeroNames { get; set; }
        public List<string> EnemyNames { get; set; }
        public string Outcome { get; set; }
        public DateTime CombatDate { get; private set; } = DateTime.Now;

        public CombatStats(int combatantsCount)
        {
            CombatantsCount = combatantsCount;
        }

        /// <summary>
        /// Records a combat action, updating total and per-combatant damage, and tracks the round a target is defeated.
        /// </summary>
        /// <param name="caster">The combatant performing the action.</param>
        /// <param name="target">The combatant receiving the action.</param>
        /// <param name="damage">The amount of damage dealt.</param>
        public void RegisterAction(ACombatant caster, ACombatant target, int damage)
        {
            TotalTurns++;
            TotalDamageDealt += damage;

            if (!DamagePerCombatant.ContainsKey(caster)) DamagePerCombatant[caster] = 0;
            DamagePerCombatant[caster] += damage;

            if (target.IsDefeated && !RoundsToDefeat.ContainsKey(target))
            {
                int currentRound = (TotalTurns - 1) / CombatantsCount + 1;
                RoundsToDefeat[target] = currentRound;
            }
        }
        /// <summary>
        /// Generates a formatted summary of combat results, including total damage dealt, the most effective combatant,
        /// and the fastest defeat.
        /// </summary>
        /// <returns>A string containing the formatted combat summary.</returns>
        public string GetSummary()
        {
            var mostEffective = DamagePerCombatant.OrderByDescending(x => x.Value).FirstOrDefault();
            var fastestDefeat = RoundsToDefeat.OrderBy(x => x.Value).FirstOrDefault();

            return string.Format(UIConfig.Messages.CombatSummaryTemplate, TotalDamageDealt, mostEffective.Key.Name, mostEffective.Value, fastestDefeat.Key.Name, fastestDefeat.Value);
        }
        /// <summary>
        /// Generates a CSV-formatted line representing the combat summary, including combat date, hero and enemy names,
        /// outcome, total rounds, total damage dealt, and the most effective combatant.
        /// </summary>
        /// <returns>A string containing the combat summary in CSV format, with fields separated by commas and lists separated by
        /// semicolons.</returns>
        public string ToCsvLine()
        {
            var mostEffective = DamagePerCombatant.OrderByDescending(x => x.Value).FirstOrDefault();
            int totalRounds = (TotalTurns - 1) / CombatantsCount + 1;

            return string.Format("{0},{1},{2},{3},{4},{5},{6}{7}",
                CombatDate,
                string.Join(";", HeroNames),
                string.Join(";", EnemyNames),
                Outcome,
                totalRounds,
                TotalDamageDealt,
                mostEffective.Key?.Name ?? "N/A",
                Environment.NewLine);
        }
    }
}