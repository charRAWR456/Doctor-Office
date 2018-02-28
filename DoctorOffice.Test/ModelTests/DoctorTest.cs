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
      public void Equals_ReturnsTrueForSameName_Doctor()
      {
        //Arrange, Act
        Doctor firstDoctor = new Doctor("Sean");
        Doctor secondDoctor = new Doctor("Sean");

        //Assert
        Assert.AreEqual(firstDoctor, secondDoctor);
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
    public void GetName_ReturnsTrueForSameNameAndSpecialty_Doctor()
    {
      //Arrange, Act
      Doctor firstDoctor = new Doctor("Sam");
      Doctor secondDoctor = new Doctor ("Sam");

      //Assert
      Assert.AreEqual(firstDoctor, secondDoctor);
    }
    [TestMethod]
    public void Save_DoctorSavesToDatabase_DoctorList()
    {
      //Arrange
      Doctor testDoctor = new Doctor("Brad");
      testDoctor.Save();

      //Act
      List<Doctor> result = Doctor.GetAll();
      List<Doctor> testList = new List<Doctor>{testDoctor};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
     public void Save_DatabaseAssignsIdToDoctor_Id()
     {
       //Arrange
       Doctor testDoctor = new Doctor("Joe");
       testDoctor.Save();

       //Act
       Doctor savedDoctor = Doctor.GetAll()[0];

       int result = savedDoctor.GetId();
       int testId = testDoctor.GetId();

       //Assert
       Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Delete_DeletesDoctorAssociationsFromDatabase_DoctorList()
    {
      //Arrange
      Patient testPatient = new Patient("Jim", "04/01/1987");
      testPatient.Save();

      string testName = "Tony";
      Doctor testDoctor = new Doctor(testName);
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
    public void Find_FindsDoctorInDatabase_Doctor()
    {
      //Arrange
      Doctor testDoctor = new Doctor("Sam");
      testDoctor.Save();

      //Act
      Doctor foundDoctor = Doctor.Find(testDoctor.GetId());

      //Assert
      Assert.AreEqual(testDoctor, foundDoctor);
    }
    public void GetPatients_ReturnsAllDoctorPatients_PatientList()
    {
      //Arrange
      Doctor testDoctor = new Doctor("Joe");
      testDoctor.Save();

      Patient testPatient1 = new Patient("Sally", "3/30/1990");
      testPatient1.Save();

      Patient testPatient2 = new Patient("Molly", "9/10/2011");
      testPatient2.Save();

      //Act
      testDoctor.AddPatient(testPatient1);
      List<Patient> savedPatients = testDoctor.GetPatients();
      List<Patient> testList = new List<Patient> {testPatient1};

      //Assert
      CollectionAssert.AreEqual(testList, savedPatients);
    }
    public void GetSpecialtys_ReturnsAllDoctorSpecialtys_SpecialtyList()
    {
      //Arrange
      Doctor testDoctor = new Doctor("Joe");
      testDoctor.Save();

      Specialty testSpecialty1 = new Specialty("Phycologist");
      testSpecialty1.Save();

      Specialty testSpecialty2 = new Specialty("Eye");
      testSpecialty2.Save();

      //Act
      testDoctor.AddSpecialty(testSpecialty1);
      List<Specialty> savedSpecialtys = testDoctor.GetSpecialtys();
      List<Specialty> testList = new List<Specialty> {testSpecialty1};

      //Assert
      CollectionAssert.AreEqual(testList, savedSpecialtys);
    }
    [TestMethod]
     public void Test_AddPatient_AddsPatientToDoctor()
     {
       //Arrange
       Doctor testDoctor = new Doctor("Carl");
       testDoctor.Save();

       Patient testPatient = new Patient("Howard", "02/28/1990");
       testPatient.Save();

       Patient testPatient2 = new Patient("Phil", "02/18/1993");
       testPatient2.Save();

       //Act
       testDoctor.AddPatient(testPatient);
       testDoctor.AddPatient(testPatient2);

       List<Patient> result = testDoctor.GetPatients();
       List<Patient> testList = new List<Patient>{testPatient, testPatient2};

       //Assert
       CollectionAssert.AreEqual(testList, result);
     }
     [TestMethod]
     public void Test_AddSpecialty_AddsSpecialtyToDoctor()
     {
       //Arrange
       Doctor testDoctor = new Doctor("Nick");
       testDoctor.Save();

       Specialty testSpecialty = new Specialty("Ear");
       testSpecialty.Save();

       Specialty testSpecialty2 = new Specialty("Eye");
       testSpecialty2.Save();

       //Act
       testDoctor.AddSpecialty(testSpecialty);
       testDoctor.AddSpecialty(testSpecialty2);

       List<Specialty> result = testDoctor.GetSpecialtys();
       List<Specialty> testList = new List<Specialty>{testSpecialty, testSpecialty2};

       //Assert
       CollectionAssert.AreEqual(testList, result);
     }
  }
}
