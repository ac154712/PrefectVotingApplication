SELECT-- this will show all users who never casted a vote
    u.Id, -- this grabs the user id
    u.FirstName -- this grabs the user name

FROM aspnetusers u

LEFT JOIN Votes v on u.Id = v.VoterId -- this joins users to their votes (if they exist)
WHERE v.Voterid is null; -- this filters to users who have no matching vote
