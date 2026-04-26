using HeroEngine.Core.Logic;

namespace HeroEngine.Core.Data
{
    public class CombatRecord
    {
        public string Date { get; set; }
        public string Heroes { get; set; }
        public string Enemies { get; set; }
        public string Outcome { get; set; }
        public int Rounds { get; set; }
        public int TotalDamage { get; set; }
        public string MostEffective { get; set; }
    }
    public class CsvStatsManager
    {
        private readonly string _filePath;
        public CsvStatsManager(string filePath)
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
        public List<CombatRecord> ReadAll()
        {
            var records = new List<CombatRecord>();

            if (File.Exists(_filePath))
            {
                string[] lines = File.ReadAllLines(_filePath);

                for (int i = 1; i < lines.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(lines[i]))
                    {
                        string[] columns = lines[i].Split(',');

                        if (columns.Length >= 7)
                        {
                            records.Add(new CombatRecord
                            {
                                Date = columns[0],
                                Heroes = columns[1].Replace(";", ", "),
                                Enemies = columns[2].Replace(";", ", "),
                                Outcome = columns[3],
                                Rounds = int.Parse(columns[4]),
                                TotalDamage = int.Parse(columns[5]),
                                MostEffective = columns[6]
                            });
                        }
                    }
                }
            }

            return records;
        }
    }
}