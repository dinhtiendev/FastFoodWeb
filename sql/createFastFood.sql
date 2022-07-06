create table Categories (
	ID int identity(1, 1) primary key,
	name varchar(225) not null
)

create table Foods (
	ID int identity(1, 1) primary key,
	name varchar(255) not null,
	image varchar(255) not null,
	price float not null,
	foodDescription varchar(1000),
	isActive bit default '1',
	categoryID int foreign key references Categories(ID)
)

create table Accounts (
	ID int identity(1, 1) primary key,
	name varchar(255) not null,
	phone varchar(255) not null,
	email varchar(255) not null,
	password varchar(255) not null,
	isAdmin bit default 0,
	isActive bit default 1
)

create table Carts (
	ID int identity(1, 1) primary key,
	quantity int not null,
	foodID int foreign key references Foods(ID),
	accountID int foreign key references Accounts(ID)
)

create table Wishs (
	ID int identity(1, 1) primary key,
	foodID int foreign key references Foods(ID),
	accountID int foreign key references Accounts(ID)
)

create table Orders (
	ID int identity(1, 1) primary key,
	quantity int not null,
	createAt date default getDate(),
	address varchar(255) not null,
	isActive bit default 1,
	foodID int foreign key references Foods(ID),
	accountID int foreign key references Accounts(ID)
)