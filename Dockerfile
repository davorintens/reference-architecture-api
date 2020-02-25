FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY ./src/Infrastructure.WebApi/bin/Debug/netcoreapp3.1/publish/ .
ENTRYPOINT ["dotnet", "Infrastructure.WebApi.dll"]