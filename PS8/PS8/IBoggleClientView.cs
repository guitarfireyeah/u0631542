﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS8
{
    interface IBoggleClientView
    {
        event Action<string, Uri, int> registerButtonClicked;
    }
}
