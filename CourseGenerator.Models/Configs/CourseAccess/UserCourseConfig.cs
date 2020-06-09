﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CourseGenerator.Models.Entities.CourseAccess;

namespace CourseGenerator.Models.Configs.CourseAccess
{
    public class UserCourseConfig: IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasKey(p => new { p.UserId, p.CourseId });
            builder.Property(p => p.Note).IsUnicode();
            builder.Property(p => p.LastThemeId).IsRequired(false);

            builder.HasOne(p => p.User)
                .WithMany(p => p.UserCourses)
                .HasForeignKey(p =>p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Course)
                .WithMany(p => p.UserCourses)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Level)
                .WithMany(p => p.UserCourses)
                .HasForeignKey(p => p.LevelNumber)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Theme)
                .WithMany(p => p.UserCourses)
                .HasForeignKey(p => p.LastThemeId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
