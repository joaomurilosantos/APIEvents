using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Service.Entity
{
    public class CityEventEntity
    {
        [Required(AllowEmptyStrings = false,  ErrorMessage = "Title is required")]
        public string title { get; set; }
        public string? description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "DateHourEvent is required")]
        public DateTime dateHourEvent { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Local is required")]
        public string local { get; set; }
        public string? address { get; set; }
        public decimal? price { get; set; }
        public bool status => true;
    }
}
