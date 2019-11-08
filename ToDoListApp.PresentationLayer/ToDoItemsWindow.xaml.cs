using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoListApp.BusinessLogicLayer;
using ToDoListApp.DataAccessLayer;

namespace ToDoListApp.PresentationLayer
{
    /// <summary>
    /// Interaction logic for ToDoItemsWindow.xaml
    /// </summary>
    public partial class ToDoItemsWindow : Window
    {
        public Process process { get; set; }

        public ToDoItemsWindow( Process _process )
        {
            this.process = _process;
            InitializeComponent();
            FillTitle();
            RefreshPage();
        }
        private void FillTitle()
        {
            Label titleLabel = new Label()
            {
                Content=process.selectedList.listName.ToString() + " Items",
                Style = (Style)FindResource("titleLabelStyle")
            };
            this.itemTitleStackPanel.Children.Add( titleLabel );

            Separator titleSeparator = new Separator();
            this.itemTitleStackPanel.Children.Add( titleSeparator );
        }

        private void FillBody()
        {
            List<ToDoItem> items = new List<ToDoItem>();

            if ( process.orderedItemsList.Count > 0 && process.filterSelection == Process.FilterSelection.Default )
            {
                items = process.orderedItemsList;
                process.orderedItemsList = process.OrderItems( Process.OrderBySelection.CreateDate );
            }
            else if ( process.orderedItemsList.Count > 0 && (process.filterSelection != Process.FilterSelection.Default) )
            {
                items = process.orderedItemsList;
            }
            else
            {
                //items = process.GetItems( process.selectedList );
                process.orderedItemsList = process.OrderItems( Process.OrderBySelection.CreateDate );
                items = process.orderedItemsList;
            }


            if ( items.Count > 0 )
            {
                foreach ( ToDoItem item in items )
                {
                    if ( item.itemStatus == Process.Status.NotCompleted.ToString() )
                    {
                        Button markButton = new Button()
                        {
                            Style=(Style)FindResource("markButtonsStyle"),
                            Tag=item
                        };
                        this.itemMarkStackPanel.Children.Add( markButton );
                        markButton.Click += MarkButton_Click;
                    }
                    else
                    {
                        Button markButton = new Button()
                        {
                            Style = (Style)FindResource("markButtonsStyle"),
                            IsEnabled = false,
                            Tag = item
                        };
                        this.itemMarkStackPanel.Children.Add( markButton );
                    }

                    if ( item.itemStatus == Process.Status.NotCompleted.ToString() )
                    {
                        Label nameLabel = new Label()
                        {
                            Content = item.itemName.ToString(),
                            Style = (Style)FindResource("nameLabelStyle"),
                            Tag = item
                        };
                        this.itemNameStackPanel.Children.Add( nameLabel );
                        nameLabel.MouseDown += NameLabel_MouseDown;
                    }
                    else if ( item.itemStatus == Process.Status.Expired.ToString() )
                    {
                        Label nameLabel = new Label()
                        {
                            Content = item.itemName.ToString(),
                            Style = (Style)FindResource("expiredNameLabelStyle"),
                            Tag = item
                        };
                        this.itemNameStackPanel.Children.Add( nameLabel );
                        nameLabel.MouseDown += NameLabel_MouseDown;
                    }
                    else
                    {
                        Label nameLabel = new Label()
                        {
                            Content = item.itemName.ToString(),
                            Style = (Style)FindResource("completedNameLabelStyle"),
                            Tag = item
                        };
                        this.itemNameStackPanel.Children.Add( nameLabel );
                        nameLabel.MouseDown += NameLabel_MouseDown;
                    }

                    Label deadlineLabel = new Label()
                    {
                        Content = item.itemDeadline.Day.ToString() + "." +item.itemDeadline.Month.ToString() + "." + item.itemDeadline.Year.ToString(),
                        Style = (Style)FindResource("itemLabelStyle")
                    };
                    this.itemDeadlineStackPanel.Children.Add( deadlineLabel );

                    Label statusLabel = new Label()
                    {
                        Content = item.itemStatus.ToString(),
                        Style = (Style)FindResource("itemLabelStyle")
                    };
                    this.itemStatusStackPanel.Children.Add( statusLabel );

                    Label xLabel = new Label()
                    {
                        Style = (Style)FindResource("deleteLabelStyle"),
                        Tag = item
                    };
                    this.itemDeleteIconStackPanel.Children.Add( xLabel );
                    xLabel.MouseDown += XLabel_MouseDown;
                }
            }

            Label filterLabel = new Label()
            {
                Style = (Style)FindResource("filterLabel")
            };
            this.filterDockPanel.Children.Add( filterLabel );

            RadioButton defaultRadioButton = new RadioButton()
            {
                Content="Default",
                Style = (Style)FindResource("statusRadioButton"),
                Tag = Process.FilterSelection.Default
            };
            this.filterDockPanel.Children.Add( defaultRadioButton );
            defaultRadioButton.Click += RadioButton_Click;

            RadioButton completedRadioButton = new RadioButton()
            {
                Content=Process.Status.Completed.ToString(),
                Style = (Style)FindResource("statusRadioButton"),
                Tag = Process.FilterSelection.Completed
            };
            this.filterDockPanel.Children.Add( completedRadioButton );
            completedRadioButton.Click += RadioButton_Click;

            RadioButton notCompletedRadioButton = new RadioButton()
            {
                Content=Process.Status.NotCompleted.ToString(),
                Style = (Style)FindResource("statusRadioButton"),
                Tag = Process.FilterSelection.NotCompleted
            };
            this.filterDockPanel.Children.Add( notCompletedRadioButton );
            notCompletedRadioButton.Click += RadioButton_Click;

            RadioButton expiredRadioButton = new RadioButton()
            {
                Content=Process.Status.Expired.ToString(),
                Style = (Style)FindResource("statusRadioButton"),
                Tag = Process.FilterSelection.Expired
            };
            this.filterDockPanel.Children.Add( expiredRadioButton );
            expiredRadioButton.Click += RadioButton_Click;

            orderByCreateDateButton.Tag = Process.OrderBySelection.CreateDate;
            orderByDeadlineButton.Tag = Process.OrderBySelection.Deadline;
            orderByNameButton.Tag = Process.OrderBySelection.Name;
            orderByStatusButton.Tag = Process.OrderBySelection.Status;
        }

