#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ENV PORT 80

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE $PORT

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["IrrigationServer/IrrigationServer.csproj", "IrrigationServer/"]
RUN dotnet restore "IrrigationServer/IrrigationServer.csproj"
COPY . .
WORKDIR "/src/IrrigationServer"
RUN dotnet build "IrrigationServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IrrigationServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet IrrigationServer.dll
