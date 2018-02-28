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

    [HttpGet("/doctors/new/{specialty}")]
    public ActionResult CreateForm(int specialty)
    {
      return View(specialty);
    }

    [HttpPost("/doctors")]
    public ActionResult Create()
    {
      Doctor newDoctor = new Doctor(Request.Form["doctor-name"]);
      newDoctor.Save();
      return RedirectToAction("Success", "Home");
    }

    [HttpGet("/doctors/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Doctor selectedDoctor = Doctor.Find(id);
      List<Patient> doctorPatients = selectedDoctor.GetPatients();
      List<Patient> allPatients = Patient.GetAll();
      model.Add("doctor", selectedDoctor);
      model.Add("doctorPatients", doctorPatients);
      model.Add("allPatients", allPatients);
      return View(model);
    }

    [HttpPost("/doctors/{specialist}")]
    public ActionResult CreateWithSpecialist(int specialist)
    {
      Doctor newDoctor = new Doctor(Request.Form["doctor-name"]);
      newDoctor.Save();
      Specialty mySpecialty = Specialty.Find(specialist);
      mySpecialty.AddDoctor(newDoctor);
      return RedirectToAction("Success", "Home");
    }

    [HttpGet("/doctors/{id}/patients/new")]
    public ActionResult CreatePatientForm()
    {
      return View("~/Views/Patients/CreatePatientForm.cshtml");
    }

    [HttpPost("/doctors/{doctorId}/patients/new")]
    public ActionResult AddPatient(int doctorId)
    {
      Doctor doctor = Doctor.Find(doctorId);
      Patient patient = Patient.Find(Int32.Parse(Request.Form["patient-id"]));
      doctor.AddPatient(patient);
      return RedirectToAction("Success", "Home");
    }

    [HttpGet("/doctors/{doctorId}/update")]
    public ActionResult UpdateForm(int doctorId)
    {
      Doctor thisDoctor = Doctor.Find(doctorId);
      return View("update", thisDoctor);
    }

    [HttpPost("/doctors/{doctorId}/update")]
    public ActionResult Update(int doctorId)
    {
      Doctor thisDoctor = Doctor.Find(doctorId);
      thisDoctor.Edit(Request.Form["newname"]);
      return RedirectToAction("Index");
    }
    
    [HttpGet("/doctors/{doctorid}/delete")]
    public ActionResult DeleteOne(int doctorId)
    {
      Doctor thisDoctor = Doctor.Find(doctorId);
      thisDoctor.Delete();
      return RedirectToAction("index");
    }
  }
}
