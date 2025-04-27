CREATE GROUP "TableBookingGroup";

CREATE USER "TableBookingUser" WITH
    IN GROUP "TableBookingGroup"
    ENCRYPTED PASSWORD 'admin'
    CREATEDB
    SUPERUSER;

CREATE DATABASE "TableBookingDB";

ALTER DATABASE "TableBookingDB" OWNER TO "TableBookingUser";

-- Seed roles:

INSERT INTO "AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
VALUES
    (gen_random_uuid(), 'SuperAdmin', 'SUPERADMIN', gen_random_uuid()),
    (gen_random_uuid(), 'User', 'USER', gen_random_uuid()),
    (gen_random_uuid(), 'Restaurant', 'RESTAURANT', gen_random_uuid());
