using Library.Models.PatronM;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers
{
    public class PatronController : Controller
    {
        IPatron _patron;
        public PatronController(IPatron patron)
        {
            _patron = patron;                
        }
        public IActionResult Index()
        {
            var allPatrons = _patron.GetAll();

            var patronModels = allPatrons.Select(p => new PatronDetailModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                LibraryCardId = p.LibraryCard.Id,
                OverDueFees = p.LibraryCard.Fees,
                HomeLibraryBranch = p.HomeLibraryBranch.Name

            });
            var model = new PatronIndexModel()
            {
                Patrons = patronModels
            };
            return View(model);
        }

       public IActionResult Detail(int Id)
        {
            var patron = _patron.Get(Id);
            var model = new PatronDetailModel
            {
                FirstName = patron.FirstName,
                LastName = patron.LastName,
                Address= patron.Address,
                HomeLibraryBranch = patron.HomeLibraryBranch.Name,
                MemberSince = patron.LibraryCard.Created,
                OverDueFees = patron.LibraryCard.Fees,
                LibraryCardId = patron.LibraryCard.Id,
                Telephone = patron.TelephoneNumber,
                AssetsCheckedOut = _patron.GetCheckouts(Id).ToList() ?? new List<Checkout>(),
                CheckoutsHistory = _patron.GetCheckoutHistory(Id),
                Holds = _patron.GetHolds(Id)
            };
            return View(model);
        }
    }
}
