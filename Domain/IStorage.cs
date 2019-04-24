using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IStorage
    {
        bool ContainsShows();
        Task<QueryResult> GetShows(GetShowsQuery query);

        Task StoreShows(List<Show> shows);
    }
}
