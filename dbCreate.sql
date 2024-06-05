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
	"IsTelegramConfirmed" Boolean NOT NULL,
	"Role" Boolean NOT NULL,
	"Name" Varchar NOT NULL,
	"Login" Varchar NOT NULL UNIQUE,
	"Password" Varchar NOT NULL,
	"Phone_Number" Varchar NOT NULL UNIQUE,
	"E-mail" Varchar,
 primary key ("Employee_ID")
);

Create table "Subject"
(
	"Subject_ID" uuid default gen_random_uuid() NOT NULL,
	"Name" Varchar NOT NULL UNIQUE,
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
	"StudentGroupCode" Varchar references "StudentGroup" ("StudentGroupCode") 
		ON UPDATE CASCADE ON DELETE CASCADE NOT NULL,
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
	"StudentGroupCode" Varchar references "StudentGroup" ("StudentGroupCode") 
		ON UPDATE CASCADE ON DELETE CASCADE NOT NULL,
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
 foreign key ("StudentGroupCode", "OfDate") references "DailySchedule_header" ("StudentGroupCode", "OfDate") 
 	ON UPDATE CASCADE ON DELETE CASCADE,
 foreign key ("ClassNumber", "ClassesTiming_header_ID") references "ClassesTiming_body" ("ClassNumber", "ClassesTiming_header_ID")
);