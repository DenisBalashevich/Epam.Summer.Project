using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public class PhotoEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public byte[] Picture { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public UserEntity User { get; set; }
        public DateTime Date { get; set; }

        public ICollection<RatingEntity> Ratings { get; set; }
        public ICollection<TagEntity> Tags { get; set; }
    }
}
