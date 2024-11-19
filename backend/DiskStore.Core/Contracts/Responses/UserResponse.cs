﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskStore.Core.Contracts.Responses
{
    public class UserResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}