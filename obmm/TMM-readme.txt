Readme for TesModManager
========================

How to install:
1) Pick the setup for installer driven installation.
or
2) You can pick the application only if you have a previous installation and want to extract the files by hand and copy them to the right folder.

Quick tip for fast and easy mod creation: Use the import button and pick a downloaded file from Nexus. TesModManager will try to load and download what it can from Nexus. It works the first time with most downloaded files.

Description
-----------
TesModManager is a new version of OBMM Extended that supports Skyrim SE, Skyrim, Oblivion and Morrowind. I renamed it TesModManager for this reason.

What you can do with it:
* create complex mods using simple script or C# scripting to control what files go where and even edit the ini files
* assemble mods from different folders, files and images
* automatically import obmm/omod mods
* automatically import NMM/fomod mods (supports both XML and C# fomod scripts)
* automatically import mods from zip/7z/rar files (some odd mods may need tweaking)
* use as download manager for Nexus for all supported games
* retrieve Mod Descriptions, Author, Version and pictures from nexusmods.
* sort mods and call BOSS (if installed) to do it
* start skyrim using SKSE or Oblivion using OBSE
* installs/updates OBSE/SKSE for you
* install SkyProc patchers and lets you run them. Supports SUM to manage them
* detect and identify installed Steam mods (if they have an esp/esm)
* detect conflicts between mods
* allows you to pick the "winning" file in case of conflict using the integrated DDS viewer
* list data files linked to plugins
* group mods logically and activate them all at once
* export/extract mods to zip. These can be easily uploaded to nexus for others to use
* view, extract and create BSA files
* manage your save files
* analyzes your saves and allows you to restore mods and load order as per your save to go back to a known working configuration

Questions:
--------------
How does this compare with NMM:
* it does not require tesnexus access to work
* it helps you address conflicts
* it lets you preview many things about a mod
* it gives you deeper access to a mod's innards
* it analyzes mods so that it can install them even if the packaging is not great (some will still require tweaking)
* it manages your saves
* it supports Skyproc
* it supports omod and NMM scripts
* it recognizes Steam mods
* it installs OBSE/SKSE for you
...

How does this compare with Wrye Bash:
* it is simpler to use as it requires less deep mod knowledge to operate
* it does more for you (the part of requiring less deep mod knowledge)
* it supports Skyproc
* it supports omod and NMM scripts
* it installs OBSE/SKSE for you
* Note that Bash has a slightly deeper knowledge of ESP/ESM and can do Bashed Patch
* TesModManager can work in conjuction with Wrye Bash
* it recognizes Steam mods


This is licensed under GPL.
Source code is available at https://github.com/monpetitbeurre/TesModManager

Troubleshooting
----------------------
Q: I get "missing basetools.dll".
A:You need to install the setup package first to get all the DLLs

Q: I get "could not get BSA list"
A: your ini is missing a section. you could rename your ini and start the game so that it recreates it hopefully with the missing section or you could add it by adding the following text at the end of skyrim.ini:
[Archive]
sResourceArchiveList=Skyrim - Misc.bsa, Skyrim - Shaders.bsa, Skyrim - Textures.bsa, Skyrim - Interface.bsa, Skyrim - Animations.bsa, Skyrim - Meshes.bsa, Skyrim - Sounds.bsa
sResourceArchiveList2=Skyrim - Voices.bsa, Skyrim - VoicesExtra.bsa 

Q: TesModManager will not start
A: windows 7 security might be blocking the registry access needed to check Nexus Download Manager registration. Try starting the application with a right click and selecting "run as administrator"

Q: I get Type: System.DllNotFoundException Error message: The DLL "devil.dll" is missing a dependency
A: You need to install Microsoft's vc DLL (known as vcredist_x86.exe) from http://www.microsoft.com/en-us/download/details.aspx?id=30679

Credits
-------
Oblivion Mod Manager - Timeslip
Oblivion Mod Manager Extended -  Scent Tree
Icons by SneakyTomato and UESP
Help Files, OCD List Submissions and Bug Finding in OBMM Extended - Isabelxxx
Optimizations, Skyrim SE, Skyrim and Morrowind port by monpetitbeurre

Version changes
===============
Version 1.6.25
--------------
* Allows users to set defaults on first start
* defaults to a game if only one is found on system

Version 1.6.24
--------------
* Fixed a case where NXM links would confuse TMM so it asks what game to start for

Version 1.6.23
--------------
* Fixed a case where URL to mod become bad when querying Nexusmods for info (extra spaces added)

Version 1.6.22
--------------
* Fixed handling of unicode FOMOD script (like RS children overhaul)

Version 1.6.21
--------------
* Added a protection against incorrect script (like embers HD for Skyrim) pointing to non existing images

Version 1.6.20
--------------
* Added a protection against null filename when saving a mod

Version 1.6.19
--------------
* Fixed an issue where OBSE version was not detected on steam installs

Version 1.6.18
--------------
* Fixed a fatal error on startup due to debug environment data staying in release version

Version 1.6.17
--------------
* Add an option to load a mod downloaded from TesNexus directly without customization. The dialog can be hidden (default is customization then) through the settings dialog

Version 1.6.16
--------------
* improved handling of misdetected game path

Version 1.6.15
--------------
* Fixed a crash when editing pHUD skyblivion OMOD
* Fixed a case where oblivion version test failed

Version 1.6.14
--------------
* Fixed a case where the BSA version number was incorrect

Version 1.6.13
--------------
* Fixed a case where a mod using FOMOD script would fail to install (an example being Maevan mature skin)

Version 1.6.12
--------------
* Added protection against bad temp folder
* Fixed bug where adding an archive my import game's data folder

Version 1.6.11
--------------
* Fixed CRC support for omodv2 for better conflict handling

version 1.6.10
--------------
* Fixed an incompatibility with BSA archive bits

version 1.6.9
-------------
* Fixed a case where mod load would be skipped

version 1.6.8
-------------
* Fixed another case where mods would not be activated in Skyrim SE

version 1.6.7
-------------
* Fixed a case where mods would not be activated in Skyrim SE

version 1.6.6
-------------
* Fixed a crash when saving a mod

version 1.6.5
-------------
* Fixed Oblivion and Morrowind "download with Mod Manager" issues. Skyrim (+SE) still have issues with nexusmods.com

version 1.6.4
-------------
* Addressed case where game would be mis-identified and you would get the "game not fuond" message

version 1.6.3
-------------
* Addressed case where not specifying game would create a crash

version 1.6.2
-------------
* Changed the way games are detected
* Addressed a few crashes

version 1.6.1
-------------
* Moved crash dump to My Documents\ as tmm_crashdump.log
* Moved initial log to My Documents\ as tmm.log
* Moved game specific log to My Documents <game name>_tmm.log
* Added better handling of cases where game folder is not found

version 1.6.0
-------------
* TMM can now be installed in it's own directory.. It will still create an obmm folder under the game directory for mods related to that game
* new installer
* omodv2 is the default save format
* Added ability to filter file list from BSA browser
* Data file is now written after each mod creation and activation to minimize loss in case of crash
* Fixed several fomod scripting issues
* Fixed an issue with Darnified UI install

version 1.5.14
--------------
Added support for System mod deactivation and conflict detection

version 1.5.13
--------------
Added some protection for ESPM restore
Changed handling of empty FOMOD dependency flags to TRUE

version 1.5.12
--------------
Fixed SKSE auto update

version 1.5.11
--------------
config.txt is accepted for configuration
added a protection for crash when displaying saves' images

version 1.5.10
-------------
Omodv2 will be checked if omod2 is your default format even when editing omodv1
Added better recognition of mod containing a Data folder

version 1.5.9
-------------
Added possibility to store conflict backup files in a separate place to save space in the game folder

version 1.5.8
-------------
Added support for detecting zip root of 'Data'
Added support for other C# FOMOD scripting bases
Added better handling of failed script (do not install mod)
Added better protection against faulty scripts
Fixed a script error when installing Better Cities for Oblivion

version 1.5.7
-------------
Added support for any key pressed during startup to reset window position and avoid the "Tes Mod Manager is in the taskbar but not visible" issue.

version 1.5.6
-------------
Added protection against invisible window
Changed Nexus client version to restore nexus downbload capability

version 1.5.5
-------------
Added a button on Utilities menu to get the obmm.log file
Added protection against corrupt xbt setting file
Added protection against null plugin entry
Enhanced mod rename form

version 1.5.4
-------------
Added LOOT support
Fixed an error when the mods folder was moved
Added logging and protection for cases where the window is displayed outside the screen

version 1.5.3
-------------
Fixed morrowind plugin dependency check
Fixed morrowind BSA registration

version 1.5.2
-------------
Fixed Morrowind load order detection and save

version 1.5.1
-------------
Fixed a morrowind image download issue
Fixed a plugin list display issue

version 1.5.0
-------------
Added preliminary limited support for Morrowind
Fixed a case where files added to a mod were removed from the source

version 1.4.19
--------------
Added support for system mods (ENB, HDT, ...) that are stored directly under the game directory (Skyrim or Oblivion) instead of Data.

version 1.4.18
--------------
Added protection against a misindexed mod

version 1.4.17
--------------
Added mod reactivation based on saved game
Added detailed view to save manager
Added column sorting to save manager
Removed dependency check for skyrim.esm as it always loads

version 1.4.16
--------------
Added protection against slightly incorrect FOMOD scripts that created null reference exceptions
Fixed a case where the path of mod files got corrupted

version 1.4.15
--------------
Added protection against plugins missing dependencies resulting in CTD
Added better updating of FOMOD mods

version 1.4.14
--------------
Fixed a case where portions of a fomod file could be left out

version 1.4.13
--------------
Fixed a case where deep fomod files (extra subdirectory inside archive) were not installed properly

version 1.4.12
--------------
Fixed a case where TMM would fail to retrieve mod info for mods with id of 2 digits
Fixed a case where an omod file could be confused with a BAIN install due to structure

version 1.4.11
--------------
Fixed broken nexus download

version 1.4.10
--------------
Fixed a problem where a readme in an omod2 would not be readable

version 1.4.9
-------------
Adapted to new nexus format for image download

version 1.4.8
-------------
Fixed an issue where Nexus could not be checked for updates

version 1.4.7
-------------
Fixed an issue with the downloads form when mods are no longer available
Fixed an issue where activating a mod would crash
Fixed an issue where a mod file could be mis-decoded

version 1.4.6
-------------
Adjusted protocol for new nexusmods site
Corrected installation issues with latest OBSE

version 1.4.5
-------------
Added some logging when writing the load order

version 1.4.4
-------------
mods can now also be saved with zip or 7z extension based on the setting
It is now possible to chose the default format (omod1 or omod2) in the settings dialog
Fixed a case where some settings were not properly kept (WarnAboutMissingInfo, ShowSimpleOverwriteForm, PreventMovingESPBeforeESM, Omod2IsDefault)

version 1.4.3
-------------
Fixed an XML Fomod script handling case

version 1.4.2
-------------
Fixed a case where the download count would show 1 when no download was actually happening
Added a setting to optionally allow moving an ESP in front of an ESM in the plugin order list

version 1.4.1
-------------
Fixed a fomod import (XPS skeleton)
Added a second download thread
Fixed an omod script problem

version 1.4.0
-------------
Added detection of and support for BAIN packages without having to create an omod
Better keep track of currently selected item to restore list position
Fixed sorting by packed date
Added a "Creation Kit" button to show presence of and start the construction set or creation kit
Added mod name to downloads dialog
Current download can now be stopped or cancelled
Detailed view can be reordered by clicking on the column header (Mod name, author, etc...)
Fixed handling of some XML Fomod flags

version 1.3.31
--------------
Added some C# and XML Fomod script fixes

version 1.3.30
--------------
Cache mod images in mods/info directory for faster image load time
Added some fomod XML script fixes

version 1.3.29
--------------
Added better nexus timeout handling
Enforced home directory after 7z extraction
Tries to find which mod a given esp/esm belongs to
Added a Mod ID column and the ability to sort by Mod ID

version 1.3.28
--------------
Fixed a mod creation issue

version 1.3.27
--------------
Improved handling of download errors and try another server
Added Protection against mis-rooted paths

version 1.3.26
--------------
Fixed the display issue with compression mode in the create mod dialog
Cleaned up and combined utilies/extended utilities menu and settings dialogs
Fixed some incomplete download issues
You can now cancel paused downloads

version 1.3.25
--------------
Added a filter to show only active mods
Added an option to disable the warning when author or description are missing
Added better handling of improperly pathed archives
Moved image fetch to background thread
Restored Nexus Download Manager feature
Added the option of displaying the old file conflict choser at mod creation time. It is still possible to change choice afterwards
Added protections to mod archive folder move
Added text and picture preview for file conflicts

version 1.3.26
--------------
Fixed the display issue with compression mode in the create mod dialog
Cleaned up and combined utilies/extended utilities menu and settings dialogs
Fixed some incomplete download issues
You can now cancel paused downloads

version 1.3.25
--------------
Added a filter to show only active mods
Added an option to disable the warning when author or description are missing
Added better handling of improperly pathed archives
Moved image fetch to background thread
Restored Nexus Download Manager feature
Added the option of displaying the old file conflict choser at mod creation time. It is still possible to change choice afterwards
Added protections to mod archive folder move
Added text and picture preview for file conflicts

version 1.3.24
--------------
Fixed some incorrect mod version detection
Fixed SKSE download
Fixed occasional improper pathing of ESP/ESM when generating a 7z mod

version 1.3.23
--------------
Allows deletion of files associated with a DLC mod

version 1.3.22
--------------
Fixed fomod script handling to support "Island fast travel"
Added better handling of Nexumods login/download issues
Fixed the convert to archive feature
Added some missing files to setup (help file, ...)

version 1.3.21
--------------
Added support for the modified SKSE download page
Added ability to add image/Tes Nexus info to a mod/fomod after creation
Added overwrite all button for conflicts

version 1.3.20
--------------
Fixed an issue with FOMOD scripts trying to copy files to the root
Added a simple and limited download manager. It can be activated by clicking on the download label in the bar at the bottom

version 1.3.19
--------------
Fixed an issue with no root pathed mods (for example, the 7z file contains a Data folder) when no *standard* file is included in the mod (e.g. bodyslide case)

version 1.3.18
--------------
Fixed an issue with no root pathed mods (for example, the 7z file contains a Data folder) when creating a 7z omod2 where the path will be wrong
Fixed an issue where scripts files might be left out of the mod

version 1.3.17
--------------
Fixed an incorrect message about a mod already being present when using the "load" button

version 1.3.16
--------------
Fixed an issue that was slowing down initial load
Added protection against corrupt ESP

version 1.3.15
--------------
Added support for fomod scripts with composite dependencies

version 1.3.14
--------------
Fixed an issue where scripts from .omod (omod1) were ignored

version 1.3.13
--------------
Fixed an issue where scripts where not recognized after editing/saving

version 1.3.12
--------------
Added protection against some malformed fomod scripts
Improved handling of strangely compressed fomods
Fixed 7z support for omod2
Added support for SUM (SkyProc Unified Manager)
Show missing plugins and prevents activating plugins without required dependency

version 1.3.11
--------------
Updated uninstall.bat to make sure that it does not leave files behind
Fixed the "image lost after editing mod" issue
It now detects whether TMM is started as admin or not (Admin rights are needed to register as Nexus Download Manager)
File conflict update speed has been greatly improved
Plugin conflict check speed has been improved

version 1.3.10
--------------
Added support for SkyProc Patchers. They can be started from the extended utilities menu

version 1.3.9
-------------
Setup now detects if user has admin rights
Video files can now be replaced
Existing files from unknown mods will be detected and saved with option of reverting back to them

version 1.3.8
-------------
Fixed zip file extension missing
Fixed corruption on mod edit
Fixed duplicate omod specific files on mod edit

version 1.3.7
-------------
Fixed the file size issues when creating omod2
Fixed BOSS reorder not seen in Oblivion mode
Fixed plugin move up and down behavior
Fixed OBSE version detection
Added file collision detection (config.ini, readme.txt, script.txt) for fallback to old naming (without extensions)

version 1.3.6
-------------
Adds the ability to import multiple files at once
reverts Oblivion mod sorting to timestamp only
Now runs BOSS from it's own directory because it was causing problems for Oblivion
Fixed OBSE install

version 1.3.5
-------------
Confilicted file picker can now open an external viewer (I recommend irfanview and nifskope) to see the file
Fixed an issue where it may not preview a lot of files

version 1.3.4
-------------
Fixed a case where an update would leave some files loaded
Added more tracing to DDS preview to troubleshoot non-working systems

version 1.3.3
-------------
Fixed some conflict detection issues
Fixed some old files removal and upgrade issues
Added SKSE version detection and automated upgrade

version 1.3.2
-------------
Mod conflict detection added when importing a mod
Crash protection added if the devil library cannot load on your system

version 1.3.1
-------------
Detects mods conflict at creation time and allows for update and activation


version 1.3.0
-------------
Contains new conflicted file picker. This lets you chose at any time which mod should win in a file conflict. It also provides (limited) DDS preview.

version 1.2.38
--------------
Adds debug traces and does not remove a missing mod file from the mod list


version 1.2.37
--------------
Added a setting to NOT deactivate mods it thinks are missing (defaults to not deactivate)

version 1.2.36
--------------
Adds a check for the presence of ghosted omod in folder

version 1.2.35
--------------
Added detailed view mode for the mod list
Added some more fomod script compatibility (skyUI...)
Fixed the conflict handling to skip unimportant files (readme, ...)
Added automatic restoration of files from other mods when a conflicting mod is removed (the file from the previously installed mod is restored). 
  Note that a backup of the file is created in the folder so that you can manually chose which file you prefer
Added more logging to track down issues in debug mode

version 1.2.34
--------------
Fixed title and author retrieval from Nexus that was broken by a Nexus update.

version 1.2.33
--------------
mods update check can now be run in background

version 1.2.32
--------------
Added protection against loading invalid files

version 1.2.31
--------------
Added crash protection exit

version 1.2.30
--------------
Sped up file handling a little
Added some protection against bad scripts

version 1.2.29
--------------
Checks for presence of downloadlist.txt to avoid a crash if absent
Select form layout has been changed and it is now always centered to the main window
Added option to name omod2 as zip for 3rd party compatibility (see settings page)

version 1.2.28
--------------
mods that have an update available are displayed in red (check for mods update needs to be run manually at least once though)
now supports fomods that are packaged with a deep structure (a folder inside a folder...)
saves list of mods being downloaded to restart download later (no resume of a broken download)


version 1.2.27
--------------
Added safeguard against wrong fomod script

version 1.2.26
--------------
Fixed fomod file naming issue
Improved field tab order on login dialog

version 1.2.25
--------------
Allows saving Nexus credentials for later and auto-login
Fixed an issue where only 32 groups would save or load
Fixed an issue where TMM would crash if an INI edit was applied by a mod

version 1.2.24
--------------
Fixed load order backup/reload
Fixed mods name for nexus import of fomod

version 1.2.23
--------------
Fixed version decoding of nexus mods

version 1.2.22
--------------
Changed to adapt to new Nexus site

version 1.2.21
--------------
Fixed a case where it would not grab the correct mod id

version 1.2.20
-------------------
Fixed a condition where it could not find BOSS

version 1.2.19
--------------
Fixed a CAS warning
Fixed a version reading issue

version 1.2.18
--------------
Fixed a script issue
Fixed a mod version detection issue
Fixed the OCD editor to handle Skyrim properly
Fixed a mod download issue
Added inactive mods ghosting
Added hidden mods toggler

version 1.2.17
--------------
Fixed a script issue

version 1.2.16
--------------
Fixed an omod2 creation issue (Some of your newly created omod2 may be bad!!!)
Fixed an omod2 activation issue
Fixed some scripting issues
Fixed a GDI crash issue


Version 1.2.15
---------------
Fixed various crashes
Fixed a version detection issue
Fixed fomod detection and import issues
Fixed an omod2 activation issue

version 1.2.14
--------------
Fixed a script issue
Added background downloader and importer for Nexus Mod Manager downloads. Downloads can be queued. They are not "memorized" so do not quit until all downloads are complete. Imports can be done later on though as they are saved to disk.

version 1.2.13
--------------
Fixed a fomod scrip issue (as shown with SMIM)


version 1.2.12
--------------
Fixed scripting for more Fomod files
Fixed some install script errors
Fixed version checking
You can now use non-numeric versioning if you use omod2 format or stay with fomod format
Fixed some Nexus download issues
You can directly load all fomods (unless I missed some..) directly with no decompression recompression step (unless you want to customize the mod of course)


version 1.2.11
--------------
Fomod files can be loaded directly using the load button. They will not go through the omod creation process and will be less advanced than a standard omod but will work
Author name is now retrieved correctly from nexus (no missing last letter)

version 1.2.10
--------------
Fixed a crash

version 1.2.9
-------------
Fixed a case where it was not able to retrieve file info for a mod
Addressed the condition where the modder did not set a version number. It uses the creation date
Checking or unchecking a plugin now updates the loadorder file immediately

version 1.2.8
-------------
Support for the new nexusmod website
Fixed an issue when adding a compressed file to a mod

version 1.2.7
-------------
drag and drop reordering of plugins is now faster
fixed some fomod script handling
added option to disable ghosting of plugins and BSAs
fixed a mod version handling issue
mod names are now trimmed of excess spaces


version 1.2.6
-------------
Handled animated GIF gracefully (only show first image)
Added debug logging for troubleshooting purposes
Drag and drop reordering is possible but without multiselect

version 1.2.5
-------------
Added some error trapping when not able to change timestamps
BSAs of inactive mods are now ghosted when TMM is closed. Any "ghosted" file is automatically deghosted WHILE TMM is running.


version 1.2.4
-------------
Fixed XML fomod script support (Hardcore enhancements now installs correctly)
The BSA Browser will now try to start nifskope when trying to preview a NIF file from a BSA

version 1.2.3
-------------
Enforces timestamp on ESM,ESP and BSA to match load order
Save synchronization also restores mod load order
I fixed an issue where the plugin would jump all over the place when trying to move it.

version 1.2.2
-------------
Improved support for fomod scripts

version 1.2.1
-------------
Fixed an exception when reading non-english mod files

version 1.2.0
-------------
Added support for fomod scripts (you can now import fomod files)
Fixed some load order things
You can now multi select esp/esm when reordering
You can multiselect mods for some actions
Data file analysis fixed
Conflict analysis improved
Plugin detection improved (steam mods, unknown esp/ems, ...)

version 1.1.35
--------------
LoadOrder.txt format is configurable as UTF-8 in the settings dialog

version 1.1.34
--------------
Fixed a setting not sticking
Fixed the tooltip text for mods

version 1.1.33
--------------
Added tooltip text to color coded conflict boxes
Added action log under My document\My Games\<game>\modaction.log
Added an option to disable esp activation using double-click
Fixed esp warning when simly disabling the plugin

version 1.1.32
--------------
Fixed the conflict detector
Improved auto import

version 1.1.31
--------------
Anchored autosort with BOSS button to lower left
Added Nexus Download Manager registration to setup to avoid needing admin rights when starting

version 1.1.30
--------------
Fixed a crash when BOSS is not installed

version 1.1.29
--------------
BOSS can be run from TMM if it is installed (BOSS for Skyrim and Oblivion can be found at http://tes.nexusmods.com/downloads/file.php?id=20516)
Fixed an error decoding mod version

version 1.1.28
--------------
Improved Nexus Download Manager support
Fixed error about ocdlist not found

version 1.1.27
--------------
Added support for 'download with mod manager' button from nexus.
Improved automated import

version 1.1.26
--------------
Saves load order for every reordering

version 1.1.25
--------------
Fixed load order sorting. Tested with Wrye Bash 295.5

version 1.1.24
--------------
Added support for loadorder.txt for BOSS compatibility

version 1.1.23
--------------
Fixed the ghosting of SKyrim.esm and Updates.esm

version 1.1.22
--------------
Added support for Skyrim ordering. TesModManager now fills out the plugins.txt file
 in the order of plugin load. Date changes is still done to maintain compatibility with all versions.


version 1.1.21
--------------
Left hand side esp list panel can now be re-sized
Missing BSA list is corrected automatically

version 1.1.20
--------------
Improved automatic import of a zip or 7z file downloaded from nexus


version 1.1.19
--------------
Fixed a crash when using mods that edit the ini file

version 1.1.18
--------------
Fixed csharp scripting support some more...

version 1.1.17
--------------
Fixed csharp scripting support
Fixed image download support for Oblivion mods
Added skyrim support to save manager

Version 1.1.16
--------------
Changed the way BSA were loaded to make sure they were actually loaded (thanks Hungryriceball)

Version 1.1.15
--------------
Added SKSE support

Version 1.1.14
--------------
* Fixed an overlap between the list of ESP and the description
* CLeaned up some old reference to oblvion.ini

Version 1.1.13
--------------
* Initial release based on OBMM Ex monpetitbeurre edition.
This adds the following to the base OBMM Extended
* fast loading even with 2000 mods
* ability to filter mods displayed based on name
* ability to create a new format of mods (omod2) which is a ZIP file that can be manipulate using standard ZIP tools
* support for the new tesnexus and skyrimnexus for automatic download of mod information

Oblivion Mod Manager Extended can be found at http://www.tesnexus.com/downloads/file.php?id=32277


