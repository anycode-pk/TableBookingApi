ALTER ROLE "TableBookingUser" SET search_path = public;

-- Create Restaurants table
CREATE TABLE IF NOT EXISTS "Restaurants"
(
    "Id"                                         UUID PRIMARY KEY,
    "Name"                                       VARCHAR(64)                                                                                                                               NOT NULL,
    "Type"                                       VARCHAR(100)                                                                                                                              NOT NULL,
    "Description"                                VARCHAR(100)                                                                                                                              NOT NULL,
    "Location"                                   VARCHAR(255)                                                                                                                              NOT NULL,
    "Phone"                                      VARCHAR(32)                                                                                                                               NOT NULL,
    "PrimaryImageUrl"                            VARCHAR(1000) DEFAULT 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png' NOT NULL,
    "SecondaryImageUrl"                          VARCHAR(1000) DEFAULT 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png' NOT NULL,
    "Rating"                                     DOUBLE PRECISION                                                                                                                          NOT NULL,
    "Price"                                      INTEGER                                                                                                                                   NOT NULL,
    "OpeningAndClosingHours_Monday_OpenTime"     INTERVAL,
    "OpeningAndClosingHours_Monday_CloseTime"    INTERVAL,
    "OpeningAndClosingHours_Monday_Closed"       BOOLEAN       DEFAULT FALSE                                                                                                               NOT NULL,
    "OpeningAndClosingHours_Tuesday_OpenTime"    INTERVAL,
    "OpeningAndClosingHours_Tuesday_CloseTime"   INTERVAL,
    "OpeningAndClosingHours_Tuesday_Closed"      BOOLEAN       DEFAULT FALSE                                                                                                               NOT NULL,
    "OpeningAndClosingHours_Wednesday_OpenTime"  INTERVAL,
    "OpeningAndClosingHours_Wednesday_CloseTime" INTERVAL,
    "OpeningAndClosingHours_Wednesday_Closed"    BOOLEAN       DEFAULT FALSE                                                                                                               NOT NULL,
    "OpeningAndClosingHours_Thursday_OpenTime"   INTERVAL,
    "OpeningAndClosingHours_Thursday_CloseTime"  INTERVAL,
    "OpeningAndClosingHours_Thursday_Closed"     BOOLEAN       DEFAULT FALSE                                                                                                               NOT NULL,
    "OpeningAndClosingHours_Friday_OpenTime"     INTERVAL,
    "OpeningAndClosingHours_Friday_CloseTime"    INTERVAL,
    "OpeningAndClosingHours_Friday_Closed"       BOOLEAN       DEFAULT FALSE                                                                                                               NOT NULL,
    "OpeningAndClosingHours_Saturday_OpenTime"   INTERVAL,
    "OpeningAndClosingHours_Saturday_CloseTime"  INTERVAL,
    "OpeningAndClosingHours_Saturday_Closed"     BOOLEAN       DEFAULT FALSE                                                                                                               NOT NULL,
    "OpeningAndClosingHours_Sunday_OpenTime"     INTERVAL,
    "OpeningAndClosingHours_Sunday_CloseTime"    INTERVAL,
    "OpeningAndClosingHours_Sunday_Closed"       BOOLEAN       DEFAULT FALSE                                                                                                               NOT NULL
);

