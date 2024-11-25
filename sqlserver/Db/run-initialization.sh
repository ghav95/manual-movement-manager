#!/usr/bin/env bash

LOG_FILE="/usr/src/app/init-log.txt"
SQLCMD="/opt/mssql-tools/bin/sqlcmd"
SA_PASSWORD="BNP@Paribas"

# Function to log errors
log_error() {
    echo "$(date): $1" >> "$LOG_FILE"
    echo "$1"
}

# Wait for SQL Server to start
echo "Checking SQL Server status..." | tee -a "$LOG_FILE"
until $SQLCMD -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" > /dev/null 2>&1; do
    echo "SQL Server is not ready yet. Retrying in 5 seconds..." | tee -a "$LOG_FILE"
    sleep 5
done
echo "SQL Server is up and running!" | tee -a "$LOG_FILE"

# Run the setup script
if $SQLCMD -S localhost -U sa -P "$SA_PASSWORD" -d master -i /usr/src/app/create-database.sql; then
    echo "Database initialization completed successfully." | tee -a "$LOG_FILE"
else
    log_error "Database initialization failed. Check SQL script for errors."
    exit 1
fi

# Run the setup script to create the DB and the schema in the DB
# Note: make sure that your password matches what is in the Dockerfile
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P BNP@Paribas -d master -i create-database.sql