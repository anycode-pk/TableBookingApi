ALTER ROLE "TableBookingUser" SET search_path = public;

-- Create Restaurants table
CREATE TABLE IF NOT EXISTS "Restaurants" (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "CloseTime" TIMESTAMP NOT NULL,
    "Description" TEXT,
    "Location" VARCHAR(255) NOT NULL,
    "OpenTime" TIMESTAMP NOT NULL,
    "Type" VARCHAR(255),
    "PrimaryImageURL" TEXT NOT NULL,
    "SecondaryImageURL" TEXT NOT NULL,
    "Price" INT,
    "Rating" DOUBLE PRECISION,
    "Phone" VARCHAR(20)
);

-- Create Tables table
CREATE TABLE IF NOT EXISTS "Tables" (
    "Id" UUID PRIMARY KEY,
    "NumberOfSeats" INT NOT NULL,
    "RestaurantId" UUID REFERENCES "Restaurants"("Id")
);

-- Create Role table
CREATE TABLE IF NOT EXISTS "Roles" (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(30),
    "NormalizedName" VARCHAR(255),
    "ConcurrencyStamp" VARCHAR(255)
);

-- Create Users table
CREATE TABLE IF NOT EXISTS "Users" (
    "Id" UUID PRIMARY KEY,
    "RefreshToken" TEXT,
    "RefreshTokenExpiryTime" TIMESTAMPTZ,
    "UserName" VARCHAR(255),
    "NormalizedUserName" VARCHAR(255),
    "Email" VARCHAR(255),
    "NormalizedEmail" VARCHAR(255),
    "EmailConfirmed" BOOLEAN,
    "PasswordHash" TEXT,
    "SecurityStamp" TEXT,
    "ConcurrencyStamp" TEXT,
    "PhoneNumber" VARCHAR(20),
    "PhoneNumberConfirmed" BOOLEAN,
    "TwoFactorEnabled" BOOLEAN,
    "LockoutEnd" TIMESTAMPTZ,
    "LockoutEnabled" BOOLEAN,
    "AccessFailedCount" INT,
    "AppRoleId" UUID REFERENCES "Roles"("Id")
);

-- Create Bookings table
CREATE TABLE IF NOT EXISTS "Bookings" (
    "Id" UUID PRIMARY KEY,
    "Date" TIMESTAMPTZ,
    "AmountOfPeople" INT,
    "DurationInMinutes" INT,
    "AppUserId" UUID REFERENCES "Users"("Id"),
    "TableId" UUID REFERENCES "Tables"("Id")
);

-- Create Ratings table
CREATE TABLE IF NOT EXISTS "Ratings" (
    "Id" UUID PRIMARY KEY,
    "RatingStars" INT,
    "NumberOfLikes" INT,
    "Comment" TEXT,
    "DateOfRating" TIMESTAMPTZ,
    "RestaurantId" UUID REFERENCES "Restaurants"("Id"),
    "AppUserId" UUID REFERENCES "Users"("Id")
);

-- seed Restaurants records
INSERT INTO "Restaurants" ("Id","Name", "CloseTime", "Description", "Location", "OpenTime", "Type", "PrimaryImageUrl","SecondaryImageUrl", "Price", "Rating", "Phone")
VALUES ('a7f7be1c-adae-40df-b315-f772857936d5', 'UNO', '2023-10-26 21:00:00', 'Description of UNO.', 'Śniadeckich 10b/2', '2023-10-26 12:00:00', 'Pizza', 'https://placehold.co/300x200?text=Restaurant','https://placehold.co/300x200?text=Restaurant', 1, 5, 123-456-789);
INSERT INTO "Restaurants" ("Id","Name", "CloseTime", "Description", "Location", "OpenTime", "Type", "PrimaryImageUrl","SecondaryImageUrl", "Price", "Rating", "Phone")
VALUES ('bfa8ba14-e4ef-4f13-a4a9-4b1d29d2f8ba','Mozaika', '2023-10-26 22:15:00', 'Description of Mozaika restaurant.', 'Ratuszowska 10', '2023-10-26 15:00:00', 'Generic restaurant', 'https://placehold.co/300x200?text=Restaurant','https://placehold.co/300x200?text=Restaurant', 2, 4, 41-334-219);
INSERT INTO "Restaurants" ("Id","Name", "CloseTime", "Description", "Location", "OpenTime", "Type", "PrimaryImageUrl","SecondaryImageUrl", "Price", "Rating", "Phone")
VALUES ('a50c6651-c1b4-497f-b8db-e101da537692','NieNaŻarty', '2023-10-27 00:15:00', 'Description of NieNaŻarty.', 'Zwycięstwa 20/2', '2023-10-26 13:30:00', 'Burger', 'https://placehold.co/300x200?text=Restaurant','https://placehold.co/300x200?text=Restaurant', 0, 3, 432-123-543);
INSERT INTO "Restaurants" ("Id","Name", "CloseTime", "Description", "Location", "OpenTime", "Type", "PrimaryImageUrl","SecondaryImageUrl", "Price", "Rating", "Phone")
VALUES ('207989de-6d2b-416a-9634-c45870cd9f4f','Heaven', '2023-10-26 20:00:00', 'Description of Heaven.', 'Staszewskiego 2', '2023-10-26 12:00:00', 'Pizza', 'https://placehold.co/300x200?text=Restaurant','https://placehold.co/300x200?text=Restaurant', 0, 2, 164-231-324);
INSERT INTO "Restaurants" ("Id","Name", "CloseTime", "Description", "Location", "OpenTime", "Type", "PrimaryImageUrl","SecondaryImageUrl", "Price", "Rating", "Phone")
VALUES ('123e1a20-6801-4a5e-a327-ecc5cb2bd906','Green', '2023-10-26 24:00:00', 'Description of Green restaurant.', 'Fałata 5/5', '2023-10-26 14:30:00', 'Generic restaurant', 'https://placehold.co/300x200?text=Restaurant','https://placehold.co/300x200?text=Restaurant', 1, 1, 357-877-667);

