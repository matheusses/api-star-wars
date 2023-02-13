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

4. Open your web browser and navigate to http://localhost:5000/swagger/index.html to access the application.

   That's it! You should now have the project running on your local machine.

5. Anytime to access the logs:

   `docker exec -it api bash`

   `cat logs.txt`

### Usage

1. Open your web browser and navigate to http://localhost:5000/swagger/index.html. \
   The API has four endpoints:

2. Load Planet by External API: \
   - Click on the "**Try it out**" button
   - Enter the **planet ID**
   - Click "**Execute**"
  
3. Get All Planets:
   - Click on the "**Try it out**" button
   - Click "**Execute**"

4. Get Planet by ID:\
   - Click on the "**Try it out**" button
   - Enter the **planet ID**
   - Click "**Execute**"

5. Delete Planet:\
   - Click on the "**Try it out**" button
   - Enter the **planet ID**
   - Click "**Execute**"

6. Get Planet by Name: \
   - Click on the "**Try it out**" button
   - Enter the **planet name**
   - Click "**Execute**"

Response Format:

```
  {
    "success": bool,
    "errors": [],
    "httpStatusCode": status_code,
    "message": string,
    "data": {}
  }
```

HTTP Status Codes:

- **200** for successful requests.
- **404** for not found results.
- **400** for general validation errors.
- **500** for unexpected errors.

Return Examples:
```
{
  "success": true,
  "errors": [],
  "httpStatusCode": 200,
  "message": null,
  "data": {
    "id": 1,
    "name": "Tatooine",
    "climate": "arid",
    "terrain": "desert",
    "films": [
      {
        "title": "A New Hope",
        "director": "George Lucas",
        "releaseDate": "1977-05-25"
      },
      ...
    ]
  }
}

```

```
{
  "success": false,
  "errors": [],
  "httpStatusCode": 404,
  "message": "Planet not found",
  "data": null
}

```

### Running the tests

The API Star Wars project uses automated unit tests, which are triggered by a push to the `master` branch on GitHub. To access the test results, follow these steps:

1 . Go to the GitHub Actions page for the API Star Wars project: *https://github.com/matheusses/api-star-wars/actions*

![image](https://user-images.githubusercontent.com/1146846/218357442-6595881d-bdb5-432a-8c59-424954d40c27.png)


2 . Find the workflow for the latest push to the `master` branch and click on it.

![image](https://user-images.githubusercontent.com/1146846/218357546-564bdee9-6c61-4d14-921b-c024164f4359.png)


3. Scroll to the bottom of the page to the "Artifacts" section. Click on the "result-test" link to download the report.

![image](https://user-images.githubusercontent.com/1146846/218356245-a648ab30-3568-4ecf-9c6b-2beaaade4556.png)

4. Extract the `result-test.zip` file in a desired location.

5. To access the coverage repor :
   - Go to the `result-test` directory
   - Enter the directory with the `uuid` name format
   - Go to the `coveragereport` directory
   - Click on the `index.html` file
6. The coverage report will open in a web browser, where you can interact with the results

![image](https://user-images.githubusercontent.com/1146846/218357272-90a153ac-ea17-419d-af07-0defd01fc44f.png)
    



