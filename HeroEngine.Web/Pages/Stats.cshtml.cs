using HeroEngine.Core.Data;
using HeroEngine.Core.Logic;
using HeroEngine.Core.Models.Combatants.Heroes;
using HeroEngine.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages
{
    public class StatsModel : PageModel
    {
        private readonly HeroRepository _repo;
        [BindProperty(SupportsGet = true)] 
        public string OutcomeFilter { get; set; }
        public List<CombatRecord> RecentCombats { get; set; }
        public Dictionary<string, int> ClassDistribution { get; set; }
        public Dictionary<string, double> AverageDamageDistribution { get; set; }
        public List<AHero> TopHeroes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchPattern { get; set; }
        public List<AHero> FilteredHeroes { get; set; }

        public StatsModel(HeroRepository repo) => _repo = repo;

        public void OnGet()
        {
            var allHeroes = _repo.LoadAll().ToList();
            var analytics = new HeroAnalytics(allHeroes);

            ClassDistribution = analytics.GetHeroDistributionByClass();

            AverageDamageDistribution = analytics.GetAverageDamagePerClass();

            TopHeroes = analytics.GetTopHeroesByLevel(3);

            if (!string.IsNullOrEmpty(SearchPattern))
            {
                FilteredHeroes = analytics.SearchHeroesByName(SearchPattern);
            }
            var reader = new CsvStatsManager(UIConfig.Path.CsvPath);
            var allCombats = reader.ReadAll();

            if (!string.IsNullOrEmpty(OutcomeFilter))
            {
                allCombats = allCombats.Where(c => c.Outcome.Contains(OutcomeFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            RecentCombats = allCombats.AsEnumerable().Reverse().Take(10).ToList();
        }
    }
}