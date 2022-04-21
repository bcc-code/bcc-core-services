INSERT INTO address (address_id, street_1, street_2, city, region, country_iso_2_code, postal_code, country_name, country_name_native)
VALUES 
    (1, 'Straat', 'Straat2', 'Apeldoorn', 'Gelderland', 'NL', '7322EE', 'Netherlands', 'Nederland'),
    (2, 'Veien', 'Veien2', 'Ski', 'Nordre Follo', 'NO', '6377NK', 'Norway', 'Norge');

INSERT INTO org (name, type, org_id, fk_visiting_address_id)
VALUES 
    ('Terwolde', 'Church', 96, 1),
    ('Oslo/Follo', 'Church', 69, 2),
    ('BCC Norway', 'Organisation', 123, 2);

INSERT INTO org_association (association_id, fk_parent_id, fk_child_id)
VALUES 
    (1, 123, 69),
    (2, 123, 96);