$files = Get-Item "C:\Binaries\MarkR\*.nupkg"

foreach ($file in $files) {
	& "nuget.exe" push $file.FullName
}