using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.Entities
{
    public interface IEntity
    {
        int Id { get; set; }

        DateTime? CreationDate { get; set; }

        DateTime? UpdateDate { get; set; }
    }
}
