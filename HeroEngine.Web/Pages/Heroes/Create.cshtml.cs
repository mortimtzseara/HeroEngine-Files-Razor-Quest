using HeroEngine.Core.Data;
using HeroEngine.Core.enums;
using HeroEngine.Core.Models.Combatants.Heroes;
using HeroEngine.Core.Models.Spells;
using HeroEngine.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace HeroEngine.Web.Pages.Heroes
{
    public class CreateModel : PageModel
    {
        private readonly HeroRepository _heroRepository;

        public CreateModel(HeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        [BindProperty]
        public HeroInput Input { get; set; } = new HeroInput();

        public class HeroInput
        {
            [Required(ErrorMessage = UIConfig.Exceptions.ErrorEmptyName)]
            [StringLength(15, MinimumLength = 3, ErrorMessage = UIConfig.Exceptions.ErrorLengthName)]
            public string Name { get; set; }

            [Required(ErrorMessage = UIConfig.Exceptions.ErrorEmptyHeroClass)]
            public string HeroClass { get; set; }
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            AHero newHero = null;

            switch (Input.HeroClass)
            {
                case "Warrior":
                    newHero = new Warrior(Input.Name, 1);
                    break;

                case "Mage":
                    var mage = new Mage(Input.Name, 1);
                    mage.LearnSpell(new AttackSpell("Fireball", TypeRarity.Common, 10, 15));
                    newHero = mage;
                    break;

                case "Rogue":
                    newHero = new Rogue(Input.Name, 1);
                    break;
            }

            try
            {
                _heroRepository.AddHero(newHero);
                return RedirectToPage("/Heroes/Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Input.Name", ex.Message);
                return Page();
            }
        }
    }
}