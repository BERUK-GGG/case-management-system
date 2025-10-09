# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["RB_CaseManagement.API/RB_CaseManagement.API.csproj", "RB_CaseManagement.API/"]
COPY ["DataLayer/DataLayer.csproj", "DataLayer/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Affärslager/Affärslager.csproj", "Affärslager/"]

# Restore dependencies
RUN dotnet restore "RB_CaseManagement.API/RB_CaseManagement.API.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR "/src/RB_CaseManagement.API"
RUN dotnet build "RB_CaseManagement.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "RB_CaseManagement.API.csproj" -c Release -o /app/publish

# Use the official .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published application
COPY --from=publish /app/publish .

# Expose port 5000
EXPOSE 5000
EXPOSE 443

# Set the entry point
ENTRYPOINT ["dotnet", "RB_CaseManagement.API.dll"]
