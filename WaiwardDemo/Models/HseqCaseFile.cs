using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WaiwardDemo.Models
{
    public class HseqCaseFile
    {
        [Key]
        public int HseqCaseFileID { get; set; }

        [Index(IsUnique = true)] 
        [Display(Name = "Case No")]
        public int CaseNo { get; set; }

        [Display(Name = "NCR Ref")]
        public int? NcrID { get; set; }
    }
}