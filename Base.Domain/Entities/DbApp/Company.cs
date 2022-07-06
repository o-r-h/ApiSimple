using System;
using System.Collections.Generic;

#nullable disable

namespace Base.Domain.Entities.DbApp
{
    public partial class Company : BaseEntity
    {
        public long CompanyId { get; set; }
        public string RegisterNumber { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public override DateTime? CreatedAt { get; set; }
        public override string CreatedBy { get; set; }
        public override DateTime? ModifiedAt { get; set; }
        public override string ModifiedBy { get; set; }
        public long? CompanyStatusId { get; set; }

        public virtual CompanyStatus CompanyStatus { get; set; }
    }
}
