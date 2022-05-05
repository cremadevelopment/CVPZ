param (
    [Parameter(Mandatory = $true)]
    [string] $MigrationName
)

dotnet ef migrations add $MigrationName `
    -s .\src\CVPZ\CVPZ.csproj `
    -p .\src\CVPZ.Infrastructure\CVPZ.Infrastructure.csproj