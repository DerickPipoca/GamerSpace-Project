FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia arquivos de projeto para restaurar dependências
COPY Backend/GamerSpace.Domain/GamerSpace.Domain.csproj Backend/GamerSpace.Domain/
COPY Backend/GamerSpace.Application/GamerSpace.Application.csproj Backend/GamerSpace.Application/
COPY Backend/GamerSpace.Infrastructure/GamerSpace.Infrastructure.csproj Backend/GamerSpace.Infrastructure/
COPY Backend/GamerSpace.API/GamerSpace.API.csproj Backend/GamerSpace.API/

RUN dotnet restore Backend/GamerSpace.API/GamerSpace.API.csproj

# Copia o restante do código e publica
COPY Backend/ Backend/
WORKDIR /src/Backend/GamerSpace.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GamerSpace.API.dll"]

