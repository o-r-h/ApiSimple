﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Entities.Customs
{
    public class Attachment
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Content { get; set; }
    }
}
