## Using T4 templates to generate an OData API

#### How It Works
.NET EF Core database scaffolding is used to create a model of the database and a database context. `dotnet ef dbcontext scaffold "$CONNECTION_STRING" Microsoft.EntityFrameworkCore.SqlServer --context "$DB_CONTEXT_NAME" --context-dir "$DB_CONTEXT_DIRECTORY" --output-dir "$ENTITY_MODELS_DIRECTORY"`. When generating classes with T4 templates, that scaffolded database context is passed to a `DbParser` class that, in conjunction with some other classes, uses reflection to create a `ControllerModel` for each entity on the database context. Those `ControllerModel`s are used for controller, repository, and proxy code generation. The controllers generated have an OData query route and basic CRUD endpoints.  The proxy classes can modify the data going in before it is saved by using attributes on the entities and their properties. These classes could be extended to modify data as it is accessed as well. In the current code this is not very flushed out, but custom attributes and proxies could likely be made to only expose certain entities and fields to certain roles.

#### Requirements
 - In order to run template generation on a T4 template that references a .NET Core assembly you MUST install and configure this Visual Studio Extension: https://github.com/RdJNL/TextTemplatingCore/releases/tag/v1.0.1
 - Once you have the extension installed, select the .tt file in the Solution Explorer, right click it and click properties. In the properties window, set Custom Tool to TextTemplatingFileGeneratorCore. 

#### Usage
 - To run the generation right click the .tt file in the Solution Explorer and click Run Custom Tool.
 - This will generate a .cs file under the .tt file that contains all of the generated code. Currently I am copying the generated code from that file to `Learning.DAL.Server.Generated` but I assume this can be automated in the future.
 - This is currently targeting a locally installed DB I obtained from https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2019.bak maybe we could work on getting an Azure instance.
 - You will have to modify the connection string (specified in `Learning.DAL.Server.Startup`) because it's currently machine specific.
 - Start the Learning.DAL.Server project and visit the '/odata'

#### TODO
 - Host database in Azure so all can access the same DB
 - Move connection string to configuration file
 - Figure out how to generate OData Controllers for Join Entities (Or any entity without PK)
 - Extend the Proxies to filter data that is being queried
 - Implement HMAC