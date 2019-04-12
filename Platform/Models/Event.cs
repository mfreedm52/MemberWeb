using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Platform.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string EventName { get; set; }
        public MemberDatabase.Member createdBy { get; set; }
        public DateTime time { get; set; }



    }
}