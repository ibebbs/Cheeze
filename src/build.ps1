# Build Cheeze!
$solutionDir = Get-Location
dotnet build .\Cheeze.Store\Cheeze.Store.csproj /p:NSwag=true /p:SolutionDir=$solutionDir
dotnet build .\Cheeze.Inventory\Cheeze.Inventory.csproj /p:NSwag=true /p:SolutionDir=$solutionDir
dotnet build .\Cheeze.Store.Client\Cheeze.Store.Client.csproj
dotnet build .\Cheeze.Inventory.Client\Cheeze.Inventory.Client.csproj
dotnet build .\Cheeze.Graph\Cheeze.Graph.csproj