using System;
using System.Collections.Generic;
using System.IO;
using Foundation;
using SQLite;
using UIKit;

namespace HowTo
{
    internal class ModulesTableViewSource : UITableViewSource
    {
        private List<HowToItemModule> modules;
        public event Action<int> HandleOnRowSelect;

        public ModulesTableViewSource(List<HowToItemModule> modules)
        {
            this.modules = modules;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            HowToItemModule module = modules[indexPath.Row];
            var cell = (ModuleCell) tableView.DequeueReusableCell("Cell_ID", indexPath);
            cell.UpdateCellAsync(module);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return modules.Count;
        }

        public void WillBeginTableEditing(UITableView tableView)
        {

        }

        //action when a row of the tableview is selected
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            nint id = GetCell(tableView, indexPath).Tag;
            HowToItemModule selectedModule = GetModuleItemByID((int) id);

            if (selectedModule.Module_Type == "Text")
            {
                //https://www.c-sharpcorner.com/article/how-to-pass-data-between-two-viewcontroller-in-xamarin-ios/
                UIAlertView alert = new UIAlertView();
                alert.Title = "Update text";
                alert.AddButton("Save");
                alert.AddButton("Close");
                alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
                alert.GetTextField(0).Text = GetModuleItemByID((int)id).Module_Text;
                alert.Clicked += (object s, UIButtonEventArgs ev) =>
                {
                    if (ev.ButtonIndex == 0)
                    {
                        //save - update module record in te database
                        UpdateTextModule((int)id, alert.GetTextField(0).Text, tableView);
                    }
                    else if (ev.ButtonIndex == 1)
                    {
                        //cancel
                    }
                };

                alert.Show();
            }
            else
            {

                HandleOnRowSelect(selectedModule.Module_Id);
            }
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {

                switch (editingStyle)
                {
                    case UITableViewCellEditingStyle.Delete:
                        // remove the item from the underlying data source
                        
                        HowToItemModule selecteditem = modules[indexPath.Row];
                        RemoveModuleFromDB(selecteditem.Module_Id);
                    modules.RemoveAt(indexPath.Row);

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

        /// <summary>
        /// Called by the table view to determine whether or not the row is editable
        /// </summary>
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true; // return false if you wish to disable editing for a specific indexPath or for all rows
        }

        /// <summary>
		/// Called by the table view to determine whether the editing control should be an insert
		/// or a delete.
		/// </summary>
		public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (tableView.Editing)
            {
                if (indexPath.Row == tableView.NumberOfRowsInSection(0) - 1)
                    return UITableViewCellEditingStyle.Insert;
                else
                    return UITableViewCellEditingStyle.Delete;
            }
            else  // not in editing mode, enable swipe-to-delete for all rows
                return UITableViewCellEditingStyle.Delete;
        }

        public override NSIndexPath CustomizeMoveTarget(UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath proposedIndexPath)
        {
            var numRows = tableView.NumberOfRowsInSection(0) - 1; // less the (add new) one
            Console.WriteLine(proposedIndexPath.Row + " " + numRows);
            if (proposedIndexPath.Row >= numRows)
                return NSIndexPath.FromRowSection(numRows - 1, 0);
            else
                return proposedIndexPath;
        }

        //removes the selected module from the sqlite database using the module id
        public void RemoveModuleFromDB(int id)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(dbPath);

            HowToItemModule selectedModule = db.Table<HowToItemModule>().Where(e => e.Module_Id == id).FirstOrDefault();

            db.Delete(selectedModule);
        }

        //updates the selected module text, using module id to find the module
        public void UpdateTextModule(int id, string text, UITableView tableView)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(dbPath);

            HowToItemModule selectedModule = db.Table<HowToItemModule>().Where(e => e.Module_Id == id).FirstOrDefault();
            selectedModule.Module_Text = text;

            db.Update(selectedModule);
            RefreshList(selectedModule.Module_HowToId, tableView);
        }

        //refreshes the list of how-to modules within the tableview
        public void RefreshList(int id, UITableView tableView)
        {
            //refresh list 
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(dbPath);

            List<HowToItemModule> howToModules = db.Table<HowToItemModule>().Where(e => e.Module_HowToId == id).ToList();

            modules = howToModules;
            tableView.ReloadData();
        }

        //returns a how to module using the ID passed in
        public HowToItemModule GetModuleItemByID(int id)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(dbPath);

            HowToItemModule selectedModule = db.Table<HowToItemModule>().Where(e => e.Module_Id == id).FirstOrDefault();
            return selectedModule;
        }
    }
}