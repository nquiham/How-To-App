using Foundation;
using System;
using UIKit;

namespace HowTo
{
    public partial class HowToCell : UITableViewCell
    {
        public HowToCell (IntPtr handle) : base (handle)
        {
        }

        internal void UpdateCell(HowToItem howTo)
        {
            Tag = howTo.Id;
            HowToTitleLabel.Text = howTo.Name;
            HowToDescriptionLabel.Text = howTo.Description;
            HowToDateLabel.Text = "Created " + howTo.CreationDate;
        }
    }
}