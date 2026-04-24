using Microsoft.AspNetCore.Mvc.RazorPages;
using HeroEngine.Core.Data;
using HeroEngine.Core.Models.Combatants.Heroes;

namespace HeroEngine.Web.Pages
{   
    /// <summary>
    /// Represents the page model for the index page, providing access to the collection of available heroes and
    /// handling page requests.
    /// </summary>
    /// <remarks>This model is used by the ASP.NET Core Razor Pages framework to manage the data and actions
    /// for the index page. It exposes the list of heroes for display and coordinates data retrieval during page
    /// requests.</remarks>
    public class IndexModel : PageModel
    {
        private readonly HeroRepository _heroRepository;
        public int TotalHeroes { get; set; }
        public IEnumerable<AHero> RecentHeroes { get; set; } = new List<AHero>();

        public IEnumerable<AHero> Heroes { get; set; } = new List<AHero>();
        public IndexModel(HeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }
        /// <summary>
        /// Handles GET requests for the page and loads all available heroes into the model.
        /// </summary>
        /// <remarks>Call this method during the page's GET request lifecycle to populate the heroes
        /// collection for display. This method is typically invoked by the ASP.NET Core framework when the page is
        /// accessed via an HTTP GET request.</remarks>
        public void OnGet()
        {
            Heroes = _heroRepository.LoadAll();
            TotalHeroes = Heroes.Count();
            RecentHeroes = Heroes.Take(3);
        }
    }
}