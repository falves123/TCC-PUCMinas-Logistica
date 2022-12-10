# Step 1
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# Step 2
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
RUN echo "Starting copy..."
WORKDIR /
COPY [".", "MyApp/"]
RUN dotnet restore "MyApp/MSNotification.sln"
#COPY . .
WORKDIR "/MyApp"
RUN echo "Build..."
RUN dotnet build "MSNotification.sln" -c Release -o /app/build --no-restore

# Step 3
RUN echo "Publish..."
FROM build AS publish
RUN dotnet publish "MSNotification.sln" -c Release -o /app/publish --no-restore

# Step 4
FROM base AS final
WORKDIR /app
RUN echo "Copy..."
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.dll"]