SELECT r.RoleName, COUNT(v.VoteId) AS TotalVotes -- Count how many votes each role (Student, Teacher, Staff) has casted
FROM Votes v
JOIN AspNetUsers u ON v.VoterId = u.Id
JOIN Role r ON u.RoleId = r.RoleId
GROUP BY r.RoleName;