-- Active: 1697616242142@@127.0.0.1@5432@SchedulerDB
insert into "Schoolyear" values
(default, '2022-2023', '2022-09-01', '2023-07-10');

insert into "Employee" values
(default, true, 'Иванов Иван Иванович', false, 'ivanov_i-i', 'vanya123', '@ivanov_ivan', '+7 910 536-73-85', 'ivanovivan@mail.ru'),
(default, true, 'Петрова Ирина Сергеевна', true, 'petrova_irina', '579irishka', '@irina505', '+7 980 517-72-54', null),
(default, true, 'Подколодный Иван Георгиевич', false, 'podkolod_ig', 'ivan1882', '@ivan_podkolodny', '+7 980 645-23-77', 'podkolodny_ivan@mail.ru'),
(default, true, 'Фомичев Семён Герасимович', false, 'semen_fomi4ev', 's3m3n', '@fomichevS', '+7 910 335-63-60', null),
(default, true, 'Яхненко Любовь Леонидовна', true, '9xnenko_lubov', 'luba_<3', '@Lubov_', '+7 977 475-53-30', 'lubovYahnenko@gmail.com'),
(default, false, 'Югова Карина Антоновна', false, 'k-yugova', 'south_K', '@karinayugova42', '+7 910 885-76-19', 'karina_yugova@yandex.com'),
(default, false, 'Чернышёв Евгений Павлович', true, 'e-chernishev', 'jeka_cherny', '@chernishevevgeny', '+7 977 534-92-14', null);

insert into "Subject" values 
(default, 'Математика', 56),
(default, 'Русский Язык', 42),
(default, 'Английский Язык', 35),
(default, 'История', 26),
(default, 'Обществознание', 20),
(default, 'Информатика', 50),
(default, 'Физика', 25),
(default, 'Биология', 29),
(default, 'Физическая культура', 40);

insert into "Tution" values 
(default, (select "Subject_ID" from "Subject" where "Name" = 'Математика'), 
(select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i'),
'2020-01-01', null),
(default, (select "Subject_ID" from "Subject" where "Name" = 'Английский Язык'), 
(select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i'),
'2020-01-01', null),
(default, (select "Subject_ID" from "Subject" where "Name" = 'Русский Язык'), 
(select "Employee_ID" from "Employee" where "Login" = 'petrova_irina'),
'2020-01-01', null),
(default, (select "Subject_ID" from "Subject" where "Name" = 'История'), 
(select "Employee_ID" from "Employee" where "Login" = 'podkolod_ig'),
'2019-11-15', null),
(default, (select "Subject_ID" from "Subject" where "Name" = 'Обществознание'), 
(select "Employee_ID" from "Employee" where "Login" = 'podkolod_ig'),
'2020-01-01', null),
(default, (select "Subject_ID" from "Subject" where "Name" = 'Информатика'), 
(select "Employee_ID" from "Employee" where "Login" = 'semen_fomi4ev'),
'2019-10-12', null),
(default, (select "Subject_ID" from "Subject" where "Name" = 'Физика'), 
(select "Employee_ID" from "Employee" where "Login" = 'k-yugova'),
'2019-03-20', '2019-10-10'),
(default, (select "Subject_ID" from "Subject" where "Name" = 'Физика'), 
(select "Employee_ID" from "Employee" where "Login" = '9xnenko_lubov'),
'2019-11-01', null),
(default, (select "Subject_ID" from "Subject" where "Name" = 'Физическая культура'), 
(select "Employee_ID" from "Employee" where "Login" = 'semen_fomi4ev'),
'2020-01-01', null),
(default, (select "Subject_ID" from "Subject" where "Name" = 'Физическая культура'), 
(select "Employee_ID" from "Employee" where "Login" = 'e-chernishev'),
'2019-02-01', '2019-07-05');

insert into "Cabinet" values
(default, 701, null),
(default, 702, 'Purple'),
(default, 703, 'Blue'),
(default, 705, 'Orange');

insert into "StudentGroup" values
(default, 'Не указана'), -- БАЗОВАЯ
(default, 'В4212'),
(default, 'C2311'),
(default, '1312');

insert into "ClassesTiming_header" values 
(default, 'Основное'), -- БАЗОВАЯ
(default, 'Сокращённое'); -- БАЗОВАЯ
insert into "ClassesTiming_body" values 
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
default, '9:00', '10:30'), -- БАЗОВАЯ
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
default, '10:40', '12:10'), -- БАЗОВАЯ
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
default, '13:00', '14:30'), -- БАЗОВАЯ
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
default, '14:40', '16:10'), -- БАЗОВАЯ
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
default, '9:00', '10:00'), -- БАЗОВАЯ
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
default, '10:10', '11:10'), -- БАЗОВАЯ
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
default, '11:20', '12:20'), -- БАЗОВАЯ
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
default, '12:30', '13:30'); -- БАЗОВАЯ


/*------------------------------------------------Расписание на 2022-09-01 (пустое-базовое)------------------------------------------------------------*/
insert into "DailySchedule_header" values -- Шапка
(default,
null, 
(select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'),
'2022-07-09',
(select "Schoolyear_ID" from "Schoolyear" where "Years" = '2022-2023'));


insert into "DailySchedule_body" values -- Табличная часть
                        /* Первый урок */
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2022-07-09'),
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2022-07-09') and 
        "StartTime" = '9:00' and 
        "EndTime" = '10:30'),
 	null,
 	null,
 	null,
 	null
),
                        /* Второй урок */
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2022-07-09'),
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2022-07-09') and 
        "StartTime" = '10:40' and 
        "EndTime" = '12:10'),
    null,
 	null,
 	null,
 	null
),
                        /* Третий урок */ 
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2022-07-09'), 
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2022-07-09') and 
        "StartTime" = '13:00' and 
        "EndTime" = '14:30'),
 	null,
 	null,
 	null,
 	null
),
                        /* Четвёртый урок */ 
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2022-07-09'), 
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2022-07-09') and 
        "StartTime" = '14:40' and 
        "EndTime" = '16:10'),
 	null,
 	null,
 	null,
 	null
);
