using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using SQLite;
using System.IO;
using System.Linq;
using Foundation;

namespace HowTo
{
    public partial class NewHowToViewController : UIViewController
    {
        public HowToItem HowToId { get; set; }
        public int seletedModule { get; set; }
        public ViewController Delegate { get; set; }

        UIImagePickerController picker;
        UIImagePickerController imagePicker;
        UIButton choosePhotoButton;
        UIImageView imageView;
        UIImage originalImage;

        public NewHowToViewController (IntPtr handle) : base (handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //editing existing how-to, will have an ID of the selected How-to
            if (HowToId != null && HowToId.Id != 0)
            {
                List<HowToItemModule> modules = new List<HowToItemModule>();
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
                var db = new SQLiteConnection(dbPath);

                var table = db.Table<HowToItemModule>();

                foreach (HowToItemModule module in table.Where(e => e.Module_HowToId == HowToId.Id))
                {
                    modules.Add(module);
                }

                ModulesTableViewSource tableViewSource = new ModulesTableViewSource(modules);
                tableViewSource.HandleOnRowSelect += HandleOnRowSelect;

                ModulesTableView.Source = tableViewSource;
                ModulesTableView.RowHeight = UITableView.AutomaticDimension;
                ModulesTableView.EstimatedRowHeight = 40; 

                NewHowToTitle.Title = HowToId.Name;
                NewTextBtn.Tag = HowToId.Id;
            }

            //creating a new how-to, wont have an ID
            //prompt user for a new how-to title and description
            else {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
                var db = new SQLiteConnection(dbPath);

                HowToItem newHowto = new HowToItem()
                {
                    CreationDate = DateTime.Now.ToString("dd/MM/yyyy"),
                };

                UIAlertView alert = new UIAlertView();
                alert.Title = "New how-to";
                alert.Message = "Enter a title for this how-to";
                alert.AddButton("Create");
                alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;

                alert.Clicked += (object s, UIButtonEventArgs ev) =>
                {
                    newHowto.Name = alert.GetTextField(0).Text;
                    
                    UIAlertView alertDescription = new UIAlertView();
                    alertDescription.Title = "New how-to";
                    alertDescription.Message = "Enter a description for this how-to";
                    alertDescription.AddButton("Save");
                    alertDescription.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
                    alertDescription.Clicked += (object e, UIButtonEventArgs eve) =>
                    {
                        newHowto.Description = alertDescription.GetTextField(0).Text;

                        db.Insert(newHowto);

                        HowToId = db.Table<HowToItem>().LastOrDefault();
                        NewHowToTitle.Title = HowToId.Name;
                        NewTextBtn.Tag = HowToId.Id;
                    };
                    alertDescription.Show();
                };
     
                alert.Show();
            }
        }

        void HandleOnRowSelect(int moduleId)
        {
            this.PerformSegue("ImageFullView",null);
        }

        partial void UIBarButtonItem2686_Activated(UIBarButtonItem sender)
        {
            // create a new picker controller
            imagePicker = new UIImagePickerController();

            // set our source to the photo library
            imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

            // set what media types
            imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

            imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
            imagePicker.Canceled += Handle_Canceled;

            // show the picker
            this.PresentModalViewController(imagePicker, true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        // this will be called before the view is displayed
        public void SetTask(ViewController d, HowToItem item)
        {
            Delegate = d;
            HowToId = item;
        }

        //add new text field button
        partial void UIBarButtonItem10417_Activated(UIBarButtonItem sender)
        {
            // dbPath contains a valid file path for the database file to be stored
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(dbPath);

            //get next item order id () to determine next order
            int howToID = (int)sender.Tag;

            HowToItem selectedHowTo = db.Table<HowToItem>().Where(e => e.Id == howToID).FirstOrDefault();
            List<HowToItemModule> modules = db.Table<HowToItemModule>().Where(e => e.Module_HowToId == howToID).OrderBy(e => e.Module_Order).ToList();

            int nextOrder;

            if (modules.Count == 0)
            {
                nextOrder = 1;
            }
            else
            {
                nextOrder = modules.Last().Module_Order + 1;
            }

            HowToItemModule module = new HowToItemModule()
            {
                Module_HowToId = HowToId.Id,
                Module_Type = "Text",
                Module_Image = new byte[0], 
                Module_Order = nextOrder
            };

            UIAlertView alert = new UIAlertView();
            alert.Title = "Editing";
            alert.AddButton("Create");
            alert.AddButton("Cancel");
            alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
            alert.Clicked += (object s, UIButtonEventArgs ev) =>
            {
                if (ev.ButtonIndex == 0)
                {
                    module.Module_Text = alert.GetTextField(0).Text;
                    db.Insert(module);
                    updateModulesTableView();
                }
                else if (ev.ButtonIndex == 1) {
                    //cancel
                }
            };

            alert.Show();
        }

        //refreshes the tableview data and reloads it for the user
        public void updateModulesTableView()
        {
            List<HowToItemModule> modules = new List<HowToItemModule>();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(dbPath);

            var table = db.Table<HowToItemModule>();

            foreach (HowToItemModule module in table.Where(e => e.Module_HowToId == HowToId.Id))
            {
                modules.Add(module);
            }

            ModulesTableView.Source = new ModulesTableViewSource(modules);
            ModulesTableView.ReloadData();
        }

        void Handle_Canceled(object sender, EventArgs e)
        {
            Console.WriteLine("picker cancelled");
            imagePicker.DismissModalViewController(true);
        }

        protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            // determine what was selected, video or image
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    Console.WriteLine("Image selected");
                    isImage = true;
                    break;

                case "public.video":
                    Console.WriteLine("Video selected");
                    break;
            }

            Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");

            // get common info (shared between images and video)
            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceURL")] as NSUrl;
            if (referenceURL != null)
                Console.WriteLine(referenceURL.ToString());

            // if it was an image, get the other image info
            if (isImage)
            {

                // get the original image
                originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    // do something with the image
                    Console.WriteLine("got the original image");

                    //imageView.Image = originalImage;
                }

                //- get the image metadata
                NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
                if (imageMetadata != null)
                {
                    // do something with the metadata
                    Console.WriteLine("got image metadata");
                }

            }
            // if it's a video
            else
            {
                // get video url
                NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    //
                    Console.WriteLine(mediaURL.ToString());
                }
            }
            //https://forums.xamarin.com/discussion/54409/convert-byte-array-to-image
            //enter reference url into database
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(dbPath);
            HowToItem selectedHowTo = db.Table<HowToItem>().Where(a => a.Id == HowToId.Id).FirstOrDefault();
            List<HowToItemModule> modules = db.Table<HowToItemModule>().Where(a => a.Module_HowToId == HowToId.Id).OrderBy(a => a.Module_Order).ToList();

            int nextOrder;

            if (modules.Count == 0)
            {
                nextOrder = 1;
            }
            else
            {
                nextOrder = modules.Last().Module_Order + 1;
            }

            HowToItemModule module = new HowToItemModule()
            {
                Module_HowToId = HowToId.Id,
                Module_Type = "Image",
                Module_Image = originalImage.AsPNG().ToArray(),
                Module_Order = nextOrder
            };

            db.Insert(module);

            // dismiss the picker
            imagePicker.DismissModalViewController(true);

            updateModulesTableView();
        }
    }
}