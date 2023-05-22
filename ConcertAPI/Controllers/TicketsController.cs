using ConcertAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;
using DbUpdateException = System.Data.Entity.Infrastructure.DbUpdateException;

namespace ConcertAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public TicketsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetTicketById/{ticketId}")]
        public async Task<ActionResult<Ticket>> GetTicketById(Guid? ticketId)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null) return NotFound("Boleta no válida.");
            return Ok(ticket);
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]
        public async Task<ActionResult> EditTicket(Guid? id, Ticket ticket)
        {
            try
            {
                if (id != ticket.Id) return NotFound("Boleta no válida.");
                ticket.UseDate = DateTime.Now;
                _context.Tickets.Update(ticket);

                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("duplicate")) return Conflict(String.Format("{0} ya existe el tiquete.", ticket.Id));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(ticket);
        }


    }
}
