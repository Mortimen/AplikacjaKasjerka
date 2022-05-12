using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StacjaBenzynowaMVVM.EventModels
{
    class DeleteEmployeeOnEventModel
    {
        public int State { get; set; }
        public DeleteEmployeeOnEventModel(int state)
        {
            State = state;
        }
    }
}
