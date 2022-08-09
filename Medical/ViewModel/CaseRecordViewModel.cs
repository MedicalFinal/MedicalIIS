using Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModels
{
    public class CaseRecordViewModel
    {
        private CaseRecord _caseRecord;
        private Member _member;
        private Reserve _reserve;
        private TreatmentDetail _treatmentDetail;
        private Doctor _doctor;
        private ClinicDetail _clinicDetail;

        public CaseRecordViewModel()
        {
            _caseRecord = new CaseRecord();
            _member = new Member();
            _reserve = new Reserve();
            _treatmentDetail = new TreatmentDetail();
            _doctor = new Doctor();
            _clinicDetail = new ClinicDetail();
        }

        public CaseRecord caseRecord
        {
            get { return _caseRecord; }
            set { _caseRecord = value; }
        }

        
        public int CaseId
        {
            get { return _caseRecord.CaseId; }
            set { _caseRecord.CaseId = value; }
        }
        
        public int MemberId
        {
            get { return _caseRecord.MemberId; }
            set { _caseRecord.MemberId = value; }
        }
        
        public string DiagnosticRecord
        {
            get { return _caseRecord.DiagnosticRecord; }
            set { _caseRecord.DiagnosticRecord = value; }
        }
        public int ReserveId
        {
            get { return _caseRecord.ReserveId; }
            set { _caseRecord.ReserveId = value; }
        }
       
        //public string SyndromeDescription
        //{
        //    get { return _caseRecord.SyndromeDescription; }
        //    set { _caseRecord.SyndromeDescription = value; }
        //}
        
        public int? TreatmentDetailId
        {
            get { return _caseRecord.TreatmentDetailId; }
            set { _caseRecord.TreatmentDetailId = value; }
        }

        public int clinicId { set; get; }

        public virtual Member Member
        {
            get { return _caseRecord.Member; }
            set { _caseRecord.Member = value; }
        }
        public virtual Reserve Reserve
        {
            get { return _caseRecord.Reserve; }
            set { _caseRecord.Reserve = value; }
        }
        public virtual TreatmentDetail TreatmentDetail
        {
            get { return _caseRecord.TreatmentDetail; }
            set { _caseRecord.TreatmentDetail = value; }
        }
        public virtual Doctor Doctor
        {
            get { return _doctor; }
            set { _doctor = value; }
        }
        public virtual ClinicDetail ClinicDetail
        {
            get { return _clinicDetail; }
            set { _clinicDetail = value; }
        }
        public IEnumerable<Member> member { get; set; }
    }
}
