using System;
using System.Collections.Generic;
using System.IO;
using Foundation;
using SQLite;
using UIKit;

namespace HowTo
{
    internal class HowToTableViewSource : UITableViewSource
    {
        private List<HowToItem> howTos;

        public HowToTableViewSource(List<HowToItem> howTos)
        {
            this.howTos = howTos;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            HowToItem howTo = howTos[indexPath.Row];
            var cell = (HowToCell)tableView.DequeueReusableCell("Cell_ID", indexPath);

            cell.UpdateCell(howTo);
            return cell;
        }

        //https://forums.xamarin.com/discussion/2355/how-to-programmatically-tap-a-uitableviewcell

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return howTos.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //https://www.c-sharpcorner.com/article/how-to-pass-data-between-two-viewcontroller-in-xamarin-ios/
        }

        public HowToItem GetItem(int id)
        {
            return howTos[id];
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {

            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    // remove the item from the underlying data source
                    HowToItem selecteditem = howTos[indexPath.Row];
                    RemoveHowToFromDB(selecteditem.Id);
                    howTos.RemoveAt(indexPath.Row);

                    // delete the row from the table
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);

                    break;

                case UITableViewCellEditingStyle.None:
                    Console.WriteLine("CommitEditingStyle:None called");
                    break;
            }
            {

            };
        }
        
        public void RemoveHowToFromDB(int id)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(dbPath);

            HowToItem selectedHowTo = db.Table<HowToItem>().Where(e => e.Id == id).FirstOrDefault();

            db.Delete(selectedHowTo);
        }
    }
}