-- seed Restaurants records
INSERT INTO "Restaurants" ("Id", "Name", "Type", "Description", "Location", "Phone",
                           "PrimaryImageUrl", "SecondaryImageUrl", "Rating", "Price",
                           "OpeningAndClosingHours_Monday_OpenTime", "OpeningAndClosingHours_Monday_CloseTime",
                           "OpeningAndClosingHours_Monday_Closed",
                           "OpeningAndClosingHours_Tuesday_OpenTime", "OpeningAndClosingHours_Tuesday_CloseTime",
                           "OpeningAndClosingHours_Tuesday_Closed",
                           "OpeningAndClosingHours_Wednesday_OpenTime", "OpeningAndClosingHours_Wednesday_CloseTime",
                           "OpeningAndClosingHours_Wednesday_Closed",
                           "OpeningAndClosingHours_Thursday_OpenTime", "OpeningAndClosingHours_Thursday_CloseTime",
                           "OpeningAndClosingHours_Thursday_Closed",
                           "OpeningAndClosingHours_Friday_OpenTime", "OpeningAndClosingHours_Friday_CloseTime",
                           "OpeningAndClosingHours_Friday_Closed",
                           "OpeningAndClosingHours_Saturday_OpenTime", "OpeningAndClosingHours_Saturday_CloseTime",
                           "OpeningAndClosingHours_Saturday_Closed",
                           "OpeningAndClosingHours_Sunday_OpenTime", "OpeningAndClosingHours_Sunday_CloseTime",
                           "OpeningAndClosingHours_Sunday_Closed")
VALUES ('a7f7be1c-adae-40df-b315-f772857936d5', 'UNO', 'Pizza', 'Description of UNO.', 'Śniadeckich 10b/2',
        '123-456-789',
        'https://placehold.co/300x200?text=Restaurant', 'https://placehold.co/300x200?text=Restaurant', 5, 1,
        '0 years 0 mons 0 days 8 hours 0 mins 0.0 secs', '0 years 0 mons 0 days 21 hours 30 mins 0.0 secs', FALSE,
        '12:00:00', '21:00:00', FALSE,
        '12:00:00', '21:00:00', FALSE,
        '12:00:00', '21:00:00', FALSE,
        '12:00:00', '21:00:00', FALSE,
        '12:00:00', '21:00:00', FALSE,
        '12:00:00', '21:00:00', FALSE);

-- Create Tables table
CREATE TABLE IF NOT EXISTS "Tables"
(
    "Id"            UUID PRIMARY KEY,
    "NumberOfSeats" INT NOT NULL,
    "RestaurantId"  UUID REFERENCES "Restaurants" ("Id")
);

-- Create Role table
CREATE TABLE IF NOT EXISTS "Roles"
(
    "Id"               UUID PRIMARY KEY,
    "Name"             VARCHAR(30),
    "NormalizedName"   VARCHAR(255),
    "ConcurrencyStamp" VARCHAR(255)
);

-- Create Users table
CREATE TABLE IF NOT EXISTS "Users"
(
    "Id"                     UUID PRIMARY KEY,
    "RefreshToken"           TEXT,
    "RefreshTokenExpiryTime" TIMESTAMPTZ,
    "UserName"               VARCHAR(255),
    "NormalizedUserName"     VARCHAR(255),
    "Email"                  VARCHAR(255),
    "NormalizedEmail"        VARCHAR(255),
    "EmailConfirmed"         BOOLEAN,
    "PasswordHash"           TEXT,
    "SecurityStamp"          TEXT,
    "ConcurrencyStamp"       TEXT,
    "PhoneNumber"            VARCHAR(20),
    "PhoneNumberConfirmed"   BOOLEAN,
    "TwoFactorEnabled"       BOOLEAN,
    "LockoutEnd"             TIMESTAMPTZ,
    "LockoutEnabled"         BOOLEAN,
    "AccessFailedCount"      INT,
    "AppRoleId"              UUID REFERENCES "Roles" ("Id")
);

-- Create Bookings table
CREATE TABLE IF NOT EXISTS "Bookings"
(
    "Id"                UUID PRIMARY KEY,
    "Date"              TIMESTAMPTZ,
    "AmountOfPeople"    INT,
    "DurationInMinutes" INT,
    "AppUserId"         UUID REFERENCES "Users" ("Id"),
    "TableId"           UUID REFERENCES "Tables" ("Id")
);

