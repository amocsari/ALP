using System;

namespace API.Service
{
    public class SessionTokenData
    {
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public DateTime SessionStart { get; set; }
        public int RoleId { get; set; }
    }
}
