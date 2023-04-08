using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestionBulkUpload.Data;
using QuestionBulkUpload.Models;

namespace QuestionBulkUpload.Controllers
{
    public class QuestionDatasController : Controller
    {
        private readonly QuestionBulkUploadContext _context;

        public QuestionDatasController(QuestionBulkUploadContext context)
        {
            _context = context;
        }

        // GET: QuestionDatas
        public async Task<IActionResult> Index()
        {
              return _context.Questions != null ? 
                          View(await _context.Questions.ToListAsync()) :
                          Problem("Entity set 'QuestionBulkUploadContext.QuestionData'  is null.");
        }

        // GET: QuestionDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }

            var questionData = await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionData == null)
            {
                return NotFound();
            }

            return View(questionData);
        }

        // GET: QuestionDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Direction,Summary,QuestionS,Explaination,Option1,Option2,Option3,Option4,Option5")] QuestionData questionData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionData);
        }

        // GET: QuestionDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }

            var questionData = await _context.Questions.FindAsync(id);
            if (questionData == null)
            {
                return NotFound();
            }
            return View(questionData);
        }

        // POST: QuestionDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Direction,Summary,QuestionS,Explaination,Option1,Option2,Option3,Option4,Option5")] QuestionData questionData)
        {
            if (id != questionData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionDataExists(questionData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(questionData);
        }

        // GET: QuestionDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }

            var questionData = await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionData == null)
            {
                return NotFound();
            }

            return View(questionData);
        }

        // POST: QuestionDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Questions == null)
            {
                return Problem("Entity set 'QuestionBulkUploadContext.QuestionData'  is null.");
            }
            var questionData = await _context.Questions.FindAsync(id);
            if (questionData != null)
            {
                _context.Questions.Remove(questionData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionDataExists(int id)
        {
          return (_context.Questions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
