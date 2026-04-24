using Microsoft.AspNetCore.Mvc.RazorPages;
using HeroEngine.Core.Data;
using HeroEngine.Core.Models.Combatants.Heroes;
using System.Collections.Generic;

namespace HeroEngine.Web.Pages.Heroes
{
    public class IndexModel : PageModel
    {
        private readonly HeroRepository _heroRepository;

        public IEnumerable<AHero> Heroes { get; set; } = new List<AHero>();

        public IndexModel(HeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public void OnGet()
        {
            Heroes = _heroRepository.LoadAll();
        }
    }
}