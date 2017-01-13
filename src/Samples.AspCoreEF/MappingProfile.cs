using AutoMapper;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonViewModel>();
            CreateMap<PersonViewModel, Person>();
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategoryViewModel, ProductCategory>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<Tag, TagViewModel>();
            CreateMap<TagViewModel, Tag>();
            CreateMap<ProductTag, ProductTagViewModel>();
            CreateMap<ProductTagViewModel, ProductTag>();
        }
    }
}
