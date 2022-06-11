# NEA Project
This Unity game was developed in 2019-2020 for a non-examined assessment in A Level Computer Science.  
The game is designed to help Computer Science students revise the following subject areas - Binary, Boolean Algebra, Data Representation, Logic Gates and Sound.

## Main Gameplay
The main component of the game is a racing type game. Here multiple questions appear along with three possible answers; all of this data is generated from the contents of a JSON file.

![image136](https://user-images.githubusercontent.com/107321078/173201991-835793ec-668b-43ac-ba44-e63a138f3a50.png)


There are three paths to choose from, where each path represents one of the answers. In order to answer, the student must move left or right and onto the path that they think represents the correct answer. 

Additionally there are extra gameplay elements such as a timer to stimulate the type of pressure they will have in an exam and items they can pick up to gain points and rewards. The game ends when the timer reaches 0, resulting in a game over, or when the student has answered all of the questions. Statistics generated from their play session, such as the percentage of correct answers and their score, is then written to a local sqlite database.

## Statistics
Several graphs are generated from the student's playtime statistics. One of the representations is a pentagon graph as shown below.

![image123](https://user-images.githubusercontent.com/107321078/173202202-2fd2004c-82a3-413b-8c85-283dcfa550f3.png)

Additionally along with the graph, the student also recieves written comments on their performance on each of the five areas.

Line graphs are also generated where the student can see their statistics based on their number of correct answers and scores for all 5 subject areas. Once again, the student also recieves written feedback on whether their performance is gradually improving or decreasing. In order to calculate this the Product Moment Correlation Coefficent formula is used.

![image66](https://user-images.githubusercontent.com/107321078/173202558-c7715967-5fbe-4974-9cc1-a97e6ed92358.png)
