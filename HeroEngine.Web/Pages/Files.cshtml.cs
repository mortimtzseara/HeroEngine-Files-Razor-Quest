using HeroEngine.Core.Logic;
using HeroEngine.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
namespace HeroEngine.Web.Pages
{
    public class FilesModel : PageModel
    {
        [BindProperty] public GameConfig Config { get; set; }
        public string CsvPath => UIConfig.Path.CsvPath;

        public void OnGet()
        {
            Config = new GameConfig(UIConfig.Path.XmlPath);
        }

        public IActionResult OnPost()
        {
            string realPath = UIConfig.Path.XmlPath;
            Config._path = realPath;
            Config.Save();

            return RedirectToPage();
        }

        public IActionResult OnGetDownloadCsv()
        {
            if (!System.IO.File.Exists(CsvPath)) return NotFound();
            byte[] fileBytes = System.IO.File.ReadAllBytes(CsvPath);
            return File(fileBytes, "text/csv", "combat_stats.csv");
        }
    }
}