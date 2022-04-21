-- +goose Up
CREATE TABLE IF NOT EXISTS orgs(
   name varchar,
   orgID int,
   PRIMARY KEY(orgID)
);

-- +goose Down
DROP TABLE orgs;
