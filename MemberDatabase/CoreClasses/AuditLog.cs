using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDatabase
{
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditId { get; set; }

        public DateTime changeDate { get; set; }
       
        public string entity { get; set; }
        public string modifyUser { get; set; }
        public string columnName { get; set; }
        public string oldProperty { get; set; }
        public string newProperty { get; set; }



        //public AuditLog(string entity, string modifyUser, DateTime changeDate, 
        //                string oldProperty, string newProperty)
        //{

        //}

    }


}
