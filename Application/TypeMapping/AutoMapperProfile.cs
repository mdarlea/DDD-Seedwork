using System;
using AutoMapper;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.Seedwork.TypeMapping
{
    public class AutoMapperProfile : Profile
    {
        public IMappingExpression<TSource, TDestination> CreateActionResultMap<TSource, TDestination>()
            where TDestination :  ActionResult
        {
            return CreateMap<TSource, TDestination>()
                 .ForMember(dto => dto.Status, c => c.UseValue(ActionResultCode.Success));
        }
    }
}
