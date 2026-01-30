FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# копируем csproj
COPY *.csproj ./
RUN dotnet restore 

# копируем остальной код
COPY . .
RUN dotnet publish  -c Release -o /out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /out .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "HelloApi.dll"]

#FROM mcr.microsoft.com/dotnet/sdk:10.0 AS migrator
#WORKDIR /src
#
#COPY . .
#RUN dotnet tool install --global dotnet-ef
#ENV PATH="$PATH:/root/.dotnet/tools"
#
#RUN dotnet restore
#
## Команду можно переопределять в compose
#ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", "HelloApi", "--startup-project", "HelloApi", "--context", "AppDbContext", "ef", "database", "update", "--project", "HelloApi","--startup-project", "HelloApi", "--context", "AppDbContext"]
