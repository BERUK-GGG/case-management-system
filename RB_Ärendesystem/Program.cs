using RB_Ärendesystem.Entities;
using RB_Ärendesystem.Datalayer;

namespace RB_Ärendesystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            
          context testDB = new context();
          
            testDB.Database.EnsureDeleted();
            testDB.Database.EnsureCreated();

            Console.ReadLine(); 
        }
    }
}
