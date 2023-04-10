using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using QuestionBulkUpload.Data;
using QuestionBulkUpload.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<string[]>> Create(string Direction)
        {


            string[] list = Direction.Split("@mq");
            List<QuestionData> questions = new List<QuestionData>();
            foreach (var item in list)
            {

                Debug.WriteLine(item +"Emputy");


                if(item.Contains("@qt"))
                { 
                QuestionData question = new QuestionData();
                var itemCollect = item.Split('@');
                var order = 0;
                foreach (var items in itemCollect)
                {
                    if (items.StartsWith("srt")) {
                        order = Convert.ToInt32(items.Substring(3, 1).Trim());
                    }
                    else if (items.StartsWith("qt")) {
                        question.QuestionS = items.Replace("qt", "").Trim();
                    }
                    else if (items.StartsWith("dt"))
                    {
                        question.Direction = items.Replace("dt", "").Trim();
                    }
                    else if (items.StartsWith("st"))
                    {
                        question.Summary = items.Replace("st", "").Trim();
                    }
                    else if (items.StartsWith("et"))
                    {
                        question.Explaination = items.Replace("et", "").Trim();
                    }
                    else if (items.StartsWith("oa"))
                    {
                        question.Option1 = items.Replace("oa", "").Trim();
                    }
                    else if (items.StartsWith("ob"))
                    {
                        question.Option2 = items.Replace("ob", "").Trim();
                    }
                    else if (items.StartsWith("oc"))
                    {
                        question.Option3 = items.Replace("oc", "").Trim();
                    }
                    else if (items.StartsWith("od"))
                    {
                        question.Option4 = items.Replace("od", "").Trim();
                    }
                    else if (items.StartsWith("oe"))
                    {
                        question.Option5 = items.Replace("oe", "").Trim();
                    }


                }
                if (order == 0)
                {
                    questions.Add(question);
                }
                else {
                    questions.Insert(order-1, question);
                }
            } }
            if (questions.Count > 0) {
                _context.AddRange(questions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(Direction) ;
        }


        // POST: QuestionDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //      [ValidateAntiForgeryToken]
        //  public async Task<IActionResult> Create(string String)
        //{
        //  if (ModelState.IsValid)
        // {
        //   _context.Add(questionData);
        // await _context.SaveChangesAsync();
        //return RedirectToAction(nameof(Index));
        // }
        //  return View();
        //}

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
