using ShiftLoggerWebAPI;

var context = new ShiftDbContext();

context.Database.EnsureDeleted();
context.Database.EnsureCreated();
Console.ReadLine();