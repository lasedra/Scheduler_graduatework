-- Active: 1697616242142@@127.0.0.1@5432@SchedulerDB
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
((select "Subject_ID" from "Subject" where "Name" = 'Математика'), 
(select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Английский Язык'), 
(select "Employee_ID" from "Employee" where "Login" = 'ivanov_i-i'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Русский Язык'), 
(select "Employee_ID" from "Employee" where "Login" = 'petrova_irina'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'История'), 
(select "Employee_ID" from "Employee" where "Login" = 'podkolod_ig'),
'2019-11-15', null),
((select "Subject_ID" from "Subject" where "Name" = 'Обществознание'), 
(select "Employee_ID" from "Employee" where "Login" = 'podkolod_ig'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Информатика'), 
(select "Employee_ID" from "Employee" where "Login" = 'semen_fomi4ev'),
'2019-10-12', null),
((select "Subject_ID" from "Subject" where "Name" = 'Физика'), 
(select "Employee_ID" from "Employee" where "Login" = 'k-yugova'),
'2019-03-20', '2019-10-10'),
((select "Subject_ID" from "Subject" where "Name" = 'Физика'), 
(select "Employee_ID" from "Employee" where "Login" = '9xnenko_lubov'),
'2019-11-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Физическая культура'), 
(select "Employee_ID" from "Employee" where "Login" = 'semen_fomi4ev'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Физическая культура'), 
(select "Employee_ID" from "Employee" where "Login" = 'e-chernishev'),
'2019-02-01', '2019-07-05');

insert into "Cabinet" values
(701, null),
(702, 'Purple'),
(703, 'Blue'),
(705, 'Orange');

insert into "StudentGroup" values
('В4212', 'Информационные технологии'),
('C2311', 'Иностранные языки'),
('1312', null);

insert into "ClassesTiming_header" values 
(default, 'Сокращённое');
insert into "ClassesTiming_body" values
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
1, '9:00', '10:00'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
2, '10:10', '11:10'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
3, '11:20', '12:20'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
4, '12:30', '13:30');


/*------------------------------------------------Расписание на 2022-09-01 (pivot example)------------------------------------------------------------*/
insert into "DailySchedule_header" values
('1312', '2022-07-09');
insert into "DailySchedule_body" values
                        /* Первый урок */
('1312', '2022-07-09',
    1, 
    (select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'),
    null, null, null),
                        /* Второй урок */
('1312', '2022-07-09',
    2, 
    (select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'),
    null, null, null),
                        /* Третий урок */
('1312', '2022-07-09',
    3, 
    (select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'),
    null, null, null),
                        /* Четвёртый урок */
('1312', '2022-07-09',
    4, 
    (select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'),
    null, null, null);

