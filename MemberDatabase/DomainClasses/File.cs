using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDatabase
{
    
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileId { get; set; }

        public DateTime UploadDate { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        
        public string uploadedBy { get; set; }
        
    }
}
