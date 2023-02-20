using Eventos.Filters;
using Eventos.Service.Entity;
using Eventos.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EventReservationController : ControllerBase
    {
        private IEventReservationService _IEventReservationService { get; set; }

        public EventReservationController(IEventReservationService eventreservationservice)
        {
            _IEventReservationService = eventreservationservice;
        }

        [Authorize]
        [HttpGet("personNameTitleConsult")]
        [TypeFilter(typeof(GeneralExceptionFilter))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventReservationEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConsultPersonNameTitle(string personName, string title)
        {
            List<EventReservationEntity> Reservations = await _IEventReservationService.ConsultPersonNameTitle(personName, title);
            if(Reservations.LongCount() == 0)
            {
                return BadRequest();
            }

            return Ok(Reservations);
        }
        [Authorize]
        [HttpPost("Add")]
        [TypeFilter(typeof(GeneralExceptionFilter))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EventReservationEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddEventReservation(EventReservationEntity eventreservationentity)
        {
            if(!(await _IEventReservationService.AddEventReservation(eventreservationentity)))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(ConsultPersonNameTitle), eventreservationentity);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("quantityUpdate")]
        [TypeFilter(typeof(GeneralExceptionFilter))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateQuantity(long idReservation, int quantity)
        {
            if(!(await _IEventReservationService.UpdateQuantity(idReservation, quantity)))
            {
                return BadRequest();
            }

            return Ok(quantity);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("Delete")]
        [TypeFilter(typeof(GeneralExceptionFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEventReservation(long idReservation)
        {
            if(!(await _IEventReservationService.DeleteEventReservation(idReservation)))
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
