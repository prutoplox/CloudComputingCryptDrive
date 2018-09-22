using Cryptdrive;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

//..

public partial class UcTreeView : TreeView
{
    bool temp = true;

    [DisplayName("Checkbox Spacing"), CategoryAttribute("Appearance"),
     Description("Number of pixels between the checkboxes.")]
    public int Spacing { get; set; }

    [DisplayName("Text Padding"), CategoryAttribute("Appearance"),
     Description("Left padding of text.")]
    public int LeftPadding { get; set; }

    public UcTreeView()
    {
        DrawMode = TreeViewDrawMode.OwnerDrawText;
        HideSelection = false;    // I like that better
        CheckBoxes = false;       // necessary!
        FullRowSelect = false;    // necessary!
        Spacing = 4;              // default checkbox spacing
        LeftPadding = 7;          // default text padding
    }

    /* public CheckBoxHelper AddNode(string label, bool check1, bool check2, bool check3)
     {
         CheckBoxHelper node = new CheckBoxHelper(label, check1, check2, check3);
         this.Nodes.Add(node);
         return node;
     }*/

    private Size glyph = Size.Empty;

    protected override void OnDrawNode(DrawTreeNodeEventArgs e)
    {
        CheckBoxHelper n = e.Node as CheckBoxHelper;
        if (n == null) { e.DrawDefault = true; return; }

        CheckBoxState cbsTrue = CheckBoxState.CheckedNormal;
        CheckBoxState cbsFalse = CheckBoxState.UncheckedNormal;

        CheckBoxState bs1 = n.Check1 ? cbsTrue : cbsFalse;
        CheckBoxState bs2 = n.Check2 ? cbsTrue : cbsFalse;
        CheckBoxState bs3 = n.Check3 ? cbsTrue : cbsFalse;

        Rectangle rect = new Rectangle(e.Bounds.Location,
                             new Size(ClientSize.Width, e.Bounds.Height));
        glyph = CheckBoxRenderer.GetGlyphSize(e.Graphics, cbsTrue);
        int offset = glyph.Width * 3 + Spacing * 2 + LeftPadding;

        if (n.IsSelected)
        {
            e.Graphics.FillRectangle(Brushes.DarkBlue, rect);
            e.Graphics.DrawString(n.Label, Font, Brushes.White,
                                  e.Bounds.X + offset, e.Bounds.Y);
        }
        else
        {
            Console.WriteLine(n.FullPath);
            var tet = n.PrevNode;
            if (true)
            {
                e.Graphics.FillRectangle(Brushes.HotPink, rect);
                temp = false;
            }
            else if (true)
            {
                e.Graphics.FillRectangle(Brushes.Cyan, rect);
                temp = true;
            }

            e.Graphics.DrawString(n.Label, Font, Brushes.Black,
                                  e.Bounds.X + offset, e.Bounds.Y);
        }

        CheckBoxRenderer.DrawCheckBox(e.Graphics, cbx(e.Bounds, 0).Location, bs1);
        CheckBoxRenderer.DrawCheckBox(e.Graphics, cbx(e.Bounds, 1).Location, bs2);
        CheckBoxRenderer.DrawCheckBox(e.Graphics, cbx(e.Bounds, 2).Location, bs3);

        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 74, 218, 237)), cbx(e.Bounds, 0));
        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 161, 77, 87)), cbx(e.Bounds, 1));
        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 43, 235, 123)), cbx(e.Bounds, 2));
    }

    protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
    {
        Console.WriteLine(e.Location + " bounds:" + e.Node.Bounds);

        CheckBoxHelper n = e.Node as CheckBoxHelper;
        if (e == null) return;

        if (cbx(n.Bounds, 0).Contains(e.Location)) n.Check1 = !n.Check1;
        else if (cbx(n.Bounds, 1).Contains(e.Location)) n.Check2 = !n.Check2;
        else if (cbx(n.Bounds, 2).Contains(e.Location)) n.Check3 = !n.Check3;
        else
        {
            if (SelectedNode == n && Control.ModifierKeys == Keys.Control)
                SelectedNode = SelectedNode != null ? null : n;
            else SelectedNode = n;
        }

        Console.WriteLine(" " + n.Check1 + " " + n.Check2 + " " + n.Check3);

        Invalidate();
    }

    Rectangle cbx(Rectangle bounds, int check)
    {
        return new Rectangle(bounds.Left + 2 + (glyph.Width + Spacing) * check,
                             bounds.Y + 2, glyph.Width, glyph.Height);
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();

        //
        // UcTreeView
        //
        this.LineColor = System.Drawing.Color.Black;
        this.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.UcTreeView_DrawNode);
        this.ResumeLayout(false);
    }

    private void UcTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
    {
        IEnumerator enumerator = e.Node.Nodes.GetEnumerator();

        while (enumerator.MoveNext())
        {
        }
    }
}