SELECT -- this will show how many votes each user made (to check if they have exactly 60)
    u.firstname, -- this gets the name of the user
    count(*) as votes_casted -- this counts how many times they voted

FROM votes v
JOIN AspNetUsers u on v.voterid = u.id -- this connects the vote to the user who casted it

GROUP BY u.firstname -- this makes sure we get one row per user
ORDER BY votes_casted desc; -- this will sort the results from most to least votes
