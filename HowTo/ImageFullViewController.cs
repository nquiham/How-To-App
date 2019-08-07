using Foundation;
using SQLite;
using System;
using System.IO;
using UIKit;

namespace HowTo
{
    public partial class ImageFullViewController : UIViewController
    {
        public int HowToModuleId { get; set; }

        public ImageFullViewController (IntPtr handle, NSObject sender) : base (handle)
        {
            NSString key = new NSString("selectedmodule");
            NSObject a = sender.ValueForKey(key);
            HowToModuleId = 1;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
                var db = new SQLiteConnection(dbPath);

                HowToItemModule selectedModule = db.Table<HowToItemModule>().Where(e => e.Module_Id == HowToModuleId).FirstOrDefault();

                using (var data = NSData.FromArray(selectedModule.Module_Image))
                {
                    ModuleImage.Image = UIImage.LoadFromData(data);
                }

        }
    }
}