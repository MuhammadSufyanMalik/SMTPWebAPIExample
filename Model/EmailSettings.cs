﻿namespace SMTPWebAPIExample.Models
{
    public class EmailSettings
    {
        public string Host { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string DisplayName { get; set; } = null!;
    }

    public class EmailOptions : EmailSettings
    {

    }
}
