//using Foundation;
//using System;
//using System.Collections.Generic;
//using UIKit;
//using SQLite;
//using System.IO;

//namespace HowTo
//{
//    public partial class NewHowToViewController : UIViewController
//    {
//        public HowToItem HowToId { get; set; }
//        public ViewController Delegate { get; set; }
//        //public string StudentName { get; set; }

//        public NewHowToViewController(IntPtr handle) : base(handle)
//        {

//        }

//        //public NewHowToViewController(nint id)
//        //{
//        //    HowToId = id;
//        //}

//        public override void ViewDidLoad()
//        {
//            base.ViewDidLoad();


//            //UIAlertView alert = new UIAlertView();
//            //alert.Title = "Editing";
//            //alert.AddButton("OK");
//            //alert.Message = "ID: " + HowToId.Id + " Name: " + HowToId.Name;
//            //alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
//            //alert.Clicked += (object s, UIButtonEventArgs ev) => {
//            //    NewHowToTitle.Title = alert.GetTextField(0).Text;
//            //};

//            //alert.Show();

//            //List<string> names = new List<string>() {
//            //    "Nicholas", "Daniel", "Brett", "Leesa"
//            //};

//            List<HowToItemModule> modules = new List<HowToItemModule>();

//            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
//            var db = new SQLiteConnection(dbPath);

//            var table = db.Table<HowToItemModule>();

//            foreach (HowToItemModule module in table.Where(e => e.Module_HowToId == HowToId.Id))
//            {
//                modules.Add(module);
//            }

//            ModulesTableView.Source = new ModulesTableViewSource(modules);
//            //ModulesTableView.EstimatedRowHeight = 20f;

//            //ModulesTableView.RowHeight = UITableView.AutomaticDimension;
//            //ModulesTableView.ReloadData();

//            NewHowToTitle.Title = HowToId.Name;
//            NewTextBtn.Tag = HowToId.Id;
//            //NewItemsList.Source = new NewItemsListViewSource(names);
//        }

//        //public void ViewDidLoad(int value)
//        //{
//        //    base.ViewDidLoad();

//        //    UIAlertView alert = new UIAlertView();
//        //    alert.Title = "Create New How-TO";
//        //    alert.AddButton("OK");
//        //    alert.Message = "Please Enter a name for this how-to";
//        //    alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
//        //    alert.Clicked += (object s, UIButtonEventArgs ev) => {
//        //        NewHowToTitle.Title = alert.GetTextField(0).Text;
//        //    };

//        //    alert.Show();

//        //    List<string> names = new List<string>() {
//        //        "Nicholas", "Daniel", "Brett", "Leesa"
//        //    };

//        //    NewItemsList.Source = new NewItemsListViewSource(names);
//        //}



//        partial void UIBarButtonItem2686_Activated(UIBarButtonItem sender)
//        {
//            throw new NotImplementedException();
//        }


//        public override void ViewWillAppear(bool animated)
//        {
//            base.ViewWillAppear(animated);
//            //TitleText.Text = currentTask.Name;
//            //NotesText.Text = currentTask.Notes;
//            //DoneSwitch.On = currentTask.Done;
//        }

//        // this will be called before the view is displayed
//        public void SetTask(ViewController d, HowToItem item)
//        {
//            Delegate = d;
//            HowToId = item;
//        }

//        //add new text field
//        partial void UIBarButtonItem10417_Activated(UIBarButtonItem sender)
//        {
//            //get next item order id () to determine next order

//            //
//            nint selectedHowTo = sender.Tag;

//            HowToItemModule module = new HowToItemModule()
//            {
//                Module_HowToId = 1,
//                Module_Type = "Text",
//                Module_Image = "",
//                Module_Order = 5
//            };

//            UIAlertView alert = new UIAlertView();
//            alert.Title = "Editing";
//            alert.AddButton("OK");
//            alert.Message = "ID: " + HowToId.Id + " Name: " + HowToId.Name;
//            alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
//            alert.Clicked += (object s, UIButtonEventArgs ev) =>
//            {
//                module.Module_Text = alert.GetTextField(0).Text;
//            };

//            alert.Show();


//            // ModulesTableView.SetEditing(true, true);
//            //---- create a new item and add it to our underlying data
//            //ModulesTableView.Insert(indexPath.Row, newModule);
//            //---- insert a new row in the table
//            // ModulesTableView.InsertRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);


//            //ModulesTableView.Source..AddNewModuleToTable(HowToItemModule module);

//            //    ModulesTableView.BeginUpdates();
//            //    // insert the 'ADD NEW' row at the end of table display
//            //    ModulesTableView.InsertRows(new NSIndexPath[] {
//            //    NSIndexPath.FromRowSection (ModulesTableView.NumberOfRowsInSection (0), - 1)
//            //}, UITableViewRowAnimation.Fade);
//            //    // create a new item and add it to our underlying data (it is not intended to be permanent)

//            //    //ModulesTableView.ins.row.Add(module);
//            //    ModulesTableView.EndUpdates(); // applies the changes


//            //    ModulesTableView.BeginUpdates();
//            //    // insert the 'ADD NEW' row at the end of table display
//            //    ModulesTableView.InsertRows(new NSIndexPath[] {
//            //    NSIndexPath.FromRowSection (ModulesTableView.NumberOfRowsInSection (0), + 1)
//            //}, UITableViewRowAnimation.Fade);
//            //    // create a new item and add it to our underlying data (it is not intended to be permanent)

//            //    HowToItemModule newModule = new HowToItemModule()
//            //    {
//            //        Module_HowToId = 0,
//            //        Module_Id = 10,
//            //        Module_Order = 4,
//            //        Module_Text = "a new item added",
//            //        Module_Type = "Text",
//            //        Module_Image = ""
//            //    };

//            //    ModulesTableView.Add(newModule);
//            //    ModulesTableView.EndUpdates(); // applies the changes

//            //    ModulesTableView.BeginUpdates();
//            //    // insert the 'ADD NEW' row at the end of table display
//            //    ModulesTableView.InsertRows(new NSIndexPath[] {
//            //    NSIndexPath.FromRowSection (ModulesTableView.NumberOfRowsInSection (0), 0)
//            //}, UITableViewRowAnimation.Fade);
//            //    // create a new item and add it to our underlying data (it is not intended to be permanent)
//            //    ModulesTableView.Add(new UIView());
//            //    ModulesTableView.EndUpdates(); // applies the changes

//            //ModulesTableView.InsertRows((4,  )
//        }

//        //https://docs.microsoft.com/en-us/xamarin/ios/user-interface/controls/tables/creating-tables-in-a-storyboard
//    }
//}