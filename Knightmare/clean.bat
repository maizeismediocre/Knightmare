@echo off
echo %cd%
echo "Cleaning project - run when Unity is closed"

rd /s /q Library
rd /s /q Temp
rd /s /q Logs
del /s /q /f *.csproj
del /s /q /f *.pidb
del /s /q /f *.unityproj
del /s /q /f *.DS_Store
del /s /q /f *.sln
del /s /q /f *.userprefs
rd /s /q .vs
rd /s /q obj
del .vsconfig
echo "done."
