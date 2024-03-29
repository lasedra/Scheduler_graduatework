-- Active: 1697616242142@@127.0.0.1@5432@SchedulerDB

Create table "EVENT_LOG"
(
	"Event_ID" serial NOT NULL,
	"Time" Timestamp without time zone NOT NULL,
	"Level" Varchar NOT NULL,
	"Message" Varchar NOT NULL,
 primary key ("Event_ID")
);

Create table "Schoolyear"
(
	"Schoolyear_ID" uuid default gen_random_uuid() NOT NULL,
	"Years" Varchar UNIQUE,
	"StartDate" Date NOT NULL,
	"EndDate" Date NOT NULL,
primary key ("Schoolyear_ID")
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
	"ClassesNumber" Integer NOT NULL,
 primary key ("Subject_ID")
);

Create table "Tution"
(
	"TutionRow_ID" uuid default gen_random_uuid() NOT NULL,
	"Subject_ID" uuid references "Subject" ("Subject_ID") NOT NULL,
	"Employee_ID" uuid references "Employee" ("Employee_ID") NOT NULL,
	"StartDate" Date NOT NULL,
	"EndDate" Date,
 primary key ("TutionRow_ID")
);

Create table "Cabinet"
(
	"Cabinet_ID" uuid default gen_random_uuid() NOT NULL,
	"Number" Integer NOT NULL UNIQUE,
	"Name" Varchar,
 primary key ("Cabinet_ID")
);

Create table "StudentGroup"
(
	"StudentGroup_ID" uuid default gen_random_uuid() NOT NULL,
	"Code" Varchar NOT NULL UNIQUE,
 primary key ("StudentGroup_ID")
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
	"TimeSlot_ID" uuid default gen_random_uuid() NOT NULL,
	"StartTime" Time NOT NULL,
	"EndTime" Time NOT NULL,
 primary key ("TimeSlot_ID")
);

Create table "DailySchedule_header"
(
	"DailySchedule_header_ID" uuid default gen_random_uuid() NOT NULL,
	"StudentGroup_ID" uuid references "StudentGroup" ("StudentGroup_ID"),
	"ClassesTiming_header_ID" uuid references "ClassesTiming_header" ("ClassesTiming_header_ID") NOT NULL,
	"OfDate" Date NOT NULL, -- Исключить август
	"Schoolyear_ID" uuid references "Schoolyear" ("Schoolyear_ID") NOT NULL,
 primary key ("DailySchedule_header_ID")
);

Create table "DailySchedule_body"
(
		"DailySchedule_header_ID" uuid references "DailySchedule_header" ("DailySchedule_header_ID") NOT NULL,
	"Lesson_ID" uuid default gen_random_uuid() NOT NULL,
	"TimeSlot_ID" uuid references "ClassesTiming_body" ("TimeSlot_ID") NOT NULL,
	"TutionRow_ID" uuid references "Tution" ("TutionRow_ID"),
	"Subject_ID" uuid references "Subject" ("Subject_ID"),
	"Employee_ID" uuid references "Employee" ("Employee_ID"),
	"Cabinet_ID" uuid references "Cabinet" ("Cabinet_ID"),
 primary key ("Lesson_ID")
);

			/* УНИВЕРСАЛЬНЫЙ СЕЛЕКТ */
SELECT
	"Schoolyear"."Years" AS "Schoolyear",
    "DailySchedule_header"."OfDate" AS "Of date",
    "StudentGroup"."Code" AS "Student group",
    "ClassesTiming_header"."Name" AS "Classes timings",
    "ClassesTiming_body"."StartTime" AS "Start time",
    "ClassesTiming_body"."EndTime" AS "End time",
    "Subject"."Name" AS "Subject",
    "Cabinet"."Number" AS "At cabinet",
    "Employee"."Name" AS "Tutor"
FROM "DailySchedule_body"
    LEFT JOIN "DailySchedule_header" ON "DailySchedule_body"."DailySchedule_header_ID" = "DailySchedule_header"."DailySchedule_header_ID"
	LEFT JOIN "Schoolyear" ON "DailySchedule_header"."Schoolyear_ID" = "Schoolyear"."Schoolyear_ID"
    LEFT JOIN "StudentGroup" ON "DailySchedule_header"."StudentGroup_ID" = "StudentGroup"."StudentGroup_ID"
    LEFT JOIN "ClassesTiming_header" ON "DailySchedule_header"."ClassesTiming_header_ID" = "ClassesTiming_header"."ClassesTiming_header_ID"
    LEFT JOIN "ClassesTiming_body" ON "DailySchedule_body"."TimeSlot_ID" = "ClassesTiming_body"."TimeSlot_ID"
    LEFT JOIN "Subject" ON "DailySchedule_body"."Subject_ID" = "Subject"."Subject_ID"
    LEFT JOIN "Cabinet" ON "DailySchedule_body"."Cabinet_ID" = "Cabinet"."Cabinet_ID"
    LEFT JOIN "Employee" ON "DailySchedule_body"."Employee_ID" = "Employee"."Employee_ID"
WHERE
	"DailySchedule_header"."OfDate" BETWEEN '01.01.2020' AND '02.01.2020'
	AND
	"StudentGroup"."Code" = 'В4212';