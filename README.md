# Longboat Game Analytic Library
Game Analytics library for heat mapping using Microsoft's Azure platform for storage.

# What is a Heat Map?
A 'Heat Map' as seen to the right is A heat map is a graphical representation of data with a color-coding to put emphasis on areas of interest. This allows you to identify issues with your game. For example: The heat map is showing places where people in your game died and there are large concentrated patches of red/orange and alot of blue surrounding it. This could mean that there is an issue killing players in that place more often.

# Why Azure?
Azure blob storage offers a simple and elegant way to store large amounts of data. It is especially great when you want to save the data but dont care what particular order you get it back in when retrieving it. Using Block Blob storage is a great solutions for this since it ios quick and easy and doesnt meddle with sorting.

# Features
Heat mapping is the primary purpose for Longboat but is by no means limited to just that. With that in mind you can do the following with Longboat without modification.
  - Create/Delete Container to hold data
  - Add/Delete data from containers
  - Retrieve Data from a specific container
  - Retrieve a list of Containers

# Requirements
If you are looking to modify Longboat code. The following is required.  
1. First and foremost, an internet connection but I am assuming you have one since you made it here.  
2. You will need a way to edit your code, I use Visual Studios since it makes package management simple but you can use the editor of your choice. Just keep in mind you will be needing packages with nuget or git.  
3. You will need a <a href="https://azure.microsoft.com/en-us/pricing/free-trial/?WT.mc_id=azurebg_CA_sem_google_BR_BRTop_Nontest_FreeTrial_azure&WT.srch=1">Microsoft Azure subscription</a>  
4. Lastly Longboat requires the following
- Azure C++ SDK wastorage <a href="https://www.nuget.org/packages/wastorage/">Version 1.0 or Higher</a>

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


