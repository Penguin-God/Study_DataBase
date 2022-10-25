USE BaseballData

SELECT *
FROM players
WHERE playerID IN (SELECT TOP (20) playerID FROM salaries ORDER BY salary DESC)