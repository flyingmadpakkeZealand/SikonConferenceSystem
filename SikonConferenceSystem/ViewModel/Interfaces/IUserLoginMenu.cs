using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace SikonConferenceSystem.ViewModel.Interfaces
{
    public interface IUserLoginMenu
    {
        User LoadedUser { get; set; }
    }
}
