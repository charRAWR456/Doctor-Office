using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoctorOffice.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace DoctorOffice.Tests
{
  [TestClass]
  public class SpecialtyTest: IDisposable
  {
    public SpecialtyTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=doctor_office_test;";
    }
    public void Dispose()
    {
      Doctor.DeleteAll();
      Specialty.DeleteAll();
    }
    [TestMethod]
        public void Equals_TrueForSameDescription_Specialty()
        {
          //Arrange, Act
          Specialty firstSpecialty = new Specialty("Eye");
          Specialty secondSpecialty = new Specialty("Eye");

          //Assert
          Assert.AreEqual(firstSpecialty, secondSpecialty);
        }
    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Specialty.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void GetDescription_ReturnsTrueForSameDescription_Specialty()
    {
      //Arrange, Act
      Specialty firstSpecialty = new Specialty("eye");
      Specialty secondSpecialty = new Specialty ("eye");

      //Assert
      Assert.AreEqual(firstSpecialty, secondSpecialty);
    }
    [TestMethod]
    public void Save_SpecialtySavesToDatabase_SpecialtyList()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("chiropractor");
      testSpecialty.Save();

      //Act
      List<Specialty> result = Specialty.GetAll();
      List<Specialty> testList = new List<Specialty>{testSpecialty};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
        public void Save_AssignsIdToObject_id()
        {
          //Arrange
          Specialty testSpecialty = new Specialty("Phychiatrist");
          testSpecialty.Save();

          //Act
          Specialty savedSpecialty = Specialty.GetAll()[0];

          int result = savedSpecialty.GetId();
          int testId = testSpecialty.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }
    [TestMethod]
    public void Delete_DeletesSpecialtyAssociationsFromDatabase_SpecialtyList()
    {
      //Arrange
      Doctor testDoctor = new Doctor("Jim");
      testDoctor.Save();

      string testDescription = "Leg";
      Specialty testSpecialty = new Specialty(testDescription);
      testSpecialty.Save();

      //Act
      testSpecialty.AddDoctor(testDoctor);
      testSpecialty.Delete();

      List<Specialty> resultDoctorSpecialtys = testDoctor.GetSpecialtys();
      List<Specialty> testDoctorSpecialtys = new List<Specialty> {};

      //Assert
      CollectionAssert.AreEqual(testDoctorSpecialtys, resultDoctorSpecialtys);
    }
    [TestMethod]
        public void Find_FindsSpecialtyInDatabase_Specialty()
        {
          //Arrange
          Specialty testSpecialty = new Specialty("Eye");
          testSpecialty.Save();

          //Act
          Specialty result = Specialty.Find(testSpecialty.GetId());

          //Assert
          Assert.AreEqual(testSpecialty, result);
        }
        [TestMethod]
  public void AddDoctor_AddsDoctorToSpecialty_DoctorList()
  {
    //Arrange
    Specialty testSpecialty = new Specialty("Therapist");
    testSpecialty.Save();

    Doctor testDoctor = new Doctor("Joe");
    testDoctor.Save();

    //Act
    testSpecialty.AddDoctor(testDoctor);

    List<Doctor> result = testSpecialty.GetDoctors();
    List<Doctor> testList = new List<Doctor>{testDoctor};

    //Assert
    CollectionAssert.AreEqual(testList, result);
  }
[TestMethod]
  public void GetDoctors_ReturnsAllSpecialtyDoctors_DoctorList()
  {
    //Arrange
    Specialty testSpecialty = new Specialty("Chiropractor");
    testSpecialty.Save();

    Doctor testDoctor1 = new Doctor("Sam");
    testDoctor1.Save();

    Doctor testDoctor2 = new Doctor("Taylor");
    testDoctor2.Save();

    //Act
    testSpecialty.AddDoctor(testDoctor1);
    List<Doctor> result = testSpecialty.GetDoctors();
    List<Doctor> testList = new List<Doctor> {testDoctor1};

    //Assert
    CollectionAssert.AreEqual(testList, result);
  }
  }
}
