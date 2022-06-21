using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Picca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Restorans : ContentPage
    {
        public Restorans()
        {
            InitializeComponent();
            var positions = new Position(55.78603025811979, 37.67634453485035);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(100)));

        }

      
    }
}