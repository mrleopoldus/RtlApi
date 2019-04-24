using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Handlers;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ReactFrontend.Controllers
{
    public class HomeController : Controller
    {
        public GetShowsQueryHandler QueryHandler { get; set; }

        public StoreShowsCommandHandler CommandHandler { get; set; }

        public HomeController(GetShowsQueryHandler queryHandler, StoreShowsCommandHandler commandHandler)
        {
            QueryHandler = queryHandler;
            CommandHandler = commandHandler;
        }

        public async Task<IActionResult> Shows(GetShowsQuery query)
        {
            // Om het even simpel te houden in de frontend API hebben we een query met side effects gecreeerd in het kader van de 3 urige opdracht.
            try
            {
                var result = await QueryHandler.Handle(query);
                return Json(result);
            }
            catch (NoShowsException)
            {
                await CommandHandler.Handle(new StoreShowsCommand());
                var result = await QueryHandler.Handle(query);
                return Json(result);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}

