SELECT u.FirstName, u.LastName, COUNT(v.VoteId) AS VotesCast -- List all voters and how many votes they cast in the active election
FROM AspNetUsers u
LEFT JOIN Votes v ON u.Id = v.VoterId AND v.ElectionId = 1
GROUP BY u.FirstName, u.LastName
ORDER BY VotesCast DESC;
