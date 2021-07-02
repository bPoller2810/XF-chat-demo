using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ctrl
{
    public class Vm : INotifyPropertyChanged
    {

        #region private member
        private static readonly Random _r = new();
        private readonly IThemeService _themeService;

        private readonly string[] _constants = new[]
        {
            "Resources exquisite set arranging moonlight sex him household had.",
            "Months had too ham cousin remove far spirit",
            "She procuring the why performed continual improving",
            "Civil songs so large shade in cause. Lady an mr here must neat sold. Children greatest ye extended delicate of. No elderly passage earnest as in removed winding or. ",
            "Sussex result matter any end see.",
        };

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region properties
        private bool _isLight;
        public bool IsLight
        {
            get => _isLight;
            set
            {
                if (_isLight == value) return;
                _isLight = value;
                _themeService.IsLightTheme = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLight)));
            }
        }

        public ObservableCollection<KeyValuePair<bool, string>> Items { get; } = new();

        public ICommand AddMsg { get; }
        #endregion

        #region ctor
        public Vm()
        {
            _themeService = SimpleIoc.Default.GetInstance<IThemeService>();
            AddMsg = new Command(HandleAddMessage);
        }
        #endregion

        #region command handling
        private void HandleAddMessage(object par)
        {
            if (!bool.TryParse((string)par, out var state)) return;

            Items.Add(new KeyValuePair<bool, string>(state, GetRandomMessage()));
        }
        #endregion

        #region private helper
        private string GetRandomMessage()
        {
            return _constants[_r.Next(_constants.Length)];
        }
        #endregion


    }
}
