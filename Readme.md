# Gaming The Syllabus
Gaming the Syllabus is an effort at making course syllabi more engaging and interactive for students. This will be done by presenting all the information from a syllabus in a minigame-like format. Students will be able to play minigames for each major section of a syllabus (course overview, grade components, etc.) and get the complete information from it. In addition, the project also allows for adaptability to different course syllabus. We are looking to build it in such a way that allows for other professors to be able to also use the project to create a more interactive syllabus. 

## Release Notes
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

