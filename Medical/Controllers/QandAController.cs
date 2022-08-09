using Medical.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Medical.Controllers
{
    public class QandAController : Controller
    {
        private readonly MedicalContext _context;
        public QandAController(MedicalContext medicalContext)
        {
            _context = medicalContext;
        }

        public IActionResult List()
        {
            IEnumerable<Question> list = null;
            list = _context.Questions.Select(x=>x);
            return View(list);
        }
    }
}
