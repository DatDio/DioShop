﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Models.Identity
{
    public class RegistrationRequest
    {
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
