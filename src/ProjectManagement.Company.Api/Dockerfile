FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ProjectManagement.Company.Api/ProjectManagement.Company.Api.csproj", "src/ProjectManagement.Company.Api/"]
RUN dotnet restore "src/ProjectManagement.Company.Api/ProjectManagement.Company.Api.csproj"
COPY . .
WORKDIR "/src/src/ProjectManagement.Company.Api"
RUN dotnet build "ProjectManagement.Company.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectManagement.Company.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectManagement.Company.Api.dll"]
