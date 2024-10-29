FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UPT/UPT.csproj", "UPT/"]
RUN dotnet restore "UPT/UPT.csproj"
COPY . .
WORKDIR "/src/UPT"
RUN dotnet build "UPT.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "UPT.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UPT.dll"]