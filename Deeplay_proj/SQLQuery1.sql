INSERT INTO [P_ctrl] (emp_id)
SELECT [emp_id] FROM [employees]
WHERE [emp_id]=(SELECT MAX([emp_id]) FROM [employees])

UPDATE P_ctrl
SET dept_id = 'd04', inspect = 'bad code'
WHERE [emp_id]=(SELECT MAX([emp_id]) FROM [employees])


SELECT (last_name) FROM employees WHERE [emp_id] = (SELECT (emp_id) FROM P_manager WHERE (dept_id = (SELECT (dept_id) FROM P_emp WHERE (emp_id = 10001) ) )) 

SELECT inspect FROM P_ctrl WHERE emp_id = 10017 

SELECT dept_name FROM Departments WHERE dept_id = (SELECT (dept_id) FROM P_emp WHERE emp_id = 10006 )

SELECT COUNT(*) FROM employees