using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using DoctorOffice.Models;

namespace DoctorOffice.Controllers
{
  public class PatientController : Controller
  {
    
    [HttpGet("/patients")]
    public ActionResult Index()
    {
      List<Patient> allPatients = Patient.GetAll();
      return View(allPatients);
    }

    [HttpGet("/patients/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/patients")]
    public ActionResult Create()
    {
      Patient newPatient = new Patient(Request.Form["patient-name"], Request.Form["patient-birthdate"]);
      newPatient.Save();
      return RedirectToAction("Success", "Home");
    }

    [HttpGet("/patients/{id}/delete")]
    public ActionResult DeleteOne(int id)
    {
      Patient thisPatient = Patient.Find(id);
      thisPatient.Delete();
      return RedirectToAction("index");
    }

    [HttpGet("/patients/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Patient selectedPatient = Patient.Find(id);
      List<Doctor> patientDoctors = selectedPatient.GetDoctors();
      List<Doctor> allDoctors = Doctor.GetAll();
      model.Add("patient", selectedPatient);
      model.Add("patientDoctors", patientDoctors);
      model.Add("allDoctors", allDoctors);
      return View( model);
    }

    [HttpGet("/patients/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Patient thisPatient = Patient.Find(id);
      return View("update", thisPatient);
    }

    [HttpPost("/patients/{id}/update")]
    public ActionResult Update(int id)
    {
      Patient thisPatient = Patient.Find(id);
      thisPatient.Edit(Request.Form["newname"], Request.Form["birthdate"]);
      return RedirectToAction("Index");
    }

    [HttpPost("/patients/{patientId}/doctors/new")]
    public ActionResult AddDoctor(int patientId)
    {
      Patient patient = Patient.Find(patientId);
      Doctor doctor = Doctor.Find(Int32.Parse(Request.Form["doctor-id"]));
      patient.AddDoctor(doctor);
      return RedirectToAction("Success", "Home");
    }
  }
}
