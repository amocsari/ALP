namespace Model.Model
{
    /// <summary>
    /// Login data of authentication
    /// </summary>
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Check if the data is valid
        /// </summary>
        /// <returns></returns>
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
