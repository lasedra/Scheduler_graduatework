-- Active: 1697616242142@@127.0.0.1@5432@SchedulerDB
insert into "Employee" values
(default, true, true, true, 'admin', 'admin', 'il_admin', '+79776736876', 'praktikasuppport@mail.ru'),
(default, true, false, false, 'Мурзина Татьяна Витальевна', 'murzina_login', 'murzina_password', 'murzina_phone', null),
(default, true, false, false, 'Сидорина Владислава Евгеньевна', 'sidorina_login', 'sidorina_password', 'sidorina_phone', null),
(default, true, false, false, 'Кириллин Николай Владимирович', 'kirillin_login', 'kirillin_password', 'kirillin_phone', null),
(default, true, false, false, 'Шапошникова Кира Александровна', 'shaposhnikova_login', 'shaposhnikova_password', 'shaposhnikova_phone', null),
(default, true, false, false, 'Саибназарова Парвина Хусниддиновна', 'saibnazarova_login', 'saibnazarova_password', 'saibnazarova_phone', null),
(default, true, false, false, 'Тебина Ольга Вячеславовна', 'tebina_login', 'tebina_password', 'tebina_phone', null),
(default, true, false, false, 'Федорченко Дмитрий Евгеньевич', 'fedor4enko_login', 'fedor4enko_password', 'fedor4enko_phone', null),
(default, true, false, false, 'Кузнецова Виктория Александровна', 'kuznetsova_login', 'kuznetsova_password', 'kuznetsova_phone', null),
(default, true, false, true, 'Адамейко Алина Сергеевна', 'adameyko_login', 'adameyko_password', 'adameyko_phone', null),
(default, true, false, false, 'Истомин Владимир Константинович', 'istomin_login', 'istomin_password', 'istomin_phone', null);


insert into "Subject" values 
(default, 'Химия'),
(default, 'Биология'),
(default, 'ОБЖ'),
(default, 'Физическая культура'),
(default, 'Интернет-маркетинг'),
(default, 'Математика'),
(default, 'Физика'),
(default, 'Русский язык'),
(default, 'Литература'),
(default, 'История'),
(default, 'Обществознание'),
(default, 'Философия'),
(default, 'Информатика'),
(default, 'Введение в специальность'),
(default, 'Индивидуальный проект'),
(default, 'Введение в язык программирования Python'),
(default, 'Основы информационных технологий'),
(default, 'Конфигурирование Windows 10'),
(default, 'География'),
(default, 'История искусства'),
(default, 'Введение в профессию'),
(default, 'Основы графического редактора Adobe Photoshop'),
(default, 'Основы графического редактора Adobe Illustrator'),
(default, 'Дизайн-мышление'),
(default, 'Навигационный и экологический дизайн'),
(default, 'Английский язык');


insert into "Tution" values
((select "Subject_ID" from "Subject" where "Name" = 'Химия'), 
(select "Employee_ID" from "Employee" where "Login" = 'murzina_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Биология'), 
(select "Employee_ID" from "Employee" where "Login" = 'murzina_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'ОБЖ'), 
(select "Employee_ID" from "Employee" where "Login" = 'murzina_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Физическая культура'), 
(select "Employee_ID" from "Employee" where "Login" = 'sidorina_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Интернет-маркетинг'), 
(select "Employee_ID" from "Employee" where "Login" = 'kirillin_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Математика'), 
(select "Employee_ID" from "Employee" where "Login" = 'shaposhnikova_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Физика'), 
(select "Employee_ID" from "Employee" where "Login" = 'shaposhnikova_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Русский язык'), 
(select "Employee_ID" from "Employee" where "Login" = 'saibnazarova_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Литература'), 
(select "Employee_ID" from "Employee" where "Login" = 'saibnazarova_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'История'), 
(select "Employee_ID" from "Employee" where "Login" = 'tebina_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Обществознание'), 
(select "Employee_ID" from "Employee" where "Login" = 'tebina_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Философия'), 
(select "Employee_ID" from "Employee" where "Login" = 'tebina_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Информатика'), 
(select "Employee_ID" from "Employee" where "Login" = 'fedor4enko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Введение в специальность'), 
(select "Employee_ID" from "Employee" where "Login" = 'fedor4enko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Индивидуальный проект'), 
(select "Employee_ID" from "Employee" where "Login" = 'fedor4enko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Введение в язык программирования Python'), 
(select "Employee_ID" from "Employee" where "Login" = 'fedor4enko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Основы информационных технологий'), 
(select "Employee_ID" from "Employee" where "Login" = 'fedor4enko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Конфигурирование Windows 10'), 
(select "Employee_ID" from "Employee" where "Login" = 'fedor4enko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'География'), 
(select "Employee_ID" from "Employee" where "Login" = 'kuznetsova_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'История искусства'), 
(select "Employee_ID" from "Employee" where "Login" = 'adameyko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Индивидуальный проект'), 
(select "Employee_ID" from "Employee" where "Login" = 'adameyko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Введение в профессию'), 
(select "Employee_ID" from "Employee" where "Login" = 'adameyko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Основы графического редактора Adobe Photoshop'), 
(select "Employee_ID" from "Employee" where "Login" = 'adameyko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Основы графического редактора Adobe Illustrator'), 
(select "Employee_ID" from "Employee" where "Login" = 'adameyko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Дизайн-мышление'), 
(select "Employee_ID" from "Employee" where "Login" = 'adameyko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Навигационный и экологический дизайн'), 
(select "Employee_ID" from "Employee" where "Login" = 'adameyko_login'),
'2020-01-01', null),
((select "Subject_ID" from "Subject" where "Name" = 'Английский язык'), 
(select "Employee_ID" from "Employee" where "Login" = 'istomin_login'),
'2020-01-01', null);


insert into "StudentGroup" values
('9/1-ГД-23/2', 'Графический дизайн'),
('9/1-РПО-23/1', 'Информационные системы и программирование');


insert into "Studying" values
('9/1-ГД-23/2', (select "Subject_ID" from "Subject" where "Name" = 'Дизайн-мышление')),
('9/1-РПО-23/1', (select "Subject_ID" from "Subject" where "Name" = 'Информатика'));


insert into "Cabinet" values
(702, 'Purple'),
(703, 'Blue'),
(705, 'Orange'),
(708, 'Red');


insert into "ClassesTiming_header" values
(default, 'Основное'),
(default, 'Сокращённое');
insert into "ClassesTiming_body" values
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
1, '9:00', '10:30'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
2, '10:40', '12:10'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
3, '13:00', '14:30'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
4, '14:40', '16:10'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
1, '9:00', '10:00'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
2, '10:10', '11:10'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
3, '11:20', '12:20'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Сокращённое'), 
4, '12:30', '13:30');