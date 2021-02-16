using System;
using System.Collections.Generic;

namespace BestInja.Model.UserManagment
{
    public class ApplicationUserDto
    {
        public Guid Id { get; set; }
        public List<RoleDto> Roles { get; set; }
        public string UserName { get; set; }
    }
}