# Discord Bot - Clean Architecture
Clean architecture - recommendations on how to organize the code, for easy reading, writing and maintaining. I tried to combine architecture with bot development, to achieve good scalability in the future.

## Install and Run
 - Download [.NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
 - Insert [your bot's](https://discord.com/developers/applications) token in file DiscordBot.Presentation/`appsettings.json`
 - Run <3

## DataBase
 - SQLite is used in the project. When you start the project, the database is created in the root (Debug or Realese) during the build.
 - You can change the database by changing the connection method in the `Program.cs` file as well as changing the connection string in `appsettings.json`.
 - ORM Entity Framework is used in the project

## Special Features
A clean architecture implies code separation, to achieve independence from each other. **For now**, I consider `Commands` and `events` to be separate logic. The user interacts not only with commands, but also with events (albeit indirectly). This in my opinion will have a positive impact on development and scaling, because the logic of interaction with user actions is in one place. 

That's why the project has `CommandLoaderService.cs`. Although this implementation is close to the official one from Discord.NET, I don't like it. This file allows you to import commands from DiscordBot.Application, by working with assembly dependencies. 

## Post scriptum

**Project is a Pet**, on learning pure architecture, the code may contain errors and inaccuracies. 

**Hopefully my code will help you start your let with a clean slate*.