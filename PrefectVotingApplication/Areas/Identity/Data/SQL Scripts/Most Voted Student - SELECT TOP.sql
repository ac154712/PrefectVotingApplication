SELECT TOP 1
    u.firstname + ' ' + u.lastname as student_name,  -- this will combine first and last name
    count(v.receiverid) as total_votes-- this counts how many votes they got

FROM votes v
JOIN aspnetusers u on v.receiverid = u.id  -- this does a join to get student details

WHERE v.electionid = 1 and u.roleid = 21     -- this will filter for election 1 and only students

GROUP BY u.firstname, u.lastname -- this will group votes by full name
ORDER BY total_votes desc;  -- this will make it so that the top voter is at the top
