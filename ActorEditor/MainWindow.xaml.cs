using ActorEditor.Model;
using ActorEditor.Model.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        Group _currentGroup;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetActorMode()
        {
            _actorMode = true;
            _particleMode = false;
            _variantMode = false;
        }


        private void SetVariantMode()
        {
            _actorMode = false;
            _particleMode = false;
            _variantMode = true;
        }


        private void OpenActor(object sender, RoutedEventArgs e)
        {
            SetActorMode();
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
                actorOptions.Visibility = Visibility.Visible;
                groupview.Visibility = Visibility.Visible;
                variantview.Visibility = Visibility.Hidden;
                Materials.ItemsSource = FileHandler.GetMaterialList();
                this.DataContext = _actor.Groups;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (_actor != null && _actorMode && _actorMode)
                FileHandler.SaveFile(_actor);
            else if (_currentGroup != null && _currentGroup.Count > 0 && _variantMode)
                FileHandler.SaveFile(_currentGroup.FirstOrDefault());
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Exit(object sender, RoutedEventArgs e)
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

        private void CreateVariant(object sender, RoutedEventArgs e)
        {
            SetVariantMode();
            _currentGroup = new Group
            {
                new Variant()
            };

            GoBackButton.Visibility = Visibility.Hidden;
            AddVariantButton.Visibility = Visibility.Hidden;
            DeleteVariantButton.Visibility = Visibility.Hidden;
            groupview.Visibility = Visibility.Hidden;
            variantview.Visibility = Visibility.Visible;
            this.DataContext = _currentGroup;
        }

        private void CreateActor(object sender, RoutedEventArgs e)
        {
            SetActorMode();
            _actor = new Actor();
            floats.IsEnabled = true;
            castsShadows.IsEnabled = true;
            castsShadows.IsChecked = _actor.CastsShadows;
            floats.IsChecked = _actor.Floats;
            actorOptions.Visibility = Visibility.Visible;
            groupview.Visibility = Visibility.Visible;
            variantview.Visibility = Visibility.Hidden;
            Materials.ItemsSource = FileHandler.GetMaterialList();
            this.DataContext = _actor.Groups;
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _actor.Groups.Add(new Group());
            ICollectionView view = CollectionViewSource.GetDefaultView(_actor.Groups);
            view.Refresh();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_currentGroup == null)
                return;

            _currentGroup.Add(new Variant());
            var groupIndex = _actor.Groups.IndexOf(_currentGroup);
            ICollectionView view = CollectionViewSource.GetDefaultView(_actor.Groups[groupIndex]);
            view.Refresh();
        }

        private void EditGroup(object sender, RoutedEventArgs e)
        {
            foreach(var item in grouplistview.Items)
            {
                var group = (Group)item;
                if (group != null & group.IsChecked == true)
                {
                    _currentGroup = group;
                    break;
                }
            }
            _currentGroup = (Group)grouplistview.SelectedItem;

            if (_currentGroup == null)
                return;
                  
            groupview.Visibility = Visibility.Hidden;
            variantview.Visibility = Visibility.Visible;
            var groupIndex = _actor.Groups.IndexOf(_currentGroup);
            this.DataContext = _actor.Groups[groupIndex];
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            for (var i = grouplistview.Items.Count - 1; i != -1 ; --i)
            {
                var group = (Group)grouplistview.Items[i];
                if (group != null & group.IsChecked == true)
                {
                    var groupIndex = _actor.Groups.IndexOf(group);
                    _actor.Groups.RemoveAt(groupIndex);
                }
            }


            ICollectionView view = CollectionViewSource.GetDefaultView(_actor.Groups);
            view.Refresh();
        }

        private void DeleteVariant(object sender, RoutedEventArgs e)
        {
            for (var i = variantListview.Items.Count - 1; i != -1; --i)
            {
                var variant = (Variant)grouplistview.Items[i];
                if (variant != null & variant.IsChecked == true)
                {
                    _currentGroup.Remove(variant);
                }
            }

            var groupIndex = _actor.Groups.IndexOf(_currentGroup);
            ICollectionView view = CollectionViewSource.GetDefaultView(_actor.Groups[groupIndex]);
            view.Refresh();

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            groupview.Visibility = Visibility.Visible;
            variantview.Visibility = Visibility.Hidden;
            this.DataContext = _actor.Groups;
        }

        private void Materials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _actor.Material = this.Materials.SelectedItem.ToString();
        }

        private void EditTextures(object sender, RoutedEventArgs e)
        {

        }

        private void EditAnimations(object sender, RoutedEventArgs e)
        {

        }

        private void EditProps(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
    }
}
