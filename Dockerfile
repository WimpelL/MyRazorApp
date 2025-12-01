# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копіюємо тільки csproj спочатку, щоб скоротити повторний restore
COPY *.csproj ./
RUN dotnet restore

# Копіюємо весь проект
COPY . ./
RUN dotnet publish -c Release -o out

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Копіюємо зібрані файли
COPY --from=build /app/out .

# Виставляємо порт, на якому Render очікує
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Запуск програми
ENTRYPOINT ["dotnet", "MyRazorApp.dll"]