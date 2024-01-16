CREATE TABLE People (
    id uniqueidentifier primary key,
    firstname nvarchar(50) not null,
    lastname nvarchar(50) not null,
    age int null,
    favouritefood nvarchar(100) not null
);