FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY ../../packages/Huntask.Common/ ./packages/Huntask.Common/
COPY Huntask.Identity.Service.sln .
COPY src/Huntask.Identity.Service/*.csproj ./src/Huntask.Identity.Service/
COPY src/Huntask.Identity.Service.Presentation/*.csproj ./src/Huntask.Identity.Service.Presentation/
RUN dotnet restore Huntask.Identity.Service.sln

COPY src/Huntask.Identity.Service/ ./src/Huntask.Identity.Service/
COPY src/Huntask.Identity.Service.Presentation/ ./src/Huntask.Identity.Service.Presentation/
RUN dotnet publish src/Huntask.Identity.Service.Presentation/Huntask.Identity.Service.Presentation.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

RUN apt-get update \
  && apt-get install -y --no-install-recommends curl \
  && rm -rf /var/lib/apt/lists/*

EXPOSE 8080

ENTRYPOINT ["dotnet", "Huntask.Identity.Service.Presentation.dll"]