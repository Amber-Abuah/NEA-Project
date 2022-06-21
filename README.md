# NEA Project
This Unity game was developed in 2019-2020 for a non-examined assessment in A Level Computer Science.  
The game is designed to help Computer Science students revise the following subject areas - Binary, Boolean Algebra, Data Representation, Logic Gates and Sound.

## Main Gameplay

![image136](https://user-images.githubusercontent.com/107321078/173201991-835793ec-668b-43ac-ba44-e63a138f3a50.png)

The main component of the game is a racing type game. Here multiple questions appear along with three possible answers.

There are three paths to choose from, where each path represents one of the answers. In order to answer, the student must move left or right and onto the path that they think represents the correct answer. 

Additionally there are extra gameplay elements such as a timer to stimulate the type of pressure they will have in an exam and items they can pick up to gain points and rewards. The game ends when the timer reaches 0, resulting in a game over, or when the student has answered all of the questions. Statistics generated from their play session, such as the percentage of correct answers and their score, is then written to a local sqlite database.

## Statistics
Several graphs are generated from the student's playtime statistics. One of the representations is a pentagon graph as shown below.

![image123](https://user-images.githubusercontent.com/107321078/173202202-2fd2004c-82a3-413b-8c85-283dcfa550f3.png)

Additionally along with the graph, the student also recieves written comments on their performance on each of the five areas.

Line graphs are also generated where the student can see their statistics based on their number of correct answers and scores for all 5 subject areas. The Product Moment Correlation Coefficent formula is used to produce written feedback on whether the student's performance is gradually improving or worsening.

![image66](https://user-images.githubusercontent.com/107321078/173202558-c7715967-5fbe-4974-9cc1-a97e6ed92358.png)

## Playing The Game
Navigate to NEA Project/Builds and run NEA Project.exe.  
Then create your own account or login with the following details -  
Username - AmyAdam  
Password - Passw_ord1*  

## Technologies Used
Game Engine - Unity (2019.3.14f1)  
Languages - C#, SQLite, JSON.  
Models - Ethan (Character Model) designed by Unity Technologies.  
Shader - MToon @Santarh  
