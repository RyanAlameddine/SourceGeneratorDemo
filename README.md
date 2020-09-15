# SourceGeneratorDemo
C# 9 source generators practice/demo project

Source generators allow users to create a Code-Analysis project which is able to actually add source code to the assembly on compile time. 
This is similar to reflection but has some key advantages in the sense of performance and also user-friendlyness because intellisense can pick up on generated source.
This project contains three demos, one which is a simple hello world generator, one which automatically generates properties with a notify property changed event for all fields with a specific attribute,
and one which generates a static OpCode class which loads all the opCodes registered in a supplied json file in the project at compile time and generates instances of an OpCode class to represent them.
