﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.Entities
{
    public class Employee : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Department Department { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool Deleted { get; set; }
    }
}
