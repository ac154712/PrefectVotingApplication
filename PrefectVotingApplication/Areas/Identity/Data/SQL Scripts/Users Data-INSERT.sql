INSERT INTO [AspNetUsers] (
    Id, UserName, NormalizedUserName, Email, NormalizedEmail,
    EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp,
    FirstName, LastName, ImagePath, Description, RoleId,
    PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount
)
VALUES
('1', 'john.doe@example.com', 'JOHN.DOE@EXAMPLE.COM', 'john.doe@example.com', 'JOHN.DOE@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'John', 'Doe', '~/images/user-default.png', 'A dedicated student.', 1, 0, 0, 1, 0),
('2', 'jane.smith@example.com', 'JANE.SMITH@EXAMPLE.COM', 'jane.smith@example.com', 'JANE.SMITH@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Jane', 'Smith', '~/images/user-default.png', 'Hardworking student.', 1, 0, 0, 1, 0),
('3', 'alice.johnson@example.com', 'ALICE.JOHNSON@EXAMPLE.COM', 'alice.johnson@example.com', 'ALICE.JOHNSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Alice', 'Johnson', '~/images/user-default.png', 'Focused and bright.', 1, 0, 0,  1, 0),
('4', 'bob.williams@example.com', 'BOB.WILLIAMS@EXAMPLE.COM', 'bob.williams@example.com', 'BOB.WILLIAMS@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Bob', 'Williams', '~/images/user-default.png', 'Passionate learner.', 1, 0, 0, 1, 0),
('5', 'charlie.brown@example.com', 'CHARLIE.BROWN@EXAMPLE.COM', 'charlie.brown@example.com', 'CHARLIE.BROWN@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Charlie', 'Brown', '~/images/user-default.png', 'A curious mind.', 1, 0, 0, 1, 0),
('6', 'david.miller@example.com', 'DAVID.MILLER@EXAMPLE.COM', 'david.miller@example.com', 'DAVID.MILLER@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'David', 'Miller', '~/images/user-default.png', 'Loves numbers.', 1, 0, 0, 1, 0),
('7', 'emma.wilson@example.com', 'EMMA.WILSON@EXAMPLE.COM', 'emma.wilson@example.com', 'EMMA.WILSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Emma', 'Wilson', '~/images/user-default.png', 'Aspiring scientist.', 1, 0, 0, 1, 0),
('8', 'frank.moore@example.com', 'FRANK.MOORE@EXAMPLE.COM', 'frank.moore@example.com', 'FRANK.MOORE@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Frank', 'Moore', '~/images/user-default.png', 'Future leader.', 1, 0, 0, 1, 0),
('9', 'grace.taylor@example.com', 'GRACE.TAYLOR@EXAMPLE.COM', 'grace.taylor@example.com', 'GRACE.TAYLOR@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Grace', 'Taylor', '~/images/user-default.png', 'Creative thinker.',1, 0, 0, 1, 0),
('10', 'henry.anderson@example.com', 'HENRY.ANDERSON@EXAMPLE.COM', 'henry.anderson@example.com', 'HENRY.ANDERSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Henry', 'Anderson', '~/images/user-default.png', 'Enjoys history.', 1, 0, 0, 1, 0),
('11', 'isabella.thomas@example.com', 'ISABELLA.THOMAS@EXAMPLE.COM', 'isabella.thomas@example.com', 'ISABELLA.THOMAS@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Isabella', 'Thomas', '~/images/user-default.png', 'Problem solver.', 1, 0, 0, 1, 0),
('12', 'jack.jackson@example.com', 'JACK.JACKSON@EXAMPLE.COM', 'jack.jackson@example.com', 'JACK.JACKSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Jack', 'Jackson', '~/images/user-default.png', 'Tech enthusiast.', 1, 0, 0, 1, 0),
('13', 'katherine.white@example.com', 'KATHERINE.WHITE@EXAMPLE.COM', 'katherine.white@example.com', 'KATHERINE.WHITE@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Katherine', 'White', '~/images/user-default.png', 'Bookworm.', 1, 0, 0, 1, 0),
('14', 'liam.harris@example.com', 'LIAM.HARRIS@EXAMPLE.COM', 'liam.harris@example.com', 'LIAM.HARRIS@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Liam', 'Harris', '~/images/user-default.png', 'Sports fanatic.', 1, 0, 0, 1, 0),
('15', 'mia.martin@example.com', 'MIA.MARTIN@EXAMPLE.COM', 'mia.martin@example.com', 'MIA.MARTIN@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Mia', 'Martin', '~/images/user-default.png', 'Loves challenges.', 1, 0, 0, 1, 0),
('16', 'noah.thompson@example.com', 'NOAH.THOMPSON@EXAMPLE.COM', 'noah.thompson@example.com', 'NOAH.THOMPSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Noah', 'Thompson', '~/images/user-default.png', 'Math genius.', 1, 0, 0, 1, 0),
('17', 'olivia.garcia@example.com', 'OLIVIA.GARCIA@EXAMPLE.COM', 'olivia.garcia@example.com', 'OLIVIA.GARCIA@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Olivia', 'Garcia', '~/images/user-default.png', 'Aspiring artist.', 1, 0, 0, 1, 0),
('18', 'paul.martinez@example.com', 'PAUL.MARTINEZ@EXAMPLE.COM', 'paul.martinez@example.com', 'PAUL.MARTINEZ@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Paul', 'Martinez', '~/images/user-default.png', 'Enjoys coding.', 1, 0, 0, 1, 0),
('19', 'quinn.robinson@example.com', 'QUINN.ROBINSON@EXAMPLE.COM', 'quinn.robinson@example.com', 'QUINN.ROBINSON@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Quinn', 'Robinson', '~/images/user-default.png', 'Future engineer.', 1, 0, 0, 1, 0),
('20', 'rachel.clark@example.com', 'RACHEL.CLARK@EXAMPLE.COM', 'rachel.clark@example.com', 'RACHEL.CLARK@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Rachel', 'Clark', '~/images/user-default.png', 'Socially active.', 1, 0, 0, 1, 0),
--this are 2 teachers
('21', 'sarah.parker@example.com', 'SARAH.PARKER@EXAMPLE.COM', 'sarah.parker@example.com', 'SARAH.PARKER@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Sarah', 'Parker', '~/images/user-default.png', 'History teacher.', 2, 0, 0, 1, 0),
('22', 'thomas.lewis@example.com', 'THOMAS.LEWIS@EXAMPLE.COM', 'thomas.lewis@example.com', 'THOMAS.LEWIS@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'Thomas', 'Lewis', '~/images/user-default.png', 'Mathematics teacher.', 2, 0, 0, 1, 0),
--this last one is an admin/imaginary principal or IT Dept guy
('23', 'william.hall@example.com', 'WILLIAM.HALL@EXAMPLE.COM', 'william.hall@example.com', 'WILLIAM.HALL@EXAMPLE.COM', 1, NULL, NEWID(), NEWID(), 'William', 'Hall', '~/images/user-default.png', 'Principal.', 3, 0, 0, 1, 0);
