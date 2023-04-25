# Gaming The Syllabus
Gaming the Syllabus is an effort at making course syllabi more engaging and interactive for students. This will be done by presenting all the information from a syllabus in a minigame-like format. Students will be able to play minigames for each major section of a syllabus (course overview, grade components, etc.) and get the complete information from it. In addition, the project also allows for adaptability to different course syllabus. We are looking to build it in such a way that allows for other professors to be able to also use the project to create a more interactive syllabus.

## Installation Guide
This guide will walk you through the steps required to install Unity on your computer.

### Step 1: System Requirements
Before installing Unity, make sure your computer meets the following system requirements:

* **Operating System**: Windows 7 SP1+, 8, 10; macOS 10.12+; Ubuntu 16.04, 18.04, and CentOS 7
* **Processor**: SSE2 instruction set support
* **RAM**: 8 GB or more
* **Graphics Card**: DX10 (shader model 4.0) or OpenGL 3.2+ compatible GPU
* **Hard Disk Space**: 20 GB or more of free space

### Step 2: Download Unity Hub
Unity Hub is a tool that lets you manage multiple Unity installations and projects. You can download Unity Hub from the Unity website.

1. Go to the **Unity website**.
2. Click on the **Get started** button at the top right corner of the page.
3. Select **Individual** from the dropdown menu.
4. Click on **Download Unity Hub**.
5. Follow the instructions to download and install Unity Hub.

###Step 3: Install Unity
After installing Unity Hub, you can use it to install Unity.

1. Open Unity Hub.
2. Click on the **Installs tab**.
3. Click on the **Add** button.
4. Select the version of Unity you want to install. If you're not sure which version to choose, we recommend selecting the latest stable version.
5. Select the modules you want to install. If you're not sure which modules to choose, we recommend selecting all of them.
6. Click on **Next**.
7. Review the installation details and click on **Done**.
8. Wait for Unity to download and install.

###Step 4: Activate Unity
After installing Unity, you'll need to activate it with a license. If you don't have a license, you can use Unity Personal, which is free for personal and small business use.

1. Open Unity Hub.
2. Click on the **Projects** tab.
3. Click on the **New** button.
4. Select the version of Unity you want to use.
5. Choose a name and location for your project and click on **Create**.
6. Unity will launch. If this is your first time using Unity, you'll be prompted to activate it.
7. If you have a license, enter your license key and click on **Activate**. If you don't have a license, select **Unity Personal** and click on **Continue**.
Congratulations! You have successfully installed and activated Unity. Now you can start creating your own games and applications.

## Release Notes

### Version 0.4.0

#### New Features
* Final boss fight is now in, meaning the game is completable
* Enemies and riddles have now been placed in their finalized positions
* Core game loop is in and functional

#### Bug Fixes
* The start menu is now functional, and will load the proper level
* No more empty rooms on the map

#### Known Issues
* Progression is still somewhat buggy
---

### Version 0.3.0

#### New Features
* Syllabus riddles can now be read from Firebase Storage, instead of being hardcoded into the game
* Game enemies can now be loaded from Firebase Storage, instead of being hardcoded into the game
* Player progress can now be tracked thanks to the integration of Firebase Database into the game
* Class keys have been implemented as the way to grant or deny access to syllabus games for students

#### Bug Fixes
* n/a

#### Known Issues
* Need more robust rules in Firebase for write/read permissions 
---

### Version 0.2.0
#### New Features
* Syllabus riddles can now be created and modified via XML files
* Combat UI is cleaner, and now displays abilities
* Abilities now have functionality in combat
* Combat now triggers on collision with enemies
* Level layouts have been designed and implemented

#### Bug Fixes
* Updated combat logic to run without loading a new scene
* Improved the loading system
* Fixed various visual glitches.

#### Known Issues
* Riddle submission is occasionally inconsistent
---

### Version 0.1.0
#### New Features
* Enemies can now be created and modified via XML files 
* Health bars are now generated for enemies with their corresponding name and placeholder icons
* Support for multiple actions from units

#### Bug Fixes
* Improved combat cycling 
* Reorganize file structure for more organized code
* Updated the .gitignore file  

#### Known Issues
* At the moment there’s no issue to report
---




