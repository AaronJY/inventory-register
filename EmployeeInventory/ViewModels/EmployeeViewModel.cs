using ES.InventoryRegister.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
