BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "MealSchedule" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"DayOfTheWeek_ID"	INTEGER NOT NULL,
	"Nutrition_ID"	INTEGER NOT NULL,
	FOREIGN KEY("Nutrition_ID") REFERENCES "Nutrition"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("DayOfTheWeek_ID") REFERENCES "DayOfTheWeek"("ID") ON UPDATE CASCADE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Nutrition" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"BreakFast"	INTEGER NOT NULL,
	"Brunch"	INTEGER NOT NULL,
	"Lunch"	INTEGER NOT NULL,
	"AfternoonSnack"	INTEGER NOT NULL,
	"Dinner"	INTEGER NOT NULL,
	FOREIGN KEY("Lunch") REFERENCES "Dish"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("AfternoonSnack") REFERENCES "Dish"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("Brunch") REFERENCES "Dish"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("BreakFast") REFERENCES "Dish"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("Dinner") REFERENCES "Dish"("ID") ON UPDATE CASCADE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "ModeOfTheDay" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"StartTime"	INTEGER NOT NULL,
	"EndTime"	INTEGER NOT NULL,
	"Occupation_ID"	INTEGER NOT NULL,
	"Group_ID"	INTEGER NOT NULL,
	FOREIGN KEY("Occupation_ID") REFERENCES "Occupation"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("Group_ID") REFERENCES "Group"("ID") ON UPDATE CASCADE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "DayOfTheWeek" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT NOT NULL CHECK(LENGTH("Name") < 20) UNIQUE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "User" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Login"	TEXT NOT NULL CHECK(LENGTH("Login") < 50) UNIQUE,
	"Password"	TEXT NOT NULL CHECK(LENGTH("Password") < 50),
	"Role_ID"	INTEGER NOT NULL,
	FOREIGN KEY("Role_ID") REFERENCES "Role"("ID") ON UPDATE CASCADE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Role" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT NOT NULL CHECK(LENGTH("Role") < 25) UNIQUE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Post" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT NOT NULL CHECK(LENGTH("Name") < 50) UNIQUE,
	"Salary"	NUMERIC NOT NULL,
	"WorkSchedule"	TEXT NOT NULL CHECK(LENGTH("WorkSchedule") < 10),
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Occupation" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT NOT NULL CHECK(LENGTH("Name") < 50) UNIQUE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Kindergarten" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT NOT NULL CHECK(LENGTH("Name") < 50) UNIQUE,
	"Address"	TEXT NOT NULL CHECK(LENGTH("Address") < 50) UNIQUE,
	"StartTime"	TEXT NOT NULL CHECK(LENGTH("StartTime") < 20) UNIQUE,
	"EndTime"	TEXT NOT NULL CHECK(LENGTH("EndTime") < 20) UNIQUE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Dish" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT NOT NULL CHECK(LENGTH("Name") < 50) UNIQUE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Group" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT NOT NULL CHECK(LENGTH("Name") < 50) UNIQUE,
	"AvailableSeats"	INTEGER NOT NULL,
	"Employee_ID"	INTEGER NOT NULL UNIQUE,
	FOREIGN KEY("Employee_ID") REFERENCES "Employee"("ID") ON UPDATE CASCADE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Employee" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Surname"	TEXT NOT NULL CHECK(LENGTH("SurName") < 50),
	"Name"	TEXT NOT NULL CHECK(LENGTH("Name") < 50),
	"Patronymic"	TEXT CHECK(LENGTH("Patronymic") < 50),
	"FullName"	TEXT NOT NULL CHECK(LENGTH("FullName") < 70),
	"DateOfBirth"	TEXT NOT NULL CHECK(LENGTH("DateOfBirth") < 25),
	"Experience"	INTEGER NOT NULL,
	"Post_ID"	INTEGER NOT NULL,
	"User_ID"	INTEGER NOT NULL UNIQUE,
	FOREIGN KEY("User_ID") REFERENCES "User"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("Post_ID") REFERENCES "Post"("ID") ON UPDATE CASCADE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Kid" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Surname"	TEXT NOT NULL CHECK(LENGTH("SurName") < 50),
	"Name"	TEXT NOT NULL CHECK(LENGTH("Name") < 50),
	"Patronymic"	TEXT CHECK(LENGTH("Patronymic") < 50),
	"FullName"	TEXT NOT NULL CHECK(LENGTH("FullName") < 70),
	"DateOfBirth"	TEXT NOT NULL CHECK(LENGTH("DateOfBirth") < 50),
	"Group_ID"	INTEGER NOT NULL,
	FOREIGN KEY("Group_ID") REFERENCES "Group"("ID") ON UPDATE CASCADE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
INSERT INTO "MealSchedule" ("ID","DayOfTheWeek_ID","Nutrition_ID") VALUES (1,1,1),
 (2,1,2),
 (3,1,3),
 (4,1,4),
 (5,1,5);
INSERT INTO "Nutrition" ("ID","BreakFast","Brunch","Lunch","AfternoonSnack","Dinner") VALUES (1,1,1,1,19,1),
 (2,2,2,13,5,2),
 (3,6,7,14,16,26),
 (4,9,4,23,4,8),
 (5,20,12,20,23,15);
