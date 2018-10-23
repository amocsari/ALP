using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.ViewModel
{
    public interface IDialogViewModel<TReturnParameter, TParameter>
    {
        TParameter Parameter { get; set; }
        TReturnParameter ReturnParameter { get; set; }
    }
}
