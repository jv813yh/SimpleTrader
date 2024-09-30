using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleTrader.WPF.Controls
{
    /// <summary>
    /// Interaction logic for TextRadioButtonControl.xaml
    /// </summary>
    public partial class TextRadioButtonControl : UserControl
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(TextRadioButtonControl), new PropertyMetadata(default(string)));
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextForegroundProperty = 
            DependencyProperty.Register(nameof(TextForeground), typeof(Brush), typeof(TextRadioButtonControl), new PropertyMetadata(default(string)));
        public Brush TextForeground
        {
            get => (Brush)GetValue(TextForegroundProperty);
            set => SetValue(TextForegroundProperty, value);
        }

        public static readonly DependencyProperty ImageSourceProperty =
           DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(TextRadioButtonControl), new PropertyMetadata(null));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty GroupNameProperty = 
                       DependencyProperty.Register(nameof(GroupNameRadioButton), typeof(string), typeof(TextRadioButtonControl), new PropertyMetadata(default(string)));
        public string GroupNameRadioButton
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }

        public static readonly DependencyProperty CommandRadioButtonProperty =
            DependencyProperty.Register(nameof(CommandRadioButton), typeof(ICommand), typeof(TextRadioButtonControl), new PropertyMetadata(default(string)));

        public ICommand CommandRadioButton
        {
            get => (ICommand)GetValue(CommandRadioButtonProperty);
            set => SetValue(CommandRadioButtonProperty, value);
        }

        public static readonly DependencyProperty CommandParameterRadioButtonProperty =
            DependencyProperty.Register(nameof(CommandParameterRadioButton), typeof(object), typeof(TextRadioButtonControl), new PropertyMetadata(default(string)));

        public object CommandParameterRadioButton
        {
            get => (object)GetValue(CommandParameterRadioButtonProperty);
            set => SetValue(CommandParameterRadioButtonProperty, value);
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(TextRadioButtonControl), new PropertyMetadata(default(bool)));
        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public TextRadioButtonControl()
        {
            InitializeComponent();
        }
    }
}
