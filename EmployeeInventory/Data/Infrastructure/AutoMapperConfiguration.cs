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
    /// <summary>
    /// Used for defining AutoMapper configuration
    /// </summary>
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// Configures AutoMappper mappings
        /// </summary>
        public static void Configure()
        {
            Mapper.CreateMap<Employee, EmployeeViewModel>();

            Mapper.CreateMap<Department, DepartmentViewModel>();

            Mapper.CreateMap<Device, DepartmentMoveViewModel>()
                .ForMember(x => x.DeviceId, opt => opt.MapFrom(args => args.Id))
                .ForMember(x => x.MoveTo, opt => opt.Ignore());

            Mapper.CreateMap<KeyListViewModel, ProductKey>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreationDate, opt => opt.Ignore())
                .ForMember(x => x.UpdateDate, opt => opt.Ignore());

            Mapper.CreateMap<Device, InventoryItemViewModel>()
                .ForMember(x => x.Number, o => o.MapFrom(args => args.Id))
                .ForMember(x => x.PurchaseDate, opt => opt.MapFrom(args => ((DateTime)args.PurchaseDate).ToString("MMMM dd yyyy")))
                .ForMember(x => x.ExpiryDate, opt => opt.MapFrom(args => ((DateTime)args.ExpiryDate).ToString("MMMM dd yyyy")))
                .ForMember(x => x.OwnerName, opt => opt.MapFrom(args => args.Owner.Name))
                .ForMember(x => x.Hidden, opt => opt.Ignore())
                .ForMember(x => x.Notes, opt => opt.MapFrom(args => args.Notes))
                .ForMember(x => x.AssetNumber, opt => opt.MapFrom(args => args.AssetNumber))
                .ForMember(x => x.Status, opt => opt.MapFrom(args => args.Status))
                .ForMember(x => x.Make, opt => opt.MapFrom(args => args.Make))
                .ForMember(x => x.Model, opt => opt.MapFrom(args => args.Model))
                .AfterMap((src, dest) =>
                    {
                        // Set to 0 for now until I figure out how
                        // to increment the number on each mapping!
                        dest.Number = 0;

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
