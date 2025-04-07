SELECT-- this will show any vote updates or deletions made by users
    a.userid, -- this shows who performed the action
    u.firstname, -- this gets the name of that user
    case a.action
        WHEN 1 THEN 'updated vote'   -- this means they changed the vote
        WHEN 2 THEN 'deleted vote' -- this means they removed the vote
        ELSE 'unknown action' -- this catches anything unusual
    END AS action_performed,
    a.timestamp -- this shows when the action happened

FROM auditlog a

JOIN AspNetusers u ON a.userid = u.id -- this links the log to the actual user
WHERE a.action IN (1, 2) -- this filters only to changes or deletions
ORDER BY a.timestamp desc; -- this will show the most recent ones first
