using Asp_Core_with_Angular.Data;
using Asp_Core_with_Angular.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Asp_Core_with_Angular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _ticketRepository.GetAll();
            if (model == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(model);
            }
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var model = await _ticketRepository.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(model);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TicketDetail ticketDetail)
        {
            var model = await _ticketRepository.Create(ticketDetail);
            if (model == null)
                return NotFound();
            else
                return CreatedAtAction(nameof(Create), model);
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] TicketDetail ticketDetail)
        {
            ticketDetail.Id = id;
            var response = await _ticketRepository.Update(id, ticketDetail);
            if (response)
                return Ok(ticketDetail);
            return NotFound();
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _ticketRepository.Delete(id);
            if (response)
                return Ok();
            return NotFound();
        }
    }
}
