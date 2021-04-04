using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;


namespace PolyClinicCapstoneDAL.Models
{
    public class PolyclinicRepository
    {
        PolyclinicDBContext context;
        
        public PolyclinicRepository()
        {
            context = new PolyclinicDBContext();
        }

        // Get patient details, by primary key;

        public Patient GetPatientDetails(string patientId)
        {
            Patient returnPatient = null;
            try
            {
                returnPatient = context.Patients.Find(patientId);
            }
            catch (Exception ex)
            {

                returnPatient = null;
            }
            return returnPatient; 
        }


        // Add new patient details.
        public bool AddNewPatientDetails(Patient patient)
        {
            bool status = false;
            try
            {
               
                Patient newPatient = patient;
                context.Patients.Add(newPatient);
                context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {

                status = false; 
            }
            return status; 
        }

        // Update one patient Age, by primary key.
        public bool UpdatePatientAge(string patientId, byte newAge)
        {
            bool status = false;
            try
            {
                Patient targetPatient = context.Patients.Find(patientId);
                if(targetPatient != null)
                {
                    targetPatient.Age = newAge;
                    context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {

                status = false;
            }
            return status; 
        }

        public int CancelAppointment(int appointmentNo)
        {
            int status;
            
            try 
            {
                Appointment targetAppointment = context.Appointments.Find(appointmentNo);

                // If its found, delete it and save to database.
                if(targetAppointment != null)
                {
                    context.Appointments.Remove(targetAppointment);
                    context.SaveChanges();
                    // Update status
                    status = 1; 
                }
                else
                {
                    status = -1;
                    Console.WriteLine("Appointment can't be found by that appintmentNo.");
                    
                }
            }
            catch (Exception)
            {
                // this only catches, if the whole try block has exception. 
                status = -99;
               
            }

            return status; 
        }

        // Stored_procedure to GetDoctorAppointment
        public int GetDoctorAppointment(string patientId, string doctorId, DateTime date, out int appointmentNo)
        {
            // Inizalie the out parameter, in case this method succeeds, we need to assign new appointmentNo. 
            appointmentNo = 0;

            // Initialize rowsAffected, to see if ExecuteSqlRaw succeeds.
            int rowsAffected;

            // Initialize results, to catch returnValue of the stored_procedured, (Ex: -2, -3 etc ...., referrence this stored_procedure for details)
            int result;

            // Create sql parameters for each argument, for input argument, just need parameter name(same in stored_procedure) and input value argument.
            // Also create sql parameter for returnValue of the stored_procedure, for this oen its named @RetVal 
            // Outside the try, because this will alwasy succeed, this won't throw any exeception.


            //Arbintary name                            // matches variable procedure name 
            SqlParameter prmPatientId = new SqlParameter("@PatientID", patientId);
            SqlParameter prmDoctorId = new SqlParameter("@DoctorID", doctorId);
            SqlParameter prmDate = new SqlParameter("@DateOfAppointment", date);

            // Out paramerters, returnValue and out argument appointmentNO
            // Out sql parameter needs to put in name, and sql data type, and change direction.
            SqlParameter prmReturnResult = new SqlParameter("@RetVal", System.Data.SqlDbType.Date);
            prmReturnResult.Direction = System.Data.ParameterDirection.Output;

            SqlParameter prmAppointmentNo = new SqlParameter("@AppointmentNo", System.Data.SqlDbType.Int);
            prmAppointmentNo.Direction = System.Data.ParameterDirection.Output; 

            try
            {
                // Execute the stored_procedure with ExecuteSqlRaw, and return value of this will be number of rows affected. If > 0, means succeed. 
                // EF core 3.1 or later, use ExecuteSqlRaw to replace ExecuteSqlCommand, no longer needs the annymous array.
                   


                // Step 3, extract the out parameter and the returnResult(NO. of rows affected);



            return returnResult; 
        }











    }// End of class
}
