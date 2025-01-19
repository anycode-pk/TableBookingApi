CREATE GROUP "TableBookingGroup";

CREATE USER "TableBookingUser" WITH
    IN GROUP "TableBookingGroup"
    ENCRYPTED PASSWORD 'admin'
    CREATEDB
    SUPERUSER;

CREATE DATABASE "TableBookingDB";

ALTER DATABASE "TableBookingDB" OWNER TO "TableBookingUser";