#!/bin/bash
set -m
/opt/mssql/bin/sqlservr&
sleep 15s
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P !root123456 -d master -i sql-create-db.sql
echo "DB Ready"
sleep infinity