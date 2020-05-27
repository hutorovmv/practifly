﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CourseGenerator.Models.Entities.Info;

namespace CourseGenerator.Models.Configs.Info
{
    public class MaterialLangConfig : IEntityTypeConfiguration<MaterialLang>
    {
        public void Configure(EntityTypeBuilder<MaterialLang> builder)
        {
            builder.HasKey(p => new { p.MaterialId, p.LangCode});

            builder.Property(p => p.Name).IsUnicode().IsRequired();

            builder.HasOne(p => p.Lang)
                .WithMany(p => p.MaterialLangs)
                .HasForeignKey(p => p.LangCode)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Material)
                .WithMany(p => p.MaterialLangs)
                .HasForeignKey(p => p.MaterialId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}