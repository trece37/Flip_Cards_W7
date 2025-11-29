# Flip_Cards_W7 Build Script
# Trece37 Tech Solutions

Write-Host "=================================" -ForegroundColor Green
Write-Host "  FLIP_CARDS-W7 BUILD SCRIPT" -ForegroundColor Green
Write-Host "  Trece37 Tech Solutions" -ForegroundColor Green
Write-Host "=================================" -ForegroundColor Green
Write-Host ""

# Check .NET SDK
Write-Host "[1/4] Verificando .NET SDK..." -ForegroundColor Cyan
$dotnetVersion = dotnet --version
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: .NET SDK no encontrado. Instala .NET 6 SDK o superior." -ForegroundColor Red
    Write-Host "Descarga desde: https://dotnet.microsoft.com/download" -ForegroundColor Yellow
    exit 1
}
Write-Host "✓ .NET SDK encontrado: $dotnetVersion" -ForegroundColor Green
Write-Host ""

# Restore dependencies
Write-Host "[2/4] Restaurando dependencias..." -ForegroundColor Cyan
dotnet restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Fallo al restaurar dependencias." -ForegroundColor Red
    exit 1
}
Write-Host "✓ Dependencias restauradas" -ForegroundColor Green
Write-Host ""

# Build Release
Write-Host "[3/4] Compilando proyecto (Release)..." -ForegroundColor Cyan
dotnet build --configuration Release --no-restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Fallo la compilación." -ForegroundColor Red
    exit 1
}
Write-Host "✓ Compilación exitosa" -ForegroundColor Green
Write-Host ""

# Publish
Write-Host "[4/4] Publicando ejecutable..." -ForegroundColor Cyan
dotnet publish --configuration Release --output ./publish --no-build
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Fallo la publicación." -ForegroundColor Red
    exit 1
}
Write-Host "✓ Ejecutable publicado en ./publish/" -ForegroundColor Green
Write-Host ""

Write-Host "=================================" -ForegroundColor Green
Write-Host "  ✓ BUILD COMPLETADO" -ForegroundColor Green
Write-Host "=================================" -ForegroundColor Green
Write-Host ""
Write-Host "Ejecutable disponible en:" -ForegroundColor Cyan
Write-Host "  ./publish/Flip_Cards_W7.exe" -ForegroundColor Yellow
Write-Host ""
Write-Host "Para ejecutar:" -ForegroundColor Cyan
Write-Host "  .\publish\Flip_Cards_W7.exe" -ForegroundColor Yellow
Write-Host ""
Write-Host "Powered by Trece37 & Perply 💚" -ForegroundColor Green
