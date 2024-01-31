# GymCard

A basic WebApi Project to manage invoices and track users

## Technology used
* ASP.NET CORE 5.0
* EF core
* Postgres SQL Database Server
* Docker
* Sql Server 2019 --Initial database used locally for testing

### Choices in framework, language and architecture
- this project used .Netcore Framework and c# as its language to develope the web service as its a famous framework for building fast and effecient web api's in the world also my favorite backend framework. the project is build with repository pattern with MVC plus REST architecture to makes it easier to test application logic and provide a method of sharing content for different audiences etc...

## Getting Started 

### Clone the repository

```
git clone https://github.com/Tharusara/GymCardActivationProj.git
```
### Prerequisites

* Doker Desktop
* .NET Core SDK 5.0.17 or (above)latest (Optional)  -- to run the project without using docker container

### Run the project
* setting up the database
    first run the docker desktop app and open cmd/terminal and run the following command to run postgresSql database server,


```
docker run --name dev_Db -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=secret -p 5432:5432 -d postgres:latest
```

* Now check the docker desktop app to see if the server is running on port: 5432 with name: devDb
Next,

### Installing API project
- run the following command to create the docker image for the Webapi


```
docker build -t GymCard_devtask-image -f Dockerfile .
```

- Before running the container run the following commands to configure DB settings  and make sure to be in root directory where you can run "dotnet run" command to check web api if dotnet sdk were installed.


```
cd GymCardDevTask\GymCardDevTask
docker cp pg_hba.conf dev_Db:/var/lib/postgresql/data/
docker cp postgresql.conf dev_Db:/var/lib/postgresql/data/postgresql.conf
```


- Now run following docker cammand to create the container,


```
docker run -d -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 --name webapi GymCard_devtask-image
```

- Now navigate to http://localhost:5000/swagger/index.html in browser and make a get request on invoice endpoint to see if everything is working. 

### //
- If the api throws and error its basically becuase of the postgresSql defualt behaviour of only accepting request from localhost(only allow to connect locally). you can see the error from docker desktop app container Logs. so to work around this first we can try changeing the app container to use the host port when running the app by running the following command,


```
docker run -d -p 5002:5002 -p 5003:5003  --network=host -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 --name webapihost GymCard_devtask-image
```

- You can see there will be another container created with the name of 'webapihost' and it doesn't throw any errors in the logs as it is using the host address. however now it will be challenging to find the port number to use the webapi endpoints.

- so another work around is to locally change the config files(pg_hba.conf, postgresql.conf) of postgresql db to grant access other networks to use the db.

* navigate to the postgresql.conf locally and change above variable,


```
listen_addresses = '*'
```

* navigate to the pg_hba.conf locally and change above variable,


```
# IPv4 local connections:   
host    all             all               0.0.0.0/0             md5
```

- Now restart the postgresql server and then webapi container from docker desktop and see if the error replicating or not. if can't find the service URl create a ssl certificate locally for the project to use HTTPS then it might resolve the issue or if thats the case need to create a custom postgresql image using docker with the above settings applied.

- If tried everything and nothing works! simply follow the below steps,
* shutdown the web api containers but keep the db container running on docker desktop
* cd into the GymCardDevTask\GymCardDevTask and run the following command,


```
dotnet published\GymCardDevTask.dll
```


- this command will run the api that is been published and will use the postgresql instance that is running on docker as the db so don't shutdown the db container.

* Note: In order to run the dotnet command .Net SDK is required.


### Swagger URl of API Documentation
- eg: http://localhost:5000/swagger/index.html
```
https://localhost:{your_Running_port}/swagger/index.html

```
### run the tests cases 

- cd into the test project and run "Dotnet test" (I would prefer to run my tests in Visual studio as it is easier to test and debug code); 


### Credentials

### Admin User
* POSTGRES_USER: admin
* POSTGRES_PASSWORD: secret

### Features

- An Invoice has a date, status, description, amount and invoice lines
- An Invoice state can have these values: Outstanding, Paid, Void
- An Invoice can have many lines. Each line should contain an amount and a description
- A Membership is associated with a user
- A Membership can be Active or Cancelled
- A Membership has an amount of credits and a state (start date and end date)
- A User can check-in to a club. When they do, their membership gets 1 credit subtracted
- A User cannot check-in and use credits if their membership is canceled
- A User cannot check-in if they have no credits available or if the membership’s end date
is reached
- allows users to checkin to fitness clubs.
- When the user checks in, an invoice line gets created for the month’s invoice. If the
invoice doesn’t exist, it gets created

### Future improvements

- Authentication & Authorization