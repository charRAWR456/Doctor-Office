using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using DoctorOffice.Models;

namespace DoctorOffice.Controllers
{
  public class DoctorController : Controller
  {

    [HttpGet("/doctors")]
    public ActionResult Index()
    {
      List<Doctor> allDoctors = Doctor.GetAll();
      return View(allDoctors);
    }
    [HttpGet("/doctors/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/doctors")]
    public ActionResult Create()
    {
      Doctor newDoctor = new Doctor(Request.Form["doctor-name"]);
      newDoctor.Save();
      return RedirectToAction("Success", "Home");
    }
  }
}
