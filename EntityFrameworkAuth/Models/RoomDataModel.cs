using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkAuth.Models
{
    public class RoomDataModel
    {
        [Key]
        public string Id { get; set; }
        public int Number { get; set; }
        public bool IsBooked { get; set; }
        public string BookedFor { get; set; }

        public string HotelDataModelId { get; set; }
    }
}
