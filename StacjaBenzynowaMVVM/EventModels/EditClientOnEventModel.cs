﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StacjaBenzynowaMVVM.EventModels
{
    class EditClientOnEventModel
    {
        public int State { get; set; }
        public EditClientOnEventModel(int state)
        {
            State = state;
        }
    }
}
