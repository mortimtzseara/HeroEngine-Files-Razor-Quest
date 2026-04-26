using HeroEngine.Core.Data;
using HeroEngine.Core.Logic;
using HeroEngine.Core.Models.Combatants.Heroes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages
{
    public class StatsModel : PageModel
    {
        private readonly HeroRepository _repo;

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
        }
    }
}