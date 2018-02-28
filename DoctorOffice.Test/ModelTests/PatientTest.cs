using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoctorOffice.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace DoctorOffice.Tests
{
  [TestClass]
  public class PatientTest: IDisposable
  {
    public PatientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=doctor_office_test;";
    }
    public void Dispose()
    {
      Patient.DeleteAll();
      Doctor.DeleteAll();
    }
    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Patient.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void GetNameAndBirthday_ReturnsTrueForSameNameAndBirthday_Patient()
    {
      //Arrange, Act
      Patient firstPatient = new Patient("Brad", "03/30/1990");
      Patient secondPatient = new Patient ("Brad", "03/30/1990");

      //Assert
      Assert.AreEqual(firstPatient, secondPatient);
    }
    [TestMethod]
    public void Save_PatientSavesToDatabase_PatientList()
    {
      //Arrange
      Patient testPatient = new Patient("Brad", "03/30/1990");
      testPatient.Save();

      //Act
      List<Patient> result = Patient.GetAll();
      List<Patient> testList = new List<Patient>{testPatient};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_id()
    {
      //Arrange
      Patient testPatient = new Patient("Joanne", "02/24/1970");
      testPatient.Save();

      //Act
      Patient savedPatient = Patient.GetAll()[0];

      int result = savedPatient.GetId();
      int testId = testPatient.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Delete_DeletesPatientAssociationsFromDatabase_PatientList()
    {
      //Arrange
      Doctor testDoctor = new Doctor("Joey");
      testDoctor.Save();

      string testName = "Jerry";
      string testBirthDate = "01/01/1900";
      Patient testPatient = new Patient(testName, testBirthDate);
      testPatient.Save();

      //Act
      testPatient.AddDoctor(testDoctor);
      testPatient.Delete();

      List<Patient> resultDoctorPatients = testDoctor.GetPatients();
      List<Patient> testDoctorPatients = new List<Patient> {};

      //Assert
      CollectionAssert.AreEqual(testDoctorPatients, resultDoctorPatients);
    }
    [TestMethod]
    public void Find_FindsPatientInDatabase_Patient()
    {
      //Arrange
      Patient testPatient = new Patient("Jenny", "05/30/1967");
      testPatient.Save();

      //Act
      Patient result = Patient.Find(testPatient.GetId());

      //Assert
      Assert.AreEqual(testPatient, result);
    }
    [TestMethod]
    public void AddDoctor_AddsDoctorToPatient_DoctorList()
    {
      //Arrange
      Patient testPatient = new Patient("Sam", "9/24/1990");
      testPatient.Save();

      Doctor testDoctor = new Doctor("Howard");
      testDoctor.Save();

      //Act
      testPatient.AddDoctor(testDoctor);

      List<Doctor> result = testPatient.GetDoctors();
      List<Doctor> testList = new List<Doctor>{testDoctor};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void GetDoctors_ReturnsAllPatientDoctors_DoctorList()
    {
      //Arrange
      Patient testPatient = new Patient("Sandy", "07/18/1987");
      testPatient.Save();

      Doctor testDoctor1 = new Doctor("Phillip");
      testDoctor1.Save();

      Doctor testDoctor2 = new Doctor("Zachary");
      testDoctor2.Save();

      //Act
      testPatient.AddDoctor(testDoctor1);
      List<Doctor> result = testPatient.GetDoctors();
      List<Doctor> testList = new List<Doctor> {testDoctor1};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
