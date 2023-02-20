
using Eventos.Service.Entity;
using Eventos.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Service.Service
{
    public class CityEventService : ICityEventService
    {
        private ICityEventRepository _ICityEventRepository { get; set; }
        public CityEventService(ICityEventRepository cityeventrepository)
        {
            _ICityEventRepository = cityeventrepository;
        }
        public async Task<bool> AddCityEvent(CityEventEntity cityevententity)
        {
            return await _ICityEventRepository.AddCityEvent(cityevententity);           
        }
        public async Task<bool> UpdateCityEvent(CityEventEntity cityevententity, int id)
        {
            return await _ICityEventRepository.UpdateCityEvent(cityevententity, id);
        }
        public async Task<List<CityEventEntity>> ConsultTitle(string title)
        {
            return await _ICityEventRepository.ConsultTitle(title);
        }
        public async Task<List<CityEventEntity>> ConsultLocalData(string local, DateTime data)
        {
            return await _ICityEventRepository.ConsultLocalData(local, data);
        }
        public async Task<List<CityEventEntity>> ConsultPriceRangeData(decimal price1, decimal price2, DateTime data)
        {
            return await _ICityEventRepository.ConsultPriceRangeData(price1, price2, data);
        }
        public async Task<bool> DeleteCityEvent(long idEvent)
        {
            return await _ICityEventRepository.DeleteCityEvent(idEvent);
        }
    }
}
