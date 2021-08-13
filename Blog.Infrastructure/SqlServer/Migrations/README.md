# Migration Steps
## Notes
Ensure you are at the application base directory

## move inside SQL Server Folder
dotnet ef migrations add "<FEATURE_NAME>" --output-dir SqlServer\Migrations\ --project .\Blog.Infrastructure\Blog.Infrastructure.csproj --startup-project .\Blog.Server.Api\Blog.Server.Api.csproj

## generate script version to production server
### list migrations generated
dotnet ef migrations list --project .\Blog.Infrastructure\Blog.Infrastructure.csproj --startup-project .\Blog.Server.Api\Blog.Server.Api.csproj

### create a script for this migration
dotnet ef migrations script --output SqlServer\SqlScripts\0001_create_content_table.sql --project .\Blog.Infrastructure\Blog.Infrastructure.csproj --startup-project .\Blog.Server.Api\Blog.Server.Api.csproj