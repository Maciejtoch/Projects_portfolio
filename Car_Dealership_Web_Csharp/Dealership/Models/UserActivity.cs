﻿using Microsoft.EntityFrameworkCore;


namespace Dealership.Models
{
    public class UserActivity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? Action { get; set; }
        public DateTime Timestamp { get; set; }

    }


}
