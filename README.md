<div align=center>
<h1>Utapoi - Karaoke API</h1>
Karaoke.API is developed using .NET 7 and ASP.NET Core. This repository contains the backend code for Karaoke.Web project.
</div>
<br>

<div align="center">
  <a href="https://dotnet.microsoft.com/">
    <img src="https://img.shields.io/badge/.NET-7-512BD4?style=for-the-badge&logo=.net&labelColor=1f1f1f" alt=".NET">
  </a>
  <a href="https://dotnet.microsoft.com/apps/aspnet">
    <img src="https://img.shields.io/badge/ASP.NET%20Core-7-512BD4?style=for-the-badge&logo=asp.net&labelColor=1f1f1f" alt="ASP.NET Core">
  </a>

  <a href="https://github.com/Utapoi/Karaoke.API/blob/main/LICENSE">
    <img src="https://img.shields.io/github/license/Utapoi/Karaoke.API?style=for-the-badge&labelColor=1f1f1f&color=B91C1C" alt="License">
  </a>
  <a href="https://github.com/Utapoi/Karaoke.API/actions">
    <img src="https://img.shields.io/github/actions/workflow/status/Utapoi/Karaoke.API/dotnet.yml?style=for-the-badge&logo=github&labelColor=1f1f1f&color=047857" alt="Build">
  </a>
  <a href="https://github.com/Utapoi/Karaoke.API/releases">
    <img src="https://img.shields.io/github/release/Utapoi/Karaoke.API?style=for-the-badge&labelColor=1f1f1f&color=B91C1C" alt="GitHub release">
  </a>
</div>

<br><br>

## Local Development Setup

To set up the project for local development, follow these steps:

1. Clone the repository:

```shell
git clone https://github.com/Utapoi/Karaoke.API.git
```

2. Navigate to the project directory:

```shell
cd Karaoke.API
```

3. Initialize `dotnet secrets`:

```shell
dotnet user-secrets init
```

4. Add the necessary secrets for OAuth2 Google authentication. Replace `YOUR_CLIENT_ID` and `YOUR_CLIENT_SECRET` with your actual values:

```shell
dotnet user-secrets set "GoogleOptions:GoogleAuthOptions:ClientId" "YOUR_CLIENT_ID"
dotnet user-secrets set "GoogleOptions:GoogleAuthOptions:ClientSecret" "YOUR_CLIENT_SECRET"
```

5. If required, configure the `appsettings.development.json` file with the following information:

```json
{
  "UseInMemoryDatabase": true,
  "ServerOptions": {
    "BaseUrl": "https://localhost:7215/",
    "FileStoragePath": "/files"
  },
  "SecurityOptions": {
    "AdminOptions": {
      "AllowedEmails": [
        ...
      ]
    },
    "JwtOptions": {
      "Key": "Development",
      "TokenExpirationInMinutes": 5,
      "RefreshTokenExpirationInDays": 15,
      "ValidAudience": "https://localhost:3000",
      "ValidIssuer": "https://localhost:7215"
    }
  },
  "GoogleOptions": {
    "GoogleAuthOptions": {
      "RedirectUrl": "https://localhost:7215/",
      "WebClientUrl": "https://localhost:3000/"
    }
  }
}
```

Make sure to replace `...` with the actual configuration values.

6. Run the application using the `dotnet run` command:

```shell
dotnet run
```

The application will start running on `https://localhost:7215/`.

<br>

## EF Core

### Update database
This command is not required since the project automatically updates the database on start.

```sh
dotnet ef database update --project Karaoke.Infrastructure --startup-project Karaoke.API
```

### Add a migration

```sh
dotnet ef migrations add "SampleMigration" --project Karaoke.Infrastructure --startup-project Karaoke.API --output-dir Persistence/Migrations
```
<br>

## Contributing

We welcome contributions to the Karaoke.API project. To contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Commit your changes and push the branch to your fork.
4. Submit a pull request, explaining your changes and referencing any relevant issues.

Please ensure that your code adheres to the project's coding standards and includes appropriate unit tests.

<br>

## License

This project is licensed under the GNU General Public License v3.0 (GPL-3.0). For more information, please refer to the [LICENSE](LICENSE) file.

