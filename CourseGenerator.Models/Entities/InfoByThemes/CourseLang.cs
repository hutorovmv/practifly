﻿using CourseGenerator.Models.Entities.Info;

namespace CourseGenerator.Models.Entities.InfoByThemes
{
    public class CourseLang
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string LangCode { get; set; }
        public Language Lang { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
