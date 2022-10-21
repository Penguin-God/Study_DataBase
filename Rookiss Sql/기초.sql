USE BaseballData

SELECT birthMonth,
	CASE birthMonth
		WHEN 1 THEN N'°Ü¿ï'
		WHEN 2 THEN N'°Ü¿ï'
		WHEN 3 THEN N'º½'
		WHEN 4 THEN N'º½'
		WHEN 5 THEN N'º½'
		WHEN 6 THEN N'¿©¸§'
		WHEN 7 THEN N'¿©¸§'
		WHEN 8 THEN N'¿©¸§'
		WHEN 9 THEN N'°¡À»'
		WHEN 10 THEN N'°¡À»'
		WHEN 11 THEN N'°¡À»'
		WHEN 12 THEN N'°Ü¿ï'
		ELSE N'¸ô?·ç'
	END AS birthSeason
FROM players

SELECT birthMonth,
	CASE
		WHEN birthMonth <= 2 THEN N'°Ü¿ï'
		WHEN birthMonth <= 5 THEN N'º½'
		WHEN birthMonth <= 8 THEN N'¿©¸§'
		WHEN birthMonth <= 11 THEN N'°¡À»'
		ELSE N'¸ô?·ç'
	END AS birthSeason
FROM players
