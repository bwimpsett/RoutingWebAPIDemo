using FirstWebAPIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstWebAPIDemo.Controllers
{
    public class StudentController
    {
        [RoutePrefix("students")]
        public class StudentsController : ApiController
        {
            static List<Student> students = new List<Student>()
            {
                new Student() { Id = 1, Name = "Pranaya" },
                new Student() { Id = 2, Name = "Priyanka" },
                new Student() { Id = 3, Name = "Anurag" },
                new Student() { Id = 4, Name = "Sambit" }
            };

            [HttpGet]
            [Route]
            public IEnumerable<Student> GetAllStudents()
            {
                return students;
            }

            [HttpGet]
            [Route("{studentID:int:nonzero}", Name = "GetStudentById")]
            public Student GetStudentDetails(int studentID)
            {
                return students.FirstOrDefault(s => s.Id == studentID);
            }

            [HttpGet]
            [Route("{studentName:alpha}")]
            public Student GetStudentDetails(string studentName)
            {
                return students.FirstOrDefault(s => s.Name == studentName);
            }

            //this is to test the name property
            [HttpPost]
            [Route]
            public HttpResponseMessage Post(Student student) 
            {
                students.Add(student);
                var response = Request.CreateResponse(HttpStatusCode.Created);
                
                //generate link
                string uri = Url.Link("GetStudentById", new { studentID = student.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            
            //[Route("api/students/{stdid:int?}")] --gets default value by route parameter
            //[Route("api/students/{stdid:int=1}")] -- assigns it in attribute
            //[Route("api/students/{stdid:int=1}")]
            //public Student GetBooksByID(int stdid)
            //{
            //    return students.FirstOrDefault(s => s.Id == stdid);
            //}

            //[Route("api/students/{studentID}/courses")]

            [HttpGet]
            [Route("{studentID}/courses")]
            public IEnumerable<string> GetStudentCourses(int studentID)
            {
                List<string> CourseList = new List<string>();
                if (studentID == 1)
                    CourseList = new List<string>() { "ASP.NET", "C#.NET", "SQL Server" };
                else if (studentID == 2)
                    CourseList = new List<string>() { "ASP.NET MVC", "C#.NET", "ADO.NET" };
                else if (studentID == 3)
                    CourseList = new List<string>() { "ASP.NET WEB API", "C#.NET", "Entity Framework" };
                else
                    CourseList = new List<string>() { "Bootstrap", "jQuery", "AngularJs" };
                return CourseList;
            }

            [HttpGet]
            [Route("tech/teachers")]
            public IEnumerable<Teacher> GetTeachers()
            {
                return new List<Teacher>()
                {
                    new Teacher { Id = 1, Name = "Manoj"},
                    new Teacher { Id = 2, Name = "Prakash"},
                    new Teacher { Id = 3, Name = "Manas"}
                };
            }

        }
    }
}