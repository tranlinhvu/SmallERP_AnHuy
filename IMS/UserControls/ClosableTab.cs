using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using IMS.UserControls;

class ClosableTab : TabItem
{


    // Constructor
    public ClosableTab()
    {
        // Create an instance of the usercontrol
        ClosableHeader closableTabHeader = new ClosableHeader();

        // Assign the usercontrol to the tab header
        this.Header = closableTabHeader;

        // Attach to the CloseableHeader events (Mouse Enter/Leave, Button Click, and Label resize)
        closableTabHeader.btnClose.MouseEnter += new MouseEventHandler(btnClose_MouseEnter);
        closableTabHeader.btnClose.MouseLeave += new MouseEventHandler(btnClose_MouseLeave);
        closableTabHeader.btnClose.Click += new RoutedEventHandler(btnClose_Click);
        closableTabHeader.label_TabTitle.SizeChanged += new SizeChangedEventHandler(label_TabTitle_SizeChanged);
    }



    /// <summary>
    /// Property - Set the Title of the Tab
    /// </summary>
    public string Title
    {
        set
        {
            ((ClosableHeader)this.Header).label_TabTitle.Content = value;
        }
    }




    //
    // - - - Overrides  - - -
    //


    // Override OnSelected - Show the Close Button
    protected override void OnSelected(RoutedEventArgs e)
    {
        base.OnSelected(e);
        ((ClosableHeader)this.Header).btnClose.Visibility = Visibility.Visible;
    }

    // Override OnUnSelected - Hide the Close Button
    protected override void OnUnselected(RoutedEventArgs e)
    {
        base.OnUnselected(e);
        ((ClosableHeader)this.Header).btnClose.Visibility = Visibility.Hidden;
    }

    // Override OnMouseEnter - Show the Close Button
    protected override void OnMouseEnter(MouseEventArgs e)
    {
        base.OnMouseEnter(e);
        ((ClosableHeader)this.Header).btnClose.Visibility = Visibility.Visible;
    }

    // Override OnMouseLeave - Hide the Close Button (If it is NOT selected)
    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);
        if (!this.IsSelected)
        {
            ((ClosableHeader)this.Header).btnClose.Visibility = Visibility.Hidden;
        }
    }





    //
    // - - - Event Handlers  - - -
    //


    // Button MouseEnter - When the mouse is over the button - change color to Red
    void btnClose_MouseEnter(object sender, MouseEventArgs e)
    {
        ((ClosableHeader)this.Header).btnClose.Foreground = Brushes.Red;
    }

    // Button MouseLeave - When mouse is no longer over button - change color back to black
    void btnClose_MouseLeave(object sender, MouseEventArgs e)
    {
        ((ClosableHeader)this.Header).btnClose.Foreground = Brushes.Black;
    }


    // Button Close Click - Remove the Tab - (or raise an event indicating a "CloseTab" event has occurred)
    void btnClose_Click(object sender, RoutedEventArgs e)
    {
        ((TabControl)this.Parent).Items.Remove(this);
    }


    // Label SizeChanged - When the Size of the Label changes (due to setting the Title) set position of button properly
    void label_TabTitle_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        ((ClosableHeader)this.Header).btnClose.Margin = new Thickness(((ClosableHeader)this.Header).label_TabTitle.ActualWidth + 5, 3, 4, 0);
    }





}
