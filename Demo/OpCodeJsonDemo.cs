using System;
using System.Collections.Generic;
using System.Text;
using OpCodeGenerated;

namespace Demo
{
    static class OpCodeJsonDemo
    {
        public static string DoSomething()
        {
            OpCode code = OpCodes.ldI;
            return code.Description;
        }
    }
}
