using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HeroEngine.Core.Data;
using HeroEngine.Core.Models.Combatants.Heroes;

namespace HeroEngine.Web.Pages.Heroes
{
    public class DetailModel : PageModel
    {
        private readonly HeroRepository _heroRepository;

        public AHero Hero { get; set; }

        public DetailModel(HeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        /// <summary>
        /// This method runs when the page is requested.
        /// ASP.NET automatically injects the 'name' from the URL.
        /// </summary>
        public IActionResult OnGet(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) RedirectToPage("/Heroes/Index");

            Hero = _heroRepository.GetHeroByName(name);

            if (Hero == null) RedirectToPage("/Heroes/Index");

            return Page();
        }
    }
}