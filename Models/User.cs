﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User : Enity
    {
        public string Name { set; get; }
        public string Graduate { set; get; }
        public string Avatar { set; get; }
        public bool? Gender { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public int? Status { set; get; }
        public string Email { set; get; }
        public long? PhoneNumber { set; get; }
        public long? Code { set; get; }
        public byte[] Salt { set; get; }
        public Guid? RoleId { set; get; }
        public Role Role { set; get; }
        public Guid? GradeId { set; get; }
        public Grade Grade { set; get; }
        public virtual ICollection<ClassUser> ClassUser { get; set; }
        public Guid? TeamId { set; get; }
        public Team Team { set; get; }
    }
}
