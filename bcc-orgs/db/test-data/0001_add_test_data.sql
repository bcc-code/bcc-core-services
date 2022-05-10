-- +goose Up
INSERT INTO new_org (org_id, name, type, visiting_address)
VALUES 
    (96, 'Terwolde', 'Church', ('Straat', NULL, 'Apeldoorn', 'Gelderland', 'NL', '7322EE', 'Netherlands', 'Nederland')),
    (69, 'Oslo/Follo', 'Church', ('ABCD', 'Straat2', 'Apeldoorn', 'Gelderland', 'NL', 'ABCD', 'Netherlands', 'Nederland')),
    (123, 'BCC Norway', 'Organisation', ('ABCDEF', 'Straat2', 'Apeldoorn', 'ABCD', 'NL', '7322EE', 'Netherlands', 'Nederland')),
    (DEFAULT, 'Xercise', 'Organisation', ('Straat', 'ABCD', 'Apeldoorn', 'Gelderland', 'NL', '7322EE', 'Netherlands', 'Nederland'));

-- INSERT INTO org_association (fk_parent_id, fk_child_id)
-- VALUES 
--     (123, 69),
--     (123, 96);
