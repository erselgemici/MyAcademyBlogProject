using AutoMapper;
using Blogy.Business.DTOs.TagDtos;
using Blogy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogy.Business.Mappings
{
    public class TagMappings : Profile
    {
        public TagMappings()
        {
            CreateMap<Tag, ResultTagDto>().ReverseMap();
            CreateMap<Tag, CreateTagDto>().ReverseMap();
            CreateMap<Tag, UpdateTagDto>().ReverseMap();
            CreateMap<ResultTagDto, UpdateTagDto>().ReverseMap();
        }
    }
}
