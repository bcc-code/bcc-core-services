INSERT INTO address (address_id, street_1, street_2, city, region, country_iso_2_code, postal_code, country_name, country_name_native)
VALUES (1, 'Street', 'Street2', 'Apeldoorn', 'Gelderland', 'NL', '7322EE', 'Netherlands', 'Nederland');

INSERT INTO org (name, org_id, fk_visiting_address_id)
VALUES ('Terwolde', 96, 1), ('Oslo/Follo', 69, NULL);