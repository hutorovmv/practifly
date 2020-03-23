﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CourseGenerator.Models.Entities.Info;
using CourseGenerator.Models.Entities.InfoByThemes;

namespace CourseGenerator.Models.Configs.InfoByThemes
{
    public class ThemeHeadingConfig: IEntityTypeConfiguration<ThemeHeading>
    {
        public void Configure(EntityTypeBuilder<ThemeHeading> builder)
        {
            builder.HasKey(p => new { p.ThemeId, p.HeadingId });
            builder.Property(p => p.Note).IsUnicode();

            builder.HasOne(p => p.Theme)
                .WithMany(p => p.ThemeHeadings)
                .HasForeignKey(p => p.ThemeId);

            builder.HasOne(p => p.Heading)
                .WithMany(p => p.ThemeHeadings)
                .HasForeignKey(p => p.HeadingId);

            builder.HasOne(p => p.Level)
                .WithMany(p => p.ThemeHeadings)
                .HasForeignKey(p => p.LevelId);
        }
    }
}