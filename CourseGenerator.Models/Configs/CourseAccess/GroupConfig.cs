﻿using CourseGenerator.Models.Entities.CourseAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseGenerator.Models.Configs.CourseAccess
{
    public class GroupConfig: IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsUnicode();
            builder.Property(p => p.Note).IsUnicode();
        }
    }
}
