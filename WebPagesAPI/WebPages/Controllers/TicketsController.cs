using ConcertAPI.DAL;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace WebPages.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public TicketsController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var url = "http://localhost:5211/api/Tickets/Get";
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);

            return View(tickets);
        }

        //fde632c7-0b27-4545-b083-08db5a401385
        //ed7bcefe-00f9-465e-b084-08db5a401385
        //http://localhost:5211/api/Tickets/Get
        [HttpGet]
        public async Task<IActionResult> GetTicketById(Guid? id)
        {
            try
            {
                var url = String.Format("http://localhost:5211/api/Tickets/Get/{0}", id);
                var json = await _httpClient.CreateClient().GetStringAsync(url);
                List<Ticket> listTicket = JsonConvert.DeserializeObject<List<Ticket>>(json);
                Ticket ticket = listTicket[0];
                //Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);
                return View(ticket);

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Ticket ticket)
        {
            var url = String.Format("http://localhost:5211/api/Tickets/Edit/{0}", id);
            await _httpClient.CreateClient().PutAsJsonAsync(url, ticket);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var url = String.Format("http://localhost:5211/api/Tickets/Get/{0}", id);
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);

            return View(ticket);
        }


    }
}
