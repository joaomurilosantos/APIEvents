using Dapper;
using Eventos.Service.Entity;
using Eventos.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;


namespace Eventos.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private string _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");

        public async Task<List<EventReservationEntity>> ConsultPersonNameTitle(string personName, string title)
        {
            string query = "SELECT EventReservation.idEvent, EventReservation.personName, EventReservation.quantity " +
                "FROM EventReservation INNER JOIN CityEvent ON EventReservation.idEvent = CityEvent.idEvent " +
                "WHERE EventReservation.personName = @personName AND CityEvent.title LIKE @title";

            DynamicParameters parameters = new();
            parameters.Add("personName", personName);
            parameters.Add("title", "%" + title + "%");

            using MySqlConnection conn = new(_stringConnection);

            return (await conn.QueryAsync<EventReservationEntity>(query, parameters)).ToList();
        }
        public async Task<bool> AddEventReservation(EventReservationEntity eventreservationentity)
        {            
            string query = "INSERT INTO EventReservation (idEvent, personName, quantity)" +
                "VALUES (@idEvent, @personName, @quantity)";

            DynamicParameters parameters = new(eventreservationentity);

            CityEventRepository cityEvent = new();

            int afectedLines = 0;

            if (cityEvent.EventExist(eventreservationentity.idEvent) && cityEvent.EventIsActive(eventreservationentity.idEvent))
            {
                using MySqlConnection conn = new(_stringConnection);

                afectedLines = await conn.ExecuteAsync(query, parameters);                              
            }

            return afectedLines > 0;
        }
        public async Task<bool> UpdateQuantity(long idReservation, int quantity)
        {
            string query = "UPDATE EventReservation SET quantity = @quantity WHERE idReservation = @idReservation";

            DynamicParameters parameters = new();
            parameters.Add("quantity", quantity);
            parameters.Add("idReservation", idReservation);

            using MySqlConnection conn = new(_stringConnection);

            int afectedLines = await conn.ExecuteAsync(query, parameters);

            return afectedLines > 0;
        }
        public async Task<bool> DeleteEventReservation(long idReservation)
        {
            string query = "DELETE FROM EventReservation WHERE idReservation = @idReservation";

            DynamicParameters parameters = new();
            parameters.Add("idReservation", idReservation);

            using MySqlConnection conn = new(_stringConnection);

            int afectedLines = await conn.ExecuteAsync(query, parameters);

            return afectedLines > 0;
        }
        public bool ReservationExist(long idEvent)
        {
            string query = $"SELECT * FROM EventReservation WHERE idEvent = {idEvent}";

            using MySqlConnection conn = new(_stringConnection);

            EventReservationEntity eventReservation = new();

            try
            {
                eventReservation = conn.QueryFirst<EventReservationEntity>(query);
            }
            catch (InvalidOperationException ex)
            {
                conn.Close();
                return false;
            }           

            return true;
        }
    }

}
