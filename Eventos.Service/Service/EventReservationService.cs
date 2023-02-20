using Eventos.Service.Entity;
using Eventos.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Service.Service
{
    public class EventReservationService : IEventReservationService
    {
        private IEventReservationRepository _IEventReservationRepository { get; set; }
        public EventReservationService(IEventReservationRepository eventreservationrepository)
        {
            _IEventReservationRepository = eventreservationrepository;
        }
        public async Task<bool> AddEventReservation(EventReservationEntity eventreservationentity)
        {
            return await _IEventReservationRepository.AddEventReservation(eventreservationentity);
            
        }
        public async Task<bool> UpdateQuantity(long idReservation, int quantity)
        {
            return await _IEventReservationRepository.UpdateQuantity(idReservation, quantity);
        }
        public async Task<bool> DeleteEventReservation(long idReservation)
        {
            return await _IEventReservationRepository.DeleteEventReservation(idReservation);
        }
        public async Task<List<EventReservationEntity>> ConsultPersonNameTitle(string personName, string title)
        {
            return await _IEventReservationRepository.ConsultPersonNameTitle(personName, title);
        }
    }
}
