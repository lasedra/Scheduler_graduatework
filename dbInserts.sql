-- Active: 1697616242142@@127.0.0.1@5432@SchedulerDB
insert into "Employee" values
(default, true, 'Иванов Иван Иванович', false, 'ivanov_i-i', 'vanya123', '@ivanov_ivan', '+7 910 536 73 85', 'ivanovivan@mail.ru'),
(default, true, 'Петрова Ирина Сергеевна', true, 'petrova_irina', '579irishka', '@irina505', '+7 980 517 72 54', null);

insert into "Subject" values 
(default, 'Математика', 56),
(default, 'Русский Язык', 42),
(default, 'Английский Язык', 35);

insert into "TutionLog" values 
(1, 
(select "Subject_ID" from "Subject" where "Name" = 'Математика'), 
(select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i')),
(2, 
(select "Subject_ID" from "Subject" where "Name" = 'Русский Язык'), 
(select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i'));

insert into "DayOfTheWeek" values 
(1, 'Понедельник'),
(2, 'Вторник'),
(3, 'Среда'),
(4, 'Четверг'),
(5, 'Пятница'),
(6, 'Суббота'),
(7, 'Воскресенье');

insert into "Cabinet" values
(default, 701, null),
(default, 702, 'Purple'),
(default, 703, 'Blue'),
(default, 705, 'Orange');

insert into "StudentGroup" values 
(default, 'В4212'),
(default, 'C2311'),
(default, '1312');

insert into "ClassesTiming_header" values 
(default, 'Основное'),
(default, 'Сокращённое');
insert into "ClassesTiming_body" values 
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
default, '9:00', '10:30'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
default, '10:40', '12:10'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
default, '13:00', '14:30'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
default, '14:40', '16:10'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
default, '9:00', '10:00'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
default, '10:10', '11:10'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
default, '11:20', '12:20'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
default, '12:30', '13:30');


/*------------------------------------------------Расписание на 2020-01-01 на два урока математики для группы B4212----------------------------------------------------*/
insert into "DailySchedule_header" values -- Шапка
(1, default,
(select "StudentGroup_ID" from "StudentGroup" where "Code" = 'В4212'), 
(select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'),
'2020-01-01'),
(2, default,
(select "StudentGroup_ID" from "StudentGroup" where "Code" = 'В4212'), 
(select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'),
'2020-01-02'),
(3, default,
(select "StudentGroup_ID" from "StudentGroup" where "Code" = 'В4212'), 
(select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'),
'2020-01-03');

insert into "DailySchedule_body" values -- Табличная часть 2020-01-01
                        /* Первый урок */
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-01'),
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-01') and 
        "StartTime" = '9:00' and 
        "EndTime" = '10:30'),
    (select "Subject_ID" from "Subject" where "Name" = 'Математика'),
    (select "Cabinet_ID" from "Cabinet" where "Number" = 703),
    (select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i')
),
                        /* Второй урок */
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-01'), 
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-01') and 
        "StartTime" = '10:40' and 
        "EndTime" = '12:10'),
    (select "Subject_ID" from "Subject" where "Name" = 'Математика'),
    (select "Cabinet_ID" from "Cabinet" where "Number" = 703),
    (select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i')
),
                        /* Третий урок */ 
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-01'), 
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-01') and 
        "StartTime" = '13:00' and 
        "EndTime" = '14:30'),
    (select "Subject_ID" from "Subject" where "Name" = 'Русский Язык'),
    (select "Cabinet_ID" from "Cabinet" where "Number" = 702),
    (select "Employee_ID" from "Employee" where "Login" = 'petrova_irina')
);

insert into "DailySchedule_body" values -- Табличная часть 2020-01-02
                        /* Первый урок */
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-02'),
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-02') and 
        "StartTime" = '9:00' and 
        "EndTime" = '10:30'),
    (select "Subject_ID" from "Subject" where "Name" = 'Математика'),
    (select "Cabinet_ID" from "Cabinet" where "Number" = 703),
    (select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i')
),
                        /* Второй урок */
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-02'), 
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-02') and 
        "StartTime" = '10:40' and 
        "EndTime" = '12:10'),
    (select "Subject_ID" from "Subject" where "Name" = 'Английский Язык'),
    (select "Cabinet_ID" from "Cabinet" where "Number" = 705),
    (select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i')
),
                        /* Третий урок */ 
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-02'), 
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-02') and 
        "StartTime" = '13:00' and 
        "EndTime" = '14:30'),
    (select "Subject_ID" from "Subject" where "Name" = 'Русский Язык'),
    (select "Cabinet_ID" from "Cabinet" where "Number" = 702),
    (select "Employee_ID" from "Employee" where "Login" = 'petrova_irina')
),
                        /* Четвёртый урок */ 
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-02'), 
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-02') and 
        "StartTime" = '14:40' and 
        "EndTime" = '16:10'),
    (select "Subject_ID" from "Subject" where "Name" = 'Русский Язык'),
    (select "Cabinet_ID" from "Cabinet" where "Number" = 702),
    (select "Employee_ID" from "Employee" where "Login" = 'petrova_irina')
);

insert into "DailySchedule_body" values -- Табличная часть 2020-01-03
                        /* Первый урок */
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-03'),
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-03') and 
        "StartTime" = '9:00' and 
        "EndTime" = '10:00'),
    (select "Subject_ID" from "Subject" where "Name" = 'Математика'),
    (select "Cabinet_ID" from "Cabinet" where "Number" = 703),
    (select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i')
),
                        /* Второй урок */
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-03'), 
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-03') and 
        "StartTime" = '10:10' and 
        "EndTime" = '11:10'),
    (select "Subject_ID" from "Subject" where "Name" = 'Английский Язык'),
    (select "Cabinet_ID" from "Cabinet" where "Number" = 705),
    (select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i')
),
                        /* Третий урок */ 
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-03'), 
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-03') and 
        "StartTime" = '11:20' and 
        "EndTime" = '12:20'),
    (select "Subject_ID" from "Subject" where "Name" = 'Английский Язык'),
    (select "Cabinet_ID" from "Cabinet" where "Number" = 705),
    (select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i')
),
                        /* Четвёртый урок */ 
((select "DailySchedule_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-03'), 
    default, 
    (select "TimeSlot_ID" from "ClassesTiming_body" where 
        "ClassesTiming_header_ID" = (select "ClassesTiming_header_ID" from "DailySchedule_header" where "OfDate" = '2020-01-03') and 
        "StartTime" = '12:30' and 
        "EndTime" = '13:30'),
    (select "Subject_ID" from "Subject" where "Name" = 'Русский Язык'),
    (select "Cabinet_ID" from "Cabinet" where "Number" = 702),
    (select "Employee_ID" from "Employee" where "Login" = 'petrova_irina')
);