using Domain;
using ExternalApis;
using Persistence;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ApiEnOpslagTest
    {
        /// <summary>
        /// Simpele test om TvMazeApi integratie te testen. 
        /// Een soortgelijke test (zonder persistentie) zou je in een release kunnen hangen als preconditie voor een succesvolle release.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IntegratieTest()
        {
                var client = new HttpClient();
                var storage = new Storage();
                var api = new TvMazeApi(client, storage);
                await api.StoreShows();

                var query = new GetShowsQuery()
                {
                    Page = 1
                };
                var shows = await storage.GetShows(query);
                Assert.NotNull(shows);
                Assert.True(shows.Shows.Length == 10);
        }
    }
}
