# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY PaySmartly.Archive/*.csproj ./PaySmartly.Archive/
RUN dotnet restore

# copy everything else and build app
COPY PaySmartly.Archive/. ./PaySmartly.Archive/
WORKDIR /source/PaySmartly.Archive
RUN dotnet publish -c release -o /PaySmartly.Archive --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

LABEL author="Stefan Bozov"

WORKDIR /PaySmartly.Archive
COPY --from=build /PaySmartly.Archive ./
ENTRYPOINT ["dotnet", "PaySmartly.Archive.dll"]