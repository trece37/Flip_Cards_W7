using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;

namespace Flip_Cards_W7
{
    public partial class MainWindow : Window
    {
        private List<WindowInfo> windows;
        private int currentIndex = 0;
        private WindowManager windowManager;
        
        public MainWindow()
        {
            InitializeComponent();
            windowManager = new WindowManager();
            this.Visibility = Visibility.Hidden;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Register global hotkey (Ctrl+Shift+Tab)
            GlobalHotkey.RegisterHotkey(this, ModifierKeys.Control | ModifierKeys.Shift, Key.Tab, OnHotkeyPressed);
        }
        
        private void OnHotkeyPressed()
        {
            if (this.Visibility == Visibility.Visible)
            {
                HideFlip3D();
            }
            else
            {
                ShowFlip3D();
            }
        }
        
        private void ShowFlip3D()
        {
            windows = windowManager.GetOpenWindows();
            if (windows.Count == 0)
            {
                MessageBox.Show("No hay ventanas abiertas para mostrar.", "Flip_Cards-W7", 
                               MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            currentIndex = 0;
            Create3DScene();
            this.Visibility = Visibility.Visible;
            this.Activate();
            UpdateCurrentWindowText();
        }
        
        private void HideFlip3D()
        {
            this.Visibility = Visibility.Hidden;
            MainViewport.Children.Clear();
        }
        
        private void Create3DScene()
        {
            MainViewport.Children.Clear();
            
            // Clear lights (keep only ambient and directional)
            var lightsToKeep = new List<ModelVisual3D>();
            foreach (Visual3D child in MainViewport.Children)
            {
                if (child is ModelVisual3D modelVisual && modelVisual.Content is Model3DGroup)
                {
                    lightsToKeep.Add(modelVisual);
                }
            }
            MainViewport.Children.Clear();
            foreach (var light in lightsToKeep)
            {
                MainViewport.Children.Add(light);
            }
            
            // Create 3D card stack
            double spacing = 1.5;
            double zStart = -(windows.Count * spacing) / 2.0;
            
            for (int i = 0; i < windows.Count; i++)
            {
                var card = CreateWindowCard(windows[i], i);
                double z = zStart + (i * spacing);
                double x = 0;
                double y = 0;
                
                // Apply 3D transformation (card stack effect)
                if (i != currentIndex)
                {
                    x = (i - currentIndex) * 0.3;
                    y = Math.Abs(i - currentIndex) * 0.2;
                }
                
                var transform = new Transform3DGroup();
                transform.Children.Add(new TranslateTransform3D(x, y, z));
                
                if (i != currentIndex)
                {
                    transform.Children.Add(new RotateTransform3D(
                        new AxisAngleRotation3D(new Vector3D(0, 1, 0), (i - currentIndex) * 10)));
                }
                
                card.Transform = transform;
                MainViewport.Children.Add(card);
            }
            
            // Animate camera
            AnimateCamera();
        }
        
        private ModelVisual3D CreateWindowCard(WindowInfo window, int index)
        {
            var modelVisual = new ModelVisual3D();
            var geometryModel = new GeometryModel3D();
            
            // Create card geometry (plane)
            var mesh = new MeshGeometry3D();
            mesh.Positions.Add(new Point3D(-2, -1.5, 0));
            mesh.Positions.Add(new Point3D(2, -1.5, 0));
            mesh.Positions.Add(new Point3D(2, 1.5, 0));
            mesh.Positions.Add(new Point3D(-2, 1.5, 0));
            
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            
            mesh.TextureCoordinates.Add(new Point(0, 1));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(1, 0));
            mesh.TextureCoordinates.Add(new Point(0, 0));
            
            geometryModel.Geometry = mesh;
            
            // Create material with window thumbnail
            var material = new DiffuseMaterial();
            
            if (window.Thumbnail != null)
            {
                var imageBrush = new ImageBrush(window.Thumbnail);
                imageBrush.Stretch = Stretch.Fill;
                material.Brush = imageBrush;
            }
            else
            {
                // Fallback to colored card with text
                var visualBrush = CreateFallbackBrush(window.Title, index);
                material.Brush = visualBrush;
            }
            
            geometryModel.Material = material;
            
            // Add glowing border effect for selected card
            if (index == currentIndex)
            {
                var emissiveMaterial = new EmissiveMaterial(new SolidColorBrush(Color.FromRgb(127, 255, 0)));
                var materialGroup = new MaterialGroup();
                materialGroup.Children.Add(material);
                materialGroup.Children.Add(emissiveMaterial);
                geometryModel.Material = materialGroup;
            }
            
            modelVisual.Content = geometryModel;
            return modelVisual;
        }
        
        private VisualBrush CreateFallbackBrush(string title, int index)
        {
            var border = new System.Windows.Controls.Border
            {
                Width = 400,
                Height = 300,
                Background = new SolidColorBrush(Color.FromRgb(10, 10, 10)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(127, 255, 0)),
                BorderThickness = new Thickness(2),
                Child = new System.Windows.Controls.TextBlock
                {
                    Text = title,
                    Foreground = new SolidColorBrush(Color.FromRgb(127, 255, 0)),
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    TextWrapping = TextWrapping.Wrap,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(20)
                }
            };
            
            border.Measure(new Size(400, 300));
            border.Arrange(new Rect(0, 0, 400, 300));
            
            return new VisualBrush(border);
        }
        
        private void AnimateCamera()
        {
            // Smooth camera animation (optional - can be enhanced with Storyboard)
            double targetZ = 10 - (currentIndex * 0.5);
            Camera.Position = new Point3D(0, 0, targetZ);
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    HideFlip3D();
                    break;
                    
                case Key.Left:
                    if (currentIndex > 0)
                    {
                        currentIndex--;
                        Create3DScene();
                        UpdateCurrentWindowText();
                    }
                    break;
                    
                case Key.Right:
                    if (currentIndex < windows.Count - 1)
                    {
                        currentIndex++;
                        Create3DScene();
                        UpdateCurrentWindowText();
                    }
                    break;
                    
                case Key.Enter:
                    if (windows.Count > 0)
                    {
                        windowManager.ActivateWindow(windows[currentIndex]);
                        HideFlip3D();
                    }
                    break;
            }
        }
        
        private void UpdateCurrentWindowText()
        {
            if (windows.Count > 0 && currentIndex < windows.Count)
            {
                CurrentWindowText.Text = $"{windows[currentIndex].Title} ({currentIndex + 1}/{windows.Count})";
            }
        }
        
        protected override void OnClosed(EventArgs e)
        {
            GlobalHotkey.UnregisterHotkey(this);
            base.OnClosed(e);
        }
    }
}
