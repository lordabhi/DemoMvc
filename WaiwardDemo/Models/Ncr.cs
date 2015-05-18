using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WaiwardDemo.Models
{
    public class Ncr : HseqRecord
    {
        [Key]
        public int NcrID { get; set; }

        public NcrSource NcrSource { get; set; }

        public NcrState NcrState { get; set; }

        public int DiscrepancyTypeID { get; set; }

        public virtual DiscrepancyType DiscrepancyType { get; set; }

        public int? HseqCaseFileID { get; set; }

        public virtual HseqCaseFile HseqCaseFile { get; set; }
    }
}