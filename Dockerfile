# Use the official .NET 10 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY SchoolConnect.EducationSystem.sln ./
COPY src/SchoolConnect.EducationSystem.Domain/SchoolConnect.EducationSystem.Domain.csproj ./src/SchoolConnect.EducationSystem.Domain/
COPY src/SchoolConnect.EducationSystem.Application/SchoolConnect.EducationSystem.Application.csproj ./src/SchoolConnect.EducationSystem.Application/
COPY src/SchoolConnect.EducationSystem.Infrastructure/SchoolConnect.EducationSystem.Infrastructure.csproj ./src/SchoolConnect.EducationSystem.Infrastructure/
COPY src/SchoolConnect.EducationSystem.Api/SchoolConnect.EducationSystem.Api.csproj ./src/SchoolConnect.EducationSystem.Api/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR /src/src/SchoolConnect.EducationSystem.Api
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 10 runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose ports
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "SchoolConnect.EducationSystem.Api.dll"]
