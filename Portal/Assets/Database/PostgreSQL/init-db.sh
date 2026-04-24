#!/bin/bash
# mPortal CMS — PostgreSQL Database Initialization
# Creates the database, applies schema, and applies baseline seed data.
#
# Usage:
#   ./init-db.sh                          # defaults: localhost / postgres / mPortal
#   ./init-db.sh myhost myuser mydb       # custom host, user, database
#   PG_PASSWORD=secret ./init-db.sh       # set password via environment
#   MPORTAL_APPLY_SEED=0 ./init-db.sh     # skip baseline seed
#   MPORTAL_APPLY_TEST_FIXTURES=0 ./init-db.sh  # skip integration fixtures
#
# Prerequisites:
#   - PostgreSQL client tools (psql) installed
#   - PostgreSQL server running and accessible

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

PG_HOST="${1:-${PGHOST:-localhost}}"
PG_USER="${2:-${PGUSER:-postgres}}"
PG_DB="${3:-${PGDATABASE:-mPortal}}"
PG_PORT="${PGPORT:-5432}"
APPLY_SEED="${MPORTAL_APPLY_SEED:-1}"
APPLY_TEST_FIXTURES="${MPORTAL_APPLY_TEST_FIXTURES:-1}"

echo "=== mPortal CMS — PostgreSQL Database Initialization ==="
echo "Host: $PG_HOST:$PG_PORT"
echo "User: $PG_USER"
echo "Database: $PG_DB"
echo ""

# Create database if it doesn't exist
echo "Creating database '$PG_DB' (if not exists)..."
psql -h "$PG_HOST" -p "$PG_PORT" -U "$PG_USER" -tc \
  "SELECT 1 FROM pg_database WHERE datname = '$PG_DB'" | grep -q 1 || \
  psql -h "$PG_HOST" -p "$PG_PORT" -U "$PG_USER" -c "CREATE DATABASE \"$PG_DB\""

# Apply schema
echo "Applying schema (121 tables)..."
psql -h "$PG_HOST" -p "$PG_PORT" -U "$PG_USER" -d "$PG_DB" -f "$SCRIPT_DIR/schema.sql"
psql -h "$PG_HOST" -p "$PG_PORT" -U "$PG_USER" -d "$PG_DB" -f "$SCRIPT_DIR/schema-integration.sql"
psql -h "$PG_HOST" -p "$PG_PORT" -U "$PG_USER" -d "$PG_DB" -f "$SCRIPT_DIR/schema-biblereader.sql"

if [[ "$APPLY_SEED" == "1" ]]; then
  echo "Applying baseline seed (seed-data.sql)..."
  psql -h "$PG_HOST" -p "$PG_PORT" -U "$PG_USER" -d "$PG_DB" -f "$SCRIPT_DIR/seed-data.sql"
else
  echo "Skipping baseline seed (MPORTAL_APPLY_SEED=$APPLY_SEED)."
fi

if [[ "$APPLY_TEST_FIXTURES" == "1" ]]; then
  echo "Applying integration fixtures (seed-test-fixtures.sql)..."
  psql -h "$PG_HOST" -p "$PG_PORT" -U "$PG_USER" -d "$PG_DB" -f "$SCRIPT_DIR/seed-test-fixtures.sql"
else
  echo "Skipping integration fixtures (MPORTAL_APPLY_TEST_FIXTURES=$APPLY_TEST_FIXTURES)."
fi

echo ""
echo "=== Done. Database '$PG_DB' is ready. ==="
echo "Connection string for appsettings.json:"
echo "  Host=$PG_HOST;Port=$PG_PORT;Database=$PG_DB;Username=$PG_USER;Password=YOUR_PASSWORD;"
