create database twitterDB
go
use twitterDB
go

create table users(
iduser int primary key identity(1,1),
nombre nvarchar(50),
apellidos nvarchar(50),
mail nvarchar(50),
username nvarchar(50),
contrasena nvarchar(50));
go

create table followers(
idfollowers int primary key identity(1,1),
idfollower int,
idfollowing int);
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
	@username nvarchar(50),
	@haserror bit out
)
as
begin try
	set @haserror = 0;
	insert into users
	values
	(@nombre,@apellidos,@mail,@username,@contrasena)
end try
begin catch
	set @haserror = 1;
end catch
go


create procedure updateuser
(
	@nombre nvarchar(50),
	@apellidos nvarchar(50),
	@contrasena nvarchar(50),
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from users where nombre = @nombre)
begin
	set @haserror = 0
	update users
	set nombre = @nombre, apellidos = @apellidos, contrasena = @contrasena
	where nombre = @nombre
end
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
	@username nvarchar(50),
	@mail nvarchar(50),
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from users where mail = @mail or username = @username)
begin
	set @haserror = 0
end
end try
begin catch
	set @haserror = 1;
end catch
go

create procedure existmail
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

create procedure existusername
(
	@username nvarchar(50),
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from users where username = @username)
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
	@username nvarchar(50),
	@contrasena nvarchar(50),
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from users where mail = @mail or username = @username and contrasena = @contrasena)
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
	@username nvarchar(50),
	@contrasena nvarchar(50),
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from users where mail = @mail or username = @username and contrasena = @contrasena)
begin
	set @haserror = 0
	select * from users where mail = @mail or username = @username and contrasena = @contrasena
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

create procedure getfollowing
(
	@idfollower int,
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from followers where idfollower = @idfollower)
begin
	set @haserror = 0
	select * from followers where idfollower = @idfollower
end
end try
begin catch
	set @haserror = 1;
end catch
go

create procedure getfollowers
(
	@iduser int,
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from followers where idfollowing = @iduser)
begin
	set @haserror = 0
	select * from followers where idfollowing = @iduser
end
end try
begin catch
	set @haserror = 1;
end catch
go

create procedure search
(
	@iduser int,
	@nombre nvarchar(50),
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from users where nombre = @nombre or apellidos = @nombre or username = @nombre )
begin
	set @haserror = 0
	select * from users where (nombre = @nombre or apellidos = @nombre or username = @nombre) and not iduser = @iduser
end
end try
begin catch
	set @haserror = 1;
end catch
go


create procedure addfollower
(
	@idfollower int,
	@idfollowing int,
	@haserror bit out
)
as
begin try
	set @haserror = 0;
	if exists(select top 1 1 from followers where idfollower = @idfollower and idfollowing = @idfollowing)
	begin 
		set @haserror = 1;
	end
	else
	begin
		insert into followers
		values
		(@idfollower,@idfollowing)
	end
end try
begin catch
	set @haserror = 1;
end catch
go

create procedure deletefollower
(
	@idfollower int,
	@idfollowing int,
	@haserror bit out
)
as
begin try
	set @haserror = 0;
	if exists(select top 1 1 from followers where idfollower = @idfollower and idfollowing = @idfollowing)
	begin 
		delete from followers where idfollower = @idfollower and idfollowing = @idfollowing
	end
	else
		set @haserror = 1
end try
begin catch
	set @haserror = 1;
end catch
go


go
use twitterDB
go
select * from users
select * from tweet
select * from followers
