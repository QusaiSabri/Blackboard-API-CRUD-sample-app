using System;
namespace ConsoleAppBBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            //Run Terms
            SendTerms.Run();

            //Run Courses
            SendCourses.Run();

            //Run Users
            SendUsers.Run();

            //Run Memberships
            SendMemberships.Run();


        }
    }
}
