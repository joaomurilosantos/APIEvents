using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Service.Entity
{
    public class EventReservationEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "IdEvent is Required")]
        public long idEvent { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "PersonName is Required")]
        public string personName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Quantity is Required")]
        public long quantity { get; set; }        
    }
}
