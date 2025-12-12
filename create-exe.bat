@echo off
setlocal enabledelayedexpansion

echo.
echo ========================================
echo  Pyaris Bakery - Create Standalone EXE
echo ========================================
echo.

set PROJECT_ROOT=%~dp0
set BACKEND_DIR=%PROJECT_ROOT%Backend\PyarisAPI
set PUBLISH_DIR=%BACKEND_DIR%\publish
set OUTPUT_DIR=%PROJECT_ROOT%Distribution

REM Check if publish directory exists
if not exist "%PUBLISH_DIR%" (
    echo Publish directory not found. Running build first...
    call "%PROJECT_ROOT%build.bat"
    if !errorlevel! neq 0 (
        echo Build failed
        exit /b 1
    )
)

REM Create output directory
if not exist "%OUTPUT_DIR%" (
    mkdir "%OUTPUT_DIR%"
)

echo Copying files to distribution package...
xcopy "%PUBLISH_DIR%\*" "%OUTPUT_DIR%\" /E /I /Y

echo.
echo Creating startup script...
(
    @echo off
    echo Pyaris Bakery API Server
    echo Starting on http://localhost:5000
    echo Press Ctrl+C to stop
    echo.
    cd /d "%%~dp0"
    PyarisAPI.exe
) > "%OUTPUT_DIR%\START.bat"

echo.
echo Creating README...
(
    echo # Pyaris Bakery - Standalone Application
    echo.
    echo ## How to Run
    echo.
    echo 1. Double-click START.bat
    echo 2. The application will start on http://localhost:5000
    echo 3. Open your web browser and navigate to http://localhost:5000
    echo.
    echo ## Requirements
    echo.
    echo - Windows 7 or later
    echo - .NET Runtime 8.0 (included in this distribution^)
    echo - SQL Server with NODEPOINT database
    echo.
    echo ## Database Configuration
    echo.
    echo Edit the appsettings.json file to configure your database connection.
    echo.
    echo Server: 192.168.0.230
    echo Database: NODEPOINT
    echo Username: pyaris
    echo.
) > "%OUTPUT_DIR%\README.txt"

echo.
echo ========================================
echo  Packaging Complete!
echo ========================================
echo.
echo Distribution files are in: %OUTPUT_DIR%
echo.
echo To create an installer:
echo   1. Install NSIS (https://nsis.sourceforge.io/)
echo   2. Use the generated files to create an installer
echo.

endlocal
