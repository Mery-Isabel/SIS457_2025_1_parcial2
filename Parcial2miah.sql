CREATE DATABASE Parcial2miah;
GO
USE [master]
GO
CREATE LOGIN [usrparcial2] WITH PASSWORD = N'12345678', 
	DEFAULT_DATABASE=[Parcial2miah], 
	CHECK_EXPIRATION=OFF, 
	CHECK_POLICY=ON;
GO
USE [Parcial2miah]
GO
CREATE USER [usrparcial2] FOR LOGIN [usrparcial2]
GO
ALTER ROLE [db_owner] ADD MEMBER [usrparcial2]
GO


--2do parcial
DROP TABLE Serie;
--2do parcial


 -- Tabla Serie
 CREATE TABLE Serie (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titulo VARCHAR(250) NOT NULL,
    Sinopsis VARCHAR(5000) NOT NULL,
    Director VARCHAR(100) NOT NULL,
    Episodio INT NOT NULL,
    IdiomaOriginal VARCHAR(15) NOT NULL ,
    UrlPortada VARCHAR(200) NOT NULL,
    FechaEstreno DATE,
    Estado SMALLINT NOT NULL DEFAULT 1, -- 1: Activo, 0: Inactivo, -1: Eliminado


 );
 GO

ALTER TABLE Serie ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Serie ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();

GO
CREATE PROC paSerieListar @parametro VARCHAR(100)
AS
BEGIN
    SELECT * 
    FROM Serie
    WHERE Estado <> -1 
      AND (
          Titulo + Director + ISNULL(CONVERT(VARCHAR, Episodio), '') + 
          ISNULL(CONVERT(VARCHAR, FechaEstreno, 120), '')
      ) LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY Estado DESC, Titulo ASC;
END;

EXEC paSerieListar 'The boys';

INSERT INTO Serie (Titulo, Sinopsis, Director, Episodio, IdiomaOriginal, UrlPortada, FechaEstreno, Estado) VALUES
('The Last Kingdom', 'Un noble sajón es criado por vikingos y lucha por reclamar su tierra natal.', 'Stephen Butchard', 46, 'Inglés', 'https://www.redbubble.com/shop/the%2Blast%2Bkingdom%2Bposters', '2015-10-10', 1),
('Mindhunter', 'Dos agentes del FBI desarrollan perfiles criminales entrevistando asesinos en serie.', 'David Fincher', 19, 'Inglés', 'https://www.amazon.com/mindhunter-poster/s?k=mindhunter+poster', '2017-10-13', 0),
('The Mandalorian', 'Un cazarrecompensas solitario viaja por la galaxia lejos de la Nueva República.', 'Jon Favreau', 24, 'Inglés', 'https://www.allposters.com/-st/The-Mandalorian-Posters_c287092_.htm', '2019-11-12', 1),
('Peaky Blinders', 'Una familia de gánsteres asciende en el mundo del crimen tras la Primera Guerra Mundial.', 'Steven Knight', 36, 'Inglés', 'https://www.amazon.com/peaky-blinders-poster/s?k=peaky+blinders+poster', '2013-09-12', 1),
('The Witcher', 'Un cazador de monstruos lucha por encontrar su lugar en un mundo lleno de bestias y magia.', 'Lauren Schmidt Hissrich', 24, 'Inglés', 'https://www.amazon.com/witcher-poster/s?k=witcher+poster', '2019-12-20', 1),
('Better Call Saul', 'La historia de cómo un abogado de poca monta se transforma en Saul Goodman.', 'Vince Gilligan', 63, 'Inglés', 'https://www.amazon.com/Better-Call-Saul-poster/s?k=Better+Call+Saul+poster', '2015-02-08', 1),
('Vikings', 'La saga legendaria de Ragnar Lothbrok y sus conquistas vikingas.', 'Michael Hirst', 89, 'Inglés', 'https://www.amazon.com/Vikings-poster/s?k=The+Vikings+poster', '2013-03-03', 0),
('The Boys', 'Un grupo de vigilantes lucha contra superhéroes corruptos.', 'Eric Kripke', 24, 'Inglés', 'https://www.cinematerial.com/tv/the-boys-i1190634', '2019-07-26', 1),
('Mr. Robot', 'Un hacker con trastornos mentales planea derribar a una corporación corrupta.', 'Sam Esmail', 45, 'Inglés', 'https://www.amazon.com/Mr-Robot-Poster/s?k=Mr+Robot+Poster', '2015-06-24', 1),
('House of the Dragon', 'La historia de la Casa Targaryen, 200 años antes de los eventos de Game of Thrones.', 'Ryan Condal', 10, 'Inglés', 'https://ew.com/house-of-the-dragon-season-2-first-look-posters-emma-darcy-olivia-cooke-8409664', '2022-08-21', 1);

SELECT * FROM Serie;

 --2do parcial
