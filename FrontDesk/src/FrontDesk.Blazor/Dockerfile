#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /app

COPY FrontDesk/*.sln .
COPY . .
WORKDIR /app/FrontDesk/src/FrontDesk.Blazor
RUN dotnet restore

RUN dotnet build "FrontDesk.Blazor.csproj" -c Release -o /build

FROM build AS publish
RUN dotnet publish "FrontDesk.Blazor.csproj" -c Release -o /publish

FROM nginx:alpine AS final

WORKDIR /usr/share/nginx/html

COPY --from=publish /publish/wwwroot /usr/local/webapp/nginx/html
COPY --from=build /build/nginx.conf /etc/nginx/nginx.conf