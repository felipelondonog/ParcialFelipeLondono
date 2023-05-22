using ConcertAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebPages.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public TicketsController(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetTicketById(Guid? id)
        {
            var url = String.Format("http://localhost:5211/api/Tickets/Get/{0}", id);
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            List<Ticket> listTicket = JsonConvert.DeserializeObject<List<Ticket>>(json);
            Ticket ticket = listTicket[0];

            return View(ticket);
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
            return View(await GetTickets(id));
        }

        private async Task<Ticket> GetTickets(Guid? id)
        {
            var url = String.Format("http://localhost:5211/api/Tickets/Get/{0}", id);
            return JsonConvert.DeserializeObject<Ticket>(await _httpClient.CreateClient().GetStringAsync(url));
        }


    }
}
