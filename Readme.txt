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