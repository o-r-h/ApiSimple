using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Base.Domain.Entities.DbApp
{
    public partial class CompanyStatus : BaseEntity
    {
        public CompanyStatus()
        {
            Companies = new HashSet<Company>();
        }

        public long CompanyStatusId { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public override DateTime? CreatedAt { get; set; }
        [NotMapped]
        public override string CreatedBy { get; set; }
        [NotMapped]
        public override DateTime? ModifiedAt { get; set; }
        [NotMapped]
        public override string ModifiedBy { get; set; }


        public virtual ICollection<Company> Companies { get; set; }
    }
}
