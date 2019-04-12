using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemberDatabase
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }



        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }


    }


}
