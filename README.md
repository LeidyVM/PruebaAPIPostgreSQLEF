# PruebaAPIPostgreSQLEF

Comandos a correr

dotnet tool install --global dotnet-ef <- instala dotnet en dado caso de que no esté isntalado
dotnet ef migrations add fisrtmigration --project PruebaAPIPostgreSQLEF.csproj <- crea las migraciones
dotnet ef database update --project PruebaAPIPostgreSQLEF.csproj <-  crea la base datos (tablas)
dotnet ef database update fisrtmigration --project PruebaAPIPostgreSQLEF.csproj <- actualiza la base de datos de las migraciones


