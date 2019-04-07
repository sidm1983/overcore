# corex

[![Build Status](https://dev.azure.com/sid1983/sid1983/_apis/build/status/sidm1983.corex?branchName=master)](https://dev.azure.com/sid1983/sid1983/_build/latest?definitionId=1&branchName=master)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A .NET Standard 2.0 class library that is also CLS-Compliant. This class library contains handy extension methods and is free to use for private and commercial projects.

## What's an extension method?

If you are new to .NET/C# programming and have never implemented or even heard of extension methods, then please read Microsoft's documentation about [Extension Methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods#binding-extension-methods-at-compile-time). There are also many other tutorials and guides online about this topic.

## Using this class library

This class library is coming to a nuget package manager near you. That's right, you will soon be able to install these extension methods via nuget, so stay tuned for more details about that.

### A note about licensing...

This class library is licensed under the MIT license. For license terms & conditions, please read the `LICENSE.txt` file in the root of the repository.

## Contributing to this repository

If you have any ideas for new extension methods or would like to report a bug fix for an existing one, then please submit a new issue in the `Issues` section of this repository.

Alternatively, please help out by contributing new features or fixing existing bugs. If you would like to contribute to this repository, please contact me so I can add you as a collaborator. You can also fork this repository and then submit a pull request back to it.

### Technology stack

This class library is being built using:
* `.NET Standard 2.0`
* `xUnit`
* `Azure Devops`

### Development process

This project uses the TDD approach to building this class library. In other words, a unit test for a particular test scenario is written first and then code is added to the extension method until the test passes. This process is repeated for each scenario (positive or negative) that the extension method needs to cover. Please adhere to this approach as much as possible. This process is extremely important for high code quality and high refactoring confidence.

As much as possible, please ensure that all compiler warnings are addressed. While warnings are not usually critical, it would be best that this project builds without any warnings. Warning may be ignored in exceptional circumstances, but only if there is a strong, valid reason to do so.

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