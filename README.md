# Api Rest .NET (core) Movies

# Configure database

- in Program.cs, uncomment the right line begining by //MEMORY, //MARIADB, //POSTGRESQL, //SQLSERVER
- in appsettings.json, replace URL by the right connection string

# Connection string examples
- InMemory : no connection string needed
- Mariadb with user/password authentication: server=localhost;user id=movie;password=password;database=dbmovie
- Postgresql with user/password authentication: Host=localhost;Username=movie;Password=password;Database=dbmovie
- Sqlserver with os authentication: Data Source=localhost;Initial Catalog=dbmovie;Integrated Security=True


