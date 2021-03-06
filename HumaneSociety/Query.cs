﻿using System;
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
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        public static void AddSpecies(string speciesName)
        {
            Species species = new Species() { Name = speciesName };
            db.Species.InsertOnSubmit(species);
            db.SubmitChanges();
        }

        public static void AddUsernameAndPassword(Employee employee)
        {
            var employeeToUpdate = db.Employees.Where(e => employee.EmployeeId == e.EmployeeId).Select(e => e).FirstOrDefault();
            employeeToUpdate.UserName = employee.UserName;
            employeeToUpdate.Password = employee.Password;
            db.SubmitChanges();
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
            var employee = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();
            return employee;
        }

        public static Employee EmployeeLogin(string userName, string password)
        {
            var employee = db.Employees.Where(e => e.UserName == userName && e.Password == password).First();
            return employee;
        }

        public static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            var animalToUpdate = db.Animals.Where(a => a.AnimalId == animal.AnimalId).FirstOrDefault();

            foreach (int key in updates.Keys)
            {
                switch (key)
                {
                    case 1:
                        animalToUpdate.SpeciesId = db.Species.Where(s => s.Name == updates[key]).Select(s => s.SpeciesId).FirstOrDefault();
                        break;
                    case 2:
                        animalToUpdate.Name = updates[key];
                        break;
                    case 3:
                        animalToUpdate.Age = int.Parse(updates[key]);
                        break;
                    case 4:
                        animalToUpdate.Demeanor = updates[key];
                        break;
                    case 5:
                        animalToUpdate.KidFriendly = bool.Parse(updates[key]);
                        break;
                    case 6:
                        animalToUpdate.PetFriendly = bool.Parse(updates[key]);
                        break;
                    case 7:
                        animalToUpdate.Weight = int.Parse(updates[key]);
                        break;
                    case 8:
                        animalToUpdate.AnimalId = int.Parse(updates[key]);
                        break;
                }
            }
            db.SubmitChanges();
        }

        public static void AddDietPlan(string planName, string foodType, int foodAmountInCups)
        {
            DietPlan dietPlan = new DietPlan() { Name = planName, FoodType = foodType, FoodAmountInCups = foodAmountInCups };
            db.DietPlans.InsertOnSubmit(dietPlan);
            db.SubmitChanges();
        }

        public static DietPlan GetDietPlan(string planName)
        {
            var dietPlan = db.DietPlans.Where(p => p.Name == planName).FirstOrDefault();
            return dietPlan;
        }

        public static Adoption[] GetPendingAdoptions()
        {
            var pendingAdoptions = db.Adoptions.Where(a => a.ApprovalStatus == "Pending").ToArray();
            return pendingAdoptions;
        }

        public static Room GetRoom(int animalID)
        {
            var room = db.Rooms.Where(r => r.AnimalId == animalID).FirstOrDefault();
            return room;
        }

        public static void UpdateRoom(Room room, Animal animal)
        {
            var query = db.Rooms.Where(r => r.RoomId == room.RoomId);

            foreach (var item in query)
            {
                Room previousRoom = db.Rooms.Where(r => r.AnimalId == animal.AnimalId).FirstOrDefault();
                string temporary = item.AnimalName;
                int? temporaryId = item.AnimalId;
                item.AnimalName = animal.Name;
                item.AnimalId = animal.AnimalId;
                previousRoom.AnimalName = temporary;
                previousRoom.AnimalId = temporaryId;
            }

            db.SubmitChanges();
        }

        public static AnimalShot[] GetShots(Animal animal)
        {
            var shots = db.AnimalShots.Where(s => s.AnimalId == animal.AnimalId).ToArray();
            return shots;
        }

        public static Species GetSpecies(string speciesGiven)
        {
            var species = db.Species.Where(s => s.Name == speciesGiven).FirstOrDefault();
            return species;
        }

        public static void RemoveAnimal(Animal animal)
        {
            var animalToRemove = db.Animals.Where(a => a.AnimalId == animal.AnimalId).Select(a => a).FirstOrDefault();
            db.Animals.DeleteOnSubmit(animalToRemove);
            db.SubmitChanges();
        }
        
        public static void UpdateAdoption(bool isApproved, Adoption adoption)
        {
            var adoptionToUpdate = db.Adoptions.Where(a => a.AdoptionId == adoption.AdoptionId).Select(a => a).FirstOrDefault();
            if (isApproved)
            {
                adoptionToUpdate.ApprovalStatus = "Approved";
                UserInterface.money += Convert.ToInt32(adoptionToUpdate.AdoptionFee);
                adoptionToUpdate.PaymentCollected = true;
            }
            else
            {
                adoptionToUpdate.ApprovalStatus = "Not Approved";
            }
            db.SubmitChanges();
        }

        public static void UpdateShot(string shotType, Animal animal)
        {
            AnimalShot newShot = new AnimalShot();
            newShot.Animal = db.Animals.Where(a => a.AnimalId == animal.AnimalId).FirstOrDefault();
            newShot.Shot = db.Shots.Where(s => s.Name == shotType).FirstOrDefault();
            newShot.DateReceived = DateTime.Today;
            db.AnimalShots.InsertOnSubmit(newShot);
            db.SubmitChanges();
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

        public static Room GetRoomByID(int id)
        {
            var query = db.Rooms.Where(r => r.RoomId == id);

            foreach(var room in query)
            {
                return room;
            }

            return new Room();
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
                    newAdoption.ApprovalStatus = "Pending";
                    newAdoption.AdoptionFee = 75;
                    clientGiven.Adoptions.Add(newAdoption);

                    db.SubmitChanges();
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
            var query = db.USStates.ToArray();
            
            return query;
        }

        public static void AddNewClient(Client client, string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            client.FirstName = firstName;
            client.LastName = lastName;
            client.UserName = username;
            client.Password = password;
            client.Email = email;

            Address clientAddress = new Address();
            clientAddress.AddressLine1 = streetAddress;
            clientAddress.Zipcode = zipCode;
            clientAddress.USStateId = state;

            db.Addresses.InsertOnSubmit(clientAddress);

            client.AddressId = db.Addresses.OrderByDescending(a => a.AddressId).First().AddressId;

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
            var query = File.ReadAllLines("..\\..\\..\\" + filename).Select(l => l.Split(',').Where(m => m != null));
            
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
                    else if (data == " null" && intData.Count >= 8)
                    {
                        stringData.Add(null);
                    }
                    else if (data == " null")
                    {
                        intData.Add(null);
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
