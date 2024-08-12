# Use the official .NET image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PasswordManagerApi.csproj", "./"]
RUN dotnet restore "./PasswordManagerApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "YourProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YourProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Copy the certificate file
COPY path/to/your/certificate.pfx /app/certificate.pfx
ENTRYPOINT ["dotnet", "PasswordManagerApi.dll"]