-- seed Table records
INSERT INTO "Tables" ("Id","NumberOfSeats", "RestaurantId") VALUES ('c29ba544-19be-4cbd-94d0-8d183b7b29af',1, '123e1a20-6801-4a5e-a327-ecc5cb2bd906');
INSERT INTO "Tables" ("Id","NumberOfSeats", "RestaurantId") VALUES ('b3bb2638-b51c-4689-a772-0a154b6afa1c',2, '123e1a20-6801-4a5e-a327-ecc5cb2bd906');
INSERT INTO "Tables" ("Id","NumberOfSeats", "RestaurantId") VALUES ('baafacef-4f85-461a-b6cb-ba887dc54401',3, '207989de-6d2b-416a-9634-c45870cd9f4f');
INSERT INTO "Tables" ("Id","NumberOfSeats", "RestaurantId") VALUES ('a5bf5296-d15b-4e28-89aa-b75bfac13289',4, '207989de-6d2b-416a-9634-c45870cd9f4f');
INSERT INTO "Tables" ("Id","NumberOfSeats", "RestaurantId") VALUES ('3eb2eb68-a47e-47cd-8a22-06c20197a0b3',4, 'a50c6651-c1b4-497f-b8db-e101da537692');
INSERT INTO "Tables" ("Id","NumberOfSeats", "RestaurantId") VALUES ('8deccd28-fdf3-42ec-86f3-9dd7fe743af5',3, 'a50c6651-c1b4-497f-b8db-e101da537692');
INSERT INTO "Tables" ("Id","NumberOfSeats", "RestaurantId") VALUES ('c748ee60-a136-4161-b6ce-60205e311a36',2, 'bfa8ba14-e4ef-4f13-a4a9-4b1d29d2f8ba');
INSERT INTO "Tables" ("Id","NumberOfSeats", "RestaurantId") VALUES ('423d89b6-1d72-479c-87e2-f573c61c611a',1, 'bfa8ba14-e4ef-4f13-a4a9-4b1d29d2f8ba');
INSERT INTO "Tables" ("Id","NumberOfSeats", "RestaurantId") VALUES ('840fffa7-ba45-4f44-a139-d794dcc4c647',4, 'a7f7be1c-adae-40df-b315-f772857936d5');
INSERT INTO "Tables" ("Id","NumberOfSeats", "RestaurantId") VALUES ('d3f5aa07-4803-4008-8fed-7fa6a4de5fe6',8, 'a7f7be1c-adae-40df-b315-f772857936d5');

--Seed Roles records
INSERT INTO "Roles"(
	"Id", "Name", "NormalizedName", "ConcurrencyStamp")
	VALUES ('65329b28-837c-46cb-8f3c-e2ce20a81cac', 'Admin', 'admin', 'xd1');
INSERT INTO "Roles"(
    "Id", "Name", "NormalizedName", "ConcurrencyStamp")
    VALUES ('5ad1268f-f61f-4b1c-b690-cbf8c3d35019', 'User', 'user', 'xd2');
INSERT INTO "Roles"(
    "Id", "Name", "NormalizedName", "ConcurrencyStamp")
    VALUES ('6e380994-fb58-4364-aeaa-83f7a06e1cc2', 'Restaurant', 'restaurant', 'xd3');

-- seed Users records
INSERT INTO "Users" (
    "Id",
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
    "AppRoleId"
) VALUES (
    'abc663f0-08b1-4c52-afe4-1d446b11017f',
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
    '65329b28-837c-46cb-8f3c-e2ce20a81cac'
);

INSERT INTO "Users" (
    "Id",
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
    "AppRoleId"
) VALUES (
    '123663f0-08b1-4c52-afe4-1d446b11017f',
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
    '5ad1268f-f61f-4b1c-b690-cbf8c3d35019'
);

-- seed for bookings
-- Booking for Restaurant 'UNO' with TableId '840fffa7-ba45-4f44-a139-d794dcc4c647'
INSERT INTO "Bookings" ("Id", "Date", "AmountOfPeople", "DurationInMinutes", "AppUserId", "TableId", "RestaurantId")
VALUES ('f2a1b111-3456-4c98-9ef1-0b77e65c8761', '2025-02-15 19:00:00+00', 4, 90, 'user-001', '840fffa7-ba45-4f44-a139-d794dcc4c647', 'a7f7be1c-adae-40df-b315-f772857936d5');

