using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HumaneSociety
{
    public static class Query
    {
        static HumaneSocietyDataContext db = new HumaneSocietyDataContext();

        public static void AddAnimal(Animal animal)
        {
            //255 UserEmployee
            db.Animals.InsertOnSubmit(animal);
        }

        public static void AddUsernameAndPassword(Employee employee)
        {
            //308 UserEmployee
            var employeeToUpdate = db.Employees.Where(e => employee.EmployeeId == e.EmployeeId).Select(e => e).FirstOrDefault();
            employeeToUpdate.UserName = employee.UserName;
            employeeToUpdate.Password = employee.Password;
        }

        public static bool CheckEmployeeUserNameExist(string username)
        {
            var existingEmployeeID = db.Employees.Where(e => e.UserName == username).Select(e => e.EmployeeId);
            if (existingEmployeeID.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            //285 UserEmployee
            var employeeValues = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber ).Select( e => new Employee {EmployeeId = e.EmployeeId, FirstName = e.FirstName, LastName = e.LastName, UserName = e.UserName, Password = e.Password, EmployeeNumber = e.EmployeeNumber, Email = e.Email }).First();
            Employee employee = new Employee() { EmployeeId = employeeValues.EmployeeId, FirstName = employeeValues.FirstName, LastName = employeeValues.LastName, UserName = employeeValues.UserName, Password = employeeValues.Password, EmployeeNumber = employeeValues.EmployeeNumber, Email = employeeValues.Email };
            return employee;
        }

        public static Employee EmployeeLogin(string userName, string password)
        {
            //267 UserEmployee
            var employeeValues = db.Employees.Where(e => e.UserName == userName && e.Password == password).Select(e => new Employee { EmployeeId = e.EmployeeId, FirstName = e.FirstName, LastName = e.LastName, UserName = e.UserName, Password = e.Password, EmployeeNumber = e.EmployeeNumber, Email = e.Email }).First();
            Employee employee = new Employee() { EmployeeId = employeeValues.EmployeeId, FirstName = employeeValues.FirstName, LastName = employeeValues.LastName, UserName = employeeValues.UserName, Password = employeeValues.Password, EmployeeNumber = employeeValues.EmployeeNumber, Email = employeeValues.Email };
            return employee;
        }

        public static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            //192 UserEmployee
        }

        public static DietPlan GetDietPlan()
        {
            //254 UserEmployee
            var dietPlans = db.DietPlans.Select(p => new DietPlan() {DietPlanId = p.DietPlanId, Name = p.Name, FoodType = p.FoodType, FoodAmountInCups = p.FoodAmountInCups }).ToArray();
            for (int i = 0; i < dietPlans.Count(); i++)
            {
                Console.WriteLine($"{i}. {dietPlans[i].Name}");
            }

            return dietPlans[int.Parse(Console.ReadLine())];
        }

        public static Adoption[] GetPendingAdoptions()
        {
            //Line 66 in UserEmployee calls .ToList() on the return value of this method. Should we use yield return?

            var pendingAdoptions = db.Adoptions.Where(a => a.ApprovalStatus == "Pending").Select(a => new Adoption() { AdoptionId = a.AdoptionId, ClientId = a.ClientId, AnimalId = a.AnimalId, ApprovalStatus = a.ApprovalStatus, AdoptionFee = a.AdoptionFee, PaymentCollected = a.PaymentCollected }).ToArray();
            return pendingAdoptions;
        }

        public static Room GetRoom(int animalID)
        {
            //133 UserInterface
            var room = db.Rooms.Where(r => r.AnimalId == animalID).Select(r => new Room() { RoomId = r.RoomId, AnimalId = r.AnimalId }).First();
            return room;
        }

        public static AnimalShot[] GetShots(Animal animal)
        {
            //Called 162 in UserEmployee. Yield Return?
            var shots = db.AnimalShots.Join(db.Shots, a => a.ShotId, s => s.ShotId, (a, s) => new { AnimalId = a.AnimalId, ShotId = a.ShotId, Name = s.Name, DateReceived = a.DateReceived }).Where(a => animal.AnimalId == a.AnimalId).Select(a => new AnimalShot { AnimalId = a.AnimalId, ShotId = a.ShotId, DateReceived = a.DateReceived, Shot = new Shot() { ShotId = a.ShotId, Name = a.Name } }).ToArray();
            return shots;
        }

        public static Species GetSpecies()
        {
            //247 UserEmployee
            var species = db.Species.Select(s => new Species { SpeciesId = s.SpeciesId, Name = s.Name }).ToArray();
            for (int i = 0; i < species.Count(); i++)
            {
                Console.WriteLine($"{i}. {species[i].Name}");
            }

            return species[int.Parse(Console.ReadLine())];
        }

        public static void RemoveAnimal(Animal animal)
        {
            //240 UserEmployee
            var animalToRemove = db.Animals.Where(a => a.AnimalId == animal.AnimalId).Select(a => a).FirstOrDefault();
            db.Animals.DeleteOnSubmit(animalToRemove);
 
        }
        
        public static void UpdateAdoption(bool GetBitData, Adoption adoption)
        {
            //89 & 93 in UserEmployee
        }

        public static void UpdateShot(string shotType, Animal animal)
        {
            //171, 178 UserEmployee
            
        }
      


        public static Client GetClient(string username, string password)
        {
            var query = db.Clients.Where(c => c.UserName == username && c.Password == password);

            foreach(var client in query)
            {
                return client;
            }

            return new Client();
        }

        public static Adoption[] GetUserAdoptionStatus(Client client)
        {
            Adoption[] clientAdoptions = client.Adoptions.ToArray();
            
            return clientAdoptions;
        }

        public static Animal GetAnimalByID(int id)
        {
            var query = db.Animals.Where(a => a.AnimalId == id);

            foreach(var animal in query)
            {
                return animal;
            }

            return new Animal();
        }

        public static void Adopt(Animal animal, Client client)
        {
            var clientQuery = db.Clients.Where(c => c.ClientId == client.ClientId);
            var animalQuery = db.Animals.Where(a => a.AnimalId == animal.AnimalId);

            foreach(Client clientGiven in clientQuery)
            {
                foreach(Animal animalGiven in animalQuery)
                {
                    Adoption newAdoption = new Adoption();
                    newAdoption.Animal = animalGiven;
                    newAdoption.Client = clientGiven;
                    clientGiven.Adoptions.Add(newAdoption);
                }
            }
        }

        public static Client[] RetrieveClients()
        {
            var query = db.Clients.ToArray();

            return query;
        }

        public static USState[] GetStates()
        {
            // HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var query = db.USStates.ToArray();
            
            return query;
        }

        public static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            Client client = new Client();
            client.FirstName = firstName;
            client.LastName = lastName;
            client.UserName = username;
            client.Password = password;
            client.Email = email;
            client.Address.AddressLine1 = streetAddress;
            client.Address.Zipcode = zipCode;
            client.Address.USStateId = state;

            db.Clients.InsertOnSubmit(client);

            db.SubmitChanges();

        }

        public static void updateClient(Client client)
        {
            var query = db.Clients.Where(c => c.ClientId == client.ClientId).ToArray();

            for(int i = 0; i < query.Length; i++)
            {
                query[i] = client;
            }

            db.SubmitChanges();
        }

        public static void UpdateUsername(Client client)
        {
            var query = db.Clients.Where(c => c.ClientId == client.ClientId);

            foreach (Client clientGiven in query)
            {
                clientGiven.UserName = client.UserName;
            }

            db.SubmitChanges();
        }

        public static void UpdateEmail(Client client)
        {
            var query = db.Clients.Where(c => c.ClientId == client.ClientId);

            foreach (Client clientGiven in query)
            {
                clientGiven.Email = client.Email;
            }

            db.SubmitChanges();
        }

        public static void UpdateAddress(Client client)
        {
            var query = db.Clients.Where(c => c.ClientId == client.ClientId);

            foreach (Client clientGiven in query)
            {
                clientGiven.Address = client.Address;
            }

            db.SubmitChanges();
        }

        public static void UpdateFirstName(Client client)
        {
            var query = db.Clients.Where(c => c.ClientId == client.ClientId);

            foreach(Client clientGiven in query)
            {
                clientGiven.FirstName = client.FirstName;
            }

            db.SubmitChanges();
        }

        public static void UpdateLastName(Client client)
        {
            var query = from c in db.Clients
                        where c.ClientId == client.ClientId
                        select c;

            foreach(Client clientGiven in query)
            {
                clientGiven.LastName = client.LastName;
            }

            db.SubmitChanges();
        }

        public delegate void EmployeeToVoidFunction(Employee employee);

        public static void RunEmployeeQueries(Employee employee, string operation)
        {
            EmployeeToVoidFunction employeeCrudFunctions = new EmployeeToVoidFunction(ReadEmployee);

            switch (operation)
            {
                case "create":
                    employeeCrudFunctions = CreateEmployee;
                    break;
                case "read":
                    employeeCrudFunctions = ReadEmployee;
                    break;
                case "update":
                    employeeCrudFunctions = UpdateEmployee;
                    break;
                case "delete":
                    employeeCrudFunctions = DeleteEmployee;
                    break;
            }

            employeeCrudFunctions(employee);
        }

        private static void CreateEmployee(Employee employee)
        {
            db.Employees.InsertOnSubmit(employee);

            db.SubmitChanges();
        }

        private static void ReadEmployee(Employee employee)
        {
            var query = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId);

            foreach(Employee employeeFound in query)
            {
                Console.WriteLine(employeeFound.FirstName + " " + employeeFound.LastName);
                Console.WriteLine("Employee #" + employeeFound.EmployeeNumber);
                Console.WriteLine("Employee Username: " + employeeFound.UserName);
                Console.WriteLine("Employee Email: " + employeeFound.Email);
                Console.WriteLine("Employee Animals: " + employeeFound.Animals);
            }
        }

        private static void UpdateEmployee(Employee employee)
        {
            var query = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId);

            foreach (Employee employeeFound in query)
            {
                employeeFound.FirstName = employee.FirstName;
                employeeFound.LastName = employee.LastName;
                employeeFound.UserName = employee.UserName;
                employeeFound.Email = employee.Email;
                employeeFound.Animals = employee.Animals;
            }

            db.SubmitChanges();
        }

        private static void DeleteEmployee(Employee employee)
        {
            var query = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId);

            foreach (Employee employeeFound in query)
            {
                db.Employees.DeleteOnSubmit(employeeFound);
            }

            try
            {
                db.SubmitChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception caught: " + e.Message);
            }
        }

        public static void ImportAnimalsFromCSV(string filename)
        {
            var query = File.ReadAllLines(filename).Select(l => l.Split(',').Where(m => m != null));
            
            foreach (var animal in query)
            {
                List<int?> intData = new List<int?>();
                List<string> stringData = new List<string>();

                foreach (var data in animal)
                {
                    bool isParsable = int.TryParse(data, out int value);
                    if (isParsable)
                    {
                        intData.Add(value);
                    }
                    else
                    {
                        stringData.Add(data);
                    }
                }

                Animal animalToAdd = CreateAnimal(intData, stringData);

                AddAnimal(animalToAdd);
            }

            db.SubmitChanges();
        }

        private static Animal CreateAnimal(List<int?> intData, List<string> stringData)
        {
            Animal animalToAdd = new Animal();

            animalToAdd.Name = stringData[0];
            animalToAdd.SpeciesId = intData[1];
            animalToAdd.Weight = intData[2];
            animalToAdd.Age = intData[3];
            animalToAdd.DietPlanId = intData[4];
            animalToAdd.Demeanor = stringData[1];
            animalToAdd.KidFriendly = ConvertIntToBool(intData[5]);
            animalToAdd.PetFriendly = ConvertIntToBool(intData[6]);
            animalToAdd.Gender = stringData[2];
            animalToAdd.AdoptionStatus = stringData[3];

            return animalToAdd;
        }

        private static bool? ConvertIntToBool(int? convertible)
        {
            switch (convertible)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    return null;
            }
        }
    }
}
