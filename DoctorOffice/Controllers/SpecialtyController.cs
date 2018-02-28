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
    }
}
