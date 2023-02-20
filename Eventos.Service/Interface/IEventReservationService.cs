using Eventos.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Service.Interface
{
    public interface IEventReservationService 
    {
        public Task<bool> AddEventReservation(EventReservationEntity eventreservationentity);
        public Task<bool> UpdateQuantity(long idReservation, int quantity);
        public Task<bool> DeleteEventReservation(long idReservation);
        public Task<List<EventReservationEntity>> ConsultPersonNameTitle(string personName, string title);
    }
}
