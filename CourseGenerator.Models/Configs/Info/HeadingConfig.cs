﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CourseGenerator.Models.Entities.Info;

namespace CourseGenerator.Models.Configs.Info
{
    public class HeadingConfig : IEntityTypeConfiguration<Heading>
    {
        public void Configure(EntityTypeBuilder<Heading> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.UDC).IsRequired();
            builder.Property(p => p.Note).IsUnicode();
        }
    }
}
