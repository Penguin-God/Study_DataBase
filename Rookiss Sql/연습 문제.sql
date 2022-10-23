-- 1. �������� ���İ� �������� ������ ���
SELECT *
FROM batting
WHERE teamID = 'BOS'

-- 2. �������� ���İ� �������� ��
SELECT COUNT(DISTINCT(playerID))
FROM batting
WHERE teamID = 'BOS'

-- 3. �������� 2004�⿡ ģ Ȩ�� ����
SELECT SUM(HR)
FROM batting
WHERE teamID = 'BOS' AND yearID = 2004

-- 4. ������ �� �Ҽ� �߿� Ư�� �⵵ ���� �ִ� Ȩ���� ģ ����� ����
SELECT TOP(1) *
FROM batting
WHERE teamID = 'BOS'
ORDER BY HR DESC