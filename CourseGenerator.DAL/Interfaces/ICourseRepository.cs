﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourseGenerator.DAL.Pagination;
using CourseGenerator.Models.Entities.InfoByThemes;

namespace CourseGenerator.DAL.Interfaces
{
    public interface ICourseRepository : IGenericEFRepository<Course>
    {
        Task<PagedList<CourseLang>> GetForUserWithLangPagedAsync(
            string userId, string langCode, int pageSize, int pageIndex);
    }
}
