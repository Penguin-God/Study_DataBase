USE BaseballData

DECLARE @testTable TABLE
(
	name VARCHAR(50) NOT NULL,
	salary INT NOT NULL
)

INSERT INTO @testTable
SELECT pl.nameFirst + ' ' + pl.nameLast, sa.salary
FROM players AS pl
	INNER JOIN salaries AS sa
	ON pl.playerID = sa.playerID

SELECT *
FROM @testTable