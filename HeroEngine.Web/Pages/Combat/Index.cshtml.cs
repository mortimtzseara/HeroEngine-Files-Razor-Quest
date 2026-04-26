using HeroEngine.Core.Data;
using HeroEngine.Core.Logic;
using HeroEngine.Core.Models;
using HeroEngine.Core.Models.Combatants;
using HeroEngine.Core.Models.Combatants.Enemies;
using HeroEngine.Core.Models.Combatants.Heroes;
using HeroEngine.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages.Combat
{
    public class IndexModel : PageModel
    {
        private readonly HeroRepository _heroRepo;

        public List<AHero> AvailableHeroes { get; set; }

        [BindProperty]
        public int MinionCount { get; set; }
        [BindProperty]
        public int EliteCount { get; set; }
        [BindProperty]
        public int BossCount { get; set; }

        public string LastCombatLog { get; set; }

        public IndexModel(HeroRepository heroRepo) => _heroRepo = heroRepo;

        public void OnGet()
        {
            LoadData();
        }

        public IActionResult OnPost()
        {
            LoadData();

            var allHeroes = AvailableHeroes.Cast<ACombatant>().ToList();

            if (allHeroes.Count == 0)
            {
                ModelState.AddModelError(string.Empty, UIConfig.Exceptions.NoHeroesAvailable);
            }

            if (MinionCount + EliteCount + BossCount <= 0)
            {
                ModelState.AddModelError(string.Empty, UIConfig.Exceptions.NoEnemiesAvailable);
            }
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            List<ACombatant> enemies = new List<ACombatant>();
            for (int i = 0; i < MinionCount; i++) enemies.Add(new Minion($"Minion {i + 1}"));
            for (int i = 0; i < EliteCount; i++) enemies.Add(new Elite($"Elite {i + 1}"));
            for (int i = 0; i < BossCount; i++) enemies.Add(new Boss($"Boss {i + 1}"));

            var engine = new CombatEngine(allHeroes, enemies);
            engine.RunBattle();

            if (System.IO.File.Exists(UIConfig.Path.TxtPath))
            {
                string allLog = System.IO.File.ReadAllText(UIConfig.Path.TxtPath);
                var sessions = allLog.Split(new string[] { UIConfig.Decoration.EndMark }, StringSplitOptions.RemoveEmptyEntries);

                if (sessions.Length > 0)
                {
                    string lastSession = sessions.LastOrDefault(s => !string.IsNullOrWhiteSpace(s))?.Trim();

                    if (lastSession != null)
                    {
                        LastCombatLog = UIConfig.Decoration.EndMark + "\n" + lastSession + "\n" + UIConfig.Decoration.EndMark;
                    }
                }
            }

            return Page();
        }
        private void LoadData()
        {
            AvailableHeroes = _heroRepo.LoadAll().ToList();
        }
    }
}