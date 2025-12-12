@echo off
echo.
echo ========================================
echo  Pyaris Bakery - Setup Check
echo ========================================
echo.

REM Check for Node.js
echo Checking for Node.js...
where /q node
if %ERRORLEVEL% neq 0 (
    echo ❌ Node.js not found!
    echo Install from: https://nodejs.org/
    echo.
) else (
    echo ✅ Node.js found
    node --version
    echo.
)

REM Check for npm
echo Checking for npm...
where /q npm
if %ERRORLEVEL% neq 0 (
    echo ❌ npm not found!
) else (
    echo ✅ npm found
    npm --version
    echo.
)

REM Check for .NET
echo Checking for .NET...
where /q dotnet
if %ERRORLEVEL% neq 0 (
    echo ❌ .NET SDK not found!
    echo Install from: https://dotnet.microsoft.com/download
    echo.
) else (
    echo ✅ .NET found
    dotnet --version
    echo.
)

REM Check for git
echo Checking for git...
where /q git
if %ERRORLEVEL% neq 0 (
    echo ⚠️  git not found (optional)
) else (
    echo ✅ git found
    git --version
    echo.
)

echo ========================================
echo  Setup Check Complete
echo ========================================
echo.
echo Next steps:
echo 1. Ensure all required tools are installed
echo 2. Update appsettings.json with database connection
echo 3. Run: build.bat
echo.
