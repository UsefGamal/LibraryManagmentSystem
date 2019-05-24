using LibraryData;
using System;
using System.Collections.Generic;
using System.Text;
using LibraryData.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices
{
    public class CheckoutService : ICheckout
    {
        private LibraryContext _context;

        public CheckoutService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(Checkout newCheckout)
        {
            _context.Add(newCheckout);
            _context.SaveChanges(); 
        }



        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts;
        }

        public Checkout GetById(int checkoutId)
        {
            return GetAll()
                .FirstOrDefault(checkout => checkout.Id ==checkoutId);
        }

        public IEnumerable<CheckoutsHistory> GetCheckutHistory(int id)
        {
            return _context.CheckoutsHistories
                 .Include(ch => ch.LibraryAsset)
                 .Include(ch => ch.LibraryCard)
                 .Where(ch => ch.LibraryAsset.Id == id);
        }

     

        public IEnumerable<Hold> GetCurrentHolds(int id)
        {
            return _context.Holds
                 .Include(h => h.LibraryAsset)
                 .Where(h => h.LibraryAsset.Id == id);
        }
       public Checkout GetLatestCheckout(int id)
        {
            return _context.Checkouts
               .Where(c => c.LibraryAsset.Id == id)
               .OrderByDescending(c => c.Since)
               .FirstOrDefault();

        }


        public void MarkFound(int assetId)
        {
            var now = DateTime.Now;
            

            UpdateAssetStatus(assetId, "Available");
            RemoveExistingCheckouts(assetId);

            CloseExistingCheckoutHistory(assetId, now);
            

            _context.SaveChanges();
        }

        private void UpdateAssetStatus(int assetId, string v)
        {
            var item = _context.LibraryAssets
                   .FirstOrDefault(a => a.Id == assetId);

            _context.Update(item);

            item.Status = _context.Statuses
                .FirstOrDefault(status => status.Name == v);
        }

        private void CloseExistingCheckoutHistory(int assetId ,DateTime now)
        {
            // close any Existing checkout History

            var history = _context.CheckoutsHistories
                .FirstOrDefault(ch => ch.LibraryAsset.Id == assetId
                && ch.CheckedIn == null);

            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }
        }

        private void RemoveExistingCheckouts(int assetId)
        {
            // remove any Existing checking outs on the item 

            var checkout = _context.Checkouts
                .FirstOrDefault(co => co.LibraryAsset.Id == assetId);

            if (checkout != null)
            {
                _context.Remove(checkout);
            }
        }

        public void MarkLost(int assetId)
        {
            UpdateAssetStatus(assetId, "Lost");
            _context.SaveChanges();
        }

      
        public void CheckinItem(int assetId)
        {
            var now = DateTime.Now;
            var item = _context.LibraryAssets
                .FirstOrDefault(a => a.Id == assetId);


            // remove any existing checkouts on the Item 
            RemoveExistingCheckouts(assetId);

            //Close Any existing Checkouts History
            CloseExistingCheckoutHistory(assetId, now);

            // look For Existing Holds on the item
            var currentHolds = _context.Holds
                .Include(a => a.LibraryAsset)
                .Include(a => a.LibraryCard)
                .Where(a => a.LibraryAsset.Id == assetId);

            /*if there are any Holds , Checkout the item to the library card
             * with the earliest Hold*/
            if (currentHolds.Any())
            {
                CheckoutToEarliestHold(assetId, currentHolds);
                    return;
            }
            // otherwise Update the item Status to Available 
            UpdateAssetStatus(assetId, "Available");


            _context.SaveChanges();
        }

        private void CheckoutToEarliestHold(int assetId, IQueryable<Hold> currentHolds)
        {
            var earliestHold = currentHolds
                .OrderBy(a => a.HoldPlaced)
                .FirstOrDefault();

            var card = earliestHold.LibraryCard;
            _context.Remove(earliestHold);
            _context.SaveChanges();
            CheckoutItem(assetId, card.Id);
        }

        public void CheckoutItem(int assetId, int libraryCardId)
        {
            if (IsCheckedOut(assetId))
            {
                return;
                // Add logic to Handle Feedback To the User 
            }

            var item = _context.LibraryAssets
               .FirstOrDefault(a => a.Id == assetId);

            UpdateAssetStatus(assetId, "Checked Out");

            var libraryCard = _context.LibraryCards
                 .Include(card => card.Checkouts)
                 .FirstOrDefault(card => card.Id == libraryCardId);

            var now = DateTime.Now;

            var checkout = new Checkout
            {
                LibraryAsset = item,
                LibraryCard = libraryCard,
                Since = now,
                Until = GetDefaultCheckoutTime(now)
            };
            _context.Add(checkout);

            var checkoutHistory = new CheckoutsHistory
            {
                CheckOut    =now,
                LibraryAsset = item,
                LibraryCard =libraryCard
            };
            _context.Add(checkoutHistory);
            _context.SaveChanges();
        }

        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
            return now.AddDays(30);
        }

        public bool IsCheckedOut(int assetId)
        {
            return _context.Checkouts
                .Where(co => co.LibraryAsset.Id == assetId)
                .Any();
        }

        public void PlaceHold(int assetId, int libraryCardId)
        {
            var now = DateTime.Now;

            var asset = _context.LibraryAssets
                .Include(a=>a.Status)
                .FirstOrDefault(c => c.Id == assetId);

            var card = _context.LibraryCards
                .FirstOrDefault(c => c.Id == libraryCardId);

            if (asset.Status.Name =="Available")
            {
                UpdateAssetStatus(assetId, "On Hold");
            }


            var hold = new Hold
            {
                HoldPlaced = now,
                LibraryAsset = asset,
                LibraryCard = card
            };

            _context.Add(hold);
            _context.SaveChanges();
        }

        public string GetCurrentHoldPatronName(int id)
        {
            var hold = _context.Holds
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .FirstOrDefault(h => h.Id == id);

            var cardId = hold?.LibraryCard.Id;
        
            var patron = _context.Patrons
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.LibraryCard.Id == cardId);

            return patron?.FirstName + " " + patron?.LastName;
        }

        public DateTime GetCurrentHoldPlaced(int id)
        {
            return _context.Holds
                  .Include(h => h.LibraryAsset)
                  .Include(h => h.LibraryCard)
                  .FirstOrDefault(h => h.Id == id).HoldPlaced;
        }

        public string GetCurrentCheckoutPatron(int assetId)
        {

            var checkout = GetCheckoutByAssetId(assetId);
            if (checkout == null)
            {
                return "";
            }
            var cardId = checkout.LibraryCard.Id;

            var patron = _context.Patrons
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.LibraryCard.Id == cardId);

            return patron.FirstName + " " + patron.LastName;
        }

        private Checkout GetCheckoutByAssetId(int assetId)
        {
            return _context.Checkouts
                .Include(co => co.LibraryAsset)
                .Include(co => co.LibraryCard)
                .FirstOrDefault(co => co.LibraryAsset.Id == assetId);
        }

       
    }
}
