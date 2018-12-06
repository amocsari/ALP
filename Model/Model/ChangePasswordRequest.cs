namespace Model.Model
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public bool Validate()
        {
            if(string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword))
            {
                return false;
            }
            return true;
        }
    }
}
