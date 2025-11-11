using AgroindustryManagement.Services.App;

var databaseContext = new AgroindustryManagement.Services.Database.AGDatabaseContext();
var runLoop = new AGApplication(databaseContext);

runLoop.Start();