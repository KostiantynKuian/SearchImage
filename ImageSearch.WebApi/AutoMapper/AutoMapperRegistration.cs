using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ImageSearch.WebApi.DataModel;
using ImageSearch.WebApi.Extensions;
using ImageSearch.WebApi.ViewModels;

namespace ImageSearch.WebApi.AutoMapper
{
    public class AutoMapperRegistration
    {
        public static void Registrate()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ImageRecord, Image>()
                   .ForMember(target => target.Link, options => options.MapFrom(source => source.Url))
                   .ForMember(target => target.Title, options => options.MapFrom(source => source.Url.ConvertUrlToImageName()));
                cfg.CreateMap<FilterRecord, Filter>()
                   .ForMember(target => target.ImageCount, options => options.MapFrom(source => 5))
                   .ForMember(target => target.Id, options => options.MapFrom(source => 5));
            });
        }
    }
}