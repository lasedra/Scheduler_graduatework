insert into "Employee" values
(default, true, 'Admin', true, 'admin', 'admin01', '@admin', '+7 000 000-00-00', 'admin@mail.ru');

insert into "ClassesTiming_header" values 
(default, 'Основное');
insert into "ClassesTiming_body" values 
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
1, '9:00', '10:30'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
2, '10:40', '12:10'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
3, '13:00', '14:30'),
((select "ClassesTiming_header_ID" from "ClassesTiming_header" where "Name" = 'Основное'), 
4, '14:40', '16:10');