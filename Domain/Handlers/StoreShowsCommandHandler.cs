using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class StoreShowsCommandHandler
    {
        public IExternalApi ExternalApi { get; set; }

        public StoreShowsCommandHandler(IExternalApi externalApi)
        {
            ExternalApi = externalApi;
        }
        public async Task Handle(StoreShowsCommand command)
        {
            await ExternalApi.StoreShows();
        }
    }
}
