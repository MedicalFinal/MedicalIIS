using Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CRatingDoctorViewModel
    {
        private RatingDoctor _ratingDoctor;
        private Doctor _doctor;
        private RatingType _ratingType;

        public CRatingDoctorViewModel()
        {
            _ratingDoctor = new RatingDoctor();
            _doctor = new Doctor();
            _ratingType = new RatingType();
        }
        public RatingDoctor RatingDoctor
        {
            get { return _ratingDoctor; }
            set { _ratingDoctor = value; }
        }
        

        public int RatingDoctorId
        {
            get { return _ratingDoctor.RatingDoctorId; }
            set { _ratingDoctor.RatingDoctorId = value; }
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
        [DisplayName("評論內容")]
        public string Rating
        {
            get { return _ratingDoctor.Rating; }
            set { _ratingDoctor.Rating = value; }
        }

        public virtual Doctor Doctor
        {
            get { return _doctor; }
            set { _doctor = value; }
        }
        public virtual RatingType RatingType
        {
            get { return _ratingType; }
            set { _ratingType = value; }
        }
        public bool? Shade
        {
            get { return _ratingDoctor.Shade; }
            set { _ratingDoctor.Shade = value; }
        }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
