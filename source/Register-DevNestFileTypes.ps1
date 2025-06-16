# Ensure the script runs as admin
if (-not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole(`
    [Security.Principal.WindowsBuiltInRole] "Administrator")) {
    Write-Warning "⚠️ This script must be run as Administrator!"
    exit
}

# Get the base path where the script is located
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$iconDir = Join-Path $scriptDir "DataNest\Resources\Icons"

# Define file types and settings
$fileTypes = @(
    @{
        Extension = ".dndat"
        TypeName = "devnest.datafile"
        Description = "DevNest Data File"
        Icon = "data.ico"
    },
    @{
        Extension = ".dncont"
        TypeName = "devnest.contentfile"
        Description = "DevNest Content File"
        Icon = "shared.ico"
    },
    @{
        Extension = ".dncfg"
        TypeName = "devnest.configfile"
        Description = "DevNest Config File"
        Icon = "shared.ico"
    },
    @{
        Extension = ".dndash"
        TypeName = "devnest.dashboardfile"
        Description = "DevNest Dashboard File"
        Icon = "dashboard.ico"
    }
)

# Default application to open files
$defaultOpenCommand = "notepad.exe `"%1`""

# Function to register file type
function Register-DevNestFileType {
    param (
        [string]$extension,
        [string]$typeName,
        [string]$description,
        [string]$iconFile
    )

    $iconPath = Join-Path $iconDir $iconFile
    if (-not (Test-Path $iconPath)) {
        Write-Warning "⚠️ Icon file not found: $iconPath"
        return
    }

    $baseKeyPath = "Registry::HKEY_LOCAL_MACHINE\SOFTWARE\Classes"

    # Associate file extension with file type
    New-Item -Path "$baseKeyPath\$extension" -Force | Out-Null
    Set-ItemProperty -Path "$baseKeyPath\$extension" -Name "(default)" -Value $typeName

    # Define the file type with description
    New-Item -Path "$baseKeyPath\$typeName" -Force | Out-Null
    Set-ItemProperty -Path "$baseKeyPath\$typeName" -Name "(default)" -Value $description

    # Set icon
    New-Item -Path "$baseKeyPath\$typeName\DefaultIcon" -Force | Out-Null
    Set-ItemProperty -Path "$baseKeyPath\$typeName\DefaultIcon" -Name "(default)" -Value $iconPath

    # Set open command
    $openCommandKey = "$baseKeyPath\$typeName\shell\open\command"
    New-Item -Path $openCommandKey -Force | Out-Null
    Set-ItemProperty -Path $openCommandKey -Name "(default)" -Value $defaultOpenCommand

    Write-Host "✅ Registered: $extension → $description"
}

# Register all file types
foreach ($fileType in $fileTypes) {
    Register-DevNestFileType -extension $fileType.Extension `
                             -typeName $fileType.TypeName `
                             -description $fileType.Description `
                             -iconFile $fileType.Icon
}
