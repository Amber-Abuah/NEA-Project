-- Create player table
CREATE TABLE Player(
PlayerID INTEGER PRIMARY KEY,
Username VARCHAR(20),
Password VARCHAR (20),
FirstName VARCHAR(15),
LastName VARCHAR(15)
);

-- Add values into player table
INSERT INTO Player(Username, Password, Firstname, Lastname)
VALUES
('AmyAdam', 'Passw_ord1*', 'Amelia', 'Adam'),
('Cat_Coco', '_00_7*', 'Coco', 'Cat'),
('Kitty23', 'Cat_34*', 'Catherine', 'Kelly'),
('Ellie_Daria', 'Pass-01', 'Ellie', 'Daria'),
('Lili-05', 'Lily_-Flower', 'Lili', 'Smith'),
('Olivia43', '*Rita_71*', 'Olivia', 'Daria');

-- Create activity table
CREATE TABLE Activity(
ActivityID INTEGER PRIMARY KEY,
ScoreValue INTEGER,
NumOfQuestions INTEGER,
NumOfCorrectAnswers INTEGER,
NumOfIncorrectAnswers INTEGER,
AverageAttempts REAL,
Date VARCHAR(10) -- In the format "2019-10-30", "YYYY-MM-DD"
);

ALTER TABLE Activity
ADD NumOfCorrectAnswers INTEGER;

ALTER TABLE PlayerActivity
ADD RevisionTopicID INTEGER
FOREIGN KEY (RevisionTopicID) REFERENCES RevisonTopics(RevisionTopicID);

ALTER TABLE Activity
DELETE NumOfCorrectAnswers;


--SELECT DATE('2019-10-30', '+1 day');

-- Insert activity data
INSERT INTO Activity(ScoreValue, NumOfQuestions, NumOfIncorrectAnswers, NumoOAverageAttempts, Date)
VALUES 
(12364, 7, 2, 1.3, '2019-11-13'),
(1675, 3, 0, 1, '2019-10-18'),
(908, 4, 2, 3.2, '2019-09-30'),
(40532, 5, 2, 2.1, '2019-12-07'),
(30000, 2, 3, 3.9, '2019-01-11');

-- Create table with items
CREATE TABLE Item(
ItemID INTEGER PRIMARY KEY,
ItemName VARCHAR(20),
ItemEffect VARCHAR(50),
ItemValue INTEGER
);

-- Insert items into item table
INSERT INTO Item(ItemName, ItemEffect, ItemValue)
VALUES
('Booster', 'Allows the player to move at double their speed.', 20),
('Bonus Time', 'Gives the player an extra two minutes to answer the question', 10),
('Pause Time', 'Temporarily pauses the countdown timer.', 40),
('Lesser Time', 'Subducts 2 minutes from the countdown timer.', -10),
('Slow', 'Forces the player to move at half their current speed.', -20)
;

-- Create table that holds all users scores
CREATE TABLE PlayerActivity(
ActivityID INTEGER,
PlayerID INTEGER,
RevisionTopicID INTEGER,
FOREIGN KEY (ActivityID) REFERENCES Activity(ActivityID),
FOREIGN KEY (PlayerID) REFERENCES Player(PlayerID),
FOREIGN KEY (RevisionTopicID) REFERENCES RevisonTopics(RevisionTopicID),
PRIMARY KEY (ActivityID, PlayerID, RevisionTopicID) -- Composite primary key of the Score's ID and the User ID
);

DROP TABLE PlayerActivity;

-- Insert user Id and the ID of their score
INSERT INTO PlayerActivity(ActivityID, PlayerID)
VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 1),
(5, 2);

-- Create table that holds all users items 
CREATE TABLE PlayerItem(
ItemID INTEGER,
PlayerID INTEGER,
FOREIGN KEY (ItemID) REFERENCES Item(ItemID),
FOREIGN KEY (PlayerID) REFERENCES Player(PlayerID),
PRIMARY KEY (ItemID, PlayerID) -- Composite primary key of Item's ID and the User's ID
);


-- Insert itemID and the ID of the user that owns it
INSERT INTO PlayerItem(ItemID, PlayerID)
VALUES
(1, 1),
(2, 1),
(3, 1),
(2, 2),
(1, 3),
(2, 3);

-- Displays all users
SELECT Username
From Player;

-- Displays the name of all items and their effects
SELECT ItemName, ItemEffect
FROM Item;

-- Displays all scores of all players, from their highest to their lowest score
SELECT Username, ScoreValue
FROM Player, PlayerActivity, Activity
WHERE Player.PlayerID = PlayerActivity.PlayerID
AND Activity.ActivityID = PlayerActivity.ActivityID
ORDER BY Player.PlayerID, Activity.ScoreValue DESC;

-- Displays all users and their current items
SELECT Username, ItemName
FROM Player, PlayerItem, Item
WHERE Player.PlayerID = PlayerItem.PlayerID
AND Item.ItemID = PlayerItem.ItemID
ORDER BY Player.PlayerID;

