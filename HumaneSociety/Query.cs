using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
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
            //321 UserEmployee
        }

        public static Employee RetrieveEmployeeUser(string email, int employeeNunber)
        {
            //285 UserEmployee
        }

        public static Employee EmployeeLogin(string userName, string password)
        {
            //267 UserEmployee
        }

        public static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            //192 UserEmployee
        }

        public static DietPlan GetDietPlan()
        {
            //254 UserEmployee
        }

        public static Adoption GetPendingAdoptions()
        {
            //Line 66 in UserEmployee calls .ToList() on the return value of this method. Should we use yield return?
        }

        public static Room GetRoom(int animalID)
        {
            //133 UserInterface
        }

        public static AnimalShot GetShots(Animal animal)
        {
            //Called 162 in UserEmployee. Yield Return?
        }

        public static Species GetSpecies()
        {
            //247 UserEmployee
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
      
    }
}
