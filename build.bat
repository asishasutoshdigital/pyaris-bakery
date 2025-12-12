@echo off
setlocal enabledelayedexpansion

echo.
echo ========================================
echo  Pyaris Bakery Build Script
echo ========================================
echo.

REM Get the directory where this script is located
set SCRIPT_DIR=%~dp0
set PROJECT_ROOT=%SCRIPT_DIR%

echo Building React Frontend...
echo.

cd /d "%PROJECT_ROOT%Frontend\pyaris-app"

if not exist "node_modules" (
    echo Installing npm dependencies...
    call npm install
    if !errorlevel! neq 0 (
        echo Error installing npm dependencies
        exit /b 1
    )
)

echo Building React app...
call npm run build
if !errorlevel! neq 0 (
    echo Error building React app
    exit /b 1
)

echo.
echo Building ASP.NET Core Backend...
echo.

cd /d "%PROJECT_ROOT%Backend\PyarisAPI"

echo Restoring NuGet packages...
call dotnet restore
if !errorlevel! neq 0 (
    echo Error restoring NuGet packages
    exit /b 1
)

echo Publishing ASP.NET Core application...
call dotnet publish -c Release -o "%PROJECT_ROOT%Backend\PyarisAPI\publish" -r win-x64 --self-contained
if !errorlevel! neq 0 (
    echo Error publishing ASP.NET Core app
    exit /b 1
)

echo.
echo ========================================
echo  Build Complete!
echo ========================================
echo.
echo React frontend built to: Frontend\pyaris-app\dist
echo ASP.NET Core backend published to: Backend\PyarisAPI\publish
echo.
echo To run the application:
echo   1. Navigate to: Backend\PyarisAPI\publish
echo   2. Run: PyarisAPI.exe
echo.
echo The application will be available at: http://localhost:5000
echo.

endlocal
