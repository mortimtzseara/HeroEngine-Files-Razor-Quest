using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using HeroEngine.Core.Models.Combatants.Heroes;
using HeroEngine.UI;

namespace HeroEngine.Core.Data
{

    public class HeroRepository
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions;

        public HeroRepository(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(UIConfig.Exceptions.EmptyPathException, nameof(filePath));
            }
            _filePath = filePath;

            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            _jsonOptions.Converters.Add(new JsonStringEnumConverter());
        }

        /// <summary>
        /// Loads all hero records from the underlying data store.
        /// </summary>
        /// <remarks>If the data file does not exist or is empty, the method returns an empty collection
        /// rather than null. The returned collection may be empty if no heroes have been saved.</remarks>
        /// <returns>An enumerable collection of <see cref="AHero"/> objects representing all loaded heroes. Returns an empty
        /// collection if no data is available or the data store does not exist.</returns>
        public IEnumerable<AHero> LoadAll()
        {
            if (!File.Exists(_filePath)) return new List<AHero>();

            string json = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json)) return new List<AHero>();

            try
            {
                return JsonSerializer.Deserialize<List<AHero>>(json, _jsonOptions) ?? new List<AHero>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine(UIConfig.Exceptions.ErrorReadingJson, ex.Message);
                return new List<AHero>();
            }
        }

        /// <summary>
        /// Serializes and saves the specified collection of heroes to a JSON file.
        /// </summary>
        /// <remarks>
        /// The output file is overwritten if it already exists. Serialization uses 
        /// indented JSON formatting for improved readability.
        /// </remarks>
        /// <param name="heroes">The collection of AHero instances to be saved. Cannot be null.</param>
        /// <exception cref="ArgumentNullException">Thrown if the heroes parameter is null.</exception>
        public void SaveAll(IEnumerable<AHero> heroes)
        {
            if (heroes == null)
            {
                throw new ArgumentNullException(nameof(heroes));
            }

            string json = JsonSerializer.Serialize(heroes, _jsonOptions);

            File.WriteAllText(_filePath, json);
        }
        /// <summary>
        /// Adds a hero to the collection of stored heroes.
        /// </summary>
        /// <param name="hero">The hero to add to the collection. Cannot be null.</param>
        public void AddHero(AHero hero)
        {
            if (hero == null) throw new ArgumentNullException(nameof(hero));

            var heroes = LoadAll().ToList();

            if (heroes.Any(h => h.Name.Equals(hero.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(string.Format(UIConfig.Exceptions.HeroAlreadyExists, hero.Name));
            }

            heroes.Add(hero);

            SaveAll(heroes);
        }
        /// <summary>
        /// Deletes the hero with the specified name from the collection.
        /// </summary>
        /// <param name="name">The name of the hero to delete. Comparison is case-insensitive.</param>
        /// <exception cref="KeyNotFoundException">Thrown if a hero with the specified name does not exist in the collection.</exception>
        public void DeleteHero(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(string.Format(UIConfig.Exceptions.ErrorEmptyName, nameof(name)));

            var heroes = LoadAll().ToList();

            var heroToRemove = heroes.FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (heroToRemove == null)
            {
                throw new KeyNotFoundException(UIConfig.Exceptions.KeyNotFoundException);
            }

            heroes.Remove(heroToRemove);
            SaveAll(heroes);
        }
        /// <summary>
        /// Retrieves a hero whose name matches the specified value, using a case-insensitive comparison.
        /// </summary>
        /// <param name="name">The name of the hero to search for. Cannot be null, empty, or consist only of white-space characters.</param>
        /// <returns>An instance of <see cref="AHero"/> whose <c>Name</c> matches the specified value, or <see langword="null"/>
        /// if no matching hero is found.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is null, empty, or consists only of white-space characters.</exception>
        public AHero GetHeroByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(string.Format(UIConfig.Exceptions.ErrorEmptyName, nameof(name)));
            var heroes = LoadAll();
            return heroes.FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
