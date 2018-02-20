namespace ActorEditor
{
    using ActorEditor.Model;
    using ActorEditor.Model.Entities;
    using Microsoft.Win32;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Actor _actor;
        private bool _actorMode;
        private bool _particleMode;
        private bool _soundGroupMode;
        private bool _variantMode;
        private Group _currentGroup;
        private Variant _currentVariant;
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Materials.ItemsSource = FileHandler.GetMaterialList();
        }

        private void SetActorMode()
        {
            _actorMode = true;
            _particleMode = false;
            _variantMode = false;
            _soundGroupMode = false;
            _particleMode = false;
        }

        private void SetVariantMode()
        {
            _variantMode = true;
            _actorMode = false;
            _particleMode = false;
            _soundGroupMode = false;
            _particleMode = false;

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
                _actor = FileHandler.OpenActorFile(openFileDialog1.FileName);

                if (_actor == null)
                {
                    MessageBoxResult result = MessageBox.Show("Error: Parsed file is either malformed or not a actor file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (result == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                    }
                    return;
                }

                floats.IsEnabled = true;
                castsShadows.IsEnabled = true;
                castsShadows.IsChecked = _actor.CastsShadows;

                var index = 0;
                foreach(var item in Materials.Items)
                {
                    if (item.ToString().Equals(_actor.Material))
                        break;
                    ++index;
                }

                Materials.SelectedIndex = index;
                floats.IsChecked = _actor.Floats;
                actorOptions.Visibility = Visibility.Visible;
                groupview.Visibility = Visibility.Visible;
                variantview.Visibility = Visibility.Collapsed;
                DeleteVariantButton.Visibility = Visibility.Visible;
                AddVariantButton.Visibility = Visibility.Visible;
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

        private void Exit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
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

            GoBackButton.Visibility = Visibility.Collapsed;
            AddVariantButton.Visibility = Visibility.Collapsed;
            DeleteVariantButton.Visibility = Visibility.Collapsed;
            groupview.Visibility = Visibility.Collapsed;
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
            variantview.Visibility = Visibility.Collapsed;
            Materials.ItemsSource = FileHandler.GetMaterialList();
            this.DataContext = _actor.Groups;
        }

        private void OpenVariant(object sender, RoutedEventArgs e)
        {
            SetVariantMode();


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
                var variant = FileHandler.OpenVariantFile(openFileDialog1.FileName);
                if (variant == null)
                {
                    MessageBoxResult result = MessageBox.Show("Error: Parsed file is either malformed or not a variant file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (result == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                    }
                    return;
                }
                _currentGroup = new Group
                {
                    variant
                };

                GoBackButton.Visibility = Visibility.Collapsed;
                AddVariantButton.Visibility = Visibility.Collapsed;
                DeleteVariantButton.Visibility = Visibility.Collapsed;
                groupview.Visibility = Visibility.Collapsed;
                variantview.Visibility = Visibility.Visible;
                this.DataContext = _currentGroup;
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {

        }


        private void AddGroup(object sender, RoutedEventArgs e)
        {
            _actor.Groups.Add(new Group());
            ICollectionView view = CollectionViewSource.GetDefaultView(_actor.Groups);
            view.Refresh();

        }

        private void GoBackToVariantView(object sender, RoutedEventArgs e)
        {
            animationView.Visibility = Visibility.Collapsed;
            textureView.Visibility = Visibility.Collapsed;
            propView.Visibility = Visibility.Collapsed;
            variantview.Visibility = Visibility.Visible;
            this.DataContext = _currentGroup;
        }

        private void DeleteGroups(object sender, RoutedEventArgs e)
        {
            for (var i = grouplistview.Items.Count - 1; i != -1; --i)
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

        private void Materials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _actor.Material = this.Materials.SelectedItem.ToString();
        }

        private void EditGroup(object sender, RoutedEventArgs e)
        {
            foreach (var item in grouplistview.Items)
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

            groupview.Visibility = Visibility.Collapsed;
            variantview.Visibility = Visibility.Visible;
            var groupIndex = _actor.Groups.IndexOf(_currentGroup);
            this.DataContext = _actor.Groups[groupIndex];
        }




        #region Animations
        private void AddAnimation(object sender, RoutedEventArgs e)
        {
            if (_currentVariant == null)
                return;

            var animations = _currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Animations;
            animations.Add(new Animation());
            ICollectionView view = CollectionViewSource.GetDefaultView(_currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Animations);
            view.Refresh();

        }

        private void DeleteAnimation(object sender, RoutedEventArgs e)
        {
            if (_currentVariant == null)
                return;

            for (var i = animationListView.Items.Count - 1; i != -1; --i)
            {
                var animation = (Animation)animationListView.Items[i];
                if (animation != null & animation.IsChecked == true)
                {
                    _currentVariant.Animations.Remove(animation);
                }
            }

            var animations = _currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Animations;
            ICollectionView view = CollectionViewSource.GetDefaultView(_currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Animations);
            view.Refresh();
        }

        private void EditAnimations(object sender, RoutedEventArgs e)
        {
            _currentVariant = (Variant)variantListview.SelectedItem;

            if (_currentVariant == null)
                return;

            animationView.Visibility = Visibility.Visible;
            variantview.Visibility = Visibility.Collapsed;
            this.DataContext = _currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Animations;
        }

        private void BrowseForAnimation(object sender, RoutedEventArgs e)
        {
            //// Create an instance of the open file dialog box.
            //OpenFileDialog animationFile = new OpenFileDialog();

            //// Set filter options and filter index.
            //animationFile.Filter = "DAE Files (.dae)|*.dae";
            //animationFile.FilterIndex = 1;

            //animationFile.Multiselect = false;

            //// Call the ShowDialog method to show the dialog box.
            //bool? userClickedOK = animationFile.ShowDialog();

            //// Process input if the user clicked OK.
            //if (userClickedOK == true)
            //{
            //    Variant variant;
            //    for (var i = variantListview.Items.Count - 1; i != -1; --i)
            //    {
            //        variant = (Variant)grouplistview.Items[i];
            //        if (variant != null & variant.IsChecked == true)
            //        {
            //            variant.Mesh = animationFile.FileName;
            //        }
            //    }

            //    variant = (Variant)variantListview.SelectedItem;
            //    variant.Mesh = animationFile.FileName;
            //}
            //var groupIndex = _actor.Groups.IndexOf(_currentGroup);
            //ICollectionView view = CollectionViewSource.GetDefaultView(_actor.Groups[groupIndex]);
            //view.Refresh();
        }


        #endregion
        #region Props
        private void EditProps(object sender, RoutedEventArgs e)
        {
            _currentVariant = (Variant)variantListview.SelectedItem;

            if (_currentVariant == null)
                return;

            propView.Visibility = Visibility.Visible;
            variantview.Visibility = Visibility.Collapsed;
            this.DataContext = _currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Props;
        }

        private void DeleteProp(object sender, RoutedEventArgs e)
        {
            _currentVariant = (Variant)variantListview.SelectedItem;

            if (_currentVariant == null)
                return;

            for (var i = propViewListView.Items.Count - 1; i != -1; --i)
            {
                var prop = (Prop)propViewListView.Items[i];
                if (prop != null & prop.IsChecked == true)
                {
                    _currentVariant.Props.Remove(prop);
                }
            }
            CollectionViewSource.GetDefaultView(_currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Props).Refresh();
        }

        private void AddProp(object sender, RoutedEventArgs e)
        {
            if (_currentVariant == null)
                return;

            var props = _currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Props;
            props.Add(new Prop());
            CollectionViewSource.GetDefaultView(_currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Props).Refresh();
        }

        private void BrowseForProp(object sender, RoutedEventArgs e)
        {
            //// Create an instance of the open file dialog box.
            //OpenFileDialog animationFile = new OpenFileDialog();

            //// Set filter options and filter index.
            //animationFile.Filter = "DAE Files (.dae)|*.dae";
            //animationFile.FilterIndex = 1;

            //animationFile.Multiselect = false;

            //// Call the ShowDialog method to show the dialog box.
            //bool? userClickedOK = animationFile.ShowDialog();

            //// Process input if the user clicked OK.
            //if (userClickedOK == true)
            //{
            //    Variant variant;
            //    for (var i = variantListview.Items.Count - 1; i != -1; --i)
            //    {
            //        variant = (Variant)grouplistview.Items[i];
            //        if (variant != null & variant.IsChecked == true)
            //        {
            //            variant.Mesh = animationFile.FileName;
            //        }
            //    }

            //    variant = (Variant)variantListview.SelectedItem;
            //    variant.Mesh = animationFile.FileName;
            //}
            //var groupIndex = _actor.Groups.IndexOf(_currentGroup);
            //ICollectionView view = CollectionViewSource.GetDefaultView(_actor.Groups[groupIndex]);
            //view.Refresh();
        }

        #endregion

        private void GoBackToGroupView(object sender, RoutedEventArgs e)
        {
            groupview.Visibility = Visibility.Visible;
            variantview.Visibility = Visibility.Collapsed;
            this.DataContext = _actor.Groups;
        }

        private void AddVariant(object sender, RoutedEventArgs e)
        {
            if (_currentGroup == null)
                return;

            _currentGroup.Add(new Variant());
            var groupIndex = _actor.Groups.IndexOf(_currentGroup);
            ICollectionView view = CollectionViewSource.GetDefaultView(_actor.Groups[groupIndex]);
            view.Refresh();
        }

        private void DeleteVariant(object sender, RoutedEventArgs e)
        {
            for (var i = variantListview.Items.Count - 1; i != -1; --i)
            {
                var variant = (Variant)variantListview.Items[i];
                if (variant != null & variant.IsChecked == true)
                {
                    _currentGroup.Remove(variant);
                }
            }

            CollectionViewSource.GetDefaultView(_currentGroup).Refresh();
        }

        private void EditTextures(object sender, RoutedEventArgs e)
        {
            _currentVariant = (Variant)variantListview.SelectedItem;

            if (_currentVariant == null)
                return;

            textureView.Visibility = Visibility.Visible;
            variantview.Visibility = Visibility.Collapsed;
            this.DataContext = _currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Textures;
        }

        private void BrowseForVariant(object sender, RoutedEventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog variantFile = new OpenFileDialog();

            // Set filter options and filter index.
            variantFile.Filter = "XML Files (.xml)|*.xml";
            variantFile.FilterIndex = 1;

            variantFile.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = variantFile.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == true)
            {
                Variant variant;
                for (var i = variantListview.Items.Count - 1; i != -1; --i)
                {
                    variant = (Variant)grouplistview.Items[i];
                    if (variant != null & variant.IsChecked == true)
                    {
                        variant.ParentVariantRelativePath = variantFile.FileName;
                    }
                }

                variant = (Variant)variantListview.SelectedItem;
                if (variant == null)
                    return;

                variant.ParentVariantRelativePath = variantFile.FileName;
            }
            var groupIndex = _actor.Groups.IndexOf(_currentGroup);
            ICollectionView view = CollectionViewSource.GetDefaultView(_actor.Groups[groupIndex]);
            view.Refresh();
        }

        private void BrowseForMesh(object sender, RoutedEventArgs e)
        {

            // Create an instance of the open file dialog box.
            OpenFileDialog meshFile = new OpenFileDialog
            {
                // Set filter options and filter index.
                Filter = "DAE Files (.dae)|*.dae",
                FilterIndex = 1,

                Multiselect = false
            };

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = meshFile.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == true)
            {
                for (var i = 0; i != variantListview.Items.Count; ++i)
                {
                    var variant = (Variant)grouplistview.Items[i];
                    if (variant != null & variant.IsChecked == true)
                    {
                        variant.Mesh = meshFile.FileName;
                    }
                }

                // _currentVariant.Mesh = meshFile.FileName;
            }

            var groupIndex = _actor.Groups.IndexOf(_currentGroup);
            ICollectionView view = CollectionViewSource.GetDefaultView(_actor.Groups[groupIndex]);
            view.Refresh();
        }


        #region Textures

        private void AddTexture(object sender, RoutedEventArgs e)
        {
            if (_currentVariant == null)
                return;

            _currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Textures.Add(new Texture());
            CollectionViewSource.GetDefaultView(_currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Textures).Refresh();
        }

        private void DeleteTexture(object sender, RoutedEventArgs e)
        {
            if (_currentVariant == null)
                return;

            for (var i = textureViewListView.Items.Count - 1; i != -1; --i)
            {
                var texture = (Texture)propViewListView.Items[i];
                if (texture != null & texture.IsChecked == true)
                {
                    _currentVariant.Textures.Remove(texture);
                }
            }

            CollectionViewSource.GetDefaultView(_currentGroup.FirstOrDefault(a => a.Equals(_currentVariant)).Textures).Refresh();
        }

        private void BrowseForTexture(object sender, RoutedEventArgs e)
        {
            //All Files(*.*)| *.*
            //All Files(*.dds)| *.dds
        } 
        #endregion
    }
}
