-- +goose Up
INSERT INTO address (street_1, street_2, city, region, country_iso_2_code, postal_code, country_name, country_name_native)
VALUES 
    ('Straat', 'Straat2', 'Apeldoorn', 'Gelderland', 'NL', '7322EE', 'Netherlands', 'Nederland'),
    ('Veien', 'Veien2', 'Ski', 'Nordre Follo', 'NO', '6377NK', 'Norway', 'Norge');

INSERT INTO org (org_id, name, type, fk_visiting_address_id)
VALUES 
    (96, 'Terwolde', 'Church', 1),
    (69, 'Oslo/Follo', 'Church', 2),
    (123, 'BCC Norway', 'Organisation', 2),
    (DEFAULT, 'Xercise', 'Organisation', 2);

INSERT INTO org_association (fk_parent_id, fk_child_id)
VALUES 
    (123, 69),
    (123, 96);
