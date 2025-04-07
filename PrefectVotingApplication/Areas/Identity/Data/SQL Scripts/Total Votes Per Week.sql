SELECT-- this will group votes by week and count how many happened in each
    datepart(iso_week, timestamp) as vote_week, -- this gets the iso week number
    year(timestamp) as vote_year,    -- this keeps track of the year in case weeks overlap years
    Count(*) as total_votes-- this counts how many votes in that week

FROM votes

GROUP BY datepart(iso_week, timestamp), year(timestamp)-- this groups by both week and year
ORDER BY vote_year, vote_week;    -- this sorts weeks chronologically
