### Dependencies
* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* tModLoader 1.4.4
* [SubworldLibrary](https://github.com/jjohnsnaill/SubworldLibrary)

### Setting Up the Project Environment
Find your ModSources directory.  It should be at one of these locations depending on your Operating System:
- (Windows) `Documents/My Games/Terraria/tModLoader/ModSources`
- (Mac) `~/Library/Application support/Terraria/tModLoader/ModSources`
- (Linux) `~/.local/share/Terraria/tModLoader/ModSources`

Next, follow these instructions to properly build the project:
1. Run `git clone https://github.com/SpiralshapeDev/TBats.git` in the ModSources folder to clone the repository.
2. If you're using Visual Studio, open the `.sln` file in the folder created by Step 1, then either press F6 or select `Build > Build Solution`.
<br>Otherwise, run `dotnet build` in `<ModSources>/TBats`.
<br>If running that command results in an error mentioning an "exit code 150", install the [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) as well.
3. Create the directory `<ModSources>/.libs`.
4. Inside of `<ModSources>/.libs` folder, add files: [SubworldLibrary.dll](https://github.com/jjohnsnaill/SubworldLibrary/blob/master/SubworldLibrary.dll) & [SubworldLibrary.xml](https://github.com/jjohnsnaill/SubworldLibrary/blob/master/SubworldLibrary.xml)
5. If you're using Visual Studio, right-click on Dependencies in your project, click on `Add Project Reference...`, then `Browse...`, select the `<ModSources>/.libs/SubworldLibrary.dll`, and click OK. The xml will be detected automatically if it's in the same location as the dll.
<br>Otherwise, in your DE of choice, add `<ModSources>/.libs/SubworldLibrary.dll` to your project dependencies as it is done in your DE normally.
6. Setup Done!