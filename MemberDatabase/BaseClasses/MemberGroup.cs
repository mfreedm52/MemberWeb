using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDatabase
{
    [Table("MemberGroup")]
    public class MemberGroup
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Member")]
        public int MemberID;

        [Key, Column(Order = 1)]
        [ForeignKey("Group")]
        public int GroupID;

        public MemberGroup(int MemberID, int GroupID)
        {
            this.MemberID = MemberID;
            this.GroupID = GroupID;
        }
        public virtual Member myMember {get; set;}

        public virtual Group myGroup { get; set; }



    }
}
