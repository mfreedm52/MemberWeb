using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDatabase
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        public Contact ContactRef { get; set; }

        public DateTime postDate { get; set; }
        public string postedBy { get; set; }
        public string comment { get; set; }


    }


}
