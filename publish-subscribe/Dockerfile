FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 6000

ENV ASPNETCORE_URLS=http://+:6000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["publish-subscribe/inventory.csproj", "publish-subscribe/"]
RUN dotnet restore "publish-subscribe/inventory.csproj"
COPY . .
WORKDIR "/src/publish-subscribe"
RUN dotnet build "inventory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "inventory.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "inventory.dll"]
