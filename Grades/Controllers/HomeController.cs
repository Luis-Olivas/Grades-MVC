using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Grades.Models;
using Grades.Data;
using Microsoft.EntityFrameworkCore;

namespace Grades.Controllers;

public class HomeController : Controller
{
    private readonly MySQLDbContext context;

    public HomeController(MySQLDbContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult SubjectMuestra()
    {
        Subject subjectMuestra = new Subject { Id = 1, Name = "Matematicas" };
        return View(subjectMuestra);
    }
    public async Task<IActionResult> VerSubjects()
    {
        List<Subject> items = await context.subjects.ToListAsync();
        return View(items);
    }

    public async Task<IActionResult> VerSubject(int id)
    {
        Subject? subject = await context.subjects.FindAsync(id);
        if (subject == null) return NotFound("No existe subject");
        return View("SubjectMuestra", subject);
    }


    //Crear
    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]

    public async Task<IActionResult> Crear(Subject subject)
    {
        if (subject == null) return Error();
        if (ModelState.IsValid)
        {
            context.subjects.Add(subject);
            await context.SaveChangesAsync();
            return RedirectToAction("VerGrades");
        }
        return View(subject);
    }

    //Modificar
    [HttpGet]
    public async Task<IActionResult> Modificar(int Id)
    {
        var subject = await context.subjects.FindAsync(Id);
        if (subject == null) return NotFound();
        return View(subject);
    }
    [HttpPost]
    public async Task<IActionResult> Modificar(Subject subject)
    {
        if (ModelState.IsValid)
        {
            try
            {
                context.subjects.Update(subject);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Content("Ocurrio on error");
            }
            return RedirectToAction("VerSubjects");
        }
        return View(subject);
    }

    //Eliminar
    [HttpGet]
    public async Task<IActionResult> Eliminar(int id)
    {
        Subject? subject = await context.subjects.FindAsync(id);
        if (subject == null) return NotFound();
        return View(subject);
    }
    [HttpPost, ActionName("Eliminar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminacionConfirmada(int id)
    {
        var subject = await context.subjects.FindAsync(id);
        if (subject == null) return NotFound();
        context.subjects.Remove(subject);
        await context.SaveChangesAsync();
        return RedirectToAction("VerSubjects");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
