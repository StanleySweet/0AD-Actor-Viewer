using ActorEditor.Model;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ActorEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Actor _actor;
        bool _actorMode;
        bool _particleMode;
        bool _variantMode;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "XML Files (.xml)|*.xml";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == true)
            {
                _actor = FileHandler.OpenFile(openFileDialog1.FileName);
                floats.IsEnabled = true;
                castsShadows.IsEnabled = true;
                castsShadows.IsChecked = _actor.CastsShadows;
                floats.IsChecked = _actor.Floats;
                castsShadows.Visibility = Visibility.Visible;
                floats.Visibility = Visibility.Visible;
                listview.Visibility = Visibility.Visible;
                this.DataContext = _actor.Groups;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (_actor != null)
                FileHandler.SaveFile(_actor);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void castsShadows_Checked(object sender, RoutedEventArgs e)
        {
            _actor.CastsShadows = (bool)castsShadows.IsChecked;
        }

        private void floats_Checked(object sender, RoutedEventArgs e)
        {
            _actor.Floats = (bool)floats.IsChecked;
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {

        }
    }
}
