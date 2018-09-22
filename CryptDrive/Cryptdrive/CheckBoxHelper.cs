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

        public bool Check1 { get; set; }

        public bool Check2 { get; set; }

        public bool Check3 { get; set; }

        /*
        public new string Text
        {
            get { return Label; }
            set { Label = value; base.Text = ""; }
        }*/

        public CheckBoxHelper() : base()
        {
        }

        public CheckBoxHelper(string text) : base(text)
        {
            Label = text;
        }

        public CheckBoxHelper(string text, bool check1, bool check2, bool check3) : this(text)
        {
            Check1 = check1; Check2 = check2; Check3 = check3;
        }

        public CheckBoxHelper(string text, CheckBoxHelper[] children)
        {
            Label = text;
            foreach (CheckBoxHelper node in children) this.Nodes.Add(node);
        }
    }
}
