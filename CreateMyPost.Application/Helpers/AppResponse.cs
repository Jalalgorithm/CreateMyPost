using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.Helpers
{
    public class AppResponse
    {
        public string ErrorMessage { get; set; }
        public bool Successful => ErrorMessage == null;
        public object Result { get; set; }

    }
}
