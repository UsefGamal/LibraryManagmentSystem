using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
    public interface ICheckout
    {

        void Add(Checkout newCheckout);
        

        IEnumerable<Checkout> GetAll();
        IEnumerable<CheckoutsHistory> GetCheckutHistory(int id);
        IEnumerable<Hold> GetCurrentHolds(int id);


        Checkout GetById(int checkoutId);
        Checkout GetLatestCheckout(int id);
        string GetCurrentCheckoutPatron(int id);
        string GetCurrentHoldPatronName(int id);
        DateTime GetCurrentHoldPlaced(int id);





        void CheckoutItem(int assetId, int libraryCardId);
        void CheckinItem(int assetId);
        void PlaceHold(int assetId, int libraryCardId);
        void MarkLost(int assetId);
        void MarkFound(int assetId);



        /*  int GetNumberOfCopies(int id);*/
          bool IsCheckedOut(int id);
          









    }
}
