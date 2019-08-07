using Foundation;
using System;
using System.Collections.Generic;
using System.Xml;
using UIKit;
using SQLite;
using System.IO;

namespace HowTo
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            GenerateDatabase();
            var HowTos = ReadDatabase();

            HowToTableView.Source = new HowToTableViewSource(HowTos);
            HowToTableView.RowHeight = UITableView.AutomaticDimension;
            HowToTableView.RowHeight = 80f;
            HowToTableView.ReloadData ();
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        internal List<HowToItem> ReadDatabase()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(dbPath);

            var table = db.Table<HowToItem>();

            List<HowToItem> howTos = new List<HowToItem>();

            foreach (HowToItem howto in table)
            {
                howTos.Add(howto);
                Console.WriteLine(howto.Id + " " + howto.Name);
            }

            return howTos;

        }

        public void GenerateDatabase()
        {
            // dbPath contains a valid file path for the database file to be stored
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(dbPath);

            db.CreateTable<HowToItem>();

            //if no data in database, generate demo data
            if (db.Table<HowToItem>().Count() == 0)
            {
                List<HowToItem> HowTos = new List<HowToItem>() {
                new HowToItem{
                        Name = "How to sharpen a pencil",
                        Description = "This how-to will show you how to sharpen a pencil.",
                        CreationDate = "14/07/2019"
                    },
                    new HowToItem{
                        Name = "Create a VM",
                        Description = "Install a new Windows Virtual Machine.",
                        CreationDate = "15/07/2019"
                    }
                };

                foreach (HowToItem item in HowTos)
                {
                    db.Insert(item);
                }
            }


            db.CreateTable<HowToItemModule>();

            //if no data in database, generate demo data
            if (db.Table<HowToItemModule>().Count() == 0)
            {
                List<HowToItemModule> modules = new List<HowToItemModule>() {
                new HowToItemModule{
                        Module_HowToId = 1,
                        Module_Type = "Text",
                        Module_Image = new byte[0],
                        Module_Order = 1,
                        Module_Text = "This is step number 1 for the how to sharpen a pencil."
                    },

                    new HowToItemModule{
                        Module_HowToId = 1,
                        Module_Type = "Text",
                        Module_Image = new byte[0],
                        Module_Order = 2,
                        Module_Text = "This is another step for the how to sharpen a pencil, it is step number 2."
                    },

                    new HowToItemModule{
                        Module_HowToId = 1,
                        Module_Type = "Text",
                        Module_Image = new byte[0],
                        Module_Order = 3,
                        Module_Text = "Step number 3 for the how to sharpen a pencil"
                    }
                };

                foreach (HowToItemModule item in modules)
                {
                    db.Insert(item);
                }
            }

        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "HowToSegue")
            { // set in Storyboard
                var navctlr = segue.DestinationViewController as NewHowToViewController;
                if (navctlr != null)
                {
                    var source = HowToTableView.Source as HowToTableViewSource;
                    var rowPath = HowToTableView.IndexPathForSelectedRow;
                    var item = source.GetItem(rowPath.Row);
                    navctlr.SetTask(this, item); // to be defined on the TaskDetailViewController
                }
            }
        }
    }
}