# corex
Hello and welcome to this repository. As developers, we all have our little convenient extension methods. Therefore, I thought I'd build a class library for dotnet core and add in as many extension methods as I can.

If you have any ideas for new extension methods or would like to report a bug fix for an existing one, then please submit a new issue in the `Issues` section of this repository.

Alternatively, please help out by contributing new features or fixing existing bugs. If you would like to contribute to this repository, please contact me so I can add you as a collaborator. You can also fork this repository and then submit a pull request back to it.

Thank you!

### Builds
master branch: [![Build Status](https://dev.azure.com/sid1983/sid1983/_apis/build/status/sidm1983.corex?branchName=master)](https://dev.azure.com/sid1983/sid1983/_build/latest?definitionId=1&branchName=master)

develop branch: [![Build Status](https://dev.azure.com/sid1983/sid1983/_apis/build/status/sidm1983.corex?branchName=develop)](https://dev.azure.com/sid1983/sid1983/_build/latest?definitionId=1&branchName=develop)

## Using this class library

This class library is coming to a nuget package manager near you. That's right, you will soon be able to install these extension methods via nuget, so stay tuned for that.

### A note about licensing...

This class library is licensed under AGPL-3.0. For more details about the license, please read the `LICENSE.txt` file in the root of the repository. 

## Contributing to this repository

### Technology stack
This class library is being built on top of `.NET Standard 2.0`.

### Tools


### Project structure
```
+-- corex.sln
+-- corex.string
|   +-- extensions
|   |   +-- StringConversion.cs
|   +-- corex.string.csproj
+-- corex.string.tests
+-- corex.<type>
+-- corex.<type>.tests
```
The solution file has multiple projects within it. The plan is to separate extension methods for a particular type into a project on its own. This means that all the extension methods of a particular type are grouped together and will ultimately be in a nuget package of their own. This allows other projects that consume this class library to only add the nuget references for the set of extension methods they need.

Additionally, within the project for a particular type, the extension methods are further categorised into separate `.cs` files based on the type of action they are performing. In the directory structure above, you can see that we have a class called `StringConversion.cs` which will only contain extension method to do with converting the `string` type to other data types. If you want to add an extension method that takes a string and changes its format or contents and outputs another string, then you could put that into a file called `StringTransformation.cs` as this method would not be converting string to another type and would simply be modifying the contents of the string. Categorising the methods in this manner should help reduce the length of each code file and will provide a logical, neat structure for the extension methods. Please adhere to these guidelines as much as possible.

## What's an extension method?

If you are new to .NET/C# programming and have never implemented or even heard of extension methods, then please read Microsoft's documentation about [Extension Methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods#binding-extension-methods-at-compile-time). There are also many other tutorials and guides online about this topic.