﻿using CourseGenerator.Models.Entities.Identity;
using CourseGenerator.Models.Entities.Info;

namespace CourseGenerator.Models.Entities.CourseAccess
{
    public class UserMaterial
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public bool IsCompleted { get; set; }
        public int Grade { get; set; }
        public int Note { get; set; }
    }
}
