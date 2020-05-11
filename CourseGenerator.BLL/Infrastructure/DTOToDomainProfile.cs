﻿using AutoMapper;
using CourseGenerator.BLL.DTO.Security;
using CourseGenerator.BLL.DTO.User;
using CourseGenerator.Models.Entities.Identity;
using CourseGenerator.Models.Entities.Security;

namespace CourseGenerator.BLL.Infrastructure
{
    public class DTOToDomainProfile : Profile
    {
        public DTOToDomainProfile()
        {
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<CodeAuthDTO, CodeAuth>();
        }
    }
}
