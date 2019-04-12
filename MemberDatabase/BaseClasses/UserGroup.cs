using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDatabase
{
    [Table("UserGroup")]
    public class UserGroup
    {
        
        [Key]
        [Column(Order = 0)]
        public int memberId
        {
            get; set;
        }
        public string userType
        {
            get; set;
        }

        [Key]
        [Column(Order=1)]
        public int groupId
        {
            get; set;
        }


    }
}
