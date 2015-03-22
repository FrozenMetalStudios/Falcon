@echo off
@echo -------------------------------------------
@echo Preparing
@echo -------------------------------------------
@echo Deleting logs.
del Logs\*.* /F /Q /S
rd Logs /Q /S
del *.txt /F /Q
del *.log /F /Q
del *.build /F /Q

@echo Cleaning build directories.
del Platforms\*.* /F /Q /S
rd Platforms /Q /S
@echo Finished Cleaning.
pause