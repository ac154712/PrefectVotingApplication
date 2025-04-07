SELECT -- this will group all votes by day so we can see which days were busiest
    cast(timestamp as date) as vote_day, -- this converts full datetime to just the date
    count(*) as total_votes-- this counts how many votes happened on that day

FROM votes

GROUP BY cast(timestamp as date)    -- this groups votes by day
ORDER BY vote_day; -- this orders the days in sequence
