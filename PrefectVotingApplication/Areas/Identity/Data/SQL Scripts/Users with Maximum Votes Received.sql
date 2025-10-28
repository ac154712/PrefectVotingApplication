SELECT TOP 10 u.FirstName, u.LastName, COUNT(v.VoteId) AS VotesReceived
FROM Votes v -- Show users who received the most votes in the current election
JOIN AspNetUsers u ON v.ReceiverId = u.Id
WHERE v.ElectionId = 1
GROUP BY u.FirstName, u.LastName
ORDER BY VotesReceived DESC;
