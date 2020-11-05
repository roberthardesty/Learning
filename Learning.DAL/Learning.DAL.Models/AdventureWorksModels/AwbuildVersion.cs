using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Learning.DAL.Models.AdventureWorksModels
{
    public partial class AwbuildVersion
    {
        [Key]
        public byte SystemInformationId { get; set; }
        public string DatabaseVersion { get; set; }
        public DateTime VersionDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
