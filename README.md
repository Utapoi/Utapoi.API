# Karaoke.API
API for Utapoi Karaoke

## EF Core - CLI

### Update database
This command is not required since the project automatically updates the database on start.

```powershell
dotnet ef database update --project Karaoke.Infrastructure --startup-project Karaoke.API
```

### Add a migration

```powershell
dotnet ef migrations add "SampleMigration" --project Karaoke.Infrastructure --startup-project Karaoke.API --output-dir Persistence/Migrations
```
