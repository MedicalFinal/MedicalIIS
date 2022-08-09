using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModels
{
    public class RatingDoctoeViewModel
    {
        private RatingDoctor _ratingDoctor;
        private Doctor _doctor;
        private RatingType _ratingType;

        public RatingDoctoeViewModel()
        {
            _ratingDoctor = new RatingDoctor();
            _doctor = new Doctor();
            _ratingType = new RatingType();
        }

        public RatingDoctor ratingDoctor
        {
            get { return _ratingDoctor; }
            set { _ratingDoctor = value; }
        }

        public int RatingDoctorId
        {
            get { return _ratingDoctor.RatingTypeId; }
            set { _ratingDoctor.RatingTypeId = value; }
        }
        public int DoctorId
        {
            get { return _ratingDoctor.DoctorId; }
            set { _ratingDoctor.DoctorId = value; }
        }
        public int RatingTypeId
        {
            get { return _ratingDoctor.RatingTypeId; }
            set { _ratingDoctor.RatingTypeId = value; }
        }
        public string Rating
        {
            get { return _ratingDoctor.Rating; }
            set { _ratingDoctor.Rating = value; }
        }

        public virtual Doctor Doctor 
        { 
            get { return _doctor ; }
            set {_doctor = value; } 
        }
        public virtual RatingType RatingType 
        { 
            get { return _ratingType; }
            set { _ratingType = value; }
        }
    }
}
