using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model.Dto;

namespace ALP.ViewModel.Employee
{
    public class EmployeeListItemViewModel : AlpViewModelBase
    {
        private EmployeeDto value;
        public EmployeeDto Value
        {
            get { return value; }
            set
            {
                if (value != this.value)
                {
                    Set(ref this.value, value);
                }
            }
        }

        public EmployeeListItemViewModel(EmployeeDto dto)
        {
            Value = dto;
        }

        protected async override Task InitializeAsync(){ }
    }
}
