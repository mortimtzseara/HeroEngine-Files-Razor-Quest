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
        public IActionResult OnGet(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return RedirectToPage("/Heroes/Index");

            Hero = _heroRepository.GetHeroByName(name);

            if (Hero == null) return RedirectToPage("/Heroes/Index");
            return Page();
        }
    }
}