@echo off
cd %~dp0
echo Do you want to delete all of TesModManager files?
echo Close command window or press Ctrl+C to abort or
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
del obmm_crashdump.txt
del "c:\documents and settings\%USERNAME%\desktop\TesModManager*"
echo.
echo TMM deleted successfully!
echo.
echo !!!!!!!!!!!!!!!!!!!!!!!!!!!
echo Ready to DELETE the entire obmm directory (including omod files)
echo Close command window to abort or press Ctrl+C
pause
del obmm /S /F /Q
echo.
echo All omod files deleted.
echo press any key to end....
pause

