CREATE DATABASE TesteAM53

GO

USE  TesteAM53
go 
CREATE TABLE Produto(
	Id int identity(1,1) primary key,
	Nome varchar(250) not null,
	Sku varchar(15) not null,
	Preco decimal(18,2),
	CreatedAt datetime not null,
	DeletedAt datetime
)

GO

CREATE Table Usuario(
	Id int identity(1,1) primary key,
	Email varchar(250) not null,
	Senha  varchar(250) not null
)

GO
INSERT INTO Usuario (Email, Senha) values ('teste@teste.com','teste')
GO

CREATE Table Permissao(
	Id int identity(1,1) primary key,
	Permissao varchar(100) not null
);

INSERT INTO Permissao(Permissao) values('Escrita');
INSERT INTO Permissao(Permissao) values('Leitura');
INSERT INTO Permissao(Permissao) values('Delete');
INSERT INTO Permissao(Permissao) values('Editar');

Create Table UsuarioPermissao(
	Id int identity(1,1) primary key,
	UsuarioId int  references Usuario(Id),
	PermissaoId int  references Permissao(Id),
)


INSERT INTO UsuarioPermissao(PermissaoId, UsuarioId) VALUES (1,1)
INSERT INTO UsuarioPermissao(PermissaoId, UsuarioId) VALUES (2,1)
INSERT INTO UsuarioPermissao(PermissaoId, UsuarioId) VALUES (3,1)
INSERT INTO UsuarioPermissao(PermissaoId, UsuarioId) VALUES (4,1)