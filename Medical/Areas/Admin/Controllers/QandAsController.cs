using Medical.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Medical.Controllers
{
    [Area(areaName: "Admin")]
    public class QandAsController : Controller
    {
        
        private readonly MedicalContext _context;
        public QandAsController(MedicalContext medicalContext)
        {
            _context = medicalContext;
        }
        
       //顯示
        public IActionResult List()
        {
            IEnumerable<Question> list = null;
            list = _context.Questions.Select(x=>x);
            return View(list);
        }
        //新增
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Question q)
        {
            _context.Questions.Add(q);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        //修改
        public IActionResult Edit(int? id)
        {
            
            Question qa = _context.Questions.FirstOrDefault(q => q.QuestionId == id);
            if (qa == null)
                return RedirectToAction("List");
            return View(qa);
        }
        [HttpPost]
        public IActionResult Edit(Question q)
        {
            
            Question qa = _context.Questions.FirstOrDefault(qa => qa.QuestionId == q.QuestionId);
            if (qa != null)
            {
                qa.QuestionId = q.QuestionId;
                qa.QuestionContain = q.QuestionContain;
                qa.Answer = q.Answer;
                _context.SaveChanges();
            }
            return RedirectToAction("List");
        }
        //刪除
        public IActionResult Delete(int? id)
        {
            
            Question cust = _context.Questions.FirstOrDefault(q => q.QuestionId == id);
            if (cust != null)
            {
                _context.Questions.Remove(cust);
                _context.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
