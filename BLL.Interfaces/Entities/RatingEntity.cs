using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public class RatingEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int PhotoId { get; set; }
    }
}
