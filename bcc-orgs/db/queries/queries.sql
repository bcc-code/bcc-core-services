
-- name: GetNewOrgs :one
    SELECT 
        org_id,
        (visiting_address).street_1
    FROM new_org;


-- name: GetOrgs :one
    SELECT o.*, a::INT as va
    FROM org as o LEFT JOIN address as a ON o.visiting_address_id = a.address_id;


-- -- name: UpdateAddress :one
--     UPDATE address
--         SET street_1 = $2, 
--             street_2 = $3, 
--             city = $4, 
--             region = $5, 
--             country_iso_2_code = $6, 
--             postal_code = $7, 
--             country_name = $8, 
--             country_name_native = $9
--         WHERE address_id = $1
--         RETURNING address_id;

-- -- name: CreateOrg :one
--     INSERT INTO org (
--         name,
--         legal_name,
--         type,
--         fk_visiting_address_id,
--         fk_postal_address_id,
--         fk_billing_address_id
--     ) VALUES ($1, $2, $3, $4, $5, $6)
--     RETURNING org_id;

-- -- name: UpdateOrg :one
--     UPDATE org 
--         SET name = $2, 
--             legal_name = $3, 
--             type = $4
--         WHERE org_id = $1
--     RETURNING org_id;

-- -- name: GetOrgs :many
-- 	SELECT
-- 		o.*,
-- 		va.street_1 AS "va__street_1",
-- 		va.street_2 AS "va__street_2",
-- 		va.city AS "va__city",
-- 		va.region AS "va__region",
-- 		va.country_iso_2_code AS "va__country_iso_2_code",
-- 		va.postal_code AS "va__postal_code",
-- 		va.country_name AS "va__country_name",
-- 		va.country_name_native AS "va__country_name_native",
-- 		pa.street_1 AS "pa__street_1",
-- 		pa.street_2 AS "pa__street_2",
-- 		pa.city AS "pa__city",
-- 		pa.region AS "pa__region",
-- 		pa.country_iso_2_code AS "pa__country_iso_2_code",
-- 		pa.postal_code AS "pa__postal_code",
-- 		pa.country_name AS "pa__country_name",
-- 		pa.country_name_native AS "pa__country_name_native",
-- 		ba.street_1 AS "ba__street_1",
-- 		ba.street_2 AS "ba__street_2",
-- 		ba.city AS "ba__city",
-- 		ba.region AS "ba__region",
-- 		ba.country_iso_2_code AS "ba__country_iso_2_code",
-- 		ba.postal_code AS "ba__postal_code",
-- 		ba.country_name AS "ba__country_name",
-- 		ba.country_name_native AS "ba__country_name_native"
-- 	FROM org AS o
-- 		LEFT JOIN address AS va ON va.address_id = o.fk_visiting_address_id
-- 		LEFT JOIN address AS pa ON pa.address_id = o.fk_postal_address_id
-- 		LEFT JOIN address AS ba ON ba.address_id = o.fk_billing_address_id;
