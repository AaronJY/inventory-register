﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ES.InventoryRegister.Entities
{
    public abstract class Device : IEntity
    {
        public enum DeviceStatus
        {
            [Display(Name = "Using")]
            InUse,

            [Display(Name = "Spare")]
            Spare,

            [Display(Name = "To Be Thrown")]
            ToBeThrown
        }

        [Key]
        public int Id { get; set; }

        public int AssetNumber { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual Employee Owner { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public bool Deleted { get; set; }

        public DeviceStatus Status { get; set; }
    }
}
