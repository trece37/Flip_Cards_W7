@echo off
echo =====================================
echo   FLIP_CARDS-W7 QUICK COMPILE
echo   Trece37 Tech Solutions
echo =====================================
echo.

echo Compilando proyecto...
dotnet build --configuration Release

if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Fallo la compilacion
    pause
    exit /b 1
)

echo.
echo =====================================
echo   COMPILACION EXITOSA
echo =====================================
echo.
echo Ejecutable en: bin\Release\net6.0-windows\Flip_Cards_W7.exe
echo.
pause
