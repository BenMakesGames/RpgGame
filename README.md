# PetGame

## What is it?

PetGame is a skeleton project for making your own pet game! (Or any browser-based game, really.)

It's written using Blazor Server, a framework for making web apps in C#, without JavaScript.

[![Buy Me a Coffee at ko-fi.com](https://raw.githubusercontent.com/BenMakesGames/AssetsForNuGet/main/buymeacoffee.png)](https://ko-fi.com/A0A12KQ16)

## Installing & Running

1. Install an IDE! [Visual Studio](https://visualstudio.microsoft.com/downloads/), [Rider](https://www.jetbrains.com/rider/), or [vscode](https://code.visualstudio.com/) are three I've tried; others exist
2. Download PetGame!
   * The best way to do so is using the "Get from Version Control" option, or similar, in your IDE, using the URL `https://github.com/BenMakesGames/PetGame.git`. But if you can't figure that out, you can download a ZIP using the green "Code" button, above; unzip it wherever you like.
3. Install .NET SDK 7.04, or later
   * https://dotnet.microsoft.com/en-us/download/dotnet/7.0
7. Run the game!
   * With Visual Studio or Rider, run `PetGame.Server: http`, OR:
   * With vscode, open a terminal in the `PetGame/PetGame` directory, and run `dotnet run`
8. In your browser, visit http://localhost:5099

If all went well, you should see a simple page with Login and Sign up buttons!

## Developing

### Libraries Used

PetGame leverages several open-source libraries to do some of its work. If you're familiar with C#, but not these libraries, you may want to take a moment to learn about some of them now (search Google, ask ChatGPT, or find tutorials on YouTube). If you're new to C#, it might be a bit much to dive into these; take some time learning the PetGame code, itself, first.

* [Entity Framework](https://learn.microsoft.com/en-us/aspnet/entity-framework), for storing data in a database
* [Blazored.Modal](https://blazored.github.io/Modal/), for displaying modals
* [FluentValidation](https://docs.fluentvalidation.net/) and [Blazored.FluentValidation](https://github.com/Blazored/FluentValidation), for validating user input

### Common Tasks

#### Adding Pages

1. Using your IDE, right-click `PetGame/PetGame/Pages`, and create a new Razor Page
   * As the game grows, you may want to create folders within `PetGame/PetGame/Pages` to help organize things!
1. If the page should only be accessible to logged-in players, add `@attribute [Authorize]` to the top of the file.
1. Look at an existing page to get an idea for how to make your own. `MyHouse.razor` is a good one to start with.

#### Looking at or Modifying the Contents of the Database, Directly

You should download a database client for viewing databases.

When getting started with PetGame, which uses Sqlite, [HeidiSQL](https://www.heidisql.com/download.php) client is a good, free/open-source database client that can view Sqlite databases, as well as many other SQL-ish databases.

* HeidiSQL download: https://www.heidisql.com/download.php

The PetGame database is stored in a file called `PetGame.db`, which will appear in the `PetGame/PetGame` directory the first time you run the game. You can open this file with HeidiSQL to view the database, and edit its contents.

#### Adding a New Table to the Database

**TODO:** I'll add more instructions on this later.

## Deploying Your Game!

**TODO:** I'll add instructions on deploying your game to the internet later. In the meanwhile, Google around for how to deploy a "hosted Blazor WASM" web app; there's info out there!