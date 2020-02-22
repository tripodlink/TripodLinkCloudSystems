using CloudImsCommon.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Extensions
{
    public class SideBarMenu
    {
        public static IEnumerable<ProgramMenu> GetSideBarMenus(string menuStr)
        {
            List<ProgramMenu> menus = new List<ProgramMenu>();

            var menuArr = menuStr.Split("~");

            foreach (var menuProgram in menuArr)
            {
                if (menuProgram.Trim() != "")
                {
                    var menu = new ProgramMenu();
                    var menuItemArr = menuProgram.Split("|");

                    foreach (var item in menuItemArr)
                    {
                        if (item.Trim() != "")
                        {
                            var itemValArr = item.Split("^");

                            if (itemValArr[0] == "id")
                            {
                                menu.ID = itemValArr[1];
                            }
                            else if (itemValArr[0] == "name")
                            {
                                menu.Name = itemValArr[1];
                            }
                        }
                    }

                    menus.Add(menu);
                }

            }

            return menus;
        }

    }
}
