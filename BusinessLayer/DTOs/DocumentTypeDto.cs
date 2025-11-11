using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    
 
    public class CreateDocumentTypeDto
    {
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; } = string.Empty;
        public CreationInfoDto Audit { get; set; } = new();


    }

    public class UpdateDocumentTypeDto 
    {
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public ModificationInfoDto Audit { get; set; } = new();



    }
}
