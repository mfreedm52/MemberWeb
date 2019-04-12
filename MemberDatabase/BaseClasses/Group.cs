using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDatabase
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupEmail { get; set; }
        public int AccessLevel { get; set; }

        public Group()
        {
            this.Members = new HashSet<UserGroup>();
        }
        public  ICollection<UserGroup> Members { get; set; }


    }
}
