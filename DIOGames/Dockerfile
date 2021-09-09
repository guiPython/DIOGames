#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DIOGames/DIOGames.csproj", "DIOGames/"]
RUN dotnet restore "DIOGames/DIOGames.csproj"
COPY . .
WORKDIR "/src/DIOGames"
RUN dotnet build "DIOGames.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DIOGames.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DIOGames.dll"]