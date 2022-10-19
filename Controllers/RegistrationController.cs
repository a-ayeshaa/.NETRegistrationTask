using LabTaskRegistration.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabTaskRegistration.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(Student s)
        {
            var db = new Dept_DBEntities();
            db.Students.Add(s);
            db.SaveChanges();
            return RedirectToAction("ShowStudent");
        }

        [HttpGet]
        public ActionResult ShowStudent()
        {
            var db = new Dept_DBEntities();
            return View(db.Students.ToList());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new Dept_DBEntities();
            var item = (from s in db.Students                  
                        where s.id == id
                        select s).SingleOrDefault();
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Student p)
        {
            var db = new Dept_DBEntities();
            var item = (from s in db.Students
                        where s.id == p.id
                        select s).SingleOrDefault();
            item.Name = p.Name;
            item.Student_id = p.Student_id;
            item.Cgpa = p.Cgpa;
            db.SaveChanges();
            return RedirectToAction("ShowStudent");

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new Dept_DBEntities();
            var item = (from p in db.Students
                        where p.id == id
                        select p).SingleOrDefault();
            db.Students.Remove(item);
            var i = (from p in db.CourseStudents
                     where p.StudentID == id
                     select p).SingleOrDefault();
            db.CourseStudents.Remove(i);
            db.SaveChanges();
            return RedirectToAction("ShowStudent");
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCourse(Cours s)
        {
            var db = new Dept_DBEntities();
            db.Courses.Add(s);
            db.SaveChanges();
            return RedirectToAction("ShowCourse");
        }

        [HttpGet]
        public ActionResult ShowCourse()
        {
            var db = new Dept_DBEntities();
            return View(db.Courses.ToList());
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var db = new Dept_DBEntities();
            var item = (from s in db.Courses
                        where s.id == id
                        select s).SingleOrDefault();
            return View(item);
        }

        [HttpPost]
        public ActionResult EditCourse(Cours p)
        {
            var db = new Dept_DBEntities();
            var item = (from s in db.Courses
                        where s.id == p.id
                        select s).SingleOrDefault();
            item.Name = p.Name;
            item.PreReq = p.PreReq;
            db.SaveChanges();
            return RedirectToAction("ShowCourse");

        }

        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            var db = new Dept_DBEntities();
            var item = (from p in db.Courses
                        where p.id == id
                        select p).SingleOrDefault();
            db.Courses.Remove(item);
            db.SaveChanges();
            return RedirectToAction("ShowCourse");
        }

        public ActionResult Registration(int id)
        {
            var db = new Dept_DBEntities();
            var s = (from p in db.CourseStudents
                     where p.StudentID == id 
                     select p).ToList();
            var test = s;


            foreach (var i in test)
            {
                if (i.Grade.Equals('W') || i.Marks < 60)
                {
                    continue;
                }
                else
                {
                    test.Remove(i);
                }
            }
            return View(s);
        }
    }
}