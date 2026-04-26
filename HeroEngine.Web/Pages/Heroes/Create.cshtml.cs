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
            [Range(1, 100, ErrorMessage = UIConfig.Exceptions.ErrorInvalidLevel)]
            public int Level { get; set; } = 1;
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
                    newHero = new Warrior(Input.Name, Input.Level);
                    break;

                case "Mage":
                    var mage = new Mage(Input.Name, Input.Level);

                    mage.LearnSpell(new AttackSpell("Thunder Smash", TypeRarity.Legendary, 40, 95));
                    mage.LearnSpell(new DefenseSpell("Iron Fortress", TypeRarity.Epic, 25, 10));
                    mage.LearnSpell(new SupportSpell("War Taunt", TypeRarity.Common, 5, 2));
                    mage.LearnSpell(new HealingSpell("Second Wind", TypeRarity.Rare, 15, 25));
                    
                    newHero = mage;
                    break;

                case "Rogue":
                    newHero = new Rogue(Input.Name, Input.Level);
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