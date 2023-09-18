FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StorageDemo/StorageDemo.csproj", "StorageDemo/"]
RUN dotnet restore "StorageDemo/StorageDemo.csproj"
COPY . .
WORKDIR "/src/StorageDemo"
RUN dotnet build "StorageDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StorageDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StorageDemo.dll"]
