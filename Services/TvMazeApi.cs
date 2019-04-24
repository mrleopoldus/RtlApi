using Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ExternalApis
{
    public class TvMazeApi : IExternalApi
    {
        private HttpClient HttpClient { get; set; }
        private IStorage Storage { get; set; }


        public TvMazeApi(HttpClient httpClient, IStorage storage)
        {
            HttpClient = httpClient;
            Storage = storage;
        }
        public async Task StoreShows()
        {
            var shows = await GetShowsAsync();
            shows = InsertCastInShows(shows);
            await Storage.StoreShows(shows);

        }

        private async Task<List<Show>> GetShowsAsync()
        {

            HttpClient.BaseAddress = new Uri("http://api.tvmaze.com/");
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await HttpClient.GetAsync("shows");
            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Show>>(stringResult);
            }
            return new List<Show>();
        }

        private List<Show> InsertCastInShows(List<Show> shows)
        {
            shows.ForEach(async show =>
            {
                show.Cast = await RetrieveAndMapCast(show);
            });

            return shows;
        }

        private async Task<CastMember[]> RetrieveAndMapCast(Show show)
        {
            var response = await HttpClient.GetAsync($"shows/{show.Id}/cast");
            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                if (stringResult == null)
                {
                    return null;
                }
                var jObject = JArray.Parse(stringResult);
                var persons = jObject.Children().Select(item => item["person"]).ToList();
                var result = persons.Select(person => person.ToObject<CastMember>()).ToArray();

                return result;
            }

            throw new Exception("Probleem opgetreden met Client of Parsing");

        }
    }
}
