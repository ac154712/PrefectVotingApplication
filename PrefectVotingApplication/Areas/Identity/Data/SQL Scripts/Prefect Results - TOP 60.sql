SELECT TOP 60
    u.firstname + ' ' + u.lastname as student_name,  -- this will display student names together
    count(v.receiverid) as total_votes-- this does the vote counting


FROM votes v
JOIN aspnetusers u on v.receiverid = u.id  -- this links each vote to a student

WHERE v.electionid = 1 and u.roleid = 21-- this will make it so that only students in election 1 are shown

GROUP BY u.firstname, u.lastname   -- this will group the votes per student
ORDER BY total_votes desc;   -- this will show the most voted students first
