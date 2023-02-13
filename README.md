# API Start Wars
 
The API Star Wars provides a comprehensive database of information from the Star Wars universe, including information about planets and the films in which they appear, some data are gotten from the external [public API](https://swapi.dev). The API is built using .Net 7 as the programming language, MongoDB as the NoSQL database management system, Swagger for API documentation, Serilog for log generation, and Polly for resilience and transient-fault-handling.

Each planet in the database is characterized by its Name, Climate, and Terrain, and can be associated with multiple films. The films are recorded with their Name, Director, and Release Date. Some informations is gotten from the external API To get started with the API, users need to have Docker and Docker Compose installed on their local machine. The installation process involves cloning the repository, changing into the project directory, and starting the Docker containers. Once the containers are running, the API can be accessed through a web browser at http://localhost:5000/swagger/index.html. Logs can be accessed at any time by executing a command in the Docker container.


## Built With

* [.Net 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) - The programming language used
* [MongoDB](https://www.mongodb.com/docs/drivers/csharp/current/) - The NoSql database management system used
* [Swagger](https://swagger.io/) - The API Documentation
* [Serilog](https://serilog.net/) - The framework to generate logs used. 
* [Polly ](http://www.thepollyproject.org/) - The framework to provide resilience and transient-fault-handling used. 

## Getting Started
 
 These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.
 
 
### Prerequisites

- [Docker](https://docs.docker.com/engine/install/ubuntu/)
- [Docker Compose](https://docs.docker.com/compose/)

### Installing

1. Clone the repository to you local machine: 

   `git clone git@github.com:matheusses/api-star-wars.git`

2. Change into the project directory:

   `cd api-star-wars/src`

3. Run the following command to start the containers:

   `docker-compose up`

4. Open your web browser and navigate to `http://localhost:5000/swagger/index.html` to access the application.

   That's it! You should now have the project running on your local machine.

5. Anytime to access the logs:

   `docker exec -it api bash`

   `cat logs.txt`

### Usage

1. Open your web browser and navigate to `http://localhost:5000/swagger/index.html` 
   API has four endpoints:

![image](https://user-images.githubusercontent.com/1146846/218358600-d796f804-ed1c-418c-8256-161e5b6610a0.png)

2. Load planet by external API
![image](https://user-images.githubusercontent.com/1146846/218358986-3787c5ef-e2e0-46c9-95f5-366755838860.png)
   - Click on the Try it out
   - Inform the planet id
   - Click Execute

3. Get all planets
![image](https://user-images.githubusercontent.com/1146846/218359905-f21ca329-dd2c-4b90-aaf4-85e5b0da397e.png)
   - Click on the Try it out
   - Click Execute

4. Get planet by id
![image](https://user-images.githubusercontent.com/1146846/218360078-ee3e1c26-f16d-44eb-949f-6679912d22cd.png)
   - Click on the Try it out
   - Inform the planet id
   - Click Execute

5. Delete planet
![image](https://user-images.githubusercontent.com/1146846/218360198-7e3def96-ddd3-4c13-95a8-a0cc7607108d.png)
   - Click on the Try it out
   - Inform the planet id
   - Click Execute

6. Get planet by name
![image](https://user-images.githubusercontent.com/1146846/218360282-802235e1-7183-455f-8c23-49a2ffe2e642.png)
   - Click on the Try it out
   - Inform the planet name
   - Click Execute


### Running the tests

The automated unit test is using GitHub Actions triggered by a push to the master branch.

`git push origin` in the master branch triggered the github actions. It will generate report cover in the artifacts.

1 . Access link in the github actions `https://github.com/matheusses/api-star-wars/actions`

![image](https://user-images.githubusercontent.com/1146846/218357442-6595881d-bdb5-432a-8c59-424954d40c27.png)


2 . Click in the workflow.

![image](https://user-images.githubusercontent.com/1146846/218357546-564bdee9-6c61-4d14-921b-c024164f4359.png)


3. At final of the page has the Artifacts session, click on the result-test link to download report.

![image](https://user-images.githubusercontent.com/1146846/218356245-a648ab30-3568-4ecf-9c6b-2beaaade4556.png)

4. Access the download path and extact the result-test.zip

5. Access cover report :
   - Click on the result-test directory
   - Click on the `uuid` name format directory
   - Click on the coveragereport directory
   - Click on the index.html
6. It going to open the web page on the navigator and it is possivel interact with the web page

![image](https://user-images.githubusercontent.com/1146846/218357272-90a153ac-ea17-419d-af07-0defd01fc44f.png)
    



