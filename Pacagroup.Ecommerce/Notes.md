# Deploy SQL Server in Docker
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Pentaho*" -e "MSSQL_PID=Standard" -p 1433:1433 --name sql --hostname sql -d mcr.microsoft.com/mssql/server:2022-latest