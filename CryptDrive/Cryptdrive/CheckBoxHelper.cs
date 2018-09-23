using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cryptdrive
{
    public class CheckBoxHelper : TreeNode
    {
        public string Label { get; set; }

        public bool DropBoxSync { get; set; }

        public bool DropBoxDelete { get; set; }

        public bool CryptDriveSync { get; set; }

        public bool CryptDriveDelete { get; set; }

        public CheckBoxHelper() : base()
        {
        }

        public CheckBoxHelper(string text) : base(text)
        {
            Label = text;
            Text = text;
            Name = text;
        }

        public CheckBoxHelper(string text, bool check1, bool check2, bool check3, bool check4) : this(text)
        {
            DropBoxSync = check1; DropBoxDelete = check2; CryptDriveSync = check3; CryptDriveDelete = check4;
        }
    }
}
