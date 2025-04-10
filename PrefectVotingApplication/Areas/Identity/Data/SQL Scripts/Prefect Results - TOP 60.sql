SELECT TOP 60
    u.firstname + ' ' + u.lastname AS student_name, -- this will show student names together (firstname and last name)
    SUM(r.VoteWeight) AS total_votes    -- this sums the vote weights of all the votes received by the student

FROM votes v
JOIN aspnetusers u ON v.receiverid = u.id   -- this links each vote to the student (candidate)
JOIN aspnetusers voter ON v.voterid = voter.id -- this gets the user who cast the vote
JOIN role r ON voter.roleid = r.roleid-- this gets the voter's role to access their vote weight which we need because teachers vote are worth 20

WHERE v.electionid = 1 AND u.roleid = 1 -- this filters for student candidates only and in election 1 only

GROUP BY u.firstname, u.lastname -- this groups the votes per student
ORDER BY total_votes DESC;-- this shows the most voted (weighted) students first
