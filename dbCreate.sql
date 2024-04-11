-- Active: 1697616242142@@127.0.0.1@5432@SchedulerDB
Create table "EVENT_LOG"
(
	"Event_ID" serial NOT NULL,
	"DateTime" Timestamp without time zone NOT NULL,
	"Level" Varchar NOT NULL,
	"Message" Varchar NOT NULL,
 primary key ("Event_ID")
);

Create table "Employee"
(
	"Employee_ID" uuid default gen_random_uuid() NOT NULL,
	"WorkingStatus" Boolean NOT NULL,
	"Name" Varchar NOT NULL,
	"Role" Boolean NOT NULL,
	"Login" Varchar NOT NULL UNIQUE,
	"Password" Varchar NOT NULL,
	"Telegram_ID" Varchar NOT NULL,
	"Phone_Number" Varchar NOT NULL,
	"E-mail" Varchar,
 primary key ("Employee_ID")
);

Create table "Subject"
(
	"Subject_ID" uuid default gen_random_uuid() NOT NULL,
	"Name" Varchar NOT NULL UNIQUE,
	"ClassesNumber" Integer,
 primary key ("Subject_ID")
);

Create table "StudentGroup"
(
	"StudentGroupCode" Varchar NOT NULL,
	"Specialization" Varchar,
 primary key ("StudentGroupCode")
);

Create table "Tution"
(
	"Subject_ID" uuid references "Subject" ("Subject_ID") NOT NULL,
	"Employee_ID" uuid references "Employee" ("Employee_ID") NOT NULL,
	"StartDate" Date NOT NULL,
	"EndDate" Date,
 primary key ("Subject_ID", "Employee_ID")
);

Create table "Studying"
(
	"StudentGroupCode" Varchar references "StudentGroup" ("StudentGroupCode") NOT NULL,
	"Subject_ID" uuid references "Subject" ("Subject_ID") NOT NULL,
	"StartDate" Date,
	"EndDate" Date,
primary key ("Subject_ID", "StudentGroupCode")
);

Create table "Cabinet"
(
	"Number" Varchar NOT NULL,
	"Name" Varchar,
 primary key ("Number")
);

Create table "ClassesTiming_header"
(
	"ClassesTiming_header_ID" uuid default gen_random_uuid() NOT NULL,
	"Name" Varchar NOT NULL UNIQUE,
 primary key ("ClassesTiming_header_ID")
);

Create table "ClassesTiming_body"
(
		"ClassesTiming_header_ID" uuid references "ClassesTiming_header" ("ClassesTiming_header_ID") NOT NULL,
	"ClassNumber" Integer NOT NULL,
	"StartTime" Time NOT NULL,
	"EndTime" Time NOT NULL,
 primary key ("ClassesTiming_header_ID", "ClassNumber")
);

Create table "DailySchedule_header"
(
	"StudentGroupCode" Varchar references "StudentGroup" ("StudentGroupCode") NOT NULL,
	"OfDate" Date NOT NULL,
 primary key ("StudentGroupCode","OfDate")
);

Create table "DailySchedule_body"
(
		"StudentGroupCode" Varchar NOT NULL,
		"OfDate" Date NOT NULL,
	"ClassNumber" Integer NOT NULL,
	"ClassesTiming_header_ID" uuid NOT NULL,
	"Employee_ID" uuid references "Employee" ("Employee_ID"),
	"Subject_ID" uuid references "Subject" ("Subject_ID"),
	"CabinetNumber" Varchar references "Cabinet" ("Number"),
 primary key ("StudentGroupCode","OfDate","ClassNumber"),
 foreign key ("StudentGroupCode", "OfDate") references "DailySchedule_header" ("StudentGroupCode", "OfDate"),
 foreign key ("ClassNumber", "ClassesTiming_header_ID") references "ClassesTiming_body" ("ClassNumber", "ClassesTiming_header_ID")
);

		/* УНИВЕРСАЛЬНЫЙ СЕЛЕКТ */
SELECT
    "DailySchedule_body"."OfDate" AS "Of date",
	to_char("DailySchedule_body"."OfDate", 'Day') AS "Day of week",
    "DailySchedule_body"."StudentGroupCode" AS "Student group",
	"ClassesTiming_header"."Name" as "Timing name",
    "DailySchedule_body"."ClassNumber" AS "Class number",
	"Employee"."Name" as "Tutor",
	"Subject"."Name" as "Subject",
	"DailySchedule_body"."CabinetNumber" as "At cabinet"
FROM "DailySchedule_body"
    LEFT JOIN "ClassesTiming_header" ON "DailySchedule_body"."ClassesTiming_header_ID" = "ClassesTiming_header"."ClassesTiming_header_ID"
	LEFT JOIN "Employee" ON "DailySchedule_body"."Employee_ID" = "Employee"."Employee_ID"
    LEFT JOIN "Subject" ON "DailySchedule_body"."Subject_ID" = "Subject"."Subject_ID"
WHERE
	"OfDate" BETWEEN '2022-07-09' AND '2022-07-09'
	AND
	"StudentGroupCode" = '1312';

SELECT * FROM "Employee" WHERE "Employee"."Employee_ID" = 
	(SELECT "Employee_ID" FROM "Tution" WHERE )