-- Create Ratings table
CREATE TABLE IF NOT EXISTS "Ratings"
(
    "Id"            UUID PRIMARY KEY,
    "RatingStars"   INT,
    "NumberOfLikes" INT,
    "Comment"       TEXT,
    "DateOfRating"  TIMESTAMPTZ,
    "RestaurantId"  UUID REFERENCES "Restaurants" ("Id"),
    "AppUserId"     UUID REFERENCES "Users" ("Id")
);

-- seed Table records
INSERT INTO "Tables" ("Id", "NumberOfSeats", "RestaurantId")
VALUES ('c29ba544-19be-4cbd-94d0-8d183b7b29af', 1, '123e1a20-6801-4a5e-a327-ecc5cb2bd906');
INSERT INTO "Tables" ("Id", "NumberOfSeats", "RestaurantId")
VALUES ('b3bb2638-b51c-4689-a772-0a154b6afa1c', 2, '123e1a20-6801-4a5e-a327-ecc5cb2bd906');
INSERT INTO "Tables" ("Id", "NumberOfSeats", "RestaurantId")
VALUES ('baafacef-4f85-461a-b6cb-ba887dc54401', 3, '207989de-6d2b-416a-9634-c45870cd9f4f');
INSERT INTO "Tables" ("Id", "NumberOfSeats", "RestaurantId")
VALUES ('a5bf5296-d15b-4e28-89aa-b75bfac13289', 4, '207989de-6d2b-416a-9634-c45870cd9f4f');
INSERT INTO "Tables" ("Id", "NumberOfSeats", "RestaurantId")
VALUES ('3eb2eb68-a47e-47cd-8a22-06c20197a0b3', 4, 'a50c6651-c1b4-497f-b8db-e101da537692');
INSERT INTO "Tables" ("Id", "NumberOfSeats", "RestaurantId")
VALUES ('8deccd28-fdf3-42ec-86f3-9dd7fe743af5', 3, 'a50c6651-c1b4-497f-b8db-e101da537692');
INSERT INTO "Tables" ("Id", "NumberOfSeats", "RestaurantId")
VALUES ('c748ee60-a136-4161-b6ce-60205e311a36', 2, 'bfa8ba14-e4ef-4f13-a4a9-4b1d29d2f8ba');
INSERT INTO "Tables" ("Id", "NumberOfSeats", "RestaurantId")
VALUES ('423d89b6-1d72-479c-87e2-f573c61c611a', 1, 'bfa8ba14-e4ef-4f13-a4a9-4b1d29d2f8ba');
INSERT INTO "Tables" ("Id", "NumberOfSeats", "RestaurantId")
VALUES ('840fffa7-ba45-4f44-a139-d794dcc4c647', 4, 'a7f7be1c-adae-40df-b315-f772857936d5');
INSERT INTO "Tables" ("Id", "NumberOfSeats", "RestaurantId")
VALUES ('d3f5aa07-4803-4008-8fed-7fa6a4de5fe6', 8, 'a7f7be1c-adae-40df-b315-f772857936d5');

--Seed Roles records
INSERT INTO "Roles"("Id", "Name", "NormalizedName", "ConcurrencyStamp")
VALUES ('65329b28-837c-46cb-8f3c-e2ce20a81cac', 'Admin', 'admin', 'xd1');
INSERT INTO "Roles"("Id", "Name", "NormalizedName", "ConcurrencyStamp")
VALUES ('5ad1268f-f61f-4b1c-b690-cbf8c3d35019', 'User', 'user', 'xd2');
INSERT INTO "Roles"("Id", "Name", "NormalizedName", "ConcurrencyStamp")
VALUES ('6e380994-fb58-4364-aeaa-83f7a06e1cc2', 'Restaurant', 'restaurant', 'xd3');

