using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ctrl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatCtrl : Grid
    {

        #region private member
        private readonly IThemeService _themeService;
        #endregion

        #region bindable properties

        #region isMine
        public static readonly BindableProperty IsMineProperty = BindableProperty.Create(
            nameof(IsMine),
            typeof(bool?),
            typeof(ChatCtrl),
            propertyChanged: SetMine);
        public bool? IsMine
        {
            get => (bool?)GetValue(IsMineProperty);
            set => SetValue(IsMineProperty, value);
        }
        private static void SetMine(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not ChatCtrl ctrl || newValue is not bool) return;

            ctrl.SetAppearance();
        }
        #endregion

        #region text
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(ChatCtrl),
            propertyChanged: SetText);

        public bool Text
        {
            get => (bool)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        private static void SetText(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not ChatCtrl ctrl) return;

            ctrl._txt.Text = (string)newValue;
        }
        #endregion

        #endregion

        #region ctor
        public ChatCtrl()
        {
            InitializeComponent();

            _themeService = SimpleIoc.Default.GetInstance<IThemeService>();
            _themeService.ThemeChanged += HandleThemeChanged;
        }
        #endregion

        private void HandleLayoutChanged(object sender, EventArgs args)
        {
            if (this.Width <= 0) return;
            _frame.WidthRequest = Math.Min(_frame.Width, Width * 0.65);
        }
        private void HandleThemeChanged(object sender, EventArgs args)
        {
            SetAppearance();
        }

        private void SetAppearance()
        {
            if (!IsMine.HasValue) return;
            _frame.HorizontalOptions = IsMine.Value ? LayoutOptions.End : LayoutOptions.Start;
            _frame.BackgroundColor = IsMine.Value ? GetMineColor(_themeService.IsLightTheme) : GetRemoteColor(_themeService.IsLightTheme);
            _txt.TextColor = _themeService.IsLightTheme ? Color.Black : Color.White;
        }

        private Color GetMineColor(bool isLight)
        {
            return isLight ? Color.LightGreen : Color.Green;
        }
        private Color GetRemoteColor(bool isLight)
        {
            return isLight ? Color.LightCoral : Color.Coral;
        }
    }
}