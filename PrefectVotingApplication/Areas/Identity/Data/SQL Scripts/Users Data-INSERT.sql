INSERT INTO [AspNetUsers] (
    Id, UserName, NormalizedUserName, Email, NormalizedEmail,
    EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp,
    FirstName, LastName, ImagePath, Description, RoleId,
    PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount
)
VALUES
('1', 'john.doe@example.com', 'JOHN.DOE@EXAMPLE.COM', 'john.doe@example.com', 'JOHN.DOE@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'John', 'Doe', '/wwwroot/images/users/john.jpg', 'A dedicated student.', 21, 0, 0, 1, 0),
('2', 'jane.smith@example.com', 'JANE.SMITH@EXAMPLE.COM', 'jane.smith@example.com', 'JANE.SMITH@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Jane', 'Smith', '/wwwroot/images/users/jane.jpg', 'Hardworking student.', 21, 0, 0, 1, 0),
('3', 'alice.johnson@example.com', 'ALICE.JOHNSON@EXAMPLE.COM', 'alice.johnson@example.com', 'ALICE.JOHNSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Alice', 'Johnson', '/wwwroot/images/users/alice.jpg', 'Focused and bright.', 21, 0, 0,  1, 0),
('4', 'bob.williams@example.com', 'BOB.WILLIAMS@EXAMPLE.COM', 'bob.williams@example.com', 'BOB.WILLIAMS@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Bob', 'Williams', '/wwwroot/images/users/bob.jpg', 'Passionate learner.', 21, 0, 0, 1, 0),
('5', 'charlie.brown@example.com', 'CHARLIE.BROWN@EXAMPLE.COM', 'charlie.brown@example.com', 'CHARLIE.BROWN@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Charlie', 'Brown', '/wwwroot/images/users/charlie.jpg', 'A curious mind.', 21, 0, 0, 1, 0),
('6', 'david.miller@example.com', 'DAVID.MILLER@EXAMPLE.COM', 'david.miller@example.com', 'DAVID.MILLER@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'David', 'Miller', '/wwwroot/images/users/david.jpg', 'Loves numbers.', 21, 0, 0, 1, 0),
('7', 'emma.wilson@example.com', 'EMMA.WILSON@EXAMPLE.COM', 'emma.wilson@example.com', 'EMMA.WILSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Emma', 'Wilson', '/wwwroot/images/users/emma.jpg', 'Aspiring scientist.', 21, 0, 0, 1, 0),
('8', 'frank.moore@example.com', 'FRANK.MOORE@EXAMPLE.COM', 'frank.moore@example.com', 'FRANK.MOORE@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Frank', 'Moore', '/wwwroot/images/users/frank.jpg', 'Future leader.', 21, 0, 0, 1, 0),
('9', 'grace.taylor@example.com', 'GRACE.TAYLOR@EXAMPLE.COM', 'grace.taylor@example.com', 'GRACE.TAYLOR@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Grace', 'Taylor', '/wwwroot/images/users/grace.jpg', 'Creative thinker.', 21, 0, 0, 1, 0),
('10', 'henry.anderson@example.com', 'HENRY.ANDERSON@EXAMPLE.COM', 'henry.anderson@example.com', 'HENRY.ANDERSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Henry', 'Anderson', '/wwwroot/images/users/henry.jpg', 'Enjoys history.', 21, 0, 0, 1, 0),
('11', 'isabella.thomas@example.com', 'ISABELLA.THOMAS@EXAMPLE.COM', 'isabella.thomas@example.com', 'ISABELLA.THOMAS@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Isabella', 'Thomas', '/wwwroot/images/users/isabella.jpg', 'Problem solver.', 21, 0, 0, 1, 0),
('12', 'jack.jackson@example.com', 'JACK.JACKSON@EXAMPLE.COM', 'jack.jackson@example.com', 'JACK.JACKSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Jack', 'Jackson', '/wwwroot/images/users/jack.jpg', 'Tech enthusiast.', 21, 0, 0, 1, 0),
('13', 'katherine.white@example.com', 'KATHERINE.WHITE@EXAMPLE.COM', 'katherine.white@example.com', 'KATHERINE.WHITE@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Katherine', 'White', '/wwwroot/images/users/katherine.jpg', 'Bookworm.', 21, 0, 0, 1, 0),
('14', 'liam.harris@example.com', 'LIAM.HARRIS@EXAMPLE.COM', 'liam.harris@example.com', 'LIAM.HARRIS@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Liam', 'Harris', '/wwwroot/images/users/liam.jpg', 'Sports fanatic.', 21, 0, 0, 1, 0),
('15', 'mia.martin@example.com', 'MIA.MARTIN@EXAMPLE.COM', 'mia.martin@example.com', 'MIA.MARTIN@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Mia', 'Martin', '/wwwroot/images/users/mia.jpg', 'Loves challenges.', 21, 0, 0, 1, 0),
('16', 'noah.thompson@example.com', 'NOAH.THOMPSON@EXAMPLE.COM', 'noah.thompson@example.com', 'NOAH.THOMPSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Noah', 'Thompson', '/wwwroot/images/users/noah.jpg', 'Math genius.', 21, 0, 0, 1, 0),
('17', 'olivia.garcia@example.com', 'OLIVIA.GARCIA@EXAMPLE.COM', 'olivia.garcia@example.com', 'OLIVIA.GARCIA@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Olivia', 'Garcia', '/wwwroot/images/users/olivia.jpg', 'Aspiring artist.', 21, 0, 0, 1, 0),
('18', 'paul.martinez@example.com', 'PAUL.MARTINEZ@EXAMPLE.COM', 'paul.martinez@example.com', 'PAUL.MARTINEZ@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Paul', 'Martinez', '/wwwroot/images/users/paul.jpg', 'Enjoys coding.', 21, 0, 0, 1, 0),
('19', 'quinn.robinson@example.com', 'QUINN.ROBINSON@EXAMPLE.COM', 'quinn.robinson@example.com', 'QUINN.ROBINSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Quinn', 'Robinson', '/wwwroot/images/users/quinn.jpg', 'Future engineer.', 21, 0, 0, 1, 0),
('20', 'rachel.clark@example.com', 'RACHEL.CLARK@EXAMPLE.COM', 'rachel.clark@example.com', 'RACHEL.CLARK@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Rachel', 'Clark', '/wwwroot/images/users/rachel.jpg', 'Socially active.', 21, 0, 0, 1, 0),
--this are 2 teachers
('21', 'sarah.parker@example.com', 'SARAH.PARKER@EXAMPLE.COM', 'sarah.parker@example.com', 'SARAH.PARKER@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Sarah', 'Parker', '/wwwroot/images/users/sarah.jpg', 'History teacher.', 22, 0, 0, 1, 0),
('22', 'thomas.lewis@example.com', 'THOMAS.LEWIS@EXAMPLE.COM', 'thomas.lewis@example.com', 'THOMAS.LEWIS@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Thomas', 'Lewis', '/wwwroot/images/users/thomas.jpg', 'Mathematics teacher.', 22, 0, 0, 1, 0),
--this last one is an admin/imaginary principal or IT Dept guy
('23', 'william.hall@example.com', 'WILLIAM.HALL@EXAMPLE.COM', 'william.hall@example.com', 'WILLIAM.HALL@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'William', 'Hall', '/wwwroot/images/users/william.jpg', 'Principal.', 23, 0, 0, 1, 0);
