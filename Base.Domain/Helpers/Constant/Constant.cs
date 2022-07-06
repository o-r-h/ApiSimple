using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Helpers
{
    public sealed class Constant
    {
        public class ContentTypeMime
        {
            public const string ZIP = "application/zip";
            public const string PDF = "application/pdf";
            public const string Word = "application/msword";
            public const string WordX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            public const string Excel = "application/vnd.ms-excel";
            public const string ExcelX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public const string PPT = "application/vnd.ms-powerpoint";
            public const string PPTX = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            public const string Text = "text/plain";
            public const string General = "application/octet-stream";
        }

        public class Sex
        {
            public const string MALE = "M";
            public const string FEMALE = "F";
            public const string OTHER = "O";
        }
    }
}
