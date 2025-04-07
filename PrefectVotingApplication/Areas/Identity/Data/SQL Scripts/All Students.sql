SELECT -- this will get all users who are students
    Id, -- this is the user's id
    FirstName, -- this is the user's firstname
    LastName,-- this gets the last name
    Email-- this is the user's email (if you have that)

FROM AspNetUsers
WHERE RoleId = 21 ; -- this filters to only student users
