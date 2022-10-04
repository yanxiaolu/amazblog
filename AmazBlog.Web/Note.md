# mssqlServer Docker startup script

docker run --name sqlExpress \
-e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=P@ssw0rd' \
-e 'MSSQL_PID=Express' -p 1433:1433 \
-e 'TZ=Asia/Shanghai' \
-v /Users/yanxl/Downloads/dbData:/var/opt/mssql \
-d mcr.microsoft.com/mssql/server


, b => b.MigrationsAssembly("AmazBlog.Web")