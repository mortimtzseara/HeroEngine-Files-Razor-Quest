using HeroEngine.Core.Logic;

namespace HeroEngine.Core.Data
{
    public class CsvStatsWriter
    {
        private readonly string _filePath;
        public CsvStatsWriter(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                string header = "Date,Heroes,Enemies,Outcome,Rounds,TotalDamage,MostEffective" + System.Environment.NewLine;
                File.WriteAllText(_filePath, header);
            }
        }

        public void AppendCombatStats(CombatStats result)
        {
            File.AppendAllText(_filePath, result.ToCsvLine());
        }
    }
}