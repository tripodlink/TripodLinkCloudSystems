using CloudImsCommon.Database;
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

        public static String CreateSideBarMenuCookieClaimValue(List<ProgramMenu> menus)
        {

            var menuStr = "";
            foreach (var menu in menus)
            {
                if (menuStr != "")
                {
                    menuStr += "~";
                }

                menuStr += "id^" + menu.ID + "|"
                        + "name^" + menu.Name + "|"
                        + "folder_id^" + menu.ProgramFolderID + "|"
                        + "controller_route^" + menu.ControllerRouteAttribute + "|"
                        + "action_route^" + menu.ActionRouteAttribute + "|"
                        + "icon_type^" + menu.IconType + "|"
                        + "icon_provider^" + menu.IconProvider + "|"
                        + "icon_name^" + menu.IconName + "|"
                        + "seqno^" + menu.SequenceNo + "|";
            }

            return menuStr;
        }

        public static List<ProgramMenu> GetSideBarProgramMenus(string menuStr)
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

                            try
                            {
                                if (itemValArr[0] == "id")
                                {
                                    menu.ID = itemValArr[1];
                                }
                                else if (itemValArr[0] == "name")
                                {
                                    menu.Name = itemValArr[1];
                                }
                                else if (itemValArr[0] == "folder_id")
                                {
                                    menu.ProgramFolderID = itemValArr[1];

                                    var dbContext = AppDbContext.CreateAppDbContext();
                                    menu.ProgramFolder = dbContext.ProgramFolders.Find(menu.ProgramFolderID);
                                }
                                else if (itemValArr[0] == "controller_route")
                                {
                                    menu.ControllerRouteAttribute = itemValArr[1];
                                }
                                else if (itemValArr[0] == "action_route")
                                {
                                    menu.ActionRouteAttribute = itemValArr[1];
                                }
                                else if (itemValArr[0] == "icon_type")
                                {
                                    menu.IconType = itemValArr[1];
                                }
                                else if (itemValArr[0] == "icon_provider")
                                {
                                    menu.IconProvider = itemValArr[1];
                                }
                                else if (itemValArr[0] == "icon_name")
                                {
                                    menu.IconName = itemValArr[1];
                                }
                                else if (itemValArr[0] == "seqno")
                                {
                                    menu.SequenceNo = int.Parse(itemValArr[1]);
                                }

                            }
                            catch (Exception e)
                            {
                                Console.Write(e);
                               
                            }

                           

                        }
                    }

                    //id ^ DIC_ITEM | name ^ Item Master 
                    //| folder_id ^ DIC 
                    //| folder_name ^ Data Dictionary 
                    //| folder_seqno ^ 20 
                    //| folder_route ^ data - dictionary 
                    //| controller_route ^ item - master 
                    //| action_route ^ index 
                    //| icon_type ^
                    //| icon_provider ^
                    //| icon_name ^
                    //| seqno ^ 40 |


                    menus.Add(menu);
                }

            }

            return menus;
        }


        public static string CreateSideBarFolderCookieClaimValue(List<ProgramFolder> folders) {
            var folderStr = "";
            foreach (var folder in folders)
            {
                if (folderStr != "")
                {
                    folderStr += "~";
                }
                folderStr += "id^" + folder.ID + "|"
                          + "name^" + folder.Name + "|"
                          + "folder_route^" + folder.RouteAttribute + "|"
                          + "icon_type^" + folder.IconType + "|"
                          + "icon_provider^" + folder.IconProvider + "|"
                          + "icon^" + folder.Icon + "|"
                          + "seqno^" + folder.SequenceNo + "|";

            }
            return folderStr;
        }


        public static List<ProgramFolder> GetSideBarFolderGroup(string folderStr)
        {
            List<ProgramFolder> folders = new List<ProgramFolder>();

            var folderArr = folderStr.Split("~");

            foreach (var folderprogram in folderArr)
            {
                if (folderprogram.Trim() != "")
                {
                    var folder = new ProgramFolder();
                    var folderItemArr = folderprogram.Split("|");

                    foreach (var item in folderItemArr)
                    {
                        if (item.Trim() != "")
                        {
                            var itemValArr = item.Split("^");

                            try
                            {
                                if (itemValArr[0] == "id")
                                {
                                    folder.ID = itemValArr[1];
                                }
                                else if (itemValArr[0] == "name")
                                {
                                    folder.Name = itemValArr[1];
                                }
                                else if (itemValArr[0] == "folder_route")
                                {
                                    folder.RouteAttribute = itemValArr[1];
                                }
                                else if (itemValArr[0] == "icon_type")
                                {
                                    folder.IconType = itemValArr[1];
                                }
                                else if (itemValArr[0] == "icon_provider")
                                {
                                    folder.IconProvider = itemValArr[1];
                                }
                                else if (itemValArr[0] == "icon")
                                {
                                    folder.Icon = itemValArr[1];
                                }
                                else if (itemValArr[0] == "seqno")
                                {
                                    folder.SequenceNo = int.Parse(itemValArr[1]);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.Write(e);

                            }



                        }
                    }
                    folders.Add(folder);
                }

            }

            return folders;
        }

    }
}
