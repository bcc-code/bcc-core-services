-- +goose Up
CREATE TYPE test_type AS (
    street_1 VARCHAR,
    street_2 VARCHAR
);

CREATE TABLE test_table (
    address test_type
);


INSERT INTO test_table (
    address
)