        private void RadioButton_Click( object sender, RoutedEventArgs e )
        {
            RadioButton selectedRadioButton = (RadioButton)sender;
            process.orderedItemsList = process.FilterItemsByStatus( ( Process.FilterSelection )selectedRadioButton.Tag, process.orderedItemsList.ToList() );
            RefreshPage();
        }

        private void NameLabel_MouseDown( object sender, MouseButtonEventArgs e )
        {
            Label selectedLabel = (Label)sender;
            ToDoItem selectedItem = ( ToDoItem )selectedLabel.Tag;
            MessageBox.Show( selectedItem.itemDescription.ToString(), "Item Description" );
        }

        private void OrderButtons_Click( object sender, RoutedEventArgs e )
        {
            Button button = (Button)sender;
            process.orderedItemsList = process.OrderItems( ( Process.OrderBySelection )button.Tag );
            RefreshPage();
        }

        private void XLabel_MouseDown( object sender, MouseButtonEventArgs e )
        {
            Label selectedLabel = (Label)sender;
            ToDoItem deletedItem = ( ToDoItem )selectedLabel.Tag;
            process.RemoveToDoItem( deletedItem );
            MessageBox.Show( "The item deleted successfully!" );
            if ( process.orderedItemsList.Count > 0 )
            {
                process.orderedItemsList.Remove( deletedItem );
            }
            RefreshPage();
        }

        private void MarkButton_Click( object sender, RoutedEventArgs e )
        {
            Button selectedButton = (Button)sender;
            //process.selectedItem = ( ToDoItem )selectedButton.Tag;
            process.UpdateItem( ( ToDoItem )selectedButton.Tag );
            MessageBox.Show( "Well Done!" );
            RefreshPage();
        }

        private void RefreshPage()
        {
            itemNameStackPanel.Children.Clear();
            itemDeleteIconStackPanel.Children.Clear();
            itemMarkStackPanel.Children.Clear();
            itemStatusStackPanel.Children.Clear();
            itemDeadlineStackPanel.Children.Clear();
            filterDockPanel.Children.Clear();
            orderByCreateDateButton.Tag = null;
            orderByDeadlineButton.Tag = null;
            orderByNameButton.Tag = null;
            orderByStatusButton.Tag = null;
            FillBody();
        }

        private void BackButton_Click( object sender, RoutedEventArgs e )
        {
            process.orderedItemsList.Clear();
            process.selectedList = null;
            process.filterSelection = Process.FilterSelection.Default;
            ToDoListsWindow newToDoListWindow = new ToDoListsWindow(process);
            newToDoListWindow.Show();
            this.Close();
        }

        private void AddToDoItemButton_Click( object sender, RoutedEventArgs e )
        {
            process.orderedItemsList.Clear();
            AddToDoItemWindow addItemWindow = new AddToDoItemWindow(process);
            addItemWindow.Show();
            this.Close();
        }
    }
}
