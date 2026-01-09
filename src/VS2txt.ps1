$excludedDirs = @(
    "\bin\", "\obj\", "\.git\", "\.vs\",
    "\node_modules\", "\packages\", "\TestResults\",
    "\wwwroot\", "\Views\"
)

$outputFile = "ProyectoCompleto_CS.txt"
Remove-Item $outputFile -ErrorAction SilentlyContinue

Write-Host "Iniciando la exportación de código C#..."

Get-ChildItem -Path . -Recurse -File -Filter "*.cs" |
Where-Object {
    $fullPath = $_.FullName
    -not ($excludedDirs | Where-Object { $fullPath -like "*$_*" })
} |
ForEach-Object {
    "--- Archivo: $($_.FullName) ---" | Out-File $outputFile -Append -Encoding UTF8
    Get-Content $_.FullName -Encoding Default | Out-File $outputFile -Append -Encoding UTF8
    "`n`n" | Out-File $outputFile -Append -Encoding UTF8

    Write-Host "Exportado: $($_.Name)"
}

Write-Host "--------------------------------------------------------"
Write-Host "✅ Exportación Finalizada. Archivo generado: $outputFile"
Read-Host "Presiona Enter para cerrar"
