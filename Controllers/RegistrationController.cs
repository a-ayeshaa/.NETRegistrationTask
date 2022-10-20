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
            var db = new Dept_DBEntities1();
            db.Students.Add(s);
            db.SaveChanges();
            return RedirectToAction("ShowStudent");
        }

        [HttpGet]
        public ActionResult ShowStudent()
        {
            var db = new Dept_DBEntities1();
            return View(db.Students.ToList());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new Dept_DBEntities1();
            var item = (from s in db.Students                  
                        where s.id == id
                        select s).SingleOrDefault();
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Student p)
        {
            var db = new Dept_DBEntities1();
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
            var db = new Dept_DBEntities1();
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
            var db = new Dept_DBEntities1();
            db.Courses.Add(s);
            db.SaveChanges();
            return RedirectToAction("ShowCourse");
        }

        [HttpGet]
        public ActionResult ShowCourse()
        {
            var db = new Dept_DBEntities1();
            return View(db.Courses.ToList());
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var db = new Dept_DBEntities1();
            var item = (from s in db.Courses
                        where s.id == id
                        select s).SingleOrDefault();
            return View(item);
        }

        [HttpPost]
        public ActionResult EditCourse(Cours p)
        {
            var db = new Dept_DBEntities1();
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
            var db = new Dept_DBEntities1();
            var item = (from p in db.Courses
                        where p.id == id
                        select p).SingleOrDefault();
            db.Courses.Remove(item);
            db.SaveChanges();
            return RedirectToAction("ShowCourse");
        }
        [HttpGet]
        public ActionResult Registration(int id)
        {
            var db = new Dept_DBEntities1();
            var s = (from p in db.CourseStudents
                     where p.StudentID == id 
                     select p).ToList();
            var test = s;
            
            var all = db.Courses.ToList();
            
            var list = new List<Cours>();
            
            if (test.Count == 0)
            {
                var courses = (from c in db.Courses
                               where c.PreReq == null
                               select c).ToList();
                list = courses;
            }
            else
            {
                for (int i=0;i<all.Count();i++)
                {
                    for(int j=0;j<s.Count();j++)
                    {
                        if (s[j].Status != null)
                        {
                            if (s[j].CourseID == all[i].id)
                            {
                                if (s[j].Grade.Equals("W") || s[j].Marks < 60)
                                {
                                    if (!list.Contains(all[i]))
                                    {
                                        list.Add(all[i]);
                                    }
                                }
                            }
                            else if (!(s[j].CourseID == all[i].id))
                            {

                                if (s[j].CourseID == all[i].PreReq && s[j].Status.Equals("Complete"))
                                {
                                    if (!list.Contains(all[i]))
                                    {
                                        list.Add(all[i]);
                                    }

                                }
                                else if (all[i].PreReq == null)
                                {
                                    int iid = all[i].id;
                                    var course = (from co in db.CourseStudents
                                                  where co.CourseID == iid && co.StudentID == id
                                                  select co).SingleOrDefault();
                                    if (course == null)
                                    {
                                        if (!list.Contains(all[i]))
                                        {
                                            list.Add(all[i]);
                                        }
                                    }
                                }



                            }
                        }
                    }
                }
            }
            Session["id"] = id;
            return View(list);
        }

        [HttpPost]

        public ActionResult Registration(int[] courses)
        {
            if (courses == null)
            {
                return RedirectToAction("ShowStudent");

            }
            if (courses.Length > 5 )
            {
                TempData["msg"] = "You cannot register for more than 5 courses";
                return RedirectToAction("ShowStudent");

            }
            var db = new Dept_DBEntities1();
            foreach (var id in courses)
            {
                db.CourseStudents.Add(new CourseStudent()
                {
                    CourseID = id,
                    StudentID = (int)Session["id"]

                });
            }
            db.SaveChanges();
            return RedirectToAction("ShowStudent");
        }
    }
}