-- Show top 3 scores and the names of the people who achieved them
SELECT ScoreValue, FirstName
FROM Player, PlayerActivity, Activity
WHERE Player.PlayerID = PlayerActivity.PlayerID
AND Activity.ActivityID = PlayerActivity.ActivityID
ORDER BY ScoreValue DESC
LIMIT 3;

-- Show top 3 scores and the names of the people who achieved them
SELECT ScoreValue, FirstName
FROM Player, PlayerActivity, Activity
WHERE Player.PlayerID =  1 AND PlayerActivity.PlayerID = 1
AND Activity.ActivityID = PlayerActivity.ActivityID
ORDER BY Date DESC;

SELECT ScoreValue, PlayerID
FROM Player, PlayerActivity, Activity
WHERE Player.PlayerID = PlayerActivity.PlayerID
AND Activity.ActivityID = PlayerActivity.ActivityID
WITH PlayerID = 3;

-- Insert new data into the database 
SELECT ActivityID 
FROM Activity
ORDER BY ActivityID DESC
LIMIT 1;
-- Get the largest value of the activity ID's, needed

SELECT PlayerID
FROM Player
WHERE Username = 'AmyAdam'; -- Get the id asscioted with use

INSERT INTO Activity(ScoreValue, NumOfAnswers, NumOfIncorrectQuestions, AverageAttempts, Date)
VALUES 
(,,,);

SELECT PlayerID FROM Player WHERE Username = 'Cat_Coco';

SELECT ActivityID FROM Activity ORDER BY ActivityID DESC LIMIT 1;

SELECT 1; SELECT 2;

CREATE TABLE RevisionTopics(
RevisionTopicID INTEGER PRIMARY KEY,
RevisionTopicName VARCHAR(20)
);

CREATE TABLE RevisionLinks(
RevisionLinkID INTEGER PRIMARY KEY,
RevisionLinkAddress VARCHAR(50),
RevisionTopicID INTEGER,
FOREIGN KEY (RevisionTopicID) REFERENCES RevisionTopics(RevisionTopicID)
);

CREATE TABLE PlayerRevisionLinks(
PlayerID INTEGER,
RevisionLinkID INTEGER,
FOREIGN KEY (PlayerID) REFERENCES Player(PlayerID)
FOREIGN KEY (RevisionLinkID) REFERENCES RevisionLinks(RevisionLinkID)
PRIMARY KEY (PlayerID, RevisionLinkID)
);

INSERT INTO RevisionTopics (RevisionTopicName) VALUES
('Logic Gates'),
('Binary'),
('Boolean Algebra'),
('Sound'),
('Data Representation');

-- logic Gate Revision
INSERT INTO RevisionLinks(RevisionLinkAddress, RevisionTopicID) VALUES
('https://pmt.physicsandmathstutor.com/download/Computer-Science/A-level/Notes/AQA/06-Fundamentals-of-Computer-Systems/Intermediate/6.4.%20Logic%20Gates.pdf', 1),
('http://www.teach-ict.com/2016/A_Level_Computing/OCR_H446/1_4_data_types_structures_algorithms/143_boolean_algebra/building/miniweb/pg2.php',1),
('https://www.youtube.com/watch?v=41Xa1hDl5K0', 1)
;

-- Binary

INSERT INTO RevisionLinks(RevisionLinkAddress, RevisionTopicID) VALUES
('https://www.teach-ict.com/as_as_computing/ocr/H447/F453/3_3_4/floating_point/miniweb/pg3.htm', 2),
('https://www.youtube.com/watch?v=Ce6Qc2c8XrY', 2),
('https://www.youtube.com/watch?v=B37AbukLYdk', 2);

-- Boolean Algebra Links

INSERT INTO RevisionLinks(RevisionLinkAddress, RevisionTopicID) VALUES
('https://www.cambridgeinternational.org/Images/285024-topic-3.3.2-boolean-algebra-9608-.pdf', 3),
('https://www.youtube.com/watch?v=1tEkjOcCAMU', 3),
('https://filestore2.aqa.org.uk/resources/computing/AQA-7516-7517-TG-BA.PDF', 3);

-- Sound

INSERT INTO RevisionLinks(RevisionLinkAddress, RevisionTopicID) VALUES
('https://www.youtube.com/watch?v=HlOTuCFtuV8', 4),
('https://www.youtube.com/watch?v=al5HsKIRhQw', 4),
('https://teachcomputerscience.com/sound-representation/', 4);

-- Data Representation

INSERT INTO RevisionLinks(RevisionLinkAddress, RevisionTopicID) VALUES
('https://en.wikibooks.org/wiki/A-level_Computing/AQA/Problem_Solving,_Programming,_Data_Representation_and_Practical_Exercise/Fundamentals_of_Data_Representation', 5),
('https://www.physicsandmathstutor.com/computer-science-revision/a-level-aqa/data-representation/', 5),
('https://www.cram.com/flashcards/aqa-computing-a-level-fundamentals-of-data-representation-8527277', 5);

DROP TABLE RevisionTopic;

DROP TABLE RevisionLinks;

DROP TABLE RevisionLinks;

SELECT RevisionLinkAddress 
FROM RevisionLinks
WHERE RevisionTopicID = 1;
