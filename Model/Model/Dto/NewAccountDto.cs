using Model.Enum;

namespace Model.Model.Dto
{
    public class NewAccountDto
    {
        public int EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleType RoleType { get; set; }

        public bool Validate()
        {
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return false;
            }

            return true;
        }
    }
}
