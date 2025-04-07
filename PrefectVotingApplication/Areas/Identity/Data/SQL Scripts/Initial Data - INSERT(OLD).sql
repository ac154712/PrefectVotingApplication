INSERT INTO [AspNetUsers] (Id, FirstName, LastName, Email, ImagePath, Description, RoleId)
VALUES --this inserts 20 students to the aspnet users table hopefully
(1,'John', 'Doe', 'john.doe@example.com', '/wwwroot/images/users/john.jpg', 'A dedicated student.', 21),
(2,'Jane', 'Smith', 'jane.smith@example.com', '/wwwroot/images/users/jane.jpg', 'Hardworking student.', 21),
(3, 'Alice', 'Johnson', 'alice.johnson@example.com', '/wwwroot/images/users/alice.jpg', 'Focused and bright.', 21),
(4, 'Bob', 'Williams', 'bob.williams@example.com', '/wwwroot/images/users/bob.jpg', 'Passionate learner.', 21),
(5,'Charlie', 'Brown', 'charlie.brown@example.com', '/wwwroot/images/users/charlie.jpg', 'A curious mind.', 21),
(6,'David', 'Miller', 'david.miller@example.com', '/wwwroot/images/users/david.jpg', 'Loves numbers.', 21),
(7,'Emma', 'Wilson', 'emma.wilson@example.com', '/wwwroot/images/users/emma.jpg', 'Aspiring scientist.', 21),
(8,'Frank', 'Moore', 'frank.moore@example.com', '/wwwroot/images/users/frank.jpg', 'Future leader.', 21),
(9,'Grace', 'Taylor', 'grace.taylor@example.com', '/wwwroot/images/users/grace.jpg', 'Creative thinker.', 21),
(10,'Henry', 'Anderson', 'henry.anderson@example.com', '/wwwroot/images/users/henry.jpg', 'Enjoys history.', 21),
(11,'Isabella', 'Thomas', 'isabella.thomas@example.com', '/wwwroot/images/users/isabella.jpg', 'Problem solver.', 21),
(12,'Jack', 'Jackson', 'jack.jackson@example.com', '/wwwroot/images/users/jack.jpg', 'Tech enthusiast.', 21),
(13,'Katherine', 'White', 'katherine.white@example.com', '/wwwroot/images/users/katherine.jpg', 'Bookworm.', 21),
(14,'Liam', 'Harris', 'liam.harris@example.com', '/wwwroot/images/users/liam.jpg', 'Sports fanatic.', 21),
(15,'Mia', 'Martin', 'mia.martin@example.com', '/wwwroot/images/users/mia.jpg', 'Loves challenges.', 21),
(16,'Noah', 'Thompson', 'noah.thompson@example.com', '/wwwroot/images/users/noah.jpg', 'Math genius.', 21),
(17,'Olivia', 'Garcia', 'olivia.garcia@example.com', '/wwwroot/images/users/olivia.jpg', 'Aspiring artist.', 21),
(18,'Paul', 'Martinez', 'paul.martinez@example.com', '/wwwroot/images/users/paul.jpg', 'Enjoys coding.', 21),
(19,'Quinn', 'Robinson', 'quinn.robinson@example.com', '/wwwroot/images/users/quinn.jpg', 'Future engineer.', 21),
(20,'Rachel', 'Clark', 'rachel.clark@example.com', '/wwwroot/images/users/rachel.jpg', 'Socially active.', 21);
 
GO
INSERT INTO [AspNetUsers] (Id, FirstName, LastName, Email, ImagePath, Description, RoleId)
VALUES --this inserts 2 teachers and an admin
(21, 'Sarah', 'Parker', 'sarah.parker@example.com', '/wwwroot/images/users/sarah.jpg', 'History teacher.', 22),
(22, 'Thomas', 'Lewis', 'thomas.lewis@example.com', '/wwwroot/images/users/thomas.jpg', 'Mathematics teacher.', 22),
(23, 'William', 'Hall', 'william.hall@example.com', '/wwwroot/images/users/william.jpg', 'Principal.', 23);
 
GO
INSERT INTO Election (ElectionTitle, StartDate, EndDate, Status)
VALUES --this inserts a fake election with a data range
('Prefect Election 2025', '2025-04-01', '2025-04-07', 0),
('Student Council Election', '2025-05-01', '2025-05-07', 0);
 
GO
INSERT INTO Votes (VoterId, ReceiverId, ElectionId, Timestamp)
VALUES 
(1, 5, 1, GETDATE()), -- John votes for Charlie
(2, 6, 1, GETDATE()), -- Jane votes for David
(3, 7, 1, GETDATE()), -- Alice votes for Emma
(4, 8, 1, GETDATE()), -- Bob votes for Frank
(5, 9, 1, GETDATE()), -- Charlie votes for Grace
(6, 10, 1, GETDATE()), -- David votes for Henry
(7, 11, 1, GETDATE()), -- Emma votes for Isabella
(8, 12, 1, GETDATE()), -- Frank votes for Jack
(9, 13, 1, GETDATE()), -- Grace votes for Katherine
(10, 14, 1, GETDATE()), -- Henry votes for Liam
(11, 15, 1, GETDATE()), -- Isabella votes for Mia
(12, 16, 1, GETDATE()), -- Jack votes for Noah
(13, 17, 1, GETDATE()), -- Katherine votes for Olivia
(14, 18, 1, GETDATE()), -- Liam votes for Paul
(15, 19, 1, GETDATE()), -- Mia votes for Quinn
(16, 20, 1, GETDATE()); -- Noah votes for Rachel
 

INSERT INTO AuditLog (VoteId, UserId, Action, Timestamp)
VALUES --this inserts some logs based on the votes casted above
(1, 1, 0, GETDATE()), -- John created a vote
(2, 2, 0, GETDATE()), -- Jane created a vote
(3, 3, 0, GETDATE()), -- Alice created a vote
(4, 4, 0, GETDATE()), -- Bob created a vote
(5, 5, 0, GETDATE()), -- Charlie created a vote
(6, 6, 0, GETDATE()), -- David created a vote
(7, 7, 0, GETDATE()), -- Emma created a vote
(8, 8, 0, GETDATE()), -- Frank created a vote
(9, 9, 0, GETDATE()), -- Grace created a vote
(10, 10, 0, GETDATE()), -- Henry created a vote
(11, 11, 0, GETDATE()), -- Isabella created a vote
(12, 12, 0, GETDATE()), -- Jack created a vote
(13, 13, 0, GETDATE()), -- Katherine created a vote
(14, 14, 0, GETDATE()), -- Liam created a vote
(15, 15, 0, GETDATE()), -- Mia created a vote
(16, 16, 0, GETDATE()); -- Noah created a vote