using MicroServer.CheckWords.MinGans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServer.CheckWords.Model
{
    public class MinganCheckInput
    {
        [MinGanCheck]
        public string Text { get; set; }
    }

    public class MinganReplaceInput
    {
        [MinGanReplace]
        public string Text { get; set; }
    }
}
