using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;


//namespace OpCodeGenerated
//{
//    public enum OpCodeCategory
//    { Other, Math, Logic, Flow, Memory }
//    public enum ParamType
//    { Register, Byte, Short }

//    public record Parameter(string Name, ParamType Type);
//    public record OpCode(string Name, string Description, OpCodeCategory Category, ImmutableArray<Parameter> Parameters);
//}


//namespace OpCodeGenerated
//{
//    public static class OpCodes
//    {
//        public static readonly OpCode Add = new OpCode()
//    }
//}

namespace SourceGenerators
{
    [Generator]
    class OpCodeJsonGenerator : ISourceGenerator
    {
        public void Initialize(InitializationContext context)
        {
            //Debugger.Launch();
        }

        public void Execute(SourceGeneratorContext context)
        {
            GenerateClasses(context);

            SourceText text = FindFile(context);


            GenerateOpCodesClass(context, text.ToString());
        }

        private void GenerateClasses(SourceGeneratorContext context)
        {
            string code = @"using System;
using System.Collections.Immutable;
namespace OpCodeGenerated
{
    public enum OpCodeCategory
    { Other, Math, Logic, Flow, Memory }
    public enum ParamType
    { Register, Byte, Short }

    public record Parameter(string Name, ParamType Type);
    public record OpCode(string Name, string Description, OpCodeCategory Category, ImmutableArray<Parameter> Parameters);
}";

#if WRITESOURCE
            File.WriteAllText("GeneratedCode/backendTypes.txt", code);
#endif
            context.AddSource("backendTypes", SourceText.From(code, Encoding.UTF8));
        }

        private SourceText FindFile(SourceGeneratorContext context)
        {
            return context.AdditionalFiles.Where((x) => Path.GetExtension(x.Path).Equals(".json", StringComparison.OrdinalIgnoreCase)).Single().GetText();
        }

        private void GenerateOpCodesClass(SourceGeneratorContext context, string jsonString)
        {
            StringBuilder classBuilder = new StringBuilder();
            classBuilder.Append(@"using System;
using System.Collections.Immutable;
namespace OpCodeGenerated
{
    public static class OpCodes
    {
        ");
            GenerateBody(classBuilder, jsonString);

            classBuilder.Append("} }");

#if WRITESOURCE
            File.WriteAllText("GeneratedCode/OpCodes.txt", classBuilder.ToString());
#endif
            context.AddSource("OpCodes", SourceText.From(classBuilder.ToString(), Encoding.UTF8));
        }

        private void GenerateBody(StringBuilder classBuilder, string jsonString)
        {
            JObject jObj = (JObject)JsonConvert.DeserializeObject(jsonString);
            foreach (var opCode in jObj.Children())
            {
                var opCodeBody = opCode.Value<JProperty>().Value.Values().ToImmutableArray();

                string name = opCode.Path;
                string fullName = opCodeBody[0].Value<string>();
                string description = opCodeBody[1].Value<string>();
                string category = opCodeBody[2].Value<string>();
                List<(string, string)> parameters = new List<(string, string)>();

                var paramObjects = opCodeBody[3].Children();
                foreach (var paramObject in paramObjects)
                {
                    var currentParam = paramObject.Values().ToArray();
                    parameters.Add((currentParam[0].Value<string>(), currentParam[1].Value<string>()));
                }
                GenerateLine(classBuilder, name, fullName, description, category, parameters);
            }
        }

        private void GenerateLine(StringBuilder classBuilder, string name, string fullName, string description, string category, List<(string, string)> parameters)
        {
            classBuilder.Append($"public static readonly OpCode {name} = ");
            classBuilder.Append($"new OpCode(\"{fullName}\", \"{description}\", {"OpCodeCategory." + category}, ");
            GenerateParametersDeclaration(classBuilder, parameters);
            classBuilder.Append($");\n\t\t");
        }

        private void GenerateParametersDeclaration(StringBuilder classBuilder, List<(string, string)> parameters)
        {
            classBuilder.Append("new Parameter[] { ");
            foreach((string name, string type) in parameters)
            {
                classBuilder.Append($"new Parameter(\"{name}\", {"ParamType." + type}), ");
            }
            classBuilder.Append("}.ToImmutableArray()");
        }
    }
}
