using System;
using System.Collections.Generic;
using System.Text;

namespace SewingPatternBuilder
{
    interface IMeasure
    {
        string Name { get; set; }

        int GetSize();
        void SetSize(int value);

        int Id { get; set; }

    }
}
