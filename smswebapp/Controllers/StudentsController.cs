using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using smswebapp.Models;

public class StudentsController : Controller{

    private static List<Student> students = new List<Student>{
        new Student {Id=1, Name="Anvi", Address="Mumbai"},
        new Student {Id=2, Name="Shamli", Address="Gujarat"}
    };

        public IActionResult Index()
        {
            return View(students);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            student.Id=students.Count+1;
            students.Add(student);
            return RedirectToAction("Index");
        }

}
