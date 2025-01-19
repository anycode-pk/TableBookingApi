-- Last time updated: 17.01.2025 
-- dotnet ef migrations script --project "D:\repositories\TableBooking\TableBooking.Model\TableBooking.Model.csproj" --startup-project "D:\repositories\TableBooking\TableBooking.Api\TableBooking.Api.csproj"

CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
                                                       "MigrationId" character varying(150) NOT NULL,
                                                       "ProductVersion" character varying(32) NOT NULL,
                                                       CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "Restaurants" (
                               "Id" uuid NOT NULL,
                               "Name" text NOT NULL,
                               "Type" text NOT NULL,
                               "Description" text,
                               "Location" text NOT NULL,
                               "Phone" text NOT NULL,
                               "PrimaryImageURL" text NOT NULL DEFAULT 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png',
                               "SecondaryImageURL" text NOT NULL DEFAULT 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png',
                               "Rating" double precision NOT NULL,
                               "Price" integer NOT NULL,
                               "OpenTime" timestamp with time zone NOT NULL,
                               "CloseTime" timestamp with time zone NOT NULL,
                               CONSTRAINT "PK_Restaurants" PRIMARY KEY ("Id")
);

CREATE TABLE "Roles" (
                         "Id" uuid NOT NULL,
                         "Name" text,
                         "NormalizedName" text,
                         "ConcurrencyStamp" text,
                         CONSTRAINT "PK_Roles" PRIMARY KEY ("Id")
);

CREATE TABLE "Tables" (
                          "Id" uuid NOT NULL,
                          "NumberOfSeats" integer NOT NULL,
                          "RestaurantId" uuid NOT NULL,
                          CONSTRAINT "PK_Tables" PRIMARY KEY ("Id"),
                          CONSTRAINT "FK_Tables_Restaurants_RestaurantId" FOREIGN KEY ("RestaurantId") REFERENCES "Restaurants" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Users" (
                         "Id" uuid NOT NULL,
                         "RefreshToken" text,
                         "RefreshTokenExpiryTime" timestamp with time zone,
                         "AppRoleId" uuid NOT NULL,
                         "UserName" text,
                         "NormalizedUserName" text,
                         "Email" text,
                         "NormalizedEmail" text,
                         "EmailConfirmed" boolean NOT NULL,
                         "PasswordHash" text,
                         "SecurityStamp" text,
                         "ConcurrencyStamp" text,
                         "PhoneNumber" text,
                         "PhoneNumberConfirmed" boolean NOT NULL,
                         "TwoFactorEnabled" boolean NOT NULL,
                         "LockoutEnd" timestamp with time zone,
                         "LockoutEnabled" boolean NOT NULL,
                         "AccessFailedCount" integer NOT NULL,
                         CONSTRAINT "PK_Users" PRIMARY KEY ("Id"),
                         CONSTRAINT "FK_Users_Roles_AppRoleId" FOREIGN KEY ("AppRoleId") REFERENCES "Roles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Bookings" (
                            "Id" uuid NOT NULL,
                            "Date" timestamp with time zone NOT NULL,
                            "DurationInMinutes" integer NOT NULL,
                            "AmountOfPeople" integer NOT NULL,
                            "AppUserId" uuid NOT NULL,
                            "TableId" uuid NOT NULL,
                            CONSTRAINT "PK_Bookings" PRIMARY KEY ("Id"),
                            CONSTRAINT "FK_Bookings_Tables_TableId" FOREIGN KEY ("TableId") REFERENCES "Tables" ("Id") ON DELETE CASCADE,
                            CONSTRAINT "FK_Bookings_Users_AppUserId" FOREIGN KEY ("AppUserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Ratings" (
                           "Id" uuid NOT NULL,
                           "RatingStars" integer NOT NULL,
                           "NumberOfLikes" integer NOT NULL,
                           "Comment" text NOT NULL,
                           "DateOfRating" timestamp with time zone NOT NULL,
                           "RestaurantId" uuid NOT NULL,
                           "AppUserId" uuid NOT NULL,
                           CONSTRAINT "PK_Ratings" PRIMARY KEY ("Id"),
                           CONSTRAINT "FK_Ratings_Restaurants_RestaurantId" FOREIGN KEY ("RestaurantId") REFERENCES "Restaurants" ("Id") ON DELETE CASCADE,
                           CONSTRAINT "FK_Ratings_Users_AppUserId" FOREIGN KEY ("AppUserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Bookings_AppUserId" ON "Bookings" ("AppUserId");

CREATE INDEX "IX_Bookings_TableId" ON "Bookings" ("TableId");

CREATE INDEX "IX_Ratings_AppUserId" ON "Ratings" ("AppUserId");

CREATE INDEX "IX_Ratings_RestaurantId" ON "Ratings" ("RestaurantId");

CREATE INDEX "IX_Tables_RestaurantId" ON "Tables" ("RestaurantId");

CREATE INDEX "IX_Users_AppRoleId" ON "Users" ("AppRoleId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250116191359_InitialCreate', '9.0.0');

ALTER TABLE "Restaurants" RENAME COLUMN "SecondaryImageURL" TO "SecondaryImageUrl";

ALTER TABLE "Restaurants" RENAME COLUMN "PrimaryImageURL" TO "PrimaryImageUrl";

ALTER TABLE "Users" ALTER COLUMN "RefreshToken" TYPE character varying(512);

ALTER TABLE "Restaurants" ALTER COLUMN "Type" TYPE character varying(100);

ALTER TABLE "Restaurants" ALTER COLUMN "SecondaryImageUrl" TYPE character varying(1000);

ALTER TABLE "Restaurants" ALTER COLUMN "PrimaryImageUrl" TYPE character varying(1000);

ALTER TABLE "Restaurants" ALTER COLUMN "Phone" TYPE character varying(32);

ALTER TABLE "Restaurants" ALTER COLUMN "Name" TYPE character varying(64);

ALTER TABLE "Restaurants" ALTER COLUMN "Location" TYPE character varying(255);

ALTER TABLE "Restaurants" ALTER COLUMN "Description" TYPE character varying(100);
UPDATE "Restaurants" SET "Description" = '' WHERE "Description" IS NULL;
ALTER TABLE "Restaurants" ALTER COLUMN "Description" SET NOT NULL;
ALTER TABLE "Restaurants" ALTER COLUMN "Description" SET DEFAULT '';

ALTER TABLE "Ratings" ALTER COLUMN "Comment" TYPE character varying(500);

CREATE TABLE "RevokedTokens" (
                                 "Id" uuid NOT NULL,
                                 "Token" text NOT NULL,
                                 "RevokedAt" timestamp with time zone NOT NULL,
                                 CONSTRAINT "PK_RevokedTokens" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250117210237_RevokedTokensTable', '9.0.0');

ALTER TABLE "RevokedTokens" ALTER COLUMN "Token" TYPE character varying(512);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250117211021_RevokedTokensTableMaxLength', '9.0.0');

ALTER TABLE "Bookings" ADD "RestaurantId" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250119150709_BookingChanges', '9.0.0');

COMMIT;