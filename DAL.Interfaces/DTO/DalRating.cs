using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalRating : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int PhotoId { get; set; }
    }
}
