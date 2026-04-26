using HeroEngine.UI;
using System.Xml.Linq;

namespace HeroEngine.Core.Logic
{
    public class GameConfig
    {
        public double LevelMultiplier { get; set; } = 1.15;
        public double CriticalHitChance { get; set; } = 0.20;
        public int MaxCombatRounds { get; set; } = 20;
        public int MaxHeroesPerBattle { get; set; } = 4;

        public string _path;
        public GameConfig() { }
        public GameConfig(string path)
        {
            _path = path;
            Load();
        }
        public void Load()
        {
            if (File.Exists(_path))
            {
                XDocument doc = XDocument.Load(_path);
                var root = doc.Element("GameConfig");
                if (root != null)
                {
                    LevelMultiplier = double.Parse(root.Element("LevelMultiplier")?.Value ?? "1.15");
                    CriticalHitChance = double.Parse(root.Element("CriticalHitChance")?.Value ?? "0.20");
                    MaxCombatRounds = int.Parse(root.Element("MaxCombatRounds")?.Value ?? "20");
                    MaxHeroesPerBattle = int.Parse(root.Element("MaxHeroesPerBattle")?.Value ?? "4");
                }
            }
            else
            {
                Save();
            }
        }

        public void Save(string path = null)
        {
            string targetPath = path ?? _path;
            if (string.IsNullOrEmpty(targetPath)) throw new Exception(UIConfig.Exceptions.NoPathForConfig);

            XDocument doc = new XDocument(
                new XElement("GameConfig",
                    new XElement("LevelMultiplier", LevelMultiplier),
                    new XElement("CriticalHitChance", CriticalHitChance),
                    new XElement("MaxCombatRounds", MaxCombatRounds),
                    new XElement("MaxHeroesPerBattle", MaxHeroesPerBattle)
                )
            );
            doc.Save(targetPath);
        }
    }
}