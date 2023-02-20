using Dapper;
using Eventos.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Eventos.Service.Interface;
using System.Data;

namespace Eventos.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private string _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");

        public async Task<bool> AddCityEvent(CityEventEntity cityevententity)
        {
            string query = "INSERT INTO CityEvent (title, description, dateHourEvent, local, address, price, status)" +
                "VALUES (@title, @description, @dateHourEvent, @local, @address, @price, @status)";

            DynamicParameters parameters = new(cityevententity); //protege de sql injection 

            using MySqlConnection conn = new(_stringConnection);
            
            int afectedlines = await conn.ExecuteAsync(query, parameters);

            return afectedlines > 0;
        }
        public async Task<bool> UpdateCityEvent(CityEventEntity cityevententity, int id)
        {
            string query = "UPDATE CityEvent SET title = @title, description = @description, dateHourEvent = @dateHourEvent," +
                "local = @local, address = @address, price = @price, status = @status where idEvent = @idEvent";

            DynamicParameters parameters = new();
            parameters.Add("@title", cityevententity.title);
            parameters.Add("@description", cityevententity.description);
            parameters.Add("@dateHourEvent", cityevententity.dateHourEvent);
            parameters.Add("@local", cityevententity.local);
            parameters.Add("@address", cityevententity.address);
            parameters.Add("@price", cityevententity.price);
            parameters.Add("@status", cityevententity.status);
            parameters.Add("@idEvent", id);

            using MySqlConnection conn = new(_stringConnection);

            int afectedlines = await conn.ExecuteAsync(query, parameters);            

            return afectedlines > 0;
        }
        public async Task<List<CityEventEntity>> ConsultTitle(string title)
        {
            string query = "SELECT * FROM CityEvent WHERE title LIKE @title";

            DynamicParameters parameters = new();
            parameters.Add("title", "%" + title + "%");

            using MySqlConnection conn = new(_stringConnection);           
            
            return (await conn.QueryAsync<CityEventEntity>(query, parameters)).ToList();                              
        }
        public async Task<List<CityEventEntity>> ConsultLocalData(string local, DateTime data)
        {
            string query = "SELECT * FROM CityEvent WHERE CAST(dateHourEvent AS date) = @dateEvent AND local LIKE @local";

            DynamicParameters parameters = new();
            parameters.Add("local", "%" + local + "%");
            parameters.Add("dateEvent", data.Date);

            using MySqlConnection conn = new(_stringConnection);

            return (await conn.QueryAsync<CityEventEntity>(query, parameters)).ToList(); 
        }
        public async Task<List<CityEventEntity>> ConsultPriceRangeData(decimal price1, decimal price2, DateTime data)
        {
            string query = "SELECT * FROM CityEvent WHERE price BETWEEN @price1 AND @price2 AND CAST(dateHourEvent AS date) = @dateEvent";

            if (price2 < price1)
            {
                decimal aux;
                aux = price1;
                price1 = price2;
                price2 = aux;
            }

            DynamicParameters parameters = new();
            parameters.Add("price1", price1);
            parameters.Add("price2", price2);
            parameters.Add("dateEvent", data.Date);

            using MySqlConnection conn = new(_stringConnection);

            return (await conn.QueryAsync<CityEventEntity>(query, parameters)).ToList();
 
        }
        public async Task<bool> DeleteCityEvent(long idEvent)
        {
            string query = "DELETE FROM CityEvent WHERE idEvent = @idEvent";

            DynamicParameters parameters = new();
            parameters.Add("idEvent", idEvent);

            EventReservationRepository eventReservation = new();

            int afectedlines;

            if (!eventReservation.ReservationExist(idEvent))
            {
                using MySqlConnection conn = new(_stringConnection);
                afectedlines = await conn.ExecuteAsync(query, parameters);                
            }
            else
            {
                afectedlines = await InactivateEvent(idEvent);                
            }

            return afectedlines > 0;
        }
        private async Task<int> InactivateEvent(long idEvent)
        {
            string query = $"UPDATE CityEvent SET status = {false} WHERE idEvent = {idEvent}";

            using MySqlConnection conn = new(_stringConnection);

            int afectedlines = await conn.ExecuteAsync(query);

            return afectedlines;
        }         
        public bool EventExist(long idEvent)
        {         
            string query = $"SELECT * FROM CityEvent WHERE idEvent = {idEvent}";

            using MySqlConnection conn = new(_stringConnection);

            CityEventEntity cityEvent = new();

            try
            {
                cityEvent = conn.QueryFirst<CityEventEntity>(query);
            }
            catch (InvalidOperationException ex)
            {
                return false;
            }

            if (!cityEvent.status)
            {
                return false;
            }                     

            return true;
        }
        public bool EventIsActive(long idEvent) 
        {
            string query = $"SELECT * FROM CityEvent WHERE idEvent = {idEvent}";

            using MySqlConnection conn = new(_stringConnection);

            CityEventEntity cityEvent = conn.QueryFirst<CityEventEntity>(query);

            return cityEvent.status;
        }
    }
}