-- Booking for Restaurant 'Mozaika' with TableId 'c748ee60-a136-4161-b6ce-60205e311a36'
INSERT INTO "Bookings" ("Id", "Date", "AmountOfPeople", "DurationInMinutes", "AppUserId", "TableId", "RestaurantId")
VALUES ('e5a2b222-5678-4abc-aef3-1b88f75d9892', '2025-02-16 13:30:00+00', 2, 60, 'user-002', 'c748ee60-a136-4161-b6ce-60205e311a36', 'bfa8ba14-e4ef-4f13-a4a9-4b1d29d2f8ba');

-- Booking for Restaurant 'NieNaŻarty' with TableId '3eb2eb68-a47e-47cd-8a22-06c20197a0b3'
INSERT INTO "Bookings" ("Id", "Date", "AmountOfPeople", "DurationInMinutes", "AppUserId", "TableId", "RestaurantId")
VALUES ('c6a3c333-6789-4def-bcf5-2c99e85d9903', '2025-02-17 18:00:00+00', 3, 120, 'user-003', '3eb2eb68-a47e-47cd-8a22-06c20197a0b3', 'a50c6651-c1b4-497f-b8db-e101da537692');

-- Booking for Restaurant 'Heaven' with TableId 'baafacef-4f85-461a-b6cb-ba887dc54401'
INSERT INTO "Bookings" ("Id", "Date", "AmountOfPeople", "DurationInMinutes", "AppUserId", "TableId", "RestaurantId")
VALUES ('d7a4d444-7890-4f01-dcf7-3daa065eaa04', '2025-02-18 20:00:00+00', 1, 45, 'user-004', 'baafacef-4f85-461a-b6cb-ba887dc54401', '207989de-6d2b-416a-9634-c45870cd9f4f');

-- Booking for Restaurant 'Green' with TableId 'c29ba544-19be-4cbd-94d0-8d183b7b29af'
INSERT INTO "Bookings" ("Id", "Date", "AmountOfPeople", "DurationInMinutes", "AppUserId", "TableId", "RestaurantId")
VALUES ('a8a5e555-8901-4f23-ecf9-4ebb165fab05', '2025-02-19 12:00:00+00', 5, 180, 'user-005', 'c29ba544-19be-4cbd-94d0-8d183b7b29af', '123e1a20-6801-4a5e-a327-ecc5cb2bd906');

-- Seed Users records
INSERT INTO "Users" ("Id", "RefreshToken", "RefreshTokenExpiryTime", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount", "AppRoleId")
VALUES ('e8213e4a-c336-4345-b93e-26231379acda', 'refreshtoken1', '2023-11-12 00:00:00+00', 'user uno', 'normalized user uno name', 'email1', 'normalized_email1', FALSE, 'hashpasswd1', 'securitystamp1', 'concurrencystamp1', '123-456-789', FALSE, TRUE, '2023-10-12 14:30:00+00', TRUE, 0, '5ad1268f-f61f-4b1c-b690-cbf8c3d35019');
INSERT INTO "Users" ("Id", "RefreshToken", "RefreshTokenExpiryTime", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount", "AppRoleId")
VALUES ('c52fd4e3-1e46-4c38-84cc-d686800b425c', 'refreshtoken2', '2023-11-12 00:00:00+00', 'user nienazarty', 'normalized user nienazarty name', 'email2', 'normalized_email2', TRUE, 'hashpasswd2', 'securitystamp2', 'concurrencystamp2', '123-333-789', TRUE, FALSE, '2023-10-12 14:30:00+00', TRUE, 2, '6e380994-fb58-4364-aeaa-83f7a06e1cc2');

-- seed Ratings records
INSERT INTO "Ratings"(
	"Id", "RatingStars", "NumberOfLikes", "Comment", "DateOfRating", "RestaurantId", "AppUserId")
	VALUES ('c55fd83c-7105-11ee-b962-0242ac120002', 4, 2, 'Dobre jedzenie, dobrze robią!', '2023-10-12 14:22:00', 'a7f7be1c-adae-40df-b315-f772857936d5', 'abc663f0-08b1-4c52-afe4-1d446b11017f');
INSERT INTO "Ratings"(
	"Id", "RatingStars", "NumberOfLikes", "Comment", "DateOfRating", "RestaurantId", "AppUserId")
	VALUES ('1e84bcfc-7106-11ee-b962-0242ac120002', 2, 6, 'Mucha w zupie! Ochyda!', '2023-10-24 15:21:00', '123e1a20-6801-4a5e-a327-ecc5cb2bd906', '123663f0-08b1-4c52-afe4-1d446b11017f');

-- seed RevokedTokens record for testing
INSERT INTO "RevokedTokens" ("Id", "Token", "RevokedAt")
VALUES ('d6d101b1-e37e-47ab-b9b9-5787f92510b6', 'some_token_string_here', '2025-01-17 22:00:00+00');