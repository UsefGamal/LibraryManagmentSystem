using Library.Models.Branch;
using LibraryData;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;


namespace Library.Controllers
{
    public class BranchController :Controller
    {
        private ILibraryBranch _branch;
        public BranchController(ILibraryBranch branch)
        { 
            _branch = branch;
        }
        public IActionResult Index()
        {
            var branches = _branch.GetAll().Select(b => new BranchDetailModel
            {
                Id = b.Id,
                Name = b.Name,
                IsOpen = _branch.IsbranchOpen(b.Id),
                NumberOfAssets = _branch.GetAssets(b.Id).Count(),
                NumberOfPatrons = _branch.GetPatrons(b.Id).Count()
            });

            var model = new BranchIndexModel()
            {
                Branches = branches
            };
            return View(model);
        }

        public IActionResult Detail(int Id)
        {
            var branch = _branch.Get(Id);

            var model = new BranchDetailModel
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                Telephone = branch.Telephone,
                OpenDate = branch.OpenDate.ToString("yyyy-MM-dd"),
                NumberOfAssets = _branch.GetAssets(Id).Count(),
                NumberOfPatrons = _branch.GetPatrons(Id).Count(),
                TotalAssetsValue = _branch.GetAssets(Id).Sum(a => a.Cost),
                ImageUrl = branch.ImageUrl,
                HoursOpen = _branch.GetBranchHours(Id)
            };

            return View(model);
        }

    }
}
