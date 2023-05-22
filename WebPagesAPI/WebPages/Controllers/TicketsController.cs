using ConcertAPI.DAL;
using Microsoft.AspNetCore.Mvc;

namespace WebPages.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public TicketsController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? ticketId, Ticket ticket)
        {
            var url = String.Format("http://localhost:5211/api/Tickets/Edit/{0}", ticketId);
            await _httpClient.CreateClient().PutAsJsonAsync(url, ticket);

            return RedirectToAction("Index");
        }
    }
}
