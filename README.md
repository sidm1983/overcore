# corex

[![Build Status](https://dev.azure.com/sid1983/sid1983/_apis/build/status/sidm1983.corex?branchName=master)](https://dev.azure.com/sid1983/sid1983/_build/latest?definitionId=1&branchName=master)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A .NET Standard 2.0 class library full of handy extension methods that is also CLS-Compliant. This class library is free to use for personal and commercial projects.

## What's an extension method?

If you are new to .NET/C# programming and have never implemented or even heard of extension methods, then please read Microsoft's documentation about [Extension Methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods#binding-extension-methods-at-compile-time). There are also many other tutorials and guides online about this topic.

## Using this class library

This class library is coming to a nuget package manager near you. That's right, you will soon be able to install these extension methods via nuget, so stay tuned for more details about that.

### A note about licensing...

This class library is licensed under the MIT license. For license terms & conditions, please read the `LICENSE.txt` file in the root of the repository.

## Contributing to this repository

If you have any ideas for new extension methods or would like to report a bug fix for an existing one, then please submit a new issue in the `Issues` section of this repository.

Alternatively, please help out by contributing new features or fixing existing bugs. If you would like to contribute to this repository, please contact me so I can add you as a collaborator. You can also fork this repository and then submit a pull request back to it.

### Getting started

#### Fetching the repository

To fetch the latest stable source, clone the `master` branch of the github repository using the following command:

`> git clone https://github.com/sidm1983/corex.git`

#### Building the solution (dotnet CLI)

Assuming you have the .NET Core SDK 2.2 installed, run the following command in the root of the repository:

`> dotnet build`

The above command will build the solution using the `Debug` configuration by default. To build the command using the `Release` configuration, run the following command:

`> dotnet build -c Release`

> **Note**: Building the solution will automatically generate a nuget package for each project in the solution. This solution/projects are set up in this manner intentionally. Please read the [Project structure](#project-structure) section below for an explanation of the setup.

To execute all the unit tests from all the projects in the solution, run:

`> dotnet test`

Alternatively, you can execute the tests for a specific project by running the following command:

`> dotnet test corex.string.tests/corex.string.tests.csproj`

### Technology stack

This class library is built using:
* `.NET Standard 2.0`
* `xUnit 2.4.0`
* `Azure Devops`

### Development process

#### Overview

This project uses the TDD approach to building this class library. In other words, a unit test for a particular test scenario is written first and then code is added to the extension method until the test passes. This process is repeated for each scenario (positive or negative) that the extension method needs to cover. Please adhere to this approach as much as possible. This process is extremely important for high code quality and high refactoring confidence.

As much as possible, please ensure that all compiler warnings are addressed. While warnings are not usually critical, it would be best that this project builds without any warnings. Warning may be ignored in exceptional circumstances, but only if there is a strong, valid reason to do so.

Documentation is key to the success of any class library (or any project in general) and in this project we want to make sure that all the extension methods are well-documented. Please use documentation comments above each method to describe what a particular method does, the inputs it requires and the output it returns including any specific formatting requirements. The idea here is to be able to automatically generate documentation based on the documentation comments above each extension method/class because while documentation is important, we all know that writing good documentation consistently is a difficult task. Therefore, we will attempt to automate as much of it as possible. For now, let's Keep It Super Simple (KISS) and stick to writing comments above each extension method.

#### Committing code

As much as possible, try to keep your code commits small. It is much better to commit small amounts of changes more frequently than committing months of development in one monolithic commit. Make sure all the changes included in a commit are related. Ideally, try to stick to one extension method per commit. This will make it easier to review pull requests and for new contributors to follow the commit history of this repository.

It is also important to add certain keywords to your commit messages so that commits can easily be tied back to the features/issues they relate to. You should also add certain keywords and issue numbers to your commit messages to create an association with a PR or to automatically close a related issue. If you want to know more, you can read about [Closing issues using keywords](https://help.github.com/en/articles/closing-issues-using-keywords) on GitHub Help.

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