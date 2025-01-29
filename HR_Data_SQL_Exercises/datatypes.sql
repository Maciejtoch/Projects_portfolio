--checking data table
SELECT * FROM HR_Data ;

--changing column format to DATE datatype
UPDATE hr_data
SET termdate = 
    CASE 
        WHEN termdate IS NOT NULL THEN CONVERT(DATE, LEFT(termdate, 19))
        ELSE NULL
    END;

-- Identify and update birthdates with two-digit years
UPDATE dbo.hr_data
SET birthdate = CASE
    WHEN LEN(birthdate) = 8 AND birthdate LIKE '__/__/__' THEN
        CONCAT(LEFT(birthdate, 6), '19', RIGHT(birthdate, 2))
    WHEN LEN(birthdate) = 8 AND birthdate LIKE '__-__-__' THEN
        CONCAT(LEFT(birthdate, 6), '19', RIGHT(birthdate, 2))
    ELSE
        birthdate
    END;
UPDATE dbo.HR_Data
SET hire_date = CASE
    WHEN LEN(hire_date) = 8 AND hire_date LIKE '__/__/__' THEN
        CONCAT(LEFT(hire_date, 6), '19', RIGHT(hire_date, 2))
    WHEN LEN(hire_date) = 8 AND hire_date LIKE '__-__-__' THEN
        CONCAT(LEFT(hire_date, 6), '19', RIGHT(hire_date, 2))
    ELSE
        hire_date
    END;

--change data type format to DATE for both columns
ALTER TABLE hr_data
    ALTER COLUMN birthdate DATE ; 

ALTER TABLE hr_data
    ALTER COLUMN hire_date DATE ; 

--cleaning up data
SELECT birthdate 
FROM HR_Data
ORDER BY birthdate ASC ; 

DELETE FROM HR_Data
WHERE birthdate < '1950-01-01' ; 

SELECT hire_date 
FROM HR_Data 
ORDER BY hire_date ASC ; 

DELETE FROM HR_Data
WHERE hire_date < '1960-01-01' ; 

SELECT termdate
FROM HR_Data
WHERE termdate IS NOT NULL
ORDER BY termdate ASC ; 

--checking termdate, birthdate, hire_date columns by DESC values for errors
SELECT hire_date
FROM HR_Data
ORDER BY hire_date DESC ; 

--checking data table
SELECT * FROM HR_Data ; 