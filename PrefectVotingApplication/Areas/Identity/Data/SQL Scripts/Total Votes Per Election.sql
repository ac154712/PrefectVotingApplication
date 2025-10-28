SELECT e.ElectionTitle, COUNT(v.VoteId) AS TotalVotes -- Count total votes for each election
FROM Votes v
JOIN Election e ON v.ElectionId = e.ElectionId
GROUP BY e.ElectionTitle;
