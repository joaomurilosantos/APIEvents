using Eventos.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Service.Interface
{
    public interface ICityEventRepository
    {
        public Task<bool> AddCityEvent(CityEventEntity cityevententity);
        public Task<bool> UpdateCityEvent(CityEventEntity cityevententity, int id);
        public Task<bool> DeleteCityEvent(long idEvent);
        public Task<List<CityEventEntity>> ConsultTitle(string title);
        public Task<List<CityEventEntity>> ConsultLocalData(string local, DateTime data);
        public Task<List<CityEventEntity>> ConsultPriceRangeData(decimal price1, decimal price2, DateTime data);
        
    }
}
