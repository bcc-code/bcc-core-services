-- +goose Up
CREATE TYPE address_type AS (
    street_1 varchar,
    street_2 varchar,
    city varchar,
    region varchar,
    country_iso_2_code varchar,
    postal_code varchar,
    country_name varchar,
    country_name_native varchar
);

CREATE TABLE new_org (
    org_id SERIAL,
    PRIMARY KEY(org_id),
    name varchar NOT NULL,
    legal_name varchar,
    type varchar NOT NULL,
    visiting_address address_type,
    postal_address address_type,
    billing_address address_type
);

-- +goose Down
DROP TABLE new_org;
DROP TYPE address_type;
