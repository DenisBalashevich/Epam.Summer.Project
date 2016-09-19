namespace BLL.Interfaces.Entities
{
    public class UserInformationEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Age { get; set; }

        public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}