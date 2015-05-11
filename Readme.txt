1. Required Software:
	The develepoment was done in VS 2013 Update 4 in Windows 8.1 pro operating system.
	A version of SQL Server 2012 or later is required.
2. Database Initialization
	In the Source folder in the root of the folder there is the DatabaseInitializetionScript.sql script.
	You need to execute this script in order to create the appropriated database schema and data.
	This script will create a database with name Scheduler and it's advisable not to chnage it.
	This script inserts some test commands that can be executed on the server.
3. Build the source code 
	Once you load the solution on VS it's highly advisible to first build it in order to trigger the nuget package restore
	and to verify that you can build it successfully.
	After successful build you need to change two connection strings for the server service and the web application.
	More specifically you need to change App.config file in Scheduler.Server folder and on line 11 change the Data Source property to the server name of your environment
	<add name="SchedulerConnection" connectionString="Data Source=ULTRABOOK;Initial Catalog=Scheduler;Integrated Security=True;MultipleActiveResultSets=true;" providerName="System.Data.SqlClient" />
	and the same in the Web.config file of Scheduler.Web project you have to update the Data Source property in line 12 to the server name of your environment
	<add name="SchedulerConnection" connectionString="Data Source=ULTRABOOK;Initial Catalog=Scheduler;Integrated Security=True;MultipleActiveResultSets=true;" providerName="System.Data.SqlClient" />
	If in the second step you chnaged the database name then you need to update the Initial Catalog property too.
	After doing this you can click debug start on Scheduler.Server console application which is the main service of the application and should be stated first.
	Afterwards you can click debug start for the Scheduler.Web to launch the web application which presents all the stored data and the clients.
	Then you can start multiple instanes of Scheduler.Client console application to simulate the various machines on the network that the server executes commands on.
4. Not implement requirements
	Due to lack of time several requirements was not implemented. Firstly there isn't any authentication or security to none of the subsystems.
	This is entirely wrong and a basic authentication mechanism should be part of the system. 
	Then the scheduled based task was not implemented at all.
	Dependency injection was not implemented on Scheduler Server. I tried to implement dependency injection on Server service but it came out that 
	is not straightforward  because of underlying SignalR technology that is used. I spend, probably, way too much time trying to solve this but I didn't make it work.
	I am pretty confident that this can be implemented but requires more investigation.
	The exception handling is absent from the application which is a major issue which should be addressed immediately.
	There are a lot dependencies to classes and not interfaces something that is not a good design for sure, especially for testing reasons.
5.General Feedback
	In general it's an interesting problem to solve but to complete all the requirements you need much more days. 
	My design document is not even close to consider complete but I am running out of time so I will upload the solution as it is now.
