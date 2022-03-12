using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientConsoleApp
{
    public class BearerResult
    {
        public bool Error { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }
    }
}
