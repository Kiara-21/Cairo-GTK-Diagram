using System;
using System.Collections.Generic;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    public List <string> global_1 = new List<string>();
    public int i = 0;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        textview1.WidthRequest = 200;
        textview1.HeightRequest = 250;
        drawingarea2.WidthRequest = 400;
        drawingarea2.HeightRequest = 400;

        drawingarea2.ExposeEvent += DrawingPlace;
        drawingarea2.Visible = false;
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void startdrawing(object sender, EventArgs e)
    {
        drawingarea2.Visible = true;
    }

    protected void addstring(object sender, EventArgs e)
    {
        i = i + 1; 
        string _part = "Период: " + Convert.ToString(i) + ", выручка: " +
        entry2.Text + "\n\n";
        global_1.Add(entry2.Text);
        TextIter insertIter = textview1.Buffer.StartIter;
        textview1.Buffer.Insert(ref insertIter, _part);
    }

    protected void killapp(object sender, EventArgs e)
    {
        Application.Quit();
    }

    protected void DrawingPlace(object sender, ExposeEventArgs args)
    {
        DrawingArea area = (DrawingArea)sender;
        Cairo.Context cc = Gdk.CairoHelper.Create(area.GdkWindow);

        cc.SelectFontFace("Sans", Cairo.FontSlant.Italic, Cairo.FontWeight.Bold);
        cc.SetFontSize(25);
        cc.MoveTo(15, 20);
        cc.ShowText("Диаграмма");
        cc.MoveTo(330, 40);
        cc.SetFontSize(15);
        cc.SelectFontFace("Sans", Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);
        cc.ShowText("Выручка");

        cc.SetSourceRGB(0.2, 0.23, 0.9);
        cc.LineWidth = 1;

        int s = 50;
        int s_next = 40;
        int w;
        int i = 0;
        int h = 65;

        foreach (var item in global_1)
        {
           w = Convert.ToInt32(item);
           cc.SetFontSize(15);
           cc.MoveTo(0, h);
           cc.SetSourceRGB(0, 0, 0);  
           cc.SelectFontFace("Sans", Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);
           cc.ShowText(Convert.ToString(i+1));
           i = i + 1;
           cc.SetSourceRGB(0.2, 0.23, 0.9);
           cc.Rectangle(10, s, w, 30);
           h = h + 40;
           s = s + s_next;
        }

        cc.LineTo(10, 50);
        cc.LineTo(10, 390);
        cc.StrokePreserve();
        cc.Fill();

        cc.LineTo(10, 50);
        cc.LineTo(390, 50);
        cc.StrokePreserve();
        cc.Fill();

        cc.MoveTo(0, 390);
        cc.SetSourceRGB(0, 0, 0);
        cc.SetFontSize(15);
        cc.SelectFontFace("Sans", Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);
        cc.ShowText("Период");

        ((IDisposable)cc).Dispose();
    
    }

}
