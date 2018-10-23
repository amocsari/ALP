using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP
{
    public class DialogResult<TResult>
    {
        public TResult Value { get; set; }
        public bool Accepted { get; set; }
    }
}
