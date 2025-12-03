# Definición de la lista de directorios a excluir
$excludedDirs = @(
    "bin", "obj", ".git", ".vs", "node_modules", 
    "packages", "TestResults", "ProyectoCompleto.txt", 
    "wwwroot", "Views"
)

# Definición de las extensiones de archivos de código que queremos incluir
# Excluimos .js, .css y otros archivos de frontend que están en otras carpetas
$includedExtensions = @(
    ".cs", ".json", ".xml", ".cshtml", ".csproj", ".sln", 
    ".config", ".bat", ".ps1" 
)

$outputFile = "ProyectoCompleto.txt"

# Elimina el archivo anterior si existe
Remove-Item $outputFile -ErrorAction SilentlyContinue

Write-Host "Iniciando la exportación..."

# 1. Obtener todos los archivos de forma recursiva, aplicando filtros de exclusión.
$filesToExport = Get-ChildItem -Path . -Recurse -File | Where-Object { 
    # Excluir directorios (la ruta no debe contener ninguna de las carpetas excluidas)
    $isExcluded = $false
    foreach ($dir in $excludedDirs) {
        # Usamos -like para verificar si el directorio contiene el nombre de la carpeta excluida
        if ($_.DirectoryName -like "*\$dir*") {
            $isExcluded = $true
            break
        }
    }
    
    # Incluir solo archivos con las extensiones especificadas
    -not $isExcluded -and ($_.Extension -in $includedExtensions)
}

# 2. Procesar cada archivo y agregar su contenido al archivo de salida
foreach ($file in $filesToExport) {
    # Encabezado para identificar el archivo
    "--- Archivo: $($file.FullName) ---" | Out-File $outputFile -Append -Encoding UTF8

    # Contenido del archivo. Usamos -Encoding Default para manejar la mayoría de los archivos de código
    # El uso de Get-Content es robusto para diferentes codificaciones.
    Get-Content $file.FullName -Encoding Default | Out-File $outputFile -Append -Encoding UTF8
    
    "`n`n" | Out-File $outputFile -Append -Encoding UTF8
    Write-Host "Exportado: $($file.Name)"
}

Write-Host "--------------------------------------------------------"
Write-Host "✅ Exportación Finalizada. Archivo generado: $outputFile"
Read-Host "Presiona Enter para cerrar"