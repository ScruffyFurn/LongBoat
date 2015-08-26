# Longboat Game Analytic Library
Game Analytics library using Microsoft's Azure platform for storage.

# Features
Heat mapping is the primary purpose for Longboat but is by no means limited to just that. With that in mind you can do the following with Longboat without modification.
  - Create/Delete Container to hold data
  - Add/Delete data from containers
  - Retrieve Data from a specific container
  - Retrieve a list of Containers

# Requirements
If you are looking to modify Longboat code. The following is required
- Longboat requires the following
  - Azure C++ SDK wastorage <a href="https://www.nuget.org/packages/wastorage/">Version 1.0</a>
    - C++ Rest SDK (Casablanca) <a href="https://www.nuget.org/packages/wastorage/">Version 2.4.0.1</a> (this will be installed automatically with wastorage through nuget)

# Download & Install
You can find the required DLL files in the release section of this github page. <a href="https://github.com/ScruffyFurn/Longboat/releases">Here</a>
Add these to your project and you are on your way. 

### For use in unity
  - Create Plugins folder in Assets folder if it does not already exist
  - Right click in plugins folder and select import assets
  - Navigate to where you downloaded the DLL’s to and select and import them one at a time

# Getting Started
###Unity
For a quick start there is a unity example project that can help you get the hang of using Longboat in unity
###Anything else
To get started with the Longboat library in any other platform or just to modify or study it. All you should need is Longboat itself and the DLL's


