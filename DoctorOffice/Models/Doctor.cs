using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace DoctorOffice.Models
{
  public class Doctor
  {
    private string _name;
    private int _id;

    public Doctor(string name, int id = 0)
    {
      _name = name;
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
        Doctor newDoctor = new Doctor(doctorName, doctorId);
        allDoctors.Add(newDoctor);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allDoctors;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO doctors (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM doctors;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Patient> GetPatients()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT patients.* FROM doctors
      JOIN doctors_patients ON (doctors.id = doctors_patients.doctor_id)
      JOIN patients ON (doctors_patients.patient_id = patients.id)
      WHERE doctors.id = @DoctorId;";

      MySqlParameter doctorIdParameter = new MySqlParameter();
      doctorIdParameter.ParameterName = "@DoctorId";
      doctorIdParameter.Value = _id;
      cmd.Parameters.Add(doctorIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Patient> patients = new List<Patient>{};

      while(rdr.Read())
      {
        int patientId = rdr.GetInt32(0);
        string patientName = rdr.GetString(1);
        string patientBirthDate = rdr.GetString(2);
        Patient newPatient = new Patient(patientName, patientBirthDate, patientId);
        patients.Add(newPatient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return patients;
    }
    public void AddPatient(Patient newPatient)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO doctors_patients (doctor_id, patient_id) VALUES (@DoctorId, @PatientId);";

      MySqlParameter doctor_id = new MySqlParameter();
      doctor_id.ParameterName = "@DoctorId";
      doctor_id.Value = _id;
      cmd.Parameters.Add(doctor_id);

      MySqlParameter patient_id = new MySqlParameter();
      patient_id.ParameterName = "@PatientId";
      patient_id.Value = newPatient.GetId();
      cmd.Parameters.Add(patient_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
    public List<Specialty> GetSpecialtys()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT specialties.* FROM doctors
      JOIN doctors_specialties ON (doctors.id = doctors_specialties.doctor_id)
      JOIN specialties ON (doctors_specialties.specialty_id = specialties.id)
      WHERE doctors.id = @DoctorId;";

      MySqlParameter doctorIdParameter = new MySqlParameter();
      doctorIdParameter.ParameterName = "@DoctorId";
      doctorIdParameter.Value = _id;
      cmd.Parameters.Add(doctorIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Specialty> specialties = new List<Specialty>{};

      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialtyDescription = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(specialtyDescription, specialtyId);
        specialties.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return specialties;
    }
    public void AddSpecialty(Specialty newSpecialty)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO doctors_specialties (doctor_id, specialty_id) VALUES (@DoctorId, @SpecialtyId);";

      MySqlParameter doctor_id = new MySqlParameter();
      doctor_id.ParameterName = "@DoctorId";
      doctor_id.Value = _id;
      cmd.Parameters.Add(doctor_id);

      MySqlParameter specialty_id = new MySqlParameter();
      specialty_id.ParameterName = "@SpecialtyId";
      specialty_id.Value = newSpecialty.GetId();
      cmd.Parameters.Add(specialty_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM doctors WHERE id = @doctorId; DELETE FROM doctors_patients WHERE doctor_id = @doctorId;";

      MySqlParameter doctorIdParameter = new MySqlParameter();
      doctorIdParameter.ParameterName = "@doctorId";
      doctorIdParameter.Value = this.GetId();
      cmd.Parameters.Add(doctorIdParameter);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
    public static Doctor Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM doctors WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int DoctorId = 0;
      string DoctorName = "";

      while(rdr.Read())
      {
        DoctorId = rdr.GetInt32(0);
        DoctorName = rdr.GetString(1);
      }
      Doctor newDoctor = new Doctor(DoctorName, DoctorId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newDoctor;
    }
    public void UpdateDoctor(string newDoctor)
{
  MySqlConnection conn = DB.Connection();
  conn.Open();
  var cmd = conn.CreateCommand() as MySqlCommand;
  cmd.CommandText = @"UPDATE doctors SET name = @newDoctor WHERE id = @searchId;";

  MySqlParameter searchId = new MySqlParameter();
  searchId.ParameterName = "@searchId";
  searchId.Value = _id;
  cmd.Parameters.Add(searchId);

  MySqlParameter name = new MySqlParameter();
  name.ParameterName = "@newDoctor";
  name.Value = newDoctor;
  cmd.Parameters.Add(name);

  cmd.ExecuteNonQuery();
  _name = newDoctor;
  conn.Close();
  if (conn != null)
  {
    conn.Dispose();
  }

}
  }
}
