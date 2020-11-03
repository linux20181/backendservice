﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Models;

namespace Test.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(o => o.Id);
            builder.HasOne<Role>(c => c.Role).WithMany().HasForeignKey("RoleId");
            builder.HasOne<Grade>(c => c.Grade).WithMany().HasForeignKey("GradeId");
            builder.HasOne<Class>(c => c.Class).WithMany().HasForeignKey("ClassId");
            builder.HasOne<Team>(c => c.Team).WithMany().HasForeignKey("TeamId");
        }
    }
}