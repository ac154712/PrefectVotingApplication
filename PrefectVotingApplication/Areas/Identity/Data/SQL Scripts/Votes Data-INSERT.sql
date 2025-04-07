INSERT INTO [Election] (ElectionTitle, StartDate, EndDate, Status)
VALUES --this inserts a fake election with a data range
('Prefect Election 2025', '2025-04-01', '2025-04-07', 0),
('Student Council Election', '2025-05-01', '2025-05-07', 0);
 
GO
INSERT INTO [Votes] (VoterId, ReceiverId, ElectionId, Timestamp)
VALUES
('1', '5', 1, getdate()),     -- john votes for charlie
('2', '5', 1, getdate()),     -- jane votes for charlie
('3', '5', 1, getdate()),     -- alice votes for charlie
('4', '6', 1, getdate()),     -- bob votes for david
('5', '6', 1, getdate()),     -- charlie votes for david
('6', '7', 1, getdate()),     -- david votes for emma
('7', '5', 1, getdate()),     -- emma votes for charlie again
('8', '8', 1, getdate()),     -- frank votes for frank (self-vote maybe?)
('9', '7', 1, getdate()),     -- grace votes for emma
('10', '9', 1, getdate()),    -- henry votes for grace
('11', '9', 1, getdate()),    -- isabella votes for grace
('12', '10', 1, getdate()),   -- jack votes for henry
('13', '10', 1, getdate()),   -- katherine votes for henry again
('14', '10', 1, getdate()),   -- liam votes for henry again again
('15', '11', 1, getdate()),   -- mia votes for isabella
('16', '12', 1, getdate()),   -- noah votes for jack
('1', '13', 1, getdate()),    -- john votes for katherine
('2', '14', 1, getdate()),    -- jane votes for liam
('3', '14', 1, getdate()),    -- alice votes for liam again
('4', '14', 1, getdate()),    -- bob votes for liam again again
('5', '15', 1, getdate()),    -- charlie votes for mia
('6', '16', 1, getdate()),    -- david votes for noah
('7', '16', 1, getdate()),    -- emma votes for noah again
('8', '16', 1, getdate()),    -- frank votes for noah again again
('9', '17', 1, getdate()),    -- grace votes for olivia
('10', '18', 1, getdate()),   -- henry votes for paul
('11', '18', 1, getdate()),   -- isabella votes for paul again
('12', '19', 1, getdate()),   -- jack votes for quinn
('13', '20', 1, getdate());   -- katherine votes for rachel

GO
INSERT INTO [AuditLog] (VoteId, UserId, Action, Timestamp)
VALUES
(1, 1, 0, getdate()),   -- john created vote
(2, 2, 0, getdate()),   -- jane created vote
(3, 3, 0, getdate()),   -- alice created vote
(4, 4, 0, getdate()),   -- bob created vote
(5, 5, 0, getdate()),   -- charlie created vote
(6, 6, 0, getdate()),   -- david created vote
(7, 7, 0, getdate()),   -- emma created vote
(8, 8, 0, getdate()),   -- frank created vote
(9, 9, 0, getdate()),   -- grace created vote
(10, 10, 0, getdate()), -- henry created vote
(11, 11, 0, getdate()), -- isabella created vote
(12, 12, 0, getdate()), -- jack created vote
(13, 13, 0, getdate()), -- katherine created vote
(14, 14, 0, getdate()), -- liam created vote
(15, 15, 0, getdate()), -- mia created vote
(16, 16, 0, getdate()), -- noah created vote
(17, 1, 0, getdate()),  -- john again
(18, 2, 0, getdate()),  -- jane again
(19, 3, 0, getdate()),  -- alice again
(20, 4, 0, getdate()),  -- bob again
(21, 5, 0, getdate()),  -- charlie again
(22, 6, 0, getdate()),  -- david again
(23, 7, 0, getdate()),  -- emma again
(24, 8, 0, getdate()),  -- frank again
(25, 9, 0, getdate()),  -- grace again
(26, 10, 0, getdate()), -- henry again
(27, 11, 0, getdate()), -- isabella again
(28, 12, 0, getdate()), -- jack again
(29, 13, 0, getdate()); -- katherine again
