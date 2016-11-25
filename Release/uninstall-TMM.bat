@echo off
cd %~dp0
echo Do you want to delete all of TesModManager files?
echo (this will also delete all the mods you created from TMM's archive)
echo Close command window to abort or
pause
del TesModManager.exe
del TesModManager.exe.config
del BaseTools.dll
del BaseTools.xml
del IronMath.dll
del IronPython.dll
del SevenZipSharp.dll
del SevenZipSharp.xml
del devil.dll
del ilu.dll
del msvcp110.dll
del msvcr110.dll
del vccollib110.dll
del obmm.log
dell obmm_crashdump.txt
del "c:\documents and settings\%USERNAME%\desktop\TesModManager*"
echo TMM deleted successfully
echo !!!!!!!!!!!!!!!!!!!!!!!!!!!
echo Ready to DELETE the entire obmm directory (including omod files)
echo Hit Ctrl-C to abort or
pause
del obmm /S /F /Q
