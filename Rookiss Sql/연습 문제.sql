-- 1. 보스턴을 거쳐간 선수들의 정보만 출력
SELECT *
FROM batting
WHERE teamID = 'BOS'

-- 2. 보스턴을 거쳐간 선수들의 수
SELECT COUNT(DISTINCT(playerID))
FROM batting
WHERE teamID = 'BOS'

-- 3. 보스턴이 2004년에 친 홈런 개수
SELECT SUM(HR)
FROM batting
WHERE teamID = 'BOS' AND yearID = 2004

-- 4. 보스턴 팀 소속 중에 특정 년도 동안 최다 홈런을 친 사람의 정보
SELECT TOP(1) *
FROM batting
WHERE teamID = 'BOS'
ORDER BY HR DESC