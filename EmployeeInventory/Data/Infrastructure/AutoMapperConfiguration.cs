using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ES.InventoryRegister.Entities;
using ES.InventoryRegister.ViewModels;
using System.Data.Entity.Core.Objects;

namespace ES.InventoryRegister.Data.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// Configures AutoMappper
        /// </summary>
        public static void Configure()
        {
            Mapper.CreateMap<Employee, EmployeeViewModel>();
            Mapper.CreateMap<Department, DepartmentViewModel>();
            Mapper.CreateMap<KeyListViewModel, ProductKey>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreationDate, opt => opt.Ignore())
                .ForMember(x => x.UpdateDate, opt => opt.Ignore());

            int num = 0;
            Mapper.CreateMap<Device, InventoryItemViewModel>()
                .ForMember(x => x.Number, o => o.MapFrom(args => args.Id))
                .ForMember(x => x.PurchaseDate, opt => opt.MapFrom(args => ((DateTime)args.PurchaseDate).ToString("MMMM dd yyyy")))
                .ForMember(x => x.ExpiryDate, opt => opt.MapFrom(args => ((DateTime)args.ExpiryDate).ToString("MMMM dd yyyy")))
                .ForMember(x => x.OwnerName, opt => opt.MapFrom(args => args.Owner.Name))
                .AfterMap((src, dest) =>
                    {
                        num++;
                        dest.Number = num;

                        // Get the entity type
                        dest.Type = ObjectContext.GetObjectType(src.GetType());
                        dest.TypeName = dest.Type.Name;

                        // Set owner name to "(none)" if owner is null
                        if (src.Owner == null)
                        {
                            dest.OwnerName = "(none)";
                        }
                            
                    });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
