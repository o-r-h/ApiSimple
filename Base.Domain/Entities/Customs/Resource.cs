using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Entities.Customs
{
    public class Resource
    {
        public string Identifier { get; set; }
        public byte[] Content { get; set; }
        public string Path { get; set; }
        public string MediaType { get; set; }

        public Resource()
        {
            MediaType = MediaTypeNames.Image.Jpeg;
        }
    }
}
