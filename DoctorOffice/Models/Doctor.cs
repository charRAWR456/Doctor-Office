using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace DoctorOffice.Models
{
  public class Doctor
  {
    private string _name;
    private string _specialty;
    private int _id;

    public Doctor(string name, string specialty, int id = 0)
    {
      _name = name;
      _specialty = specialty;
      _id = id;
    }
    public override bool Equals(System.Object otherDoctor)
    {
      if (!(otherDoctor is Doctor))
      {
        return false;
      }
      else
      {
        Doctor newDoctor = (Doctor) otherDoctor;
        return this.GetId().Equals(newDoctor.GetId());
      }
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetSpecialty()
    {
      return _specialty;
    }
    public static List<Doctor> GetAll()
    {
      List<Doctor> allDoctors = new List<Doctor>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM doctors;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int doctorId = rdr.GetInt32(0);
        string doctorName = rdr.GetString(1);
        string doctorSpecialty = rdr.GetString(2);
        Doctor newDoctor = new Doctor(doctorName, doctorSpecialty, doctorId);
        allDoctors.Add(newDoctor);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allDoctors;
    }
  }
}
