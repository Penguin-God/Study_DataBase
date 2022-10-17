--ORDER BY 
--SELECT TOP 10 nameFirst, nameLast, birthYear, birthMonth, birthDay
--FROM players WHERE birthYear IS NOT NULL ORDER BY birthYear DESC, birthMonth DESC, birthDay DESC;

-- birthCity LIKE 'NEW YOR_'; : LIKE�� �̿��� ���ڿ� ���� ��Ī.


-- ����
SELECT 2023 - birthYear AS KoearAge 
FROM players 
WHERE deathYear IS NULL and birthYear IS NOT NULL AND 2023 - birthYear <= 70
ORDER BY KoearAge;

-- ���ڿ� ���� ����
SELECT nameFirst + ' ' + nameLast AS fullName
FROM players
WHERE nameFirst IS NOT NULL AND nameLast IS NOT NULL;