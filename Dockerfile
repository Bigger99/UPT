# Используем официальный образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Клонируем репозиторий с GitHub
ARG REPO_URL=https://github.com/Bigger99/UPT.git
ARG BRANCH=master
RUN git clone -b ${BRANCH} ${REPO_URL} .

# Восстанавливаем зависимости
RUN dotnet restore

# Собираем приложение
RUN dotnet publish -c Debug -o /app/publish

# Используем официальный образ .NET Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Копируем опубликованные файлы из стадии сборки
COPY --from=build /app/publish .

# Указываем точку входа
ENTRYPOINT ["dotnet", "UPT.dll"]
