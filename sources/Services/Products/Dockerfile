FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app

#Exibir a versão do .NET
RUN dotnet --version

# Copiar csproj e restaurar dependencias
COPY Services/Products/Products.API/*.csproj ./
RUN dotnet restore

# Build da aplicacao
COPY Common/. ./
COPY Services/Products/Products.API/. ./
RUN dotnet publish Products.API.csproj -c Release -o out

# Build da imagem
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Products.API.dll"]