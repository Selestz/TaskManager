using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index(string sortOrder, string searchString, string filterStatus, string filterPriority)
        {
            ViewData["TitleSortParam"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PrioritySortParam"] = sortOrder == "Priority" ? "priority_desc" : "Priority";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentStatusFilter"] = filterStatus;
            ViewData["CurrentPriorityFilter"] = filterPriority;

            var tasks = from t in _context.Tasks
                        select t;

            // Поиск
            if (!String.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Title.Contains(searchString) 
                                      || (t.Description != null && t.Description.Contains(searchString)));
            }

            // Фильтрация по статусу
            if (!String.IsNullOrEmpty(filterStatus))
            {
                bool isCompleted = filterStatus == "completed";
                tasks = tasks.Where(t => t.IsCompleted == isCompleted);
            }

            // Фильтрация по приоритету
            if (!String.IsNullOrEmpty(filterPriority))
            {
                if (Enum.TryParse<TaskPriority>(filterPriority, out var priority))
                {
                    tasks = tasks.Where(t => t.Priority == priority);
                }
            }

            // Сортировка
            tasks = sortOrder switch
            {
                "title_desc" => tasks.OrderByDescending(t => t.Title),
                "Date" => tasks.OrderBy(t => t.DueDate),
                "date_desc" => tasks.OrderByDescending(t => t.DueDate),
                "Priority" => tasks.OrderBy(t => t.Priority),
                "priority_desc" => tasks.OrderByDescending(t => t.Priority),
                _ => tasks.OrderBy(t => t.Title),
            };

            return View(await tasks.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            return View(taskItem);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DueDate,IsCompleted,Priority")] TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                taskItem.CreatedAt = DateTime.Now;
                _context.Add(taskItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskItem);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = await _context.Tasks.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }
            return View(taskItem);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DueDate,IsCompleted,Priority,CreatedAt")] TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskItemExists(taskItem.Id))
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
            return View(taskItem);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            return View(taskItem);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);
            if (taskItem != null)
            {
                _context.Tasks.Remove(taskItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Tasks/ToggleComplete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            taskItem.IsCompleted = !taskItem.IsCompleted;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool TaskItemExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
