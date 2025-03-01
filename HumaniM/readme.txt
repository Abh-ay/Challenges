#
<--EFCore-->
dotnet tool install --global dotnet-ef
#
<--EFCore dependecy-->
dotnet add package Microsoft.EntityFrameworkCore.Design
#
<--package for postgres it's like sqlclient.server-->
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

#
<--Cmd to create scanffold dbcpntect for Postgres db-->
dotnet ef dbcontext scaffold "DefaultConnection" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Models --context SaleDbContext --force

