test:
	dotnet restore
	dotnet test

pack:
	dotnet pack BriskDotNet/BriskDotNet.csproj --output nupkgs -c Release
	NUPKG_FILE=$(find nupkgs -mindepth 1 -print -quit)
	echo $NUPKG_FILE