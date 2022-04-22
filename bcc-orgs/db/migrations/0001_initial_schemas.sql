-- +goose Up
CREATE TABLE address (
   address_id SERIAL,
   PRIMARY KEY(address_id),
   street_1 varchar,
   street_2 varchar,
   city varchar,
   region varchar,
   country_iso_2_code varchar,
   postal_code varchar,
   country_name varchar,
   country_name_native varchar
);

CREATE TABLE org (
   org_id SERIAL,
   PRIMARY KEY(org_id),
   name varchar NOT NULL,
   legal_name varchar,
   type varchar NOT NULL,
   fk_visiting_address_id int REFERENCES address (address_id),
   fk_postal_address_id int REFERENCES address (address_id),
   fk_billing_address_id int REFERENCES address (address_id)
);

CREATE TABLE org_association (
   association_id SERIAL,
   PRIMARY KEY(association_id),
   fk_parent_id int REFERENCES org (org_id) NOT NULL,
   fk_child_id int REFERENCES org (org_id) NOT NULL
);

-- +goose Down
DROP TABLE org_association;
DROP TABLE org;
DROP TABLE address;

