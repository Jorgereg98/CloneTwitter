create database twitterDB
go
use twitterDB
go

create table users(
iduser int primary key identity(1,1),
nombre nvarchar(50),
apellidos nvarchar(50),
mail nvarchar(50),
contrasena nvarchar(50));
go

create table tweet(
idtweet int primary key identity(1,1),
descripcion nvarchar(500),
hora DateTime,
likes int,
idUser int);
go

create procedure adduser
(
	@nombre nvarchar(50),
	@apellidos nvarchar(50),
	@mail nvarchar(50),
	@contrasena nvarchar(50),
	@haserror bit out
)
as
begin try
	set @haserror = 0;
	insert into users
	values
	(@nombre,@apellidos,@mail,@contrasena)
end try
begin catch
	set @haserror = 1;
end catch
go

create procedure addtweet
(
	@descripcion nvarchar(500),
	@hora DateTime,
	@likes int,
	@idUser int,
	@haserror bit out
)
as
begin try
	set @haserror = 0;
	insert into tweet
	values
	(@descripcion,@hora,@likes,@idUser)
end try
begin catch
	set @haserror = 1;
end catch
go

create procedure exist
(
	@mail nvarchar(50),
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from users where mail = @mail)
begin
	set @haserror = 0
end
end try
begin catch
	set @haserror = 1;
end catch
go

create procedure verify
(
	@mail nvarchar(50),
	@contrasena nvarchar(50),
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from users where mail = @mail and contrasena = @contrasena)
begin
	set @haserror = 0
end
end try
begin catch
	set @haserror = 1;
end catch
go

create procedure getuser
(
	@mail nvarchar(50),
	@contrasena nvarchar(50),
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from users where mail = @mail and contrasena = @contrasena)
begin
	set @haserror = 0
	select * from users where mail = @mail and contrasena = @contrasena
end
end try
begin catch
	set @haserror = 1;
end catch
go


create procedure getusertweets
(
	@idUser int,
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from tweet where idUser = @idUser)
begin
	set @haserror = 0
	select * from tweet where idUser = @idUser
end
end try
begin catch
	set @haserror = 1;
end catch
go


select * from users
select * from tweet
