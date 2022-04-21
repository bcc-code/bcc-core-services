CREATE TABLE address (
   addressID int,
   PRIMARY KEY(addressID),
   street1 varchar,
   street2 varchar,
   city varchar,
   region varchar,
   countryIso2Code varchar,
   postalCode varchar,
   countryName varchar,
   countryNameNative varchar
);

CREATE TABLE org (
   orgID int,
   PRIMARY KEY(orgID),
   name varchar,
   legalName varchar NULL,
   type varchar,
   fk_visitingAddressID int references address (addressID),
   fk_postalAddressID int references address (addressID),
   fk_billingAddressID int references address (addressID)
);