CREATE TABLE address (
   address_id int,
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
   org_id int,
   PRIMARY KEY(org_id),
   name varchar,
   legal_name varchar NULL,
   type varchar,
   fk_visiting_address_id int references address (address_id),
   fk_postal_address_id int references address (address_id),
   fk_billing_address_id int references address (address_id)
);

CREATE TABLE org_association (
   parent_org_id int,
   child_org_id int
);