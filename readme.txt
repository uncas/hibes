hibes (HIstory Based EStimation)
----

Contents
----
1) Introduction
2) Quick start
3) Files
4) Requirements


1) Introduction
----
This application lets the user enter estimates and elapsed times for
tasks. It then uses the estimates from completed tasks to evaluate
possible completion dates for future tasks.

The user interface is a website. It is created with ASP.NET,
programmed in C#, and the data can be stored in a Microsoft Sql Server
2005+ database.


2) Quick start
----
To setup website and database on a local machine:
- Download the source code.
- Install the requirements (see section 4).
- Configure a new website with IIS Manager (e.g. c:\inetpub\wwwroot\hibes)
- Copy local.properties.xml.template to local.properties.xml.
- Open local.properties.xml and edit and save the following:
    sqlServer: the server (e.g. . or .\SqlExpress)
    database: the name of the database (e.g. hibes)
    testFolder: the physical path to the IIS website from above
- Run ClickToSetupDatabase.bat
- Run ClickToPublishToTest.bat


3) Files
----
/config:
    Templates for configuration files

/doc:
    - Spec1Functional.txt: Some functional specs
    - Spec2Design.txt: Some notes about feature ideas and architecture
    - some scripts to extract data from the web application, 
      create pdf files, and send with email (not finished/tested yet)

/sql:
    Database schema scripts (tested on Microsoft Sql Server 2005 and 2008):
    To setup the database for the web application:
      1) create a new database
      2) run the scripts 
           schema-001.0000-base.sql, schema-001.0001-change.sql, etc.
         on the new database, which creates tables, 
         seed data and indexes from scratch

/src:
    The source code for the web application (C#, ASP.NET):
    User interface, simulation engine, data handling, and tests (NUnit).

hibes.sln:
    The Visual Studio 2008 solution file

hibes.build:
    NAnt build scripts used for batch files and command-line.

Batch files:
    The batch files function as short cuts to common tasks like compiling,
    testing and publishing

    The batch files need NAnt to run:
    The path to NAnt.exe is set in build.bat

    For database setup, integration testing and publishing
    some properties need to be defined
    by copying local.properties.xml.template to
    local.properties.xml and editing or adding values.

    ClickToClean.bat: Removes build products
    ClickToCompile.bat: Compiles the solution
    ClickToSetupDatabase.bat: Sets up the database
    ClickToTest.bat: Compiles and tests (unit+integration) the application
    ClickToPublishToTest.bat: Publishes to test
    ClickToPublishToProduction.bat: Publishes to production
    
Command-line:
    It is also possible to run the scripts above, and more, 
    from the command line:

    build [command]

    where command can be:
      clean (see above)
      compile (see above)
      unitTest: compiles and unit tests the application
      getBuildProducts: 
          produces web site output in the folder build\WebSite
    and the following, which depend on local properties:
      setupDatabase (see above)
      test (see above)
      publishToTest (see above)
      publishToProduction (see above)


4) Requirements for compilation / development
----
The following are required to compile and test the solution:

    .NET 3.5 SP1

    Microsoft Sql Server (Only for database setup and integration
    tests. The scripts have been tested with Sql Server 2005 and
    2008. Free 'Express' versions are available here:
    http://www.microsoft.com/Sqlserver/2005/en/us/express.aspx and
    http://www.microsoft.com/express/sql.)

    ASP.NET Charting control (Only for UI. Can be found here:
    http://www.microsoft.com/downloads/details.aspx?FamilyID=130f7986-bf49-4fe5-9ca8-910ae6ea442c.
    For more info, see this blog post:
    http://weblogs.asp.net/scottgu/archive/2008/11/24/new-asp-net-charting-control-lt-asp-chart-runat-quot-server-quot-gt.aspx.)

    NUnit (Only for unit and integration tests. Here is used version
    2.4.8 with a reference in the project settings to the path
    C:\Program Files\NUnit 2.4.8\bin\nunit.framework.dll, so that has
    to be modified for other versions. Can be found here:
    http://www.nunit.org.)

The following is required for the batch files and build scripts:

    NAnt (Can be found here: http://nant.sourceforge.net.)


Cheers,
Ole Lynge Sørensen
