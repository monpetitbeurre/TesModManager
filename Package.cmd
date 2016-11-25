@echo off
title Packager
echo Moving release to manual...
copy /Y Release\*.exe Manual > nul
copy /Y Release\*.dll Manual > nul
copy /Y Release\*.txt Manual > nul
echo Done.
echo Moving archives out of manual...
if exist "Manual\*.zip" move Manual\*.zip Zip
if exist "Manual\*.7z" move Manual\*.7z Zip
echo Done.
set Manual=%CD%\Manual
cd Install
set /p "ver=Version>"
echo Generating installer...
TMMSetup -generate "%Manual%" "Tes Mod Manager %ver%" data.dat
echo Generating report...
TMMSetup -report data.dat report.txt
TMMSetup -embed Data.resx data data.dat Interop.IWshRuntimeLibrary Interop.IWshRuntimeLibrary.dll
copy /Y Data.resx ..\OBMMExInstaller\Data.resx
echo Recompile installer to update with new files.
echo Finished.
pause > nul