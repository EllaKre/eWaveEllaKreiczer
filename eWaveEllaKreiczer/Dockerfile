# שלב הבנייה
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# העתק את כל קבצי הפרויקט
COPY . ./

# ודא שהקובץ csproj קיים ובצע restore
RUN dotnet restore

# הרץ build
RUN dotnet publish -c Release -o out

# שלב ההרצה
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# פתח פורט 80
EXPOSE 80

# הפעל את האפליקציה
ENTRYPOINT ["dotnet", "eWaveEllaKreiczer.dll"]

COPY NuGet.Config /root/.nuget/NuGet/NuGet.Config