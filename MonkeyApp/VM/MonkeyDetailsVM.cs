using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using MonkeyApp.Model;

namespace MonkeyApp.VM
{
    [QueryProperty("Monkey", "Monkey")]
    public partial class MonkeyDetailsVM : BaseVM
    {
        public MonkeyDetailsVM()
        {

        }
        [ObservableProperty]
        Monkey monkey;
    
    }
}
