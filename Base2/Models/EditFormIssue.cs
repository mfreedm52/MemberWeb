using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base2.Web.Infrastructure.Mapping;

namespace Base2.Models
{
    public class EditIssueForm : IMapFrom<Domain.Issue>
    {
        [HiddenInput]
        public int IssueID { get; set; }

        [ReadOnly(true)]
        public string CreatorUserName { get; set; }

        [Required]
        public string Subject { get; set; }

        public IssueType IssueType { get; set; }

        [Display(Name = "Assigned To")]
        public string AssignedToID { get; set; }

        [Required]
        public string Body { get; set; }
    }
}