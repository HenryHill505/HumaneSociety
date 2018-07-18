using System;
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
            if (existingEmployeeID != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Employee RetrieveEmployeeUser(string email, int employeeNunber)
        {
            //285 UserEmployee
            Employee e = new Employee();
            return e;
        }

        public static Employee EmployeeLogin(string userName, string password)
        {
            //267 UserEmployee
            Employee e = new Employee();
            return e;
        }

        public static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            //192 UserEmployee
        }

        public static DietPlan GetDietPlan()
        {
            //254 UserEmployee
            DietPlan e = new DietPlan();
            return e;
        }

        public static Adoption[] GetPendingAdoptions()
        {
            //Line 66 in UserEmployee calls .ToList() on the return value of this method. Should we use yield return?
            Adoption[] e = new Adoption[0];
            return e;
        }

        public static Room GetRoom(int animalID)
        {
            //133 UserInterface
            Room e = new Room();
            return e;
        }

        public static AnimalShot[] GetShots(Animal animal)
        {
            //Called 162 in UserEmployee. Yield Return?
            AnimalShot[] e = new AnimalShot[0];
            return e;
        }

        public static Species GetSpecies()
        {
            //247 UserEmployee
            Species e = new Species();
            return e;
        }

        public static void RemoveAnimal(Animal animal)
        {
            //240 UserEmployee
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
            Client e = new Client();
            return e;
        }

        public static Adoption[] GetUserAdoptionStatus(Client client)
        {
            Adoption[] e = new Adoption[0];
            return e;
        }

        public static Animal GetAnimalByID(int id)
        {
            Animal e = new Animal();
            return e;
        }

        public static void Adopt(Animal animal, Client client)
        {

        }

        public static Client[] RetrieveClients()
        {
            Client[] e = new Client[0];
            return e;
        }

        public static USState[] GetStates()
        {
            USState[] e = new USState[0];
            return e;
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
