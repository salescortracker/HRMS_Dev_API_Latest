using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class CreationInfoDto
    {
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; } = 1;
        public DateTime? CreatedDate { get; set; }
    }

    public class ModificationInfoDto
    {
        public int ModifiedBy { get; set; } = 1;
        public DateTime? ModifiedAt { get; set; }
    }
}
