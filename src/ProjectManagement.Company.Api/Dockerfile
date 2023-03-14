FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["company-api/src/ProjectManagement.Company.Api/ProjectManagement.Company.Api.csproj", "company-api/src/ProjectManagement.Company.Api/"]
RUN dotnet restore "company-api/src/ProjectManagement.Company.Api/ProjectManagement.Company.Api.csproj"
COPY . .
WORKDIR "/src/company-api/src/ProjectManagement.Company.Api"
RUN dotnet build "ProjectManagement.Company.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectManagement.Company.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectManagement.Company.Api.dll"]
