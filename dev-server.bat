@echo off
setlocal enabledelayedexpansion

echo.
echo ========================================
echo  Pyaris Bakery - Development Server
echo ========================================
echo.

set PROJECT_ROOT=%~dp0

REM Start React Dev Server
echo Starting React development server...
echo URL: http://localhost:5173
echo.

cd /d "%PROJECT_ROOT%Frontend\pyaris-app"

if not exist "node_modules" (
    echo Installing dependencies...
    call npm install
)

echo.
echo Starting Vite dev server...
echo.
call npm run dev

endlocal
