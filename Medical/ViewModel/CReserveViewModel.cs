using Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CReserveViewModel
    {
        public List<Reserve> patientList { get; set; }

        private Reserve _reserve;

        public CReserveViewModel()
        {
            _reserve = new Reserve();
        }

        public Reserve reserve
        {
            get { return _reserve; }
            set { _reserve = value; }
        }

        public int ReserveId 
        { get { return _reserve.ReserveId; }
            set { _reserve.ReserveId = value; } 
        }
        public int ClinicDetailId
        {
            get { return _reserve.ClinicDetailId; }
            set { _reserve.ClinicDetailId = value; }
        }
        //public int? StateId
        //{
        //    get { return _reserve.StateId; }
        //    set { _reserve.StateId = value; }
        //}
        public int MemberId
        {
            get { return _reserve.MemberId; }
            set { _reserve.MemberId = value; }
        }
        [DisplayName("預約日期")]
        public DateTime ReserveDate
        {
            get { return _reserve.ReserveDate; }
            set { _reserve.ReserveDate = value; }
        }
        public string RemarkPatient
        {
            get { return _reserve.RemarkPatient; }
            set { _reserve.RemarkPatient = value; }
        }
        public string RemarkAdmin
        {
            get { return _reserve.RemarkAdmin; }
            set { _reserve.RemarkAdmin = value; }
        }
        public int? SequenceNumber
        {
            get { return _reserve.SequenceNumber; }
            set { _reserve.SequenceNumber = value; }
        }
        public virtual ClinicDetail ClinicDetail
        {
            get { return _reserve.ClinicDetail; }
            set { _reserve.ClinicDetail = value; }
        }
        public virtual Member Member
        {
            get { return _reserve.Member; }
            set { _reserve.Member = value; }
        }
        //public virtual Source Source
        //{
        //    get { return _reserve.Source; }
        //    set { _reserve.Source = value; }
        //}
        //public virtual State State
        //{
        //    get { return _reserve.State; }
        //    set { _reserve.State = value; }
        //}
        public virtual ICollection<CaseRecord> CaseRecords
        {
            get { return _reserve.CaseRecords; }
            set { _reserve.CaseRecords = value; }
        }


    }
}
