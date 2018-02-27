using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoctorOffice.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace DoctorOffice.Tests
{
  [TestClass]
  public class DoctorTest: IDisposable
  {
    public DoctorTest()
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
      int result = Doctor.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void GetNameAndSpecialty_ReturnsTrueForSameNameAndSpecialty_Doctor()
    {
      //Arrange, Act
      Doctor firstDoctor = new Doctor("Sam","Eyes");
      Doctor secondDoctor = new Doctor ("Sam","Eyes");

      //Assert
      Assert.AreEqual(firstDoctor, secondDoctor);
    }
    [TestMethod]
    public void Save_DoctorSavesToDatabase_DoctorList()
    {
      //Arrange
      Doctor testDoctor = new Doctor("Brad", "Foot");
      testDoctor.Save();

      //Act
      List<Doctor> result = Doctor.GetAll();
      List<Doctor> testList = new List<Doctor>{testDoctor};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Delete_DeletesDoctorAssociationsFromDatabase_DoctorList()
    {
      //Arrange
      Patient testPatient = new Patient("Jim", "04/01/1987");
      testPatient.Save();

      string testName = "Tony";
      string testSpecialty = "Leg";
      Doctor testDoctor = new Doctor(testName, testSpecialty);
      testDoctor.Save();

      //Act
      testDoctor.AddPatient(testPatient);
      testDoctor.Delete();

      List<Doctor> resultPatientDoctors = testPatient.GetDoctors();
      List<Doctor> testPatientDoctors = new List<Doctor> {};

      //Assert
      CollectionAssert.AreEqual(testPatientDoctors, resultPatientDoctors);
    }
    [TestMethod]
    public void DuplicateSpecialty_DoctorSameSpecialty_DoctorList()
    {
      //Arrange
      Doctor doctorOne = new Doctor("Brad", "Foot");
      doctorOne.Save();
      Doctor doctorTwo = new Doctor("Sally", "Foot");
      doctorTwo.Save();

      //Act
      List<Doctor> result = Doctor.GetAll();
      List<Doctor> testList = new List<Doctor>{doctorOne, doctorTwo};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_FindsDoctorInDatabase_Doctor()
    {
      //Arrange
      Doctor testDoctor = new Doctor("Sam", "Ear");
      testDoctor.Save();

      //Act
      Doctor foundDoctor = Doctor.Find(testDoctor.GetId());

      //Assert
      Assert.AreEqual(testDoctor, foundDoctor);
    }
  }
}
