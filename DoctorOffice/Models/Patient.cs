using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace DoctorOffice.Models
{
  public class Patient
  {
    private string _name;
    private int _id;

    public Patient(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }
    public static List<Patient> GetAll()
    {
      List<Patient> allPatients = new List<Patient>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM patients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int patientId = rdr.GetInt32(0);
        string patientName = rdr.GetString(1);
        Patient newPatient = new Patient(patientName, patientId);
        allPatients.Add(newPatient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allPatients;
    }
  }
}
