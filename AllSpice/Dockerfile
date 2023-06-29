FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY *.csproj ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
COPY . ./
RUN dotnet publish -c Release -o out

# Start runtime image
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build-env /app/out .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet AllSpice.dll
