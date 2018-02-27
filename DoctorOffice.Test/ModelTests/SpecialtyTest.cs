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
  }
}
