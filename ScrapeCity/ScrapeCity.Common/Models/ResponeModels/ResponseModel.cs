using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeCity.Common.Models.ResponeModels
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }
        public T Data { get; set; }
        public bool IsSuccess { get; set; }

    }
}
