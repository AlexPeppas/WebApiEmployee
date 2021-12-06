create Database Employee

create table dbo.Department(
DepartmentId int identity (1,1),
DepartmentName varchar (500)
)

select * from dbo.Department
insert into dbo.Department values(
'IT'
)

create table dbo.Employess (
EmployeeId int identity (1,1),
EmployeeName varchar (500),
Department varchar (500),
DateOfJoining date
)

insert into dbo.Employess values(
'Alex','IT','03/11/2020'
)

select * from dbo.Employess

ALTER TABLE dbo.Employess
ADD PhotoFileName varchar(500)

UPDATE dbo.Employess
SET PhotoFileName ='test.png'
WHERE EmployeeId=1
