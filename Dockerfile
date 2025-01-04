# Use the .NET 8.0 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
EXPOSE 80

WORKDIR /source

# Copy and restore dependencies
COPY ./*.fsproj ./
RUN dotnet restore

# Copy source code and build
COPY . .
RUN dotnet publish -c release -o /app --self-contained -r windows-x64

# Use Selenium Standalone Chrome for running
FROM selenium/standalone-chrome
WORKDIR /app

# Copy the built app into the container
COPY --from=build /app .
ENTRYPOINT ["sudo", "./fetch-webpage-console"]  
# previously instead of sudo, it was dotnet
