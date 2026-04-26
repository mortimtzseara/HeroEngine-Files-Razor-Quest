using System.Text.RegularExpressions;
using HeroEngine.Core.Models.Combatants.Heroes;

namespace HeroEngine.Core.Logic
{
    public class HeroAnalytics
    {
        private readonly List<AHero> _heroes;

        public HeroAnalytics(List<AHero> heroes)
        {
            _heroes = heroes;
        }

        public List<AHero> GetTopHeroesByLevel(int n)
        {
            return _heroes
                .OrderByDescending(h => h.Level)
                .Take(n)
                .ToList();
        }
        public List<AHero> SearchHeroesByName(string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return _heroes
                .Where(h => regex.IsMatch(h.Name))
                .ToList();
        }

        public Dictionary<string, int> GetHeroDistributionByClass()
        {
            return _heroes
                .GroupBy(h => h.GetType().Name)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<string, double> GetAverageDamagePerClass()
        {
            return _heroes
                .GroupBy(h => h.GetType().Name)
                .ToDictionary(
                    grupo => grupo.Key,
                    grupo => grupo.Average(h => (double)h.TotalDamage)
                );
        }
    }
}