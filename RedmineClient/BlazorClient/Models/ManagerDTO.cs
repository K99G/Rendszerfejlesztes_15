using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Models
{
    public class ManagerDTO
    {
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; } // Note: It's unusual to send passwords in a DTO
    }
}