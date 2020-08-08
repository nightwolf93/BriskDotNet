test:
	dotnet restore
	dotnet test

pack:
	dotnet pack BriskDotNet/BriskDotNet.csproj --output nupkgs