using AutoMapper;
using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.Seedwork.Extensions
{
    public static class MapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> CreateActionResultMap<TSource, TDestination>()
            where TDestination : ActionResult
        {
            return Mapper.CreateMap<TSource, TDestination>()
                 .ForMember(dto => dto.Status, c => c.UseValue(ActionResultCode.Success));
        }
    }
}