-- seed Users records
INSERT INTO "Users" ("Id",
                     "RefreshToken",
                     "RefreshTokenExpiryTime",
                     "UserName",
                     "NormalizedUserName",
                     "Email",
                     "NormalizedEmail",
                     "EmailConfirmed",
                     "PasswordHash",
                     "SecurityStamp",
                     "ConcurrencyStamp",
                     "PhoneNumber",
                     "PhoneNumberConfirmed",
                     "TwoFactorEnabled",
                     "LockoutEnd",
                     "LockoutEnabled",
                     "AccessFailedCount",
                     "AppRoleId")
VALUES ('abc663f0-08b1-4c52-afe4-1d446b11017f',
        'refreshtoken1',
        '2023-11-12 00:00:00+00'::timestamptz,
        'user uno',
        'normalized user uno name',
        'email1',
        'normalized_email1',
        TRUE,
        'hashpasswd1',
        'securitystamp1',
        'concurrencystamp1',
        '123-456-789',
        FALSE,
        FALSE,
        '2023-10-12 14:30:00+00'::timestamptz,
        TRUE,
        0,
        '65329b28-837c-46cb-8f3c-e2ce20a81cac');

INSERT INTO "Users" ("Id",
                     "RefreshToken",
                     "RefreshTokenExpiryTime",
                     "UserName",
                     "NormalizedUserName",
                     "Email",
                     "NormalizedEmail",
                     "EmailConfirmed",
                     "PasswordHash",
                     "SecurityStamp",
                     "ConcurrencyStamp",
                     "PhoneNumber",
                     "PhoneNumberConfirmed",
                     "TwoFactorEnabled",
                     "LockoutEnd",
                     "LockoutEnabled",
                     "AccessFailedCount",
                     "AppRoleId")
VALUES ('123663f0-08b1-4c52-afe4-1d446b11017f',
        'refreshtoken2',
        '2023-11-12 00:00:00+00'::timestamptz,
        'user nienazarty',
        'normalized user nienazarty name',
        'email2',
        'normalized_email2',
        TRUE,
        'hashpasswd2',
        'securitystamp2',
        'concurrencystamp2',
        '123-333-789',
        TRUE,
        TRUE,
        '2023-10-12 14:30:00+00'::timestamptz,
        TRUE,
        2,
        '5ad1268f-f61f-4b1c-b690-cbf8c3d35019');

-- seed for bookings
-- Booking for Restaurant 'UNO' with TableId '840fffa7-ba45-4f44-a139-d794dcc4c647'
INSERT INTO "Bookings" ("Id", "Date", "AmountOfPeople", "DurationInMinutes", "AppUserId", "TableId", "RestaurantId")
VALUES ('f2a1b111-3456-4c98-9ef1-0b77e65c8761', '2025-02-15 19:00:00+00', 4, 90, 'user-001',
        '840fffa7-ba45-4f44-a139-d794dcc4c647', 'a7f7be1c-adae-40df-b315-f772857936d5');

-- Booking for Restaurant 'Mozaika' with TableId 'c748ee60-a136-4161-b6ce-60205e311a36'
INSERT INTO "Bookings" ("Id", "Date", "AmountOfPeople", "DurationInMinutes", "AppUserId", "TableId", "RestaurantId")
VALUES ('e5a2b222-5678-4abc-aef3-1b88f75d9892', '2025-02-16 13:30:00+00', 2, 60, 'user-002',
        'c748ee60-a136-4161-b6ce-60205e311a36', 'bfa8ba14-e4ef-4f13-a4a9-4b1d29d2f8ba');

-- Booking for Restaurant 'NieNaŻarty' with TableId '3eb2eb68-a47e-47cd-8a22-06c20197a0b3'
INSERT INTO "Bookings" ("Id", "Date", "AmountOfPeople", "DurationInMinutes", "AppUserId", "TableId", "RestaurantId")
VALUES ('c6a3c333-6789-4def-bcf5-2c99e85d9903', '2025-02-17 18:00:00+00', 3, 120, 'user-003',
        '3eb2eb68-a47e-47cd-8a22-06c20197a0b3', 'a50c6651-c1b4-497f-b8db-e101da537692');

