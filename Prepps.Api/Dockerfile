FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Prepps.Api/Prepps.Api.csproj", "Prepps.Api/"]
RUN dotnet restore "Prepps.Api/Prepps.Api.csproj"
COPY . .
WORKDIR "/src/Prepps.Api"
RUN dotnet build "Prepps.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Prepps.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Prepps.Api.dll"]
