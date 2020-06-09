﻿using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CourseGenerator.Api.Models.Selection;
using CourseGenerator.BLL.DTO.Selection;
using CourseGenerator.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CourseGenerator.Api.Controllers
{
    /// <summary>
    /// Контролер для роботи з даними про курси
    /// </summary>
    [Authorize]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "Course")]
    [Route("api/[controller]")]
    [SwaggerTag("Контролер для роботи з даними про курси")]
    [Produces(MediaTypeNames.Application.Json, 
        new string[] { MediaTypeNames.Application.Xml })]
    public class CoursesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mapper">Об'єкт мапера</param>
        /// <param name="courseService">Сервіс для роботи з курсами</param>
        public CoursesController(IMapper mapper,
            ICourseService courseService)
        {
            _mapper = mapper;
            _courseService = courseService;
        }

        /// <summary>
        /// Відображає курси, які доступні користувачу
        /// </summary>
        /// <param name="langCode">Код мови, якій надавати перевагу</param>
        /// <returns>Список курсів</returns>
        /// <response code="200">Операція виконана успішно</response>
        /// <response code="401">Неавторизовано</response>
        /// <response code="403">Заборонено</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<CourseSelectModel>> GetUserCoursesLocalAsync(string langCode)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<CourseSelectDTO> courseSelectDtos = await _courseService
                .GetUserCoursesLocalizedAsync(userId, langCode);

            IEnumerable<CourseSelectModel> courseSelectModels = _mapper
                .Map<IEnumerable<CourseSelectModel>>(courseSelectDtos);
            return courseSelectModels;
        }

        /// <summary>
        /// Отримує рівні складності тем, доступні в курсі
        /// </summary>
        /// <param name="courseId">Ідентифікатор курсу</param>
        /// <param name="langCode">Код мови, якій надавати перевагу</param>
        /// <returns>Доступні рівні складності тем</returns>
        /// <response code="200">Операція виконана успішно</response>
        /// <response code="401">Неавторизовано</response>
        /// <response code="403">Заборонено</response>
        [Route("~/api/[controller]/levels")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<LevelSelectModel>> GetCourseLevelsLocalAsync(int courseId, 
            string langCode)
        {
            IEnumerable<LevelSelectDTO> levelSelectDtos = await _courseService
                .GetCourseLevelsLocalAsync(courseId, langCode);

            IEnumerable<LevelSelectModel> levelSelectModels = _mapper
                .Map<IEnumerable<LevelSelectModel>>(levelSelectDtos);
            return levelSelectModels;
        }

        /// <summary>
        /// Підтеми для вказаної теми
        /// </summary>
        /// <param name="themeId">Ідентифікатор теми</param>
        /// <param name="langCode">Код мови, якій надавати перевагу</param>
        /// <returns>Список підтем</returns>
        /// <response code="200">Операція виконана успішно</response>
        /// <response code="302">Перенаправлення до матеріалу теми</response>
        /// <response code="401">Неавторизовано</response>
        /// <response code="403">Заборонено</response>
        [Route("~/api/[controller]/themes/children")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        public async Task<IActionResult> GetUserThemeChildrenAsync(int themeId, 
            string langCode)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            IEnumerable<UserThemeSelectDTO> childThemeDtos = await _courseService
                .GetChildrenLocalAsync(userId, themeId, langCode);
            if (childThemeDtos == null)
                return RedirectToAction(""); // TODO: specify appropriate action name

            IEnumerable<UserThemeSelectModel> themeSelectModels = _mapper
                .Map<IEnumerable<UserThemeSelectModel>>(childThemeDtos);
            return Ok(childThemeDtos);
        }

        /// <summary>
        /// Теми вищого рівня
        /// </summary>
        /// <param name="courseId">Ідентифікатор курсу</param>
        /// <param name="levelId">Рівень складності</param>
        /// <param name="langCode">Код мови, якій надавати перевагу</param>
        /// <returns>Список тем вищого рівня для курсу</returns>
        /// <response code="200">Операція виконана успішно</response>
        /// <response code="302">Перенаправлення на останню тему, переглянуту
        /// користувачем</response>
        /// <response code="401">Неавторизовано</response>
        /// <response code="403">Заборонено</response>
        [Route("~/api/[controller]/themes/parents")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        public async Task<IActionResult> GetUserThemesLocalAsync(int courseId, 
            int levelId, string langCode)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            int? lastThemeId = await _courseService.GetLastThemeIdOrNullAsync(userId, courseId);
            if (lastThemeId != null)
                return RedirectToAction(""); // TODO: specify appropriate action name

            IEnumerable<UserThemeSelectDTO> themeSelectDtos = await _courseService
                .GetUserCourseThemesLocalizedAsync(userId, courseId, levelId, langCode);

            IEnumerable<UserThemeSelectModel> themeSelectModels = _mapper
                .Map<IEnumerable<UserThemeSelectModel>>(themeSelectDtos);
            return Ok(themeSelectModels);
        }
    }
}