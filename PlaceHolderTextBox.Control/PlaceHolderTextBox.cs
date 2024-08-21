using System.Windows;
using System.Windows.Controls;

namespace PlaceHolderTextBox.Control
{

    public class PlaceHolderTextBox : TextBox
    {
        // Dependency property for the placeholder text of the textbox => text that is displayed when the textbox is empty
        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register(nameof(PlaceHolder), typeof(string), typeof(PlaceHolderTextBox), new PropertyMetadata(string.Empty));

        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }


        // Dependency property for the IsEmpty property => true if the textbox is empty, false otherwise
        // I use DependencyPropertyKey to make it readonly
        private static readonly DependencyPropertyKey IsEmptyPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsEmpty), typeof(bool), typeof(PlaceHolderTextBox), new PropertyMetadata(true));


        public static readonly DependencyProperty IsEmptyProperty = IsEmptyPropertyKey.DependencyProperty;
        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            private set { SetValue(IsEmptyPropertyKey, value); }
        }

        static PlaceHolderTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceHolderTextBox), new FrameworkPropertyMetadata(typeof(PlaceHolderTextBox)));
        }


        // When the text of the textbox changes, check if it is empty
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            IsEmpty = string.IsNullOrEmpty(Text);

            base.OnTextChanged(e);
        }
    }
}
