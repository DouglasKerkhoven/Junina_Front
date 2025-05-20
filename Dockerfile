FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "curitibano.microservico.junina.csproj"
RUN dotnet build "curitibano.microservico.junina.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "curitibano.microservico.junina.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "curitibano.microservico.junina.dll"]
