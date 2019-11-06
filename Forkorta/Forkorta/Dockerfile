FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Forkorta/Forkorta.csproj", "Forkorta/"]
RUN dotnet restore "Forkorta/Forkorta.csproj"
COPY . .
WORKDIR "/src/Forkorta"
RUN dotnet build "Forkorta.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Forkorta.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Forkorta.dll"]