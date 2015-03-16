@echo off
@echo -----------------------------------------------------------------------
@echo  Make Builds
@echo -----------------------------------------------------------------------
@echo Start Time:
time /t
@echo.

@echo -------------------------------------------
@echo Preparing
@echo -------------------------------------------
@echo Deleting logs.
del Logs\*.* /F /Q /S
rd Logs /Q /S
md Logs
del *.txt /F /Q
del *.log /F /Q
del *.build /F /Q

@echo Cleaning build directories.
del Platforms\*.* /F /Q /S
rd Platforms /Q /S
md Platforms
@echo.

@echo -------------------------------------------
@echo Build Platforms
@echo -------------------------------------------
@echo Win32 Debug
unity -nographics -batchmode -quit -logFile .\Logs\Build_Win32_Debug.log -ProjectPath %~dp0..\Project -executeMethod BuildTools.BuildWin32 -buildConfig Debug
@echo Win32 Release
unity -nographics -batchmode -quit -logFile .\Logs\Build_Win32_Release.log -ProjectPath %~dp0..\Project -executeMethod BuildTools.BuildWin32 -buildConfig Release
@echo Builds Finished.
pause