using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Starwars
{
    class DataController
    {
        List<Planet> planets;
        NameComparer NameComparer;
        
        //Hvorfor ref? Det er ikke særligt smart og ikke nødvendigt!
        //Du skal give mig en virkelig god grund, før end jeg vil godtage denne ;)
        public DataController(ref List<Planet> _planets)
        {
            planets = _planets;
            NameComparer NameComparer = new NameComparer();
        }
       
        /// <summary>
        /// Opgave 1
        /// </summary>
        /// <param name="Namepatten"></param>
        /// <returns></returns>
        public List<Planet> GetPlanetsByName(string Namepatten)
        {
            var result = planets.Where(x => x.Name.Any(y => x.Name.StartsWith(Namepatten)));
            return result.ToList<Planet>();
        }
        /// <summary>
        /// Opgave 2
        /// </summary>
        /// <param name="LetterToLookFor"></param>
        /// <returns></returns>
        public List<Planet> GetPlanetsByNameConatiningLetter(string LetterToLookFor)
        {
            var result = planets.Where(x => x.Name.ToLower().Contains(LetterToLookFor.ToLower()));
            return result.ToList<Planet>();
        }
        /// <summary>
        /// Opgave 3
        /// </summary>
        /// <param name="MinNumbers"></param>
        /// <param name="MaxNumbers"></param>
        /// <returns></returns>
        public List<Planet> GetplanetsByNumerbsOfLetters(int MinNumbers,int MaxNumbers)
        {
            var result = planets.Where(x => x.Name.Length > MinNumbers || x.Name.Length < MaxNumbers);
            return result.ToList<Planet>();
        }
        /// <summary>
        /// Opgave 4
        /// </summary>
        /// <param name="EndLetter"></param>
        /// <param name="LetterAtSecPos"></param>
        /// <returns></returns>
        
        //Attributter ALTID lille begyndelsesbogstav
        public List<Planet> GetPlanetByEndLetter(string endLetter, string letterAtSecPos)
        {
            var result = planets.Where(x => x.Name.ToLower().EndsWith(EndLetter.ToLower())).ToList<Planet>();
            var subResult = result.Where(x => x.Name.ToLower().Substring(1,1).Contains(LetterAtSecPos));
            return subResult.ToList<Planet>();
        }
        /// <summary>
        /// Opgave 5
        /// </summary>
        /// <param name="Piroded"></param>
        /// <returns></returns>
        public List<Planet> GetPlanetsByRotaionsPir(int Piroded)
        {
            var result = planets.Where(x => x.RotationPeriod > Piroded).OrderBy(x => x.RotationPeriod).ToList<Planet>();            
            return result.ToList<Planet>();
        }
        /// <summary>
        /// Opgave 6
        /// </summary>
        /// <param name="MinPir"></param>
        /// <param name="MaxPir"></param>
        /// <returns></returns>
        public List<Planet> GetPlanetsByRotaionsRange(int MinPir,int MaxPir)
        {
            var result = planets.Where(x => x.RotationPeriod > MinPir).OrderBy(x => x.RotationPeriod).ToList<Planet>();
            var subresult = planets.Where(x => x.RotationPeriod > MaxPir).OrderBy(x => x.RotationPeriod).ToList<Planet>();
            return subresult.ToList<Planet>();
        }
        /// <summary>
        /// Opgave 7
        /// </summary>
        /// <param name="MinPir"></param>
        /// <returns></returns>
        public List<Planet> GetPlanetsByRotaionsPirSorted(int MinPir)
        {
            var result = planets.Where(x => x.RotationPeriod > MinPir).OrderBy(x => x.RotationPeriod).ThenBy(x => x.Name).ToList<Planet>();
            
            return result.ToList<Planet>();
        }
        /// <summary>
        /// Opgave 8
        /// </summary>
        /// <param name="maxPir"></param>
        /// <param name="surfaceWaterAmount"></param>
        /// <param name="containsLetter"></param>
        /// <returns></returns>
        
        //Her kan du godt finde ud af lille begyndelsesbogstav!! 
        public List<Planet> GetPlanetsByRot(int maxPir,int surfaceWaterAmount, string containsLetter)
        {
            var result = planets.Where(x => x.RotationPeriod > maxPir && x.SurfaceWater > surfaceWaterAmount && x.Name.ToLower().Contains(containsLetter)).OrderBy(x => x.Name).ThenBy(x => x.SurfaceWater).ThenBy(x=> x.RotationPeriod).ToList<Planet>();

            return result.ToList<Planet>();
        }
        /// <summary>
        /// Opgave 9
        /// </summary>
        /// <param name="surfacewater"></param>
        /// <returns></returns>
        public List<Planet> GetPlanetsBySurwaterSorted(int surfacewater)
        {
            var result = planets.Where(x => x.SurfaceWater > surfacewater).OrderByDescending(x => x.SurfaceWater).ToList<Planet>();
            return result.ToList<Planet>();
        }
        /// <summary>
        /// Opgave 10
        /// </summary>
        /// <returns></returns>
        public List<Planet> GetPlanetBySurfaceAre()
        {
            //For læsevenlighedens skyld bør man altid indkapsle i paranteser
            var result = planets.Where((x => x.Diameter != 0) && (x.Population != 0)).OrderBy(x => 
            {
               
                double tempArea = 4 * Math.PI * (Math.Pow((x.Diameter / 2), 2));
                return tempArea / x.Population;
            }).ToList<Planet>();
            return result;
        }
        /// <summary>
        /// Opgave 11
        /// </summary>
        /// <returns></returns>
        public List<Planet> GetPlanetsByExtop()
        {
            //Hvorfor stort begyndelsesbogstav igen?
            var PCloneList = planets.Where(x => x.RotationPeriod > 0);
            var result = planets.Except(PCloneList);
            return result.ToList<Planet>();
        }
        /// <summary>
        /// Opgave 12
        /// </summary>
        /// <param name="startswith"></param>
        /// <param name="endswith"></param>
        /// <returns></returns>
        public List<Planet> GetPlanetsByUnion(string startswith,string endswith)
        {
            var plantesByName = planets.Where(x => x.Name.ToLower().StartsWith(startswith) || x.Name.ToLower().EndsWith(endswith)).ToList<Planet>();
            var plantesByTerria = planets.Where(x => x.Terrain != null && x.Terrain.Contains("rainforests"));
            var result = plantesByName.Union(plantesByTerria);
            return result.ToList<Planet>();
        }
       /// <summary>
       /// opgave 13
       /// </summary>
       /// <returns></returns>
        public List<Planet> GetPlanetsByClimant()
        {
            var result = planets.Where(x => x.Terrain != null && x.Terrain.Any(p => p.ToLower().Contains("des")));
            return result.ToList<Planet>();
        }
        /// <summary>
        /// opgave 14
        /// </summary>
        /// <returns></returns>
        public List<Planet> GetPlanetsBySwampsSorted()
        {
            var result = planets.Where(x => x.Terrain != null && x.Terrain.Any(p => p.Contains("swa"))).OrderBy(x => x.RotationPeriod).ThenBy(x => x.Name);
            return result.ToList<Planet>();
        }
        /// <summary>
        /// opgave 15
        /// </summary>
        /// <returns></returns>
        public List<Planet> GetPlanetsByWoels()
        {
            Regex reg = new Regex(@"([aeiou])\1");
            var result = planets.Where(x => reg.Matches(x.Name.ToLower()).Count > 0);
            return result.ToList<Planet>();
        }
        /// <summary>
        /// opgave 16
        /// </summary>
        /// <returns></returns>
        public List<Planet> GetPlanetsByDoubleLetters()
        {
            Regex reg = new Regex(@"([klrn])\1");
            var result = planets.Where(x => reg.Matches(x.Name.ToLower()).Count > 0).OrderByDescending(x => x.Name);
            return result.ToList<Planet>();
        }
    }
}