INSERT INTO "ModeOfTheDay" ("ID","StartTime","EndTime","Occupation_ID","Group_ID") VALUES (1,'8:00','8:20',7,1),
 (2,'8:20','8:25',2,1),
 (3,'8:25','8:50',3,1),
 (4,'9:00','9:08',4,1),
 (5,'9:10','9:35',5,1);
INSERT INTO "DayOfTheWeek" ("ID","Name") VALUES (1,'Понедельник'),
 (2,'Вторник'),
 (3,'Среда'),
 (4,'Четверг'),
 (5,'Пятница');
INSERT INTO "User" ("ID","Login","Password","Role_ID") VALUES (1,'admin','admin',3),
 (2,'sigma','kriper',2),
 (3,'svinka','sigma',2),
 (4,'abobus','alloha',2),
 (5,'123','123',2),
 (6,'uchilka','uchilka',2),
 (7,'vospitatel','vospitatel',2);
INSERT INTO "Role" ("ID","Name") VALUES (1,'Гость'),
 (2,'Сотрудник'),
 (3,'Администратор');
INSERT INTO "Post" ("ID","Name","Salary","WorkSchedule") VALUES (1,'Уборщик',15000,'5/2'),
 (2,'Воспитатель',25000,'5/2'),
 (3,'Повар',25000,'5/2'),
 (4,'Администратор',27000,'5/2');
INSERT INTO "Occupation" ("ID","Name") VALUES (1,'Игровая деятельность'),
 (2,'Утренняя гимнастика'),
 (3,'Завтрак'),
 (4,'Образовательная деятельность'),
 (5,'Подготовка к прогулке'),
 (6,'Второй завтрак'),
 (7,'Приём'),
 (8,'Сон');
INSERT INTO "Kindergarten" ("ID","Name","Address","StartTime","EndTime") VALUES (1,'Детский сад "Тюрьма №106"','ул.Фонтанчикова д.106 ','7:00','18:00');
INSERT INTO "Dish" ("ID","Name") VALUES (1,'Белый хлеб'),
 (2,'Чай'),
 (3,'Молоко'),
 (4,'Печенье'),
 (5,'Какао'),
 (6,'Омлет'),
 (7,'Яблоко'),
 (8,'Банан'),
 (9,'Апельсин'),
 (10,'Кисель'),
 (11,'Пюре с рыбой'),
 (12,'Рисовая каша'),
 (13,'Сок'),
 (14,'Борщ'),
 (15,'Рыба тушеная'),
 (16,'Овощной салат'),
 (17,'Мокровный салат'),
 (18,'Чёрный хлеб'),
 (19,'Белый хлеб с маслом'),
 (20,'Бутерброд'),
 (21,'Манная каша'),
 (22,'Гречка'),
 (23,'Мандарин'),
 (24,'Щавельный суп'),
 (25,'Щи'),
 (26,'Макароны с сосисками');
INSERT INTO "Group" ("ID","Name","AvailableSeats","Employee_ID") VALUES (1,'1-ая Младшая группа',4,1),
 (2,'2-ая Младшая группа',5,6),
 (3,'Средняя группа',2,4),
 (4,'Старшая группа',7,7);
INSERT INTO "Employee" ("ID","Surname","Name","Patronymic","FullName","DateOfBirth","Experience","Post_ID","User_ID") VALUES (1,'Скуднов','Владислав','Витальевич','Скуднов Владислав Витальевич','04.01.2000',3,2,2),
 (2,'Александров','Александр','Андерович','Александров Александр Андерович','21.01.2002',1,3,3),
 (3,'Золочевский','Иван','Сергеевич','Золочевский Иван Сергеевич','27.07.2004',2,1,5),
 (4,'Смирнов','Александр','Иванович','Смирнов Александр Иванович','15.01.1995',5,2,4),
 (5,'Администратов','Админ','Админович','Администратов Админ Админович','11.11.1999',3,4,1),
 (6,'Жукова','Клавдия','Агафоновна','Жукова Квладия Агафоновна','25.03.1999',2,2,6),
 (7,'Борисов','Антонин','Владиславович','Борисов Антонин Владиславович','20.04.1989',10,2,7);
INSERT INTO "Kid" ("ID","Surname","Name","Patronymic","FullName","DateOfBirth","Group_ID") VALUES (1,'Скуднов','Алексей','Витальевич','Скуднов Алексей Витальевич','21.04.2018',1),
 (2,'Анищенко','Николай','Петрович','Анищенко Николай Петрович','10.10.2018',1),
 (3,'Григорьев','Алексей','Николаевич','Григорьев Алексей Николаевич','11.10.2018',1),
 (4,'Тимофеев','Максим','Петрович','Тимофеев Максим Петрович','12.10.2018',1),
 (5,'Гришко','Алла','Николаевна','Гришко Алла Николаевна','13.10.2018',1);
COMMIT;
