USE BaseballData

SELECT *,
	RANK() OVER (PARTITION BY playerID ORDER BY salary DESC)
FROM salaries 
ORDER BY playerID