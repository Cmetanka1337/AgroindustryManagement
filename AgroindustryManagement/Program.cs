using AgroindustryManagement.Services.App;
using AgroindustryManagement.Services.Database;

var databaseContext = new AGDatabaseContext();
databaseContext.Database.EnsureCreated();
var dbService = new AGDatabaseService(databaseContext);
var runLoop = new AGApplication(dbService);

runLoop.Start();