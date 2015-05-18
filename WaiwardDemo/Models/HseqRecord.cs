using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WaiwardDemo.Models
{
    public abstract class HseqRecord
    {

        [Required]
        public String Title { get; set; }

        [Required]
        public String Description { get; set; }
        
        public String RecordType { get; set; }
        
        public String EnteredBy { get; set; }
        
        public String ReportedBy { get; set; }
        
        public String QualityCoordinator { get; set; }
        
        public String Status { get; set; }

    }
}