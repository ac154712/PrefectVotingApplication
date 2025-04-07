SELECT -- this will find users who voted more than once within the same minute (to ake sure there is no spamming)
    voterid, -- this shows the id of the voter
    count(*) as rapid_votes, -- this counts how many votes they made in that time
    format(timestamp, 'yyyy-MM-dd hh:mm') as vote_minute -- this rounds timestamp to the minute

FROM votes

GROUP BY voterid, format(timestamp, 'yyyy-MM-dd hh:mm') -- this groups by user and vote minute
HAVING count(*) > 1; -- this filters to only users who did it more than once
