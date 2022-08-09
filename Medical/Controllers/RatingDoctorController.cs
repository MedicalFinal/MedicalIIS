using Microsoft.AspNetCore.Mvc;
using Medical.Models;
using Medical.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Controllers
{
    public class RatingDoctorController : Controller
    {
        

        public IActionResult List()
        {
            MedicalContext medical = new MedicalContext();
            IEnumerable<RatingDoctoeViewModel> datas = null;
            datas = medical.RatingDoctors.Select(d => new RatingDoctoeViewModel
            {
                ratingDoctor = d,
                Doctor = d.Doctor,
                RatingType = d.RatingType
            });
            return View(datas);
        }
        public IActionResult Delete(int? id)
        {
            MedicalContext medical = new MedicalContext();
            RatingDoctor ratingDoctor = medical.RatingDoctors.FirstOrDefault(r => r.RatingDoctorId == id);
            if(ratingDoctor != null)
            {
                medical.RatingDoctors.Remove(ratingDoctor);
                medical.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            MedicalContext medical = new MedicalContext();
            RatingDoctor ratingDoctor = medical.RatingDoctors.FirstOrDefault(r => r.RatingDoctorId == id);
            if (ratingDoctor == null)
            {
                return RedirectToAction("List");
            }
            return View(ratingDoctor);
        }
        [HttpPost]
        public IActionResult Edit(RatingDoctor rd)
        {
            MedicalContext medical = new MedicalContext();
            RatingDoctor ratingDoctor = medical.RatingDoctors.FirstOrDefault(r => r.RatingDoctorId == rd.RatingDoctorId);
            if (ratingDoctor != null)
            {
                ratingDoctor.DoctorId = rd.DoctorId;
                ratingDoctor.RatingTypeId = rd.RatingTypeId;
                ratingDoctor.Rating = rd.Rating;
                medical.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