-- Booking for Restaurant 'Heaven' with TableId 'baafacef-4f85-461a-b6cb-ba887dc54401'
INSERT INTO "Bookings" ("Id", "Date", "AmountOfPeople", "DurationInMinutes", "AppUserId", "TableId", "RestaurantId")
VALUES ('d7a4d444-7890-4f01-dcf7-3daa065eaa04', '2025-02-18 20:00:00+00', 1, 45, 'user-004',
        'baafacef-4f85-461a-b6cb-ba887dc54401', '207989de-6d2b-416a-9634-c45870cd9f4f');

-- Booking for Restaurant 'Green' with TableId 'c29ba544-19be-4cbd-94d0-8d183b7b29af'
INSERT INTO "Bookings" ("Id", "Date", "AmountOfPeople", "DurationInMinutes", "AppUserId", "TableId", "RestaurantId")
VALUES ('a8a5e555-8901-4f23-ecf9-4ebb165fab05', '2025-02-19 12:00:00+00', 5, 180, 'user-005',
        'c29ba544-19be-4cbd-94d0-8d183b7b29af', '123e1a20-6801-4a5e-a327-ecc5cb2bd906');

-- Seed Users records
INSERT INTO "Users" ("Id", "RefreshToken", "RefreshTokenExpiryTime", "UserName", "NormalizedUserName", "Email",
                     "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp",
                     "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled",
                     "AccessFailedCount", "AppRoleId")
VALUES ('e8213e4a-c336-4345-b93e-26231379acda', 'refreshtoken1', '2023-11-12 00:00:00+00', 'user uno',
        'normalized user uno name', 'email1', 'normalized_email1', FALSE, 'hashpasswd1', 'securitystamp1',
        'concurrencystamp1', '123-456-789', FALSE, TRUE, '2023-10-12 14:30:00+00', TRUE, 0,
        '5ad1268f-f61f-4b1c-b690-cbf8c3d35019');
INSERT INTO "Users" ("Id", "RefreshToken", "RefreshTokenExpiryTime", "UserName", "NormalizedUserName", "Email",
                     "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp",
                     "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled",
                     "AccessFailedCount", "AppRoleId")
VALUES ('c52fd4e3-1e46-4c38-84cc-d686800b425c', 'refreshtoken2', '2023-11-12 00:00:00+00', 'user nienazarty',
        'normalized user nienazarty name', 'email2', 'normalized_email2', TRUE, 'hashpasswd2', 'securitystamp2',
        'concurrencystamp2', '123-333-789', TRUE, FALSE, '2023-10-12 14:30:00+00', TRUE, 2,
        '6e380994-fb58-4364-aeaa-83f7a06e1cc2');

-- seed Ratings records
INSERT INTO "Ratings"("Id", "RatingStars", "NumberOfLikes", "Comment", "DateOfRating", "RestaurantId", "AppUserId")
VALUES ('c55fd83c-7105-11ee-b962-0242ac120002', 4, 2, 'Dobre jedzenie, dobrze robią!', '2023-10-12 14:22:00',
        'a7f7be1c-adae-40df-b315-f772857936d5', 'abc663f0-08b1-4c52-afe4-1d446b11017f');
INSERT INTO "Ratings"("Id", "RatingStars", "NumberOfLikes", "Comment", "DateOfRating", "RestaurantId", "AppUserId")
VALUES ('1e84bcfc-7106-11ee-b962-0242ac120002', 2, 6, 'Mucha w zupie! Ochyda!', '2023-10-24 15:21:00',
        '123e1a20-6801-4a5e-a327-ecc5cb2bd906', '123663f0-08b1-4c52-afe4-1d446b11017f');

-- seed RevokedTokens record for testing
INSERT INTO "RevokedTokens" ("Id", "Token", "RevokedAt")
VALUES ('d6d101b1-e37e-47ab-b9b9-5787f92510b6', 'some_token_string_here', '2025-01-17 22:00:00+00');