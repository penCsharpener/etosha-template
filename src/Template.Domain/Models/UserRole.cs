namespace Template.Domain.Models
{
    public class UserRole
    {
        public UserRole() { }

        public UserRole(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
