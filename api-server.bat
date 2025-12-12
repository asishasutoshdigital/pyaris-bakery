@echo off
setlocal enabledelayedexpansion

echo.
echo ========================================
echo  Pyaris Bakery - API Development Server
echo ========================================
echo.

set PROJECT_ROOT=%~dp0

cd /d "%PROJECT_ROOT%Backend\PyarisAPI"

echo Starting ASP.NET Core development server...
echo API URL: http://localhost:5000
echo.

dotnet run

endlocal
