#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/nightly/sdk:5.0.100-rc.1-buster-slim AS build
WORKDIR /app

COPY FrontDesk/src/FrontDesk.Blazor ./FrontDesk.Blazor
COPY FrontDesk/src/BlazorShared ./BlazorShared
RUN dotnet restore /app/FrontDesk.Blazor/FrontDesk.Blazor.csproj
#COPY . .

WORKDIR /app/FrontDesk.Blazor
RUN dotnet build -c Release -o /bin/Release/net5.0

FROM build AS publish
RUN dotnet publish -c Release -r linux-x64 -o /bin/Release/net5.0/browser-wasm/publish /p:LinkDuringPublish=true

#RUN dotnet exec "C:\Program Files\dotnet\sdk\5.0.100-rc.1.20452.10\Sdks\Microsoft.NET.Sdk.Razor\build\netstandard2.0\..\..\tools\rzc.dll" rewritecss
#RUN dotnet "C:\Program Files\dotnet\sdk\5.0.100-rc.1.20452.10\Sdks\Microsoft.NET.Sdk.BlazorWebAssembly\tools\net5.0\Microsoft.NET.Sdk.BlazorWebAssembly.Tools.dll" brotli -s /obj/Release/net5.0/blazor.publish.boot.json -o /obj/Release/net5.0/compress/96E2h3iK.br

#COPY --from=build /build/FrontDesk.Blazor.StaticWebAssets.xml /publish/wwwroot/FrontDesk.Blazor.StaticWebAssets.xml
#COPY --from=build /build/ref/FrontDesk.Blazor.dll /publish/wwwroot/_framework/FrontDesk.Blazor.dll
#COPY --from=build /build/wwwroot/_framework/ /publish/wwwroot/_framework/

FROM nginx:alpine AS final

WORKDIR /usr/share/nginx/html

COPY --from=publish /bin/Release/net5.0/browser-wasm/publish/wwwroot /usr/local/webapp/nginx/html
COPY --from=build /bin/Release/net5.0/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80