using CloudCmsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApLandingPage.ViewModels
{
    public class ApLandingPageUserAccountFoldersViewModel
    {
        public UserAccount UserAccount { get; set; }
        public IEnumerable<ProgramFolder> ProgramFolders { get; set; }
    }
}
