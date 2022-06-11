INSERT INTO Activity(ScoreValue, NumOfQuestions, NumOfCorrectAnswers, NumOfIncorrectAnswers, AverageAttempts, Date) 
VALUES 
(10022, 11, 11, 9, 1.55, '2019-11-22'),
(19002, 9, 11, 0, 2.88, '2019-11-30'),
(30000, 20, 10, 10, 3.99, '2019-12-1'),
(10984, 14, 6, 10, 1, '2019-12-25'),
(344, 3, 3, 14, 9.0, '2019-12-10'),
(560, 7, 13, 2, 3.22, '2020-01-1'),
(720, 23, 4, 24, 2.44, '2020-01-22'),
(14000, 29, 3, 29, 1.33, '2020-01-30')
;

DELETE FROM Activity
WHERE ActivityID > 13;


INSERT INTO PlayerActivity(ActivityID, PlayerID) VALUES
(13, 1),
(14, 1),
(15, 1),
(16, 1),
(17, 1),
(18, 1),
(19, 1),
(20, 1),
(21, 1)
;