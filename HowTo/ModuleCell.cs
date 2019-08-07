using AssetsLibrary;
using Foundation;
using System;
using UIKit;

namespace HowTo
{
    public partial class ModuleCell : UITableViewCell
    {
        public ModuleCell (IntPtr handle) : base (handle)
        {

        }

        internal void UpdateCellAsync(HowToItemModule module)
        {
            this.Tag = module.Module_Id;

            //check wat type of module it is and display text or image depending on the type of module
            if (module.Module_Type == "Text")
            {
                moduleText3.Text = module.Module_Text;
                
            }
            else {
                using (var data = NSData.FromArray(module.Module_Image))
                {
                    ModuleImage.Image = UIImage.LoadFromData(data);
                }

                moduleText3.Hidden = true;


            }
        }
    }
}