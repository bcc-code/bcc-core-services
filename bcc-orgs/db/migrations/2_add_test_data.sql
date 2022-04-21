INSERT INTO address (addressID, street1, street2, city, region, countryIso2Code, postalCode, countryName, countryNameNative)
VALUES (1, 'Street', 'Street2', 'Apeldoorn', 'Gelderland', 'NL', '7322EE', 'Netherlands', 'Nederland');

INSERT INTO org (name, orgID, fk_visitingAddressID)
VALUES ('Terwolde', 96, 1), ('Oslo/Follo', 69, NULL);