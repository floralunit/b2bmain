# Используем официальный образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем все файлы и собираем приложение
COPY . ./
RUN dotnet publish -c Release -o out

# Создаем образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 16161
EXPOSE 1616

# Указываем команду для запуска приложения
ENTRYPOINT ["dotnet", "B2BWebService.dll"]
   
