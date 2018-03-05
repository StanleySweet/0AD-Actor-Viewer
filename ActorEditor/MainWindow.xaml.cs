namespace ActorEditor
{
    using ActorEditor.Model;
    using ActorEditor.Model.Entities;
    using ActorEditor.Model.Entities.Mod;
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Actor _actor;
        private ModJsonFile _modJsonFile;
        private bool _actorMode;
        private bool _particleMode;
        private bool _soundGroupMode;
        private bool _modJsonMode;
        private string _rootPath;
        private string _currentFilePath;
        private bool _variantMode;
        private bool _IsInDarkMode;
        private Group _currentGroup;
        private Variant _currentVariant;
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _rootPath = ConfigurationManager.AppSettings["material_path"];
            var truncateIndex = _rootPath.IndexOf("\\materials\\");
            if (truncateIndex > 0)
                _rootPath = _rootPath.Substring(0, truncateIndex);
            Materials.ItemsSource = FileHandler.GetMaterialList(ConfigurationManager.AppSettings["material_path"]);
        }

        private void CastsShadows_Checked(object sender, RoutedEventArgs e)
        {
            _actor.CastsShadows = (bool)castsShadows.IsChecked;
        }

        private void Floats_Checked(object sender, RoutedEventArgs e)
        {
            _actor.Floats = (bool)floats.IsChecked;
        }

        private void Materials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _actor.Material = this.Materials.SelectedItem.ToString();
        }

        private void AddGroup(object sender, RoutedEventArgs e)
        {
            _actor.Groups.Add(new Group());
            CollectionViewSource.GetDefaultView(_actor.Groups).Refresh();
        }

        private void DeleteGroups(object sender, RoutedEventArgs e)
        {
            for (var i = grouplistview.Items.Count - 1; i != -1; --i)
            {
                var group = (Group)grouplistview.Items[i];
                if (group != null && group.IsChecked == true)
                {
                    var groupIndex = _actor.Groups.IndexOf(group);
                    _actor.Groups.RemoveAt(groupIndex);
                }
            }

            CollectionViewSource.GetDefaultView(_actor.Groups).Refresh();
        }

        private void EditGroup(object sender, RoutedEventArgs e)
        {
            GetFirstGroup();

            if (_currentGroup == null)
                return;

            groupview.Visibility = Visibility.Collapsed;
            variantview.Visibility = Visibility.Visible;
            this.DataContext = _currentGroup;
        }

        private void DarkMode(object sender, RoutedEventArgs e)
        {
            if (!_IsInDarkMode)
            {
                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml", UriKind.Absolute);
                Application.Current.Resources.MergedDictionaries.Add(dict);
                _IsInDarkMode = true;
            }
            else
            {
                var resource = Application.Current.Resources.MergedDictionaries.FirstOrDefault(a => a.Source.ToString() == "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml");
                Application.Current.Resources.MergedDictionaries.Remove(resource);
                _IsInDarkMode = false;
            }
        }


        #region Utils

        private void SetActorMode()
        {
            _actorMode = true;
            _particleMode = false;
            _variantMode = false;
            _soundGroupMode = false;
            _particleMode = false;
            _modJsonMode = false;
        }

        private void SetVariantMode()
        {
            _variantMode = true;
            _actorMode = false;
            _particleMode = false;
            _soundGroupMode = false;
            _particleMode = false;
            _modJsonMode = false;
        }

        private void SetModJsonMode()
        {
            _variantMode = false;
            _actorMode = false;
            _particleMode = false;
            _soundGroupMode = false;
            _particleMode = false;
            _modJsonMode = true;
        }

        private void BrowseForObject(string filter, out string relativePath, out bool? WasCancelled, string initialDirectory = "", bool multiSelection = false)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = filter,
                FilterIndex = 1,
                Multiselect = multiSelection
            };

            if (!string.IsNullOrEmpty(initialDirectory))
                openFileDialog.InitialDirectory = initialDirectory;

            relativePath = string.Empty;
            WasCancelled = null;

            var result = openFileDialog.ShowDialog();

            if (result == false)
                WasCancelled = true;

            relativePath = openFileDialog.FileName;
        }

        private void SaveObject(string filter, out string relativePath)
        {
            var openFileDialog = new SaveFileDialog
            {
                Filter = filter,
                FilterIndex = 1,
            };
            relativePath = string.Empty;

            if (openFileDialog.ShowDialog() != true)
                return;

            relativePath = openFileDialog.FileName;
        }

        private void GetFirstGroup()
        {
            GetFirstCheckedGroup();
            if (_currentGroup != null)
                return;

            GetFirstSelectedGroup();
        }

        private void GetFirstCheckedGroup()
        {
            foreach (var item in grouplistview.Items)
            {
                var group = (Group)item;
                if (group != null && group.IsChecked)
                {
                    _currentGroup = group;
                    break;
                }
            }
        }

        private void GetFirstSelectedGroup()
        {
            _currentGroup = (Group)grouplistview.SelectedItem;
        }

        private void GetFirstVariant()
        {
            GetFirstCheckedVariant();
            if (_currentVariant != null)
                return;

            GetFirstSelectedVariant();
        }

        private void GetFirstCheckedVariant()
        {
            if (_currentGroup == null)
                return;

            _currentVariant = _currentGroup.FirstOrDefault(a => a.IsChecked);
        }

        private void GetFirstSelectedVariant()
        {
            _currentVariant = (Variant)variantListview.SelectedItem;
        }
        #endregion

        #region Animations
        private void AddAnimation(object sender, RoutedEventArgs e)
        {
            if (_currentVariant == null)
                return;

            _currentVariant.Animations.Add(new Animation());
            CollectionViewSource.GetDefaultView(_currentVariant.Animations).Refresh();
        }

        private void DeleteAnimation(object sender, RoutedEventArgs e)
        {
            if (_currentVariant == null)
                return;

            for (var i = animationListView.Items.Count - 1; i != -1; --i)
            {
                var animation = (Animation)animationListView.Items[i];
                if (animation != null && animation.IsChecked == true)
                {
                    _currentVariant.Animations.Remove(animation);
                }
            }

            CollectionViewSource.GetDefaultView(_currentVariant.Animations).Refresh();
        }

        private void BrowseForAnimation(object sender, RoutedEventArgs e)
        {
            BrowseForObject("DAE Files (.dae)|*.dae", out string path, out bool? WasCancelled);
            if (WasCancelled == true)
                return;

            var truncateIndex = path.IndexOf("\\art\\animations\\");
            if (truncateIndex > 0)
                path = path.Substring(truncateIndex + "\\art\\animations\\".Length);

            Animation variant;
            for (var i = variantListview.Items.Count - 1; i != -1; --i)
            {
                variant = (Animation)variantListview.Items[i];
                if (variant != null && variant.IsChecked == true)
                    variant.RelativePath = path;
            }

            variant = (Animation)variantListview.SelectedItem;

            if (variant != null)
                variant.RelativePath = path;

            CollectionViewSource.GetDefaultView(_currentVariant.Animations).Refresh();
        }


        #endregion

        #region Props
        private void AddProp(object sender, RoutedEventArgs e)
        {
            if (_currentVariant == null)
                return;

            _currentVariant.Props.Add(new Prop());
            CollectionViewSource.GetDefaultView(_currentVariant.Props).Refresh();
        }

        private void DeleteProp(object sender, RoutedEventArgs e)
        {
            if (_currentVariant == null)
                return;

            for (var i = propViewListView.Items.Count - 1; i != -1; --i)
            {
                var prop = (Prop)propViewListView.Items[i];
                if (prop != null && prop.IsChecked == true)
                {
                    _currentVariant.Props.Remove(prop);
                }
            }
            CollectionViewSource.GetDefaultView(_currentVariant.Props).Refresh();
        }

        private void BrowseForProp(object sender, RoutedEventArgs e)
        {
            BrowseForObject("XML Files (.xml)|*.xml", out string path, out bool? WasCancelled);
            if (WasCancelled == true)
                return;

            var truncateIndex = path.IndexOf("\\art\\actors\\props\\");
            if (truncateIndex > 0)
                path = path.Substring(truncateIndex + "\\art\\actors\\props\\".Length);

            Prop prop;
            for (var i = propViewListView.Items.Count - 1; i != -1; --i)
            {
                prop = (Prop)propViewListView.Items[i];
                if (prop != null && prop.IsChecked == true)
                    prop.RelativePath = path;
            }

            prop = (Prop)propViewListView.SelectedItem;

            if (prop != null)
                prop.RelativePath = path;

            CollectionViewSource.GetDefaultView(_currentVariant.Props).Refresh();
        }

        #endregion

        #region Textures

        private void AddTexture(object sender, RoutedEventArgs e)
        {
            if (_currentVariant == null)
                return;

            _currentVariant.Textures.Add(new Texture());
            CollectionViewSource.GetDefaultView(_currentVariant.Textures).Refresh();
        }

        private void DeleteTexture(object sender, RoutedEventArgs e)
        {
            if (_currentVariant == null)
                return;

            for (var i = textureViewListView.Items.Count - 1; i != -1; --i)
            {
                var texture = (Texture)propViewListView.Items[i];
                if (texture != null && texture.IsChecked == true)
                {
                    _currentVariant.Textures.Remove(texture);
                }
            }
            CollectionViewSource.GetDefaultView(_currentVariant.Textures).Refresh();
        }

        private void BrowseForTexture(object sender, RoutedEventArgs e)
        {
            BrowseForObject("DDS Files(*.dds)| *.dds|PNG Files(*.png)| *.png", out string path, out bool? WasCancelled);
            if (WasCancelled == true)
                return;

            var truncateIndex = path.IndexOf("\\art\\textures\\skins\\");
            if (truncateIndex > 0)
                path = path.Substring(truncateIndex + "\\art\\textures\\skins\\".Length);

            Texture texture;
            for (var i = textureViewListView.Items.Count - 1; i != -1; --i)
            {
                texture = (Texture)textureViewListView.Items[i];
                if (texture != null && texture.IsChecked == true)
                    texture.RelativePath = path;
            }

            texture = (Texture)textureViewListView.SelectedItem;

            if (texture != null)
                texture.RelativePath = path;

            CollectionViewSource.GetDefaultView(_currentVariant.Textures).Refresh();

        }
        #endregion

        #region Variant
        private void GoBackToGroupView(object sender, RoutedEventArgs e)
        {
            groupview.Visibility = Visibility.Visible;
            variantview.Visibility = Visibility.Collapsed;
            _currentGroup = null;
            this.DataContext = _actor.Groups;
        }

        private void AddVariant(object sender, RoutedEventArgs e)
        {
            if (_currentGroup == null)
                return;

            _currentGroup.Add(new Variant());
            var groupIndex = _actor.Groups.IndexOf(_currentGroup);
            CollectionViewSource.GetDefaultView(_actor.Groups[groupIndex]).Refresh();
        }

        private void DeleteVariant(object sender, RoutedEventArgs e)
        {
            for (var i = variantListview.Items.Count - 1; i != -1; --i)
            {
                var variant = (Variant)variantListview.Items[i];
                if (variant != null && variant.IsChecked == true)
                {
                    _currentGroup.Remove(variant);
                }
            }

            CollectionViewSource.GetDefaultView(_currentGroup).Refresh();
        }

        private void EditAnimations(object sender, RoutedEventArgs e)
        {
            GetFirstVariant();

            if (_currentVariant == null)
                return;

            animationView.Visibility = Visibility.Visible;
            variantview.Visibility = Visibility.Collapsed;
            this.DataContext = _currentVariant.Animations;
        }

        private void EditProps(object sender, RoutedEventArgs e)
        {
            GetFirstVariant();

            if (_currentVariant == null)
                return;

            propView.Visibility = Visibility.Visible;
            variantview.Visibility = Visibility.Collapsed;
            this.DataContext = _currentVariant.Props;
        }

        private void EditTextures(object sender, RoutedEventArgs e)
        {
            GetFirstVariant();

            if (_currentVariant == null)
                return;

            textureView.Visibility = Visibility.Visible;
            variantview.Visibility = Visibility.Collapsed;
            this.DataContext = _currentVariant.Textures;
        }

        private void BrowseForVariant(object sender, RoutedEventArgs e)
        {
            BrowseForObject("XML Files (.xml)|*.xml", out string path, out bool? WasCancelled);
            if (WasCancelled == true)
                return;
            var truncateIndex = path.IndexOf("\\art\\variants\\");
            if (truncateIndex > 0)
                path = path.Substring(truncateIndex + "\\art\\variants\\".Length);

            Variant variant;
            for (var i = variantListview.Items.Count - 1; i != -1; --i)
            {
                variant = (Variant)variantListview.Items[i];
                if (variant != null && variant.IsChecked == true)
                    variant.ParentVariantRelativePath = path;
            }

            variant = (Variant)variantListview.SelectedItem;

            if (variant != null)
                variant.ParentVariantRelativePath = path;

            CollectionViewSource.GetDefaultView(_currentGroup).Refresh();
        }

        private void BrowseForMesh(object sender, RoutedEventArgs e)
        {

            BrowseForObject("DAE Files (.dae)|*.dae", out string path, out bool? WasCancelled);
            if (WasCancelled == true)
                return;
            var truncateIndex = path.IndexOf("\\art\\meshes\\");
            if (truncateIndex > 0)
                path = path.Substring(truncateIndex + "\\art\\meshes\\".Length);


            Variant variant;
            for (var i = variantListview.Items.Count - 1; i != -1; --i)
            {
                variant = (Variant)variantListview.Items[i];
                if (variant != null && variant.IsChecked == true)
                    variant.Mesh = path;
            }

            variant = (Variant)variantListview.SelectedItem;

            if (variant != null)
                variant.Mesh = path;

            CollectionViewSource.GetDefaultView(_currentGroup).Refresh();
        }

        private void GoBackToVariantView(object sender, RoutedEventArgs e)
        {
            animationView.Visibility = Visibility.Collapsed;
            textureView.Visibility = Visibility.Collapsed;
            propView.Visibility = Visibility.Collapsed;
            variantview.Visibility = Visibility.Visible;
            this._currentVariant = null;
            this.DataContext = _currentGroup;
        }

        #endregion

        #region Menu
        private void CreateVariant(object sender, RoutedEventArgs e)
        {
            SetVariantMode();
            _currentGroup = new Group
            {
                new Variant()
            };
            SaveAsMenu.IsEnabled = true;
            SaveAsButton.IsEnabled = true;
            SaveButton.IsEnabled = false;
            SaveMenu.IsEnabled = false;
            _currentFilePath = string.Empty;
            GoBackButton.Visibility = Visibility.Collapsed;
            AddVariantButton.Visibility = Visibility.Collapsed;
            DeleteVariantButton.Visibility = Visibility.Collapsed;
            actorOptions.Visibility = Visibility.Collapsed;
            groupview.Visibility = Visibility.Collapsed;
            logoBox.Visibility = Visibility.Collapsed;
            ModJsonView.Visibility = Visibility.Collapsed;
            variantview.Visibility = Visibility.Visible;
            this.DataContext = _currentGroup;
        }

        private void CreateActor(object sender, RoutedEventArgs e)
        {
            SetActorMode();
            _actor = new Actor();
            _actor.Groups.Add(new Group());
            floats.IsEnabled = true;
            castsShadows.IsEnabled = true;
            castsShadows.IsChecked = _actor.CastsShadows;
            floats.IsChecked = _actor.Floats;
            _currentFilePath = string.Empty;
            SaveAsButton.IsEnabled = true;
            SaveAsMenu.IsEnabled = true;
            SaveButton.IsEnabled = false;
            SaveMenu.IsEnabled = false;
            actorOptions.Visibility = Visibility.Visible;
            groupview.Visibility = Visibility.Visible;
            logoBox.Visibility = Visibility.Collapsed;
            variantview.Visibility = Visibility.Collapsed;
            DeleteVariantButton.Visibility = Visibility.Visible;
            AddVariantButton.Visibility = Visibility.Visible;
            ModJsonView.Visibility = Visibility.Collapsed;
            GoBackButton.Visibility = Visibility.Visible;
            this.DataContext = _actor.Groups;
            Materials.SelectedIndex = 0;
        }

        private void OpenModFile(object sender, RoutedEventArgs e)
        {
            SetModJsonMode();
            BrowseForObject("Json Files (.json)|*.json", out string path, out bool? WasCancelled);
            if (WasCancelled == true)
                return;
            {
                _modJsonFile = FileHandler.OpenModJsonFile(path);

                if (_modJsonFile == null)
                {
                    MessageBoxResult result = MessageBox.Show("Error: Parsed file is either malformed or not a actor file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (result == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                    }
                    return;
                }
                SaveAsButton.IsEnabled = true;
                SaveButton.IsEnabled = true;
                SaveMenu.IsEnabled = true;
                SaveAsMenu.IsEnabled = true;
                actorOptions.Visibility = Visibility.Collapsed;
                groupview.Visibility = Visibility.Collapsed;
                variantview.Visibility = Visibility.Collapsed;
                logoBox.Visibility = Visibility.Collapsed;
                DeleteVariantButton.Visibility = Visibility.Collapsed;
                AddVariantButton.Visibility = Visibility.Collapsed;
                GoBackButton.Visibility = Visibility.Collapsed;
                ModJsonView.Visibility = Visibility.Visible;
                this.DataContext = _modJsonFile;
            }
        }

        private void OpenActor(object sender, RoutedEventArgs e)
        {
            SetActorMode();
            BrowseForObject("XML Files (.xml)|*.xml", out string path, out bool? WasCancelled);
            if (WasCancelled == true)
                return;

            {
                _actor = FileHandler.Open0adXmlFile<Actor>(path);

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
                foreach (var item in Materials.Items)
                {
                    if (item.ToString().Equals(_actor.Material))
                        break;
                    ++index;
                }
                SaveAsButton.IsEnabled = true;
                SaveButton.IsEnabled = true;
                SaveMenu.IsEnabled = true;
                SaveAsMenu.IsEnabled = true;
                Materials.SelectedIndex = index;
                floats.IsChecked = _actor.Floats;
                actorOptions.Visibility = Visibility.Visible;
                groupview.Visibility = Visibility.Visible;
                variantview.Visibility = Visibility.Collapsed;
                logoBox.Visibility = Visibility.Collapsed;
                DeleteVariantButton.Visibility = Visibility.Visible;
                AddVariantButton.Visibility = Visibility.Visible;
                GoBackButton.Visibility = Visibility.Visible;
                ModJsonView.Visibility = Visibility.Collapsed;
                this.DataContext = _actor.Groups;
            }
        }


        private void OpenVariant(object sender, RoutedEventArgs e)
        {
            SetVariantMode();
            BrowseForObject("XML Files (.xml)|*.xml", out string path, out bool? WasCancelled);
            if (WasCancelled == true)
                return;

            var variant = FileHandler.Open0adXmlFile<Variant>(path);
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
            actorOptions.Visibility = Visibility.Hidden;
            GoBackButton.Visibility = Visibility.Collapsed;
            SaveAsButton.IsEnabled = true;
            SaveAsMenu.IsEnabled = true;
            SaveButton.IsEnabled = true;
            SaveMenu.IsEnabled = true;
            AddVariantButton.Visibility = Visibility.Collapsed;
            DeleteVariantButton.Visibility = Visibility.Collapsed;
            groupview.Visibility = Visibility.Collapsed;
            logoBox.Visibility = Visibility.Collapsed;
            variantview.Visibility = Visibility.Visible;
            this.DataContext = _currentGroup;

        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            string relativePath = string.Empty;
            if(_modJsonFile != null && _modJsonMode)
            {
                SaveObject("Json Files (.json)|*.json", out relativePath);
            }
            else
            {
                SaveObject("XML Files (.xml)|*.xml", out relativePath);
            }

            SaveHandler(relativePath);
        }

        private void SaveHandler(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            SaveButton.IsEnabled = true;
            SaveMenu.IsEnabled = true;
            _currentFilePath = path;
            if (_actor != null && _actorMode)
                FileHandler.SaveFile(_actor, path);
            else if (_currentGroup != null && _currentGroup.Count > 0 && _variantMode)
                FileHandler.SaveFile(_currentGroup.FirstOrDefault(), path);
            else if(_modJsonFile != null && _modJsonMode)
                FileHandler.SaveFile(_modJsonFile, path);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            SaveHandler(_currentFilePath);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {

        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        #endregion

        private void CreateModFile(object sender, RoutedEventArgs e)
        {
            SetModJsonMode();
            _modJsonFile = new ModJsonFile();
            SaveAsButton.IsEnabled = true;
            SaveButton.IsEnabled = false;
            SaveMenu.IsEnabled = false;
            SaveAsMenu.IsEnabled = true;
            actorOptions.Visibility = Visibility.Collapsed;
            groupview.Visibility = Visibility.Collapsed;
            variantview.Visibility = Visibility.Collapsed;
            logoBox.Visibility = Visibility.Collapsed;
            DeleteVariantButton.Visibility = Visibility.Collapsed;
            AddVariantButton.Visibility = Visibility.Collapsed;
            GoBackButton.Visibility = Visibility.Collapsed;
            ModJsonView.Visibility = Visibility.Visible;
            this.DataContext = _modJsonFile;
        }

        /// <summary>
        /// Allow to add dependencies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDependency(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Allows to remove dependencies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveDependency(object sender, RoutedEventArgs e)
        {
        }
    }
}
