using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class GetShowsQueryHandler
    {
        public IExternalApi ExternalApi { get; set; }

        public IStorage Storage { get; set; }

        public GetShowsQueryHandler(IExternalApi externalApi, IStorage storage)
        {
            ExternalApi = externalApi;
            Storage = storage;
        }
        public async Task<QueryResult> Handle(GetShowsQuery query)
        {
            if (!Storage.ContainsShows())
            {
                throw new NoShowsException("Er zijn geen shows gepersisteerd.");
            }

            var result = new QueryResult();
            result = await Storage.GetShows(query);

            // Ordering shows and cast
            var shows = result.Shows.ToList();
            shows = shows.OrderBy(show => show.Id).ToList();
            shows.ForEach(show =>
            {
                if (show.Cast != null)
                { 
                    show.Cast = show.Cast.OrderByDescending(cast => cast.BirthDay).ToArray();
                }
            });
            result.Shows = shows.ToArray();
            return result;
        }
    }
}
