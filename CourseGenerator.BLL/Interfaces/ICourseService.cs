﻿using CourseGenerator.BLL.DTO.Selection;
using CourseGenerator.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseGenerator.BLL.Interfaces
{
    public interface ICourseService : IDisposable
    {
        Task<OperationInfo> AddUserToCourseAsync(string userId, int courseId, int LevelNumber);

        Task<IEnumerable<CourseSelectDTO>> GetUserCoursesLocalizedAsync(string userId,
            string langCode);

        Task<IEnumerable<UserThemeSelectDTO>> GetUserCourseThemesLocalizedAsync(string userId,
            int courseId, int LevelNumber, string langCode);

        Task<IEnumerable<UserThemeSelectDTO>> GetChildrenLocalAsync(string userId, int themeId, 
            string langCode);

        Task<IEnumerable<LevelSelectDTO>> GetCourseLevelsLocalAsync(int courseId, string langCode);

        Task<int?> GetLastThemeIdOrNullAsync(string userId, int courseId);
    }
}
