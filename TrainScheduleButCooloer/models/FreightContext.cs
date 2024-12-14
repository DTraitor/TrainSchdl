using System.ComponentModel;
using Microsoft.Data.SqlClient;

namespace TrainScheduleButCooloer.models;

public class FreightContext
{
    private readonly string connectionString = @"Server=localhost\SQLEXPRESS;Database=TrainCargoManagementSystem;TrustServerCertificate=True;Integrated Security=True;";

    public BindingList<Client> Clients { get; set; } = new BindingList<Client>();
    public BindingList<Cargo> Cargos { get; set; } = new BindingList<Cargo>();
    public BindingList<Route> Routes { get; set; } = new BindingList<Route>();
    public BindingList<Station> Stations { get; set; } = new BindingList<Station>();
    public BindingList<Schedule> Schedules { get; set; } = new BindingList<Schedule>();
    public BindingList<Train> Trains { get; set; } = new BindingList<Train>();
    public BindingList<Wagon> Wagons { get; set; } = new BindingList<Wagon>();
    public BindingList<Order> Orders { get; set; } = new BindingList<Order>();
    public BindingList<Shipment> Shipments { get; set; } = new BindingList<Shipment>();

    public FreightContext()
    {
        CreateTablesIfNotExists();
        LoadClients();
        LoadCargos();
        LoadStations();
        LoadRoutes();
        LoadTrains();
        LoadSchedules();
        LoadWagons();
        LoadOrders();
        LoadShipments();
    }

    private void CreateTablesIfNotExists()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var createClientsTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Clients' AND xtype='U')
            CREATE TABLE Clients (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Name NVARCHAR(100),
                ContactPerson NVARCHAR(100),
                PhoneNumber NVARCHAR(20),
                Email NVARCHAR(100),
                Address NVARCHAR(200),
                BillingInformation NVARCHAR(200)
            )";

        var createCargosTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Cargos' AND xtype='U')
            CREATE TABLE Cargos (
                CargoID INT IDENTITY(1,1) PRIMARY KEY,
                Description NVARCHAR(200),
                Weight FLOAT,
                Volume FLOAT
            )";

        var createRoutesTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Routes' AND xtype='U')
            CREATE TABLE Routes (
                RouteID INT IDENTITY(1,1) PRIMARY KEY,
                OriginStationId INT,
                DestinationStationId INT,
                Distance FLOAT,
                EstimatedTime NVARCHAR(50)
            )";

        var createStationsTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Stations' AND xtype='U')
            CREATE TABLE Stations (
                StationID INT IDENTITY(1,1) PRIMARY KEY,
                StationName NVARCHAR(100),
                Location NVARCHAR(200)
            )";

        var createSchedulesTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Schedules' AND xtype='U')
            CREATE TABLE Schedules (
                ScheduleID INT IDENTITY(1,1) PRIMARY KEY,
                TrainID INT,
                RouteID INT,
                DepartureTime DATETIME,
                ArrivalTime DATETIME,
                Date DATETIME
            )";

        var createTrainsTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Trains' AND xtype='U')
            CREATE TABLE Trains (
                TrainID INT IDENTITY(1,1) PRIMARY KEY,
                Name NVARCHAR(100),
                MaxCapacity INT
            )";

        var createWagonsTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Wagons' AND xtype='U')
            CREATE TABLE Wagons (
                WagonID INT IDENTITY(1,1) PRIMARY KEY,
                WagonType NVARCHAR(100),
                LoadCapacity INT,
                AssignedTrainID INT
            )";

        var createOrdersTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Orders' AND xtype='U')
            CREATE TABLE Orders (
                OrderID INT IDENTITY(1,1) PRIMARY KEY,
                ClientID INT,
                CargoID INT,
                RouteID INT,
                OrderDate DATETIME,
                Status NVARCHAR(50),
                TotalCost FLOAT
            )";

