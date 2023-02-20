using Eventos.Service.Entity;
using Eventos.Service.Service;
using Eventos.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Eventos.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Eventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    
    public class CityEventController : ControllerBase
    {
        private ICityEventService _ICityEventService { get; set; }
        
        public CityEventController(ICityEventService cityeventservice) 
        {
            _ICityEventService = cityeventservice;            
        }
        
        [HttpGet("titleConsult")]
        [TypeFilter(typeof(GeneralExceptionFilter))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityEventEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConsultTitle(string title)
        {
            List<CityEventEntity> cityEvents = await _ICityEventService.ConsultTitle(title);

            if(cityEvents.LongCount() == 0)
            {
                return BadRequest();
            }         
                           
            return Ok(cityEvents);
        }

        [HttpGet("localDataConsult")]
        [TypeFilter(typeof(GeneralExceptionFilter))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityEventEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConsultLocalData(string local, DateTime data)
        {
            List<CityEventEntity> cityEvents = await _ICityEventService.ConsultLocalData(local, data);

            if (cityEvents.LongCount() == 0)
            {
                return BadRequest();
            }

            return Ok(cityEvents);
        }

        [HttpGet("priceRangeDataConsult")]
        [TypeFilter(typeof(GeneralExceptionFilter))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityEventEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConsultPriceRangeData(decimal price1, decimal price2, DateTime data)
        {
            List<CityEventEntity> cityEvents = await _ICityEventService.ConsultPriceRangeData(price1, price2, data);

            if (cityEvents.LongCount() == 0)
            {
                return BadRequest();
            }

            return Ok(cityEvents);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("Add")]
        [TypeFilter(typeof(GeneralExceptionFilter))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CityEventEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCityEvent(CityEventEntity cityevententity)
        {
            if(!(await _ICityEventService.AddCityEvent(cityevententity)))
            {
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(ConsultTitle), cityevententity);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("Update")]
        [TypeFilter(typeof(GeneralExceptionFilter))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityEventEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCityEvent(CityEventEntity cityevententity, int id)
        {
            if(!(await _ICityEventService.UpdateCityEvent(cityevententity, id)))
            {
                return BadRequest();
            }

            return Ok(cityevententity);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("Delete")]
        [TypeFilter(typeof(GeneralExceptionFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCityEvent(long idEvent)
        {
            if(!(await _ICityEventService.DeleteCityEvent(idEvent)))
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}