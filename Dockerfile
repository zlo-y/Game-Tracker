FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "WebAPI/WebAPI.csproj"

COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]