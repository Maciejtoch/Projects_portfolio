--AGE DISTRIBUTION 
ALTER TABLE hr_data
ADD age nvarchar(50) ; 
UPDATE hr_data
SET age = DATEDIFF(YEAR, birthdate, GETDATE());
SELECT * FROM hr_data ; 

--age distribution
SELECT 
MIN(age) AS youngest,
MAX(age) AS oldest
FROM hr_data ; 

--age group distribution
SELECT  age
FROM hr_data
ORDER BY age ;

SELECT age_group, COUNT(*) AS count
FROM (
    SELECT 
        CASE 
            WHEN age >= 22 AND age <= 27 THEN 'GenZ'
            WHEN age >= 28 AND age <= 43 THEN 'Millennials'
            WHEN age >= 44 AND age <= 59 THEN 'GenX'
            ELSE '?'
        END AS age_group
    FROM hr_data
    WHERE termdate IS NULL
) AS subquery
GROUP BY age_group
ORDER BY age_group;

--age group by gender
SELECT age_group, gender,
COUNT(*) AS count
FROM (
    SELECT 
        CASE 
            WHEN age >= 22 AND age <= 27 THEN 'GenZ'
            WHEN age >= 28 AND age <= 43 THEN 'Millennials'
            WHEN age >= 44 AND age <= 59 THEN 'GenX'
            ELSE '?'
        END AS age_group, gender
    FROM hr_data
    WHERE termdate IS NULL
) AS subquery
GROUP BY age_group, gender
ORDER BY age_group, gender ;

--GENDER BREAKDOWN
 SELECT gender,
 COUNT(gender) AS COUNT
 FROM Hr_data
 WHERE termdate IS NULL
 GROUP BY gender
 ORDER BY gender ;

--GENDER VARY ACROSS DEPARTMENTS AND POSITIONS
SELECT department, gender,
COUNT(gender) AS count 
FROM hr_data
WHERE termdate IS NULL 
GROUP BY department, gender
ORDER BY department, gender ASC ; 

SELECT department, jobtitle, gender,
COUNT(gender) AS count 
FROM hr_data
WHERE termdate IS NULL 
GROUP BY department, jobtitle, gender
ORDER BY department, jobtitle, gender ASC ;

--RACE DISTRIBUTION
SELECT race, 
COUNT(*) AS count 
FROM hr_data 
WHERE termdate IS NULL
GROUP BY race 
ORDER BY count ASC ; 

--AVG LENGTH OF EMPLOYMENT
SELECT 
AVG(DATEDIFF(year, hire_date, termdate)) AS avg_employment_time
FROM hr_data
WHERE termdate IS NOT NULL AND termdate <= GETDATE();
--HIGHEST TURNOVER RATE DEPARTMENT
SELECT department, 
COUNT(*) AS total_count,
SUM(CASE
        WHEN termdate IS NOT NULL AND termdate <=GETDATE() THEN 1 ELSE 0
        END
        ) AS terminated_count
    FROM hr_data
    GROUP BY department ; 

SELECT 
department,
total_count,
terminated_count,
    ROUND((CAST(terminated_count AS FLOAT)/total_count) * 100, 1) AS turnover_rate
    FROM
        (SELECT department,
        COUNT(*) AS total_count,
        SUM(CASE
            WHEN termdate IS NOT NULL AND termdate <= GETDATE() THEN 1 ELSE 0
            END
            ) AS terminated_count
        FROM hr_data
        GROUP BY department 
            ) AS subquery
    ORDER BY turnover_rate DESC; 

--TENURE DISTRIBUTION
SELECT department,
AVG(DATEDIFF(year, hire_date, termdate)) AS avg_employment_time
FROM hr_data
WHERE termdate IS NOT NULL AND termdate <= GETDATE()
GROUP BY department
ORDER BY avg_employment_time DESC ; 
--HOW MANY EMPLYEES WORK REMOTELY FOR EACH DEPARTMENT
SELECT location,
COUNT(*) AS total_count
FROM HR_Data
WHERE termdate IS NULL 
GROUP BY location ; 
--DISTRIBUTION OF EMPLOYEES BY STATES
SELECT location_state,
COUNT(*) AS total_count 
FROM hr_data 
WHERE termdate IS NULL 
GROUP BY location_state
ORDER BY total_count DESC ; 
--JOB TITLES DISTRIBUTION
SELECT jobtitle,
COUNT(*) AS total_count
FROM HR_Data
WHERE termdate IS NULL
GROUP BY jobtitle 
ORDER BY total_count DESC ; 
--EMPLOYEE HIRE COUNT VARY OVER TIME

--Average Age by Job Title
SELECT jobtitle,
AVG(age) AS avg_age
FROM HR_Data
GROUP BY jobtitle
ORDER BY avg_age ASC ; 
--Location-based Turnover Rate:

--Location-based Gender Diversity:

