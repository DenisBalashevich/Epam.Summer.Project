using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalPhoto : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public byte[] Picture { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }

        public DalUser User { get; set; }
        public DateTime Date { get; set; }

        public ICollection<DalRating> Ratings { get; set; }
        public ICollection<DalTag> Tags { get; set; }

    }
}
