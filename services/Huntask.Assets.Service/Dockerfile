FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY ../../packages/Huntask.Common/ ./packages/Huntask.Common/
COPY Huntask.Assets.Service.sln .
COPY src/Huntask.Assets.Service/*.csproj ./src/Huntask.Assets.Service/
COPY src/Huntask.Assets.Service.Presentation/*.csproj ./src/Huntask.Assets.Service.Presentation/
RUN dotnet restore Huntask.Assets.Service.sln

COPY src/Huntask.Assets.Service/ ./src/Huntask.Assets.Service/
COPY src/Huntask.Assets.Service.Presentation/ ./src/Huntask.Assets.Service.Presentation/
RUN dotnet publish src/Huntask.Assets.Service.Presentation/Huntask.Assets.Service.Presentation.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "Huntask.Assets.Service.Presentation.dll"]