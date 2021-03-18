
use DamuMed;

create table Doctors
(
	doctorId uniqueidentifier primary key,
	[name] nvarchar(max) not null,
	destinationId uniqueidentifier
);

create table Destinations
(
	DestinationId uniqueidentifier primary key,
	[name] nvarchar(max) not null,
);

create table schedules
(
	scheduleId uniqueidentifier primary key,
	doctorId uniqueidentifier,
	patientId uniqueidentifier,
	[year] int,
	[month] int,
	[day] int,
	[hour] int,
);

create table patients
(
	patientId uniqueidentifier primary key,
	[name] nvarchar(max) not null,
);

alter table Doctors add foreign key (destinationId) references Destinations(DestinationId);
alter table schedules add foreign key (doctorId) references Doctors(doctorId);
alter table schedules add foreign key (patientId) references patients(patientId);


ALTER TABLE patients ADD IIN VARCHAR(MAX) NOT NULL;
