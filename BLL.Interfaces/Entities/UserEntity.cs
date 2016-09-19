using System.Collections.Generic;

namespace BLL.Interfaces.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<RoleEntity> Roles { get; set; }
        public ICollection<PhotoEntity> Photos { get; set; }
    }
}
