#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
COPY ["IS_Projekt.pfx", "/https/aspnetcert.pfx"]
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="!root123456"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetcert.pfx"

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IS_Projekt/IS_Projekt.csproj", "IS_Projekt/"]
RUN dotnet restore "IS_Projekt/IS_Projekt.csproj"
COPY . .
WORKDIR "/src/IS_Projekt"
RUN dotnet dev-certs https --trust
RUN dotnet build "IS_Projekt.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IS_Projekt.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IS_Projekt.dll"]