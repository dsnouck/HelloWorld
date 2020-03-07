Migrations can be added with
```
dotnet ef migrations add NameOfMigration --startup-project ../HelloWorld.WebApi/HelloWorld.WebApi.csproj
```

The database can be updated with
```
dotnet ef database update --startup-project ../HelloWorld.WebApi/HelloWorld.WebApi.csproj
```

The EF Core tools can be updated with
```
dotnet tool update --global dotnet-ef
```