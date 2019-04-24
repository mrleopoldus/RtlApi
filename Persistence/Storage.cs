using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class Storage : IStorage
    {
        public bool ContainsShows()
        {
            return File.Exists(@"c:\shows.json");
        }

        public async Task<QueryResult> GetShows(GetShowsQuery query)
        {
            var result = new QueryResult();

            // om het simpel te houden in het kader van de 3 urige opdracht pagesize hardcoded. normaliter komt dit uit de application layer
            var pageSize = 10;

            string json;
            using (var sourceReader = File.OpenText(@"c:\shows.json"))
            {
                json = await sourceReader.ReadToEndAsync();
            }

            var shows = JsonConvert.DeserializeObject<List<Show>>(json);

            result.Shows = shows.Skip(query.Page * pageSize).Take(pageSize).ToArray();
            result.TotalPages = shows.Count / pageSize;
            result.Page = query.Page;

            return result;
        }

        public async Task StoreShows(List<Show> shows)
        {
            // onze persistentie laag doet voor ons automatisch de opslag op id, oplopend
            shows = shows.OrderBy(show => show.Id).ToList();

            using (var writer = File.CreateText(@"c:\shows.json"))
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(shows));
            }
        }
    }
}
