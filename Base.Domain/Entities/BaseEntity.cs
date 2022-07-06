using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Entities
{
    public class BaseEntity
    {
        public virtual DateTime? CreatedAt { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? ModifiedAt { get; set; }
        public virtual string ModifiedBy { get; set; }


        [NotMapped]
        public virtual long Id { get; set; }

        [NotMapped]
        public int PageNumber { get; set; }

        [NotMapped]
        public int PageSize { get; set; }
    }
}
