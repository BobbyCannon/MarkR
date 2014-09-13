param (
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

$build = [Math]::Floor([DateTime]::UtcNow.Subtract([DateTime]::Parse("01/01/2000").Date).TotalDays)
$revision = [Math]::Floor([DateTime]::UtcNow.TimeOfDay.TotalSeconds / 2)

.\IncrementVersion.ps1 MarkR $build $revision

$msbuild = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
cmd /c $msbuild "$scriptPath\MarkR.sln" /p:Configuration="$Configuration" /p:Platform="Any CPU" /t:Rebuild /p:VisualStudioVersion=12.0 /v:m /m

Set-Location $scriptPath

Copy-Item MarkR\bin\$Configuration\MarkR.dll $destination

$versionInfo = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$destination\MarkR.dll")
$version = $versionInfo.FileVersion.ToString()

cmd /c ".nuget\NuGet.exe" pack MarkR.nuspec -Prop Configuration="$Configuration" -Version $version
Move-Item "MarkR.$version.nupkg" "$destination" -force
Copy-Item "$destination\MarkR.$version.nupkg" "$nugetDestination" -force

.\ResetAssemblyInfos.ps1

Write-Host
Set-Location $scriptPath
Write-Host "MarkR Deploy: " $watch.Elapsed