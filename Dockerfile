FROM microsoft/aspnetcore
WORKDIR /app
COPY ./publish .
ENTRYPOINT ["dotnet", "Infrastructure.WebApi.dll"]