param (
    [Parameter(Mandatory = $true)]
    [string] $YourName
)

$userSecretsId = "e95811e3-398f-4274-a092-8f96818d2e76"

dotnet user-secrets set UserConfig:YourName "$YourName" --id "$userSecretsId"
