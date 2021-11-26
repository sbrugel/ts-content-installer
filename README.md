## The Basics
This is a custom built Train Simulator content installer. Originally built for the 4 Aspect Simulations [Fen Line route](https://www.4aspectsimulations.com/routes) (which is free to download, fantasticly made, and also uses this 
installer!), the program extracts a ZIP file located in its Resources folder and installs it to a user-specified Train Simulator game directory.

The user goes through several windows, setting installation language, agreeing to an EULA, setting their RailWorks directory, then installs the content bundled within the program. The content, which must be in a .ZIP file,
is extracted to the user's Temp folder, then the extracted contents are copied into their game directory. This program is best suited for TS routes or larger reskin/rolling stock projects.

## Features
The full list of features of this program are as follows:
- Support for multiple languages. (The example in the case of the Fen Line Installer is English with German localization)
- Properly interactable EULA that the end user must agree to in order to proceed with installation
- Train Simulator directory automatically detected via a registry key. In the case that this fails the program falls back to the default Steam directory
- Automatic content requirement detection based on supplied folders within the program *(see the Below section for more info)*
- Developer-customizable background images for visual appeal, which rotate as the user proceeds with installation
- Conditional content installation: Some assets can only be installed if the user has certain prerequisites, and are skipped if these requirements are not installed

## Notes
- Due to the compiled installer being over 100MB (as it contains the FenLine.zip file, which is around 600MB), the .exe for this program has been .gitignore'd. You'll need to build the exe yourself.
- Currently the order of forms appearing is as follows: LangSelect -> Form1 -> Form2 -> Form3 -> Form5. (Form4 would have been used for something else but has been kept for... reasons
- Form3: For requirement checking, it is highly recommended to use the Content folder for routes since these contain the route's terrain/scenery data. Some third party packs, such as the AP Weather Pack, may add in an Assets
folder for several routes (even if the end user does not have said routes). In this case, if the Assets folder is checked, the program will return a false-positive to the user.
- Form5: The original ZIP file that would be installed to TS is named "FenLine.zip", for reference for any code in this repo that aliases it. For obvious reasons this file has been .gitignore'd, but you can add your own file to the
installer (through the Resources settings in your IDE; I use Visual Studio 2019) and rename the resource aliasing and extracted folder names where appropiate.