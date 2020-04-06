# Contributing to OpenChart

Thanks for your interest in contributing to OpenChart. This guide aims to outline some of the standards we've set for contributions to the project.

## Coding standards
In order to keep the standards of our codebase high, we ask that any code you write adheres to the the [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions). This helps to keep everything consistent and readable. 

Also, in the interest of maintaining proper documentation, we require that any public classes, methods or properties include [XMLdocs](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/). Please include a description of what the method/class/property is for, as well as any method arguments. You can usually generate these automatically by typing three forward slashes (`///`) - in Visual Studio Code, [there is an extension to facilitate this](https://marketplace.visualstudio.com/items?itemName=k--kato.docomment).

To keep the formatting consistent, it might be helpful for you to use the automatic formatting tools built into your IDE or text editor of choice.

In addition, we ask that where possible, any code you write is covered with clear, concise unit tests. This really helps us to ensure that any regressions are caught in the future, and gives us a fighting chance at not releasing buggy code.

## Pull requests and issues
When raising pull requests and issues, please provide a brief description of the proposed changes and why you think they are justified. Moreover, when reporting bugs as issues, please provide any steps you take to reproduce the bug, as well as any other information you think might be useful (system specs, operating system, etc.). 

This comes in extremely useful when project maintainers come to review any proposed changes or bugfixes, and helps us push things through without having to ask too many questions.

Please base any pull requests on the `develop` branch.

## Any questions?

We're here to help you make a success out of any contributions to OpenChart. If you need help or have any questions, [please join our Discord server](https://discord.gg/wSGmN52).