using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ACM.Presentation.Controls.Text
{
    public class ACMTextBox : TextBox
    {
        #region Static Constructor
        static ACMTextBox()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(ACMTextBox), new FrameworkPropertyMetadata(typeof(ACMTextBox)));
        }

        #endregion

        #region Properties

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(ACMTextBox), new FrameworkPropertyMetadata(String.Empty));
        public static readonly DependencyProperty CaptionPlacementProperty = DependencyProperty.Register("CaptionPlacement", typeof(captionPlacement), typeof(ACMTextBox), new PropertyMetadata(captionPlacement.Right));
        public static readonly DependencyProperty FontColorProperty = DependencyProperty.Register("FontColor", typeof(fontColor), typeof(ACMTextBox), new FrameworkPropertyMetadata(fontColor.Gray));
        public static readonly DependencyProperty TextBoxTypeProperty = DependencyProperty.Register("TextBoxType", typeof(ACMTextBoxType), typeof(ACMTextBox), new FrameworkPropertyMetadata(ACMTextBoxType.DefaultMode));
        public static readonly DependencyProperty IsTypingStartProperty = DependencyProperty.Register("IsTypingStart", typeof(bool), typeof(ACMTextBox), new PropertyMetadata(false, OnTypingStartPropertyChanged));
        public static readonly DependencyProperty IsTypingCompletedProperty = DependencyProperty.Register("IsTypingCompleted", typeof(bool), typeof(ACMTextBox), new PropertyMetadata(false, OnTypingCompletedChanged));
        public static readonly RoutedEvent TypingCompletedRoutedEvent = EventManager.RegisterRoutedEvent("TypingCompleted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ACMTextBox));

        #endregion

        #region Getters and Setters

        #region Getter and Setter for 'Caption'
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        #endregion

        #region Getter and Setter for 'CaptionPlacement'

        public captionPlacement CaptionPlacement
        {
            get { return (captionPlacement)GetValue(CaptionPlacementProperty); }
            set { SetValue(CaptionPlacementProperty, value); }
        }

        #endregion

        #region Getter and Setter for 'FontColor'

        public fontColor FontColor
        {
            get { return (fontColor)GetValue(FontColorProperty); }
            set { SetValue(FontColorProperty, value); }
        }

        #endregion

        #region Getter and Setter for 'TextBoxType'

        public ACMTextBoxType TextBoxType
        {
            get { return (ACMTextBoxType)GetValue(TextBoxTypeProperty); }
            set { SetValue(TextBoxTypeProperty, value); }
        }

        #endregion

        #region Getter and Setter for 'IsTypingStart'
       
        public bool IsTypingStart
        {
            get { return (bool)GetValue(IsTypingStartProperty); }
            set { SetValue(IsTypingStartProperty, value); }
        }

        #endregion

        #region Getter and Setter for 'IsTypingCompleted'
        public bool IsTypingCompleted
        {
            get { return (bool)GetValue(IsTypingCompletedProperty); }
            set { SetValue(IsTypingCompletedProperty, value); }
        }

        #endregion

        #endregion

        #region ENUM variables

        #region ACMTextBoxType
        /// <summary>
        /// The Mode that the textbox is in
        /// </summary>
        public enum ACMTextBoxType
        {
            DefaultMode,        // Default mode
            TypingMode,         // When user starts typing in the textbox
        }

        #endregion

        #region CaptionPlacement
        /// <summary>
        /// Where the title is placed (depending on the mode of the textbox
        /// </summary>
        public enum captionPlacement
        {
            Left,
            Top,
            Right,
            Bottom
        }

        #endregion

        #region fontColor

        public enum fontColor
        {
            Green,
            Gray
        }

        #endregion

        #endregion

        /// <summary>
        /// When the user started typing 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        static void OnTypingStartPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ACMTextBox @this = obj as ACMTextBox;

            bool newValue = (bool)args.NewValue;
            if (newValue != true) return;

            @this.IsTypingCompleted = false;
            @this.FontColor = fontColor.Green;
            @this.CaptionPlacement = captionPlacement.Top;
        }


        /// <summary>
        /// When the user finished typing
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        static void OnTypingCompletedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ACMTextBox @this = obj as ACMTextBox;

            bool newValue = (bool)args.NewValue;
            if (newValue != true) return;

            @this.RaiseTypingCompletedEvent();
        }

        private void RaiseTypingCompletedEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs();
            args.RoutedEvent = TypingCompletedRoutedEvent;
            RaiseEvent(args);
        }
    }

    public class converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;

            if (text != null)
            {
                return text.Length > 0;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}