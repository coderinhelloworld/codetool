using AutoMapper;
using CodingTool.Models.Entities.Template.dto;
using CoodingTool.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTool.Services.AutoMapper
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Template, TemplateDto>();
        }
    }
}
