using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using DoctorOffice.Models;

namespace DoctorOffice.Controllers
{
    public class SpecialtyController : Controller
    {

        [HttpGet("/specialtys")]
        public ActionResult Index()
        {
          List<Specialty> allSpecialtys = Specialty.GetAll();
        return View(allSpecialtys);
        }
        [HttpGet("/specialtys/new")]
        public ActionResult CreateForm()
        {
          return View();
        }
        [HttpPost("/specialtys")]
        public ActionResult Create()
        {
          Specialty newSpecialty = new Specialty(Request.Form["specialty-description"]);
          newSpecialty.Save();
          return RedirectToAction("Success", "Home");
        }
        [HttpGet("/specialtys/{id}/delete")]
        public ActionResult DeleteOne(int id)
        {
          Specialty thisSpecialty = Specialty.Find(id);
          thisSpecialty.Delete();
          return RedirectToAction("index");
        }

        [HttpGet("/specialtys/{id}")]
        public ActionResult Details(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Specialty selectedSpecialty = Specialty.Find(id);
            List<Doctor> specialtyDoctors = selectedSpecialty.GetDoctors();
            List<Doctor> allDoctors = Doctor.GetAll();
            model.Add("specialty", selectedSpecialty);
            model.Add("specialtyDoctors", specialtyDoctors);
            model.Add("allDoctors", allDoctors);
            return View( model);

        }
        [HttpGet("/specialtys/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            Specialty thisSpecialty = Specialty.Find(id);
            return View("update", thisSpecialty);
        }
        [HttpPost("/specialtys/{id}/update")]
        public ActionResult Update(int id)
        {
          Specialty thisSpecialty = Specialty.Find(id);
          thisSpecialty.Edit(Request.Form["description"]);
          return RedirectToAction("Index");
        }
        [HttpPost("/specialtys/{specialtyId}/doctors/new")]
        public ActionResult AddDoctor(int specialtyId)
        {
            Specialty specialty = Specialty.Find(specialtyId);
            Doctor doctor = Doctor.Find(Int32.Parse(Request.Form["doctor-id"]));
            specialty.AddDoctor(doctor);
            return RedirectToAction("Success", "Home");
        }
    }
}
