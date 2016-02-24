param (
    [Parameter()]
    [switch] $IncludeDocumentation,
    [Parameter()]
    [string] $Configuration = "Release"
)

$watch = [System.Diagnostics.Stopwatch]::StartNew()
$scriptPath = Split-Path (Get-Variable MyInvocation).Value.MyCommand.Path 
Set-Location $scriptPath
$destination = "C:\Binaries\MarkR"
$nugetDestination = "C:\Workspaces\Nuget\Developer"

if (Test-Path $destination -PathType Container){
    Remove-Item $destination -Recurse -Force
}

New-Item $destination -ItemType Directory | Out-Null

if (!(Test-Path $nugetDestination -PathType Container)){
    New-Item $nugetDestination -ItemType Directory | Out-Null
}

$build = [Math]::Floor([DateTime]::Now.Subtract([DateTime]::Parse("01/01/2000").Date).TotalDays)
$revision = [Math]::Floor([DateTime]::Now.TimeOfDay.TotalSeconds / 2)

.\IncrementVersion.ps1 -Build $build -Revision $revision

$msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"
& $msbuild "$scriptPath\MarkR.sln" /p:Configuration="$Configuration" /p:Platform="Any CPU" /t:Rebuild /p:VisualStudioVersion=14.0 /v:m /m

Set-Location $scriptPath
Copy-Item MarkR\bin\$Configuration\MarkR.dll $destination

$versionInfo = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$destination\MarkR.dll")
$version = $versionInfo.FileVersion.ToString()

& "NuGet.exe" pack MarkR.nuspec -Prop Configuration="$Configuration" -Version $version
Move-Item "MarkR.$version.nupkg" "$destination" -force
Copy-Item "$destination\MarkR.$version.nupkg" "$nugetDestination" -force

Write-Host
Set-Location $scriptPath
Write-Host "MarkR Deploy: " $watch.Elapsed