﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        static HumaneSocietyDataContext db = new HumaneSocietyDataContext();

        public static void AddAnimal(Animal animal)
        {
            //255 UserEmployee
        }

        public static void AddUsernameAndPassword(Employee employee)
        {
            //308 UserEmployee
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

        }

        public static void updateClient(Client client)
        {

        }

        public static void UpdateUsername(Client client)
        {

        }

        public static void UpdateEmail(Client client)
        {

        }

        public static void UpdateAddress(Client client)
        {

        }

        public static void UpdateFirstName(Client client)
        {
            var query = db.Clients.Where(c => c.ClientId == client.ClientId);


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

        public static void RunEmployeeQueries(Employee employee, string operation)
        {

        }
    }
}
