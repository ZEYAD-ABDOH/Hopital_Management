using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asw.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   
    public class ReopController : ControllerBase
    {

        private readonly My_db_s _context;

        public ReopController(My_db_s context)
        {
            _context = context;
        }

        [HttpGet]
        
        public async Task<IActionResult> actionResult() 
        {
           
            var repos = await _context.Appointments.Join(
                //with patients
                _context.patients,
                app=> app.PatientID,
                patient=>patient.ID,
                (app, patient)=>new { app, patient }

                ).Join (
                //with doctors
                _context.doctors,
                app_d=>app_d.app.DoctorID,
                doctor=>doctor.ID,
                  (app_d, doctor) => new { app_d, doctor }

                ).Join (
                 _context.invoices,
                 appint=>appint.app_d.app.ID,
                 inveice=>inveice.PatientID,
                 (appint, inveice) =>new
                 {

                     name_appint=appint.app_d.patient.FullName,
                     name_doctor=appint.doctor.FullName,
                     date_app=appint.app_d.app.AppointmentDate,
                     title=inveice.Total,
                     discount=inveice.Discount,
                     amount_paid=inveice.Amount_paid,
                     remain_amout=inveice.Remain_amount,
                     state_inveice=inveice.PaymentStatus,





                 }
                ).ToListAsync ();
            return Ok(repos);

        }
    }
}
