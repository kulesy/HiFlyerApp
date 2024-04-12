using HiFlyerClassLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public ErrorState State { get; set; }
    }
}
