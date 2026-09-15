# Imagem utilizada para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copia primeiro o csproj para aproveitar cache do Docker
COPY ["api.csproj", "./"]

# Instala/restaura as dependências NuGet
RUN dotnet restore "api.csproj"

# Copia o restante do projeto
COPY . .

# Publica a aplicação
RUN dotnet publish "api.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore


# Imagem final, apenas com o runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "api.dll"]