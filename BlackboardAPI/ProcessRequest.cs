using System;
using System.Threading.Tasks;

namespace BlackboardAPI
{
    public enum Operations
    {
        Create,
        Read,
        Update,
        Delete
    }

    public class ProcessRequest
    {
        private static Token token = new Token();

       
        public async Task<Term> doTerm(Operations operations, Term term)
        {
            Term termResult = new Term();

            if (token == null)
            {
                Authorizer authorizer = new Authorizer();

                Console.WriteLine("calling authorize()");

                token = await authorizer.Authorize();

                Console.WriteLine("doTerm(): Token=" + token.ToString());
            }

            TermService termService = new TermService(token);

            if (operations == Operations.Create)
            {
                termResult = await termService.CreateObject(term);
                Console.WriteLine("Term Create: " + termResult.ToString());
            }

            if (operations == Operations.Read)
            {
                termResult = await termService.ReadObject(term.id); // term_id ?? is it id or ?
                Console.WriteLine("Term Read: " + termResult.ToString());
            }

            if (operations == Operations.Update)
            {
                termResult = await termService.UpdateObject(term);
                Console.WriteLine("Term Update: " + termResult.ToString());
            }

            if (operations == Operations.Delete)
            {
                termResult = await termService.DeleteObject(term.id);
                Console.WriteLine("Term Delete: " + termResult.ToString());
            }
            return (termResult);
        }

        public async Task<Course> doCourse(Operations operations, Course course)
        {
            var courseResult = new Course();

            if (token == null)
            {
                Authorizer authorizer = new Authorizer();

                Console.WriteLine("calling authorize()");

                token = await authorizer.Authorize();

                Console.WriteLine("doCourse(): Token=" + token.ToString());
            }

            CourseService courseService = new CourseService(token);

            if (operations == Operations.Create)
            {
                courseResult = await courseService.CreateObject(course);
                Console.WriteLine("Course Create: " + courseResult.ToString());
            }

            if (operations == Operations.Read)
            {
                courseResult = await courseService.ReadObject(course.courseId);
                Console.WriteLine("Course Read: " + courseResult.ToString());
            }

            if (operations == Operations.Update)
            {
                courseResult = await courseService.UpdateObject(course);
                Console.WriteLine("Course Update: " + courseResult.ToString());
            }

            if (operations == Operations.Delete)
            {
                courseResult = await courseService.DeleteObject(course.courseId);
                Console.WriteLine("Course Delete: " + courseResult.ToString());
            }
            return (courseResult);
        }

        public async Task<User> doUser(Operations operations, User user)
        {
            var userResult = new User();

            if (token == null)
            {
                Authorizer authorizer = new Authorizer();

                Console.WriteLine("calling authorize()");

                token = await authorizer.Authorize();

                Console.WriteLine("doUser(): Token=" + token.ToString());
            }

            UserService userService = new UserService(token);

            if (operations == Operations.Create)
            {
                userResult = await userService.CreateObject(user);
                Console.WriteLine("User Create: " + userResult.ToString());
            }

            if (operations == Operations.Read)
            {
                userResult = await userService.ReadObject(user.id); // is it the id field?
                Console.WriteLine("User Read: " + userResult.ToString());
            }

            if (operations == Operations.Update)
            {
                userResult = await userService.UpdateObject(user);
                Console.WriteLine("User Update: " + userResult.ToString());
            }

            if (operations == Operations.Delete)
            {
                userResult = await userService.DeleteObject(user.id);
                Console.WriteLine("User Delete: " + userResult.ToString());
            }
            return (userResult);
        }

        public async Task<Membership> doMembership(Operations operations, Membership membership)
        {
            var membershipResult = new Membership();

            if (token == null)
            {
                Authorizer authorizer = new Authorizer();

                Console.WriteLine("calling authorize()");

                token = await authorizer.Authorize();

                Console.WriteLine("doMembership(): Token=" + token.ToString());
            }

            MembershipService membershipService = new MembershipService(token);

            if (operations == Operations.Create)
            {
                membershipResult = await membershipService.CreateObject(membership);
                Console.WriteLine("Membership Create: " + membershipResult.ToString());
            }

            if (operations == Operations.Read)
            {
                membershipResult = await membershipService.ReadObject(membership.courseId, membership.userId);
                Console.WriteLine("Membership Read: " + membershipResult.ToString());
            }

            if (operations == Operations.Update)
            {
                membershipResult = await membershipService.UpdateObject(membership);
                Console.WriteLine("Membership Update: " + membershipResult.ToString());
            }

            if (operations == Operations.Delete)
            {
                membershipResult = await membershipService.DeleteObject(membership.courseId, membership.userId);
                Console.WriteLine("Membership Delete: " + membershipResult.ToString());
            }
            return (membershipResult);
        }

    }
}