        var createShipmentsTable = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Shipments' AND xtype='U')
            CREATE TABLE Shipments (
                ShipmentID INT IDENTITY(1,1) PRIMARY KEY,
                OrderID INT,
                TrainID INT,
                WagonID INT,
                StartStationID INT,
                EndStationID INT,
                DepartureTime DATETIME,
                ArrivalTime DATETIME,
                Status NVARCHAR(50)
            )";

        using var command = connection.CreateCommand();
        command.CommandText = createClientsTable;
        command.ExecuteNonQuery();

        command.CommandText = createCargosTable;
        command.ExecuteNonQuery();

        command.CommandText = createRoutesTable;
        command.ExecuteNonQuery();

        command.CommandText = createStationsTable;
        command.ExecuteNonQuery();

        command.CommandText = createSchedulesTable;
        command.ExecuteNonQuery();

        command.CommandText = createTrainsTable;
        command.ExecuteNonQuery();

        command.CommandText = createWagonsTable;
        command.ExecuteNonQuery();

        command.CommandText = createOrdersTable;
        command.ExecuteNonQuery();

        command.CommandText = createShipmentsTable;
        command.ExecuteNonQuery();
    }

    private void LoadClients()
    {
        Clients.Clear();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Clients";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Clients.Add(new Client
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                ContactPerson = reader.GetString(2),
                PhoneNumber = reader.GetString(3),
                Email = reader.GetString(4),
                Address = reader.GetString(5),
                BillingInformation = reader.GetString(6),
            });
        }
    }

    private void LoadCargos()
    {
        Cargos.Clear();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Cargos";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Cargos.Add(new Cargo
            {
                CargoID = reader.GetInt32(0),
                Weight = reader.GetDecimal(1),
                Volume = reader.GetDecimal(2),
                Description = reader.GetString(3)
            });
        }
    }

    private void LoadRoutes()
    {
        Routes.Clear();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Routes";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Routes.Add(new Route
            {
                RouteID = reader.GetInt32(0),
                OriginStationId = reader.GetInt32(1),
                DestinationStationId = reader.GetInt32(2),
                Distance = reader.GetDecimal(3),
                EstimatedTime = reader.GetString(4),
                OriginStation = Stations.FirstOrDefault(s => s.StationID == reader.GetInt32(1)),
                DestinationStation = Stations.FirstOrDefault(s => s.StationID == reader.GetInt32(2))
            });
        }
    }

    private void LoadStations()
    {
        Stations.Clear();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Stations";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Stations.Add(new Station
            {
                StationID = reader.GetInt32(0),
                StationName = reader.GetString(1),
                Location = reader.GetString(2)
            });
        }
    }

    private void LoadSchedules()
    {
        Schedules.Clear();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Schedules";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Schedules.Add(new Schedule
            {
                ScheduleID = reader.GetInt32(0),
                TrainID = reader.GetInt32(1),
                RouteID = reader.GetInt32(2),
                DepartureTime = reader.GetDateTime(3),
                ArrivalTime = reader.GetDateTime(4),
                Date = reader.GetDateTime(5),
                Train = Trains.FirstOrDefault(t => t.TrainID == reader.GetInt32(1)),
                Route = Routes.FirstOrDefault(r => r.RouteID == reader.GetInt32(2))
            });
        }
    }

    private void LoadTrains()
    {
        Trains.Clear();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Trains";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Trains.Add(new Train
            {
                TrainID = reader.GetInt32(0),
                Name = reader.GetString(1),
                MaxCapacity = reader.GetDecimal(2)
            });
        }
    }

    private void LoadWagons()
    {
        Wagons.Clear();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Wagons";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Wagons.Add(new Wagon
            {
                WagonID = reader.GetInt32(0),
                WagonType = reader.GetString(1),
                LoadCapacity = reader.GetDecimal(2),
                CurrentStatus = reader.GetString(3),
                AssignedTrainID = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                AssignedTrain = reader.IsDBNull(4) ? null : Trains.FirstOrDefault(t => t.TrainID == reader.GetInt32(4))
            });
        }
    }

    private void LoadOrders()
    {
        Orders.Clear();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Orders";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Orders.Add(new Order
            {
                OrderID = reader.GetInt32(0),
                ClientID = reader.GetInt32(1),
                CargoID = reader.GetInt32(2),
                RouteID = reader.GetInt32(3),
                OrderDate = reader.GetDateTime(4),
                Status = reader.GetString(5),
                TotalCost = reader.GetDecimal(6),
                Client = Clients.FirstOrDefault(c => c.Id == reader.GetInt32(1)),
                Cargo = Cargos.FirstOrDefault(c => c.CargoID == reader.GetInt32(2)),
                Route = Routes.FirstOrDefault(r => r.RouteID == reader.GetInt32(3))
            });
        }
    }

    private void LoadShipments()
    {
        Shipments.Clear();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Shipments";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Shipments.Add(new Shipment
            {
                ShipmentID = reader.GetInt32(0),
                OrderID = reader.GetInt32(1),
                TrainID = reader.GetInt32(2),
                WagonID = reader.GetInt32(3),
                StartStationID = reader.GetInt32(4),
                EndStationID = reader.GetInt32(5),
                DepartureTime = reader.GetDateTime(6),
                ArrivalTime = reader.GetDateTime(7),
                Status = reader.GetString(8),
                Order = Orders.FirstOrDefault(o => o.OrderID == reader.GetInt32(1)),
                Train = Trains.FirstOrDefault(t => t.TrainID == reader.GetInt32(2)),
                Wagon = Wagons.FirstOrDefault(w => w.WagonID == reader.GetInt32(3)),
                StartStation = Stations.FirstOrDefault(s => s.StationID == reader.GetInt32(4)),
                EndStation = Stations.FirstOrDefault(s => s.StationID == reader.GetInt32(5))
            });
        }
    }

    public void SaveChangesCargo()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        foreach (var cargo in Cargos)
        {
            var command = new SqlCommand("IF EXISTS (SELECT * FROM Cargos WHERE CargoID = @CargoID) UPDATE Cargos SET Description = @Description, Weight = @Weight, Volume = @Volume WHERE CargoID = @CargoID ELSE INSERT INTO Cargos (Description, Weight, Volume) VALUES (@Description, @Weight, @Volume)", connection);
            command.Parameters.AddWithValue("@Description", cargo.Description);
            command.Parameters.AddWithValue("@Weight", cargo.Weight);
            command.Parameters.AddWithValue("@Volume", cargo.Volume);
            command.Parameters.AddWithValue("@CargoID", cargo.CargoID);
            command.ExecuteNonQuery();
        }
        LoadCargos();
    }

    public void SaveChangesClients()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        foreach (var client in Clients)
        {
            var command = new SqlCommand("IF EXISTS (SELECT * FROM Clients WHERE Id = @ClientID) UPDATE Clients SET Name = @Name, ContactPerson = @ContactPerson, PhoneNumber = @PhoneNumber, Email = @Email, Address = @Address, BillingInformation = @BillingInformation WHERE Id = @ClientID ELSE INSERT INTO Clients (Name, ContactPerson, PhoneNumber, Email, Address, BillingInformation) VALUES (@Name, @ContactPerson, @PhoneNumber, @Email, @Address, @BillingInformation)", connection);
            command.Parameters.AddWithValue("@Name", client.Name);
            command.Parameters.AddWithValue("@ContactPerson", client.ContactPerson);
            command.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
            command.Parameters.AddWithValue("@Email", client.Email);
            command.Parameters.AddWithValue("@Address", client.Address);
            command.Parameters.AddWithValue("@BillingInformation", client.BillingInformation);
            command.Parameters.AddWithValue("@ClientID", client.Id);
            command.ExecuteNonQuery();
        }
        LoadClients();
    }

    public void DeleteCargo(Cargo cargo)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("DELETE FROM Cargos WHERE CargoID = @CargoID", connection);
        command.Parameters.AddWithValue("@CargoID", cargo.CargoID);
        command.ExecuteNonQuery();
        Cargos.Remove(cargo);
    }

    public void DeleteClient(Client client)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("DELETE FROM Clients WHERE Id = @ClientID", connection);
        command.Parameters.AddWithValue("@ClientID", client.Id);
        command.ExecuteNonQuery();
        Clients.Remove(client);
    }

    public List<Cargo> SearchCargos(string searchTerm)
    {
        var filteredCargos = new List<Cargo>();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Cargos WHERE LOWER(Description) LIKE @SearchTerm", connection);
        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            filteredCargos.Add(new Cargo
            {
                CargoID = reader.GetInt32(0),
                Weight = reader.GetDecimal(1),
                Volume = reader.GetDecimal(2),
                Description = reader.GetString(3)
            });
        }
        return filteredCargos;
    }

    public List<Client> SearchClients(string searchTerm)
    {
        var filteredClients = new List<Client>();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Clients WHERE LOWER(Name) LIKE @SearchTerm", connection);
        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            filteredClients.Add(new Client
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                ContactPerson = reader.GetString(2),
                PhoneNumber = reader.GetString(3),
                Email = reader.GetString(4),
                Address = reader.GetString(5),
                BillingInformation = reader.GetString(6)
            });
        }
        return filteredClients;
    }

    public void SaveChangesOrders()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        foreach (var order in Orders)
        {
            var command = new SqlCommand("IF EXISTS (SELECT * FROM Orders WHERE OrderID = @OrderID) UPDATE Orders SET ClientID = @ClientID, CargoID = @CargoID, RouteID = @RouteID, OrderDate = @OrderDate, Status = @Status, TotalCost = @TotalCost WHERE OrderID = @OrderID ELSE INSERT INTO Orders (ClientID, CargoID, RouteID, OrderDate, Status, TotalCost) VALUES (@ClientID, @CargoID, @RouteID, @OrderDate, @Status, @TotalCost)", connection);
            command.Parameters.AddWithValue("@ClientID", order.ClientID);
            command.Parameters.AddWithValue("@CargoID", order.CargoID);
            command.Parameters.AddWithValue("@RouteID", order.RouteID);
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            command.Parameters.AddWithValue("@Status", order.Status);
            command.Parameters.AddWithValue("@TotalCost", order.TotalCost);
            command.Parameters.AddWithValue("@OrderID", order.OrderID);
            command.ExecuteNonQuery();
        }
        LoadOrders();
    }

    public void DeleteOrder(Order order)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
            var command = new SqlCommand("DELETE FROM Orders WHERE OrderID = @OrderID", connection);
        command.Parameters.AddWithValue("@OrderID", order.OrderID);
        command.ExecuteNonQuery();
        Orders.Remove(order);
    }

    public List<Order> SearchOrders(string searchTerm)
    {
        var filteredOrders = new List<Order>();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Orders WHERE LOWER(Status) LIKE @SearchTerm", connection);
        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            filteredOrders.Add(new Order
            {
                OrderID = reader.GetInt32(0),
                ClientID = reader.GetInt32(1),
                CargoID = reader.GetInt32(2),
                RouteID = reader.GetInt32(3),
                OrderDate = reader.GetDateTime(4),
                Status = reader.GetString(5),
                TotalCost = reader.GetDecimal(6),
                Client = Clients.FirstOrDefault(c => c.Id == reader.GetInt32(1)),
                Cargo = Cargos.FirstOrDefault(c => c.CargoID == reader.GetInt32(2)),
                Route = Routes.FirstOrDefault(r => r.RouteID == reader.GetInt32(3))
            });
        }
        return filteredOrders;
    }

    public void SaveChangesRoutes()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        foreach (var route in Routes)
        {
                var command = new SqlCommand("IF EXISTS (SELECT * FROM Routes WHERE RouteID = @RouteID) UPDATE Routes SET OriginStationId = @OriginStationId, DestinationStationId = @DestinationStationId, Distance = @Distance, EstimatedTime = @EstimatedTime WHERE RouteID = @RouteID ELSE INSERT INTO Routes (OriginStationId, DestinationStationId, Distance, EstimatedTime) VALUES (@OriginStationId, @DestinationStationId, @Distance, @EstimatedTime)", connection);
            command.Parameters.AddWithValue("@OriginStationId", route.OriginStationId);
            command.Parameters.AddWithValue("@DestinationStationId", route.DestinationStationId);
            command.Parameters.AddWithValue("@Distance", route.Distance);
            command.Parameters.AddWithValue("@EstimatedTime", route.EstimatedTime);
            command.Parameters.AddWithValue("@RouteID", route.RouteID);
            command.ExecuteNonQuery();
        }
        LoadRoutes();
    }

    public void DeleteRoute(Route route)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("DELETE FROM Routes WHERE RouteID = @RouteID", connection);
        command.Parameters.AddWithValue("@RouteID", route.RouteID);
        command.ExecuteNonQuery();
        Routes.Remove(route);
    }

    public List<Route> SearchRoutes(string searchTerm)
    {
        var filteredRoutes = new List<Route>();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Routes WHERE LOWER(EstimatedTime) LIKE @SearchTerm", connection);
        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            filteredRoutes.Add(new Route
            {
                RouteID = reader.GetInt32(0),
                OriginStationId = reader.GetInt32(1),
                DestinationStationId = reader.GetInt32(2),
                Distance = reader.GetDecimal(3),
                EstimatedTime = reader.GetString(4),
                OriginStation = Stations.FirstOrDefault(s => s.StationID == reader.GetInt32(1)),
                DestinationStation = Stations.FirstOrDefault(s => s.StationID == reader.GetInt32(2))
            });
        }
        return filteredRoutes;
    }

    public void SaveChangesSchedules()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        foreach (var schedule in Schedules)
        {
            var command = new SqlCommand("IF EXISTS (SELECT * FROM Schedules WHERE ScheduleID = @ScheduleID) UPDATE Schedules SET TrainID = @TrainID, RouteID = @RouteID, DepartureTime = @DepartureTime, ArrivalTime = @ArrivalTime, Date = @Date WHERE ScheduleID = @ScheduleID ELSE INSERT INTO Schedules (TrainID, RouteID, DepartureTime, ArrivalTime, Date) VALUES (@TrainID, @RouteID, @DepartureTime, @ArrivalTime, @Date)", connection);
            command.Parameters.AddWithValue("@TrainID", schedule.TrainID);
            command.Parameters.AddWithValue("@RouteID", schedule.RouteID);
            command.Parameters.AddWithValue("@DepartureTime", schedule.DepartureTime);
            command.Parameters.AddWithValue("@ArrivalTime", schedule.ArrivalTime);
            command.Parameters.AddWithValue("@Date", schedule.Date);
            command.Parameters.AddWithValue("@ScheduleID", schedule.ScheduleID);
            command.ExecuteNonQuery();
        }
        LoadSchedules();
    }

    public void DeleteSchedule(Schedule schedule)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("DELETE FROM Schedules WHERE ScheduleID = @ScheduleID", connection);
        command.Parameters.AddWithValue("@ScheduleID", schedule.ScheduleID);
        command.ExecuteNonQuery();
        Schedules.Remove(schedule);
    }

    public List<Schedule> SearchSchedules(string searchTerm)
    {
        var filteredSchedules = new List<Schedule>();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Schedules WHERE LOWER(RouteName) LIKE @SearchTerm", connection);
        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            filteredSchedules.Add(new Schedule
            {
                ScheduleID = reader.GetInt32(0),
                TrainID = reader.GetInt32(1),
                RouteID = reader.GetInt32(2),
                DepartureTime = reader.GetDateTime(3),
                ArrivalTime = reader.GetDateTime(4),
                Date = reader.GetDateTime(5),
                Train = Trains.FirstOrDefault(t => t.TrainID == reader.GetInt32(1)),
                Route = Routes.FirstOrDefault(r => r.RouteID == reader.GetInt32(2))
            });
        }
        return filteredSchedules;
    }

    public void SaveChangesShipments()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        foreach (var shipment in Shipments)
        {
            var command = new SqlCommand("IF EXISTS (SELECT * FROM Shipments WHERE ShipmentID = @ShipmentID) UPDATE Shipments SET OrderID = @OrderID, TrainID = @TrainID, WagonID = @WagonID, StartStationID = @StartStationID, EndStationID = @EndStationID, DepartureTime = @DepartureTime, ArrivalTime = @ArrivalTime, Status = @Status WHERE ShipmentID = @ShipmentID ELSE INSERT INTO Shipments (OrderID, TrainID, WagonID, StartStationID, EndStationID, DepartureTime, ArrivalTime, Status) VALUES (@OrderID, @TrainID, @WagonID, @StartStationID, @EndStationID, @DepartureTime, @ArrivalTime, @Status)", connection);
            command.Parameters.AddWithValue("@OrderID", shipment.OrderID);
            command.Parameters.AddWithValue("@TrainID", shipment.TrainID);
            command.Parameters.AddWithValue("@WagonID", shipment.WagonID);
            command.Parameters.AddWithValue("@StartStationID", shipment.StartStationID);
            command.Parameters.AddWithValue("@EndStationID", shipment.EndStationID);
            command.Parameters.AddWithValue("@DepartureTime", shipment.DepartureTime);
            command.Parameters.AddWithValue("@ArrivalTime", shipment.ArrivalTime);
            command.Parameters.AddWithValue("@Status", shipment.Status);
            command.Parameters.AddWithValue("@ShipmentID", shipment.ShipmentID);
            command.ExecuteNonQuery();
        }
        LoadShipments();
    }

    public void DeleteShipment(Shipment shipment)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("DELETE FROM Shipments WHERE ShipmentID = @ShipmentID", connection);
        command.Parameters.AddWithValue("@ShipmentID", shipment.ShipmentID);
        command.ExecuteNonQuery();
        Shipments.Remove(shipment);
    }

    public List<Shipment> SearchShipments(string searchTerm)
    {
        var filteredShipments = new List<Shipment>();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Shipments WHERE LOWER(Status) LIKE @SearchTerm", connection);
        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            filteredShipments.Add(new Shipment
            {
                ShipmentID = reader.GetInt32(0),
                OrderID = reader.GetInt32(1),
                TrainID = reader.GetInt32(2),
                WagonID = reader.GetInt32(3),
                StartStationID = reader.GetInt32(4),
                EndStationID = reader.GetInt32(5),
                DepartureTime = reader.GetDateTime(6),
                ArrivalTime = reader.GetDateTime(7),
                Status = reader.GetString(8),
                Order = Orders.FirstOrDefault(o => o.OrderID == reader.GetInt32(1)),
                Train = Trains.FirstOrDefault(t => t.TrainID == reader.GetInt32(2)),
                Wagon = Wagons.FirstOrDefault(w => w.WagonID == reader.GetInt32(3)),
                StartStation = Stations.FirstOrDefault(s => s.StationID == reader.GetInt32(4)),
                EndStation = Stations.FirstOrDefault(s => s.StationID == reader.GetInt32(5))
            });
        }
        return filteredShipments;
    }

    public void SaveChangesStations()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        foreach (var station in Stations)
        {
            var command = new SqlCommand("IF EXISTS (SELECT * FROM Stations WHERE StationID = @StationID) UPDATE Stations SET StationName = @StationName, Location = @Location WHERE StationID = @StationID ELSE INSERT INTO Stations (StationName, Location) VALUES (@StationName, @Location)", connection);
            command.Parameters.AddWithValue("@StationName", station.StationName);
            command.Parameters.AddWithValue("@Location", station.Location);
            command.Parameters.AddWithValue("@StationID", station.StationID);
            command.ExecuteNonQuery();
        }
        LoadStations();
    }

    public void DeleteStation(Station station)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("DELETE FROM Stations WHERE StationID = @StationID", connection);
        command.Parameters.AddWithValue("@StationID", station.StationID);
        command.ExecuteNonQuery();
        Stations.Remove(station);
    }

    public List<Station> SearchStations(string searchTerm)
    {
        var filteredStations = new List<Station>();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Stations WHERE LOWER(StationName) LIKE @SearchTerm", connection);
        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            filteredStations.Add(new Station
            {
                StationID = reader.GetInt32(0),
                StationName = reader.GetString(1),
                Location = reader.GetString(2)
            });
        }
        return filteredStations;
    }

    public void SaveChangesTrains()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        foreach (var train in Trains)
        {
            var command = new SqlCommand("IF NOT EXISTS (SELECT * FROM Trains WHERE TrainID = @TrainID) INSERT INTO Trains (Name, MaxCapacity) VALUES (@Name, @Capacity) ELSE UPDATE Trains SET Name = @Name, MaxCapacity = @Capacity WHERE TrainID = @TrainID", connection);
            command.Parameters.AddWithValue("@Name", train.Name);
            command.Parameters.AddWithValue("@Capacity", train.MaxCapacity);
            command.Parameters.AddWithValue("@TrainID", train.TrainID);
            command.ExecuteNonQuery();
        }
        LoadTrains();
    }

    public void DeleteTrain(Train train)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("DELETE FROM Trains WHERE TrainID = @TrainID", connection);
        command.Parameters.AddWithValue("@TrainID", train.TrainID);
        command.ExecuteNonQuery();
        Trains.Remove(train);
    }

    public List<Train> SearchTrains(string searchTerm)
    {
        var filteredTrains = new List<Train>();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Trains WHERE LOWER(Name) LIKE @SearchTerm", connection);
        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            filteredTrains.Add(new Train
            {
                TrainID = reader.GetInt32(0),
                Name = reader.GetString(1),
                MaxCapacity = reader.GetInt32(2)
            });
        }
        return filteredTrains;
    }

    public void SaveChangesWagons()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        foreach (var wagon in Wagons)
        {
            var command = new SqlCommand("IF NOT EXISTS (SELECT * FROM Wagons WHERE WagonID = @WagonID) INSERT INTO Wagons (WagonType, LoadCapacity, AssignedTrainID) VALUES (@WagonType, @Capacity, @TrainID) ELSE UPDATE Wagons SET WagonType = @WagonType, LoadCapacity = @Capacity, AssignedTrainID = @TrainID WHERE WagonID = @WagonID", connection);
            command.Parameters.AddWithValue("@WagonType", wagon.WagonType);
            command.Parameters.AddWithValue("@Capacity", wagon.LoadCapacity);
            command.Parameters.AddWithValue("@TrainID", wagon.AssignedTrainID);
            command.Parameters.AddWithValue("@WagonID", wagon.WagonID);
            command.ExecuteNonQuery();
        }
        LoadWagons();
    }

    public void DeleteWagon(Wagon wagon)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("DELETE FROM Wagons WHERE WagonID = @WagonID", connection);
        command.Parameters.AddWithValue("@WagonID", wagon.WagonID);
        command.ExecuteNonQuery();
        Wagons.Remove(wagon);
    }

    public List<Wagon> SearchWagons(string searchTerm)
    {
        var filteredWagons = new List<Wagon>();
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Wagons WHERE LOWER(WagonType) LIKE @SearchTerm", connection);
        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            filteredWagons.Add(new Wagon
            {
                WagonID = reader.GetInt32(0),
                WagonType = reader.GetString(1),
                LoadCapacity = reader.GetInt32(2),
                AssignedTrainID = reader.GetInt32(3),
                AssignedTrain = Trains.FirstOrDefault(t => t.TrainID == reader.GetInt32(3))
            });
        }
        return filteredWagons;
    }
}
