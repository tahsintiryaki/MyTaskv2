using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBiletTask.Common
{
    public class BaseResponseModel
    {
        public string Status { get; set; }
        public object data { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
    }
}
