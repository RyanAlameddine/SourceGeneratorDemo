using System;
using System.Collections.Generic;
using System.Text;
using OpCodeGenerated;

namespace Demo
{
    static class OpCodeJsonDemo
    {
        public static void DoSomething()
        {
            OpCode code = OpCodes.ldI;

            Console.WriteLine(code.Parameters[0].Name);
            Console.WriteLine(OpCodes.noOp.Description);
            Console.WriteLine(OpCodes.eql.Category == OpCodeCategory.Flow);
            Console.WriteLine(OpCodes.xor.Parameters[2].Type == ParamType.Register);
        }
    }
}
