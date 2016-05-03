using Assessment.Models;
using Assessment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Controllers
{
    public class PrizeController : Controller
    {
        private ApplicationDbContext _context;

        public PrizeController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Price       
        public ActionResult Index()
        {
            var viewModel = new PrizeViewModel();
            viewModel.Attendees = _context.Attendees.ToList();

            return View(viewModel);
        }

        // POST: Price
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index(PrizeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var attendee = new Attendee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedOn = DateTime.Now
                };

                _context.Attendees.Add(attendee);
                _context.SaveChanges();
                HttpContext.Session.Remove("attendees");
                return RedirectToAction("Index");


            }
            model.Attendees = _context.Attendees.ToList();
            return View(model);

        }

        public ActionResult PickWinners()
        {
            var attendees = _context.Attendees.ToList();
            if (attendees.Count == 0)
                return RedirectToAction("Index");
            var viewModel = new PickWinnerViewModel();
            List<Attendee> InDraw = attendees;
            List<Attendee> NonWinner = new List<Attendee>();

            var random = new Random();

            while (InDraw.Count != 0)
            {
                var remove = InDraw[random.Next(InDraw.Count)];
                if (InDraw.Count == 1)
                {
                    viewModel.Winners.Add(remove);
                    InDraw.Remove(remove);
                    InDraw.AddRange(NonWinner);
                    NonWinner.Clear();
                }
                else
                {
                    InDraw.Remove(remove);
                    NonWinner.Add(remove);
                }
            }
            return View(viewModel);
        }
    }
}
