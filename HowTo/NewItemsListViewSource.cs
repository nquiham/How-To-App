using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

//to do
//create custom viewmodel that takees in a list of items, each item could be of text or image
//return custom view model to table
//display custom cell 

namespace HowTo
{
    class NewItemsListViewSource : UITableViewSource
    {
        private List<string> names;

        public NewItemsListViewSource(List<string> names)
        {
            this.names = names;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Default, "");
            cell.TextLabel.Text = names[indexPath.Row];

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return names.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var selectedRow = names[indexPath.Row];
        }
    }
}