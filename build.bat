@echo off

rem Relative to this file.
set buildDir=./Build
set cakeDir=%buildDir%/Cake
set buildCakeScript=%cakeDir%/Build.cake
set buildPowershellFile=%cakeDir%/Build.ps1

if exist %buildPowershellFile% goto build
echo Can't find %buildPowershellFile%
goto exit 

:build
Powershell.exe -executionpolicy remotesigned -File %buildPowershellFile% -Arguments %1,%2,%3,%4,%5,%6,%7,%8 -Script %buildCakeScript%

:exit