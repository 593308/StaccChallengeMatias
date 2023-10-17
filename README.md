## Task description
For this project, I focused on creating a backend system to make the process of saving money more engaging and interactive for the users. The primary goal was to help users save money in a fun way. The backend includes functionality like:
-Authentication (registration and login)
-Sending, accepting and declining friend requests
-Tracking individual savings and total savings.
-Achieving milestones set by administrators based on savings.
-Receiving notifications about milestones reached and friend requests.
-Comparing savings with friends in the system

## How to run
It can't be run in a traditional sense. However, the code is there to be explored, also, it shoud be possible to explore the API endpoints using tools like postman. (I have not done this myself)

To setup project:
1. navigate to the project directory
2. Run the command 'dotnet restore' to restore necessary packages.
3. Use 'dotnet run' to start the application

## Comments
-The project is only the back-end component of the desired application. There is no front-end to visualize the functionalities.
-The database operations are set up using Entity Framework Core, but i faced challenges setting up the database. The application can't therefore be fully tested in it's current state.
-The design choices were primarely made to ensure scalability and modularity. With dividing up the classes so they had single responsibility. Also, each functionality is encapsulated in its controller, making it easier to expand in the future.
-I chose to use Data Transfer Objects, to ensure that only necessary data is transferred between operations, enhancing security and performance.
-Future changes would be to include APIs for added functionality, and implementing gamification elements.
-Many of the endpoint operations has hardcoded userId for testing, but I were unable to test it due to challenges with setting up a database for the project..
