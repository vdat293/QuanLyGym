@echo off
echo Compiling ViewDatabase.cs...
csc /r:System.Data.Common.dll /r:Microsoft.Data.Sqlite.dll ViewDatabase.cs

if %ERRORLEVEL% EQU 0 (
    echo.
    echo Running...
    ViewDatabase.exe
    del ViewDatabase.exe
) else (
    echo.
    echo Compilation failed! Please run this in Developer Command Prompt for VS.
    pause
)
