using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkAuth.Data
{
    public class HotelDataModel
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public List<RoomDataModel> Rooms { get; set; }
    }
}
