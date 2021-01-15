FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY publish/ .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet ProjectManagement.API.dll