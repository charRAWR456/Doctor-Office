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
    //  Patient.DeleteAll();
      //Doctor.DeleteAll();
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
  }
}
