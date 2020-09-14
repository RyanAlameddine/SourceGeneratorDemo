using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace SourceGenerators
{
    [Generator]
    public class HelloWorldGenerator : ISourceGenerator
    {
        public void Execute(SourceGeneratorContext context)
        {
            StringBuilder sourceBuilder = new StringBuilder(@"
using System;
namespace HelloWorldGenerated
{
    public static class HelloWorld
    {
        public static void SayHello() 
        {
            Console.WriteLine(""Hello from generated code!"");
        }
    }
}");

#if WRITESOURCE
            File.WriteAllText("GeneratedCode/helloWorldGenerated.txt", sourceBuilder.ToString());
#endif
            // inject the created source into the users compilation
            context.AddSource("helloWorldGenerated", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(InitializationContext context)
        {
            // No initialization required
        }
    }
}
















namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { }
}