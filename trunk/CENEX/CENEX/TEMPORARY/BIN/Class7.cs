using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    
    /// <summary>
    /// Shell for the sample.
    /// </summary>
    public class Class7
    {
       
        /// <summary>
        /// Opens the Internet Explorer application.
        /// </summary>
        public void OpenApplication(string myFavoritesPath)
        {
            // Start Internet Explorer. Defaults to the home page.
            // Process.Start("IExplore.exe"); - clean window of IE
                    
            // Display the contents of the favorites folder in the browser.
            // Process.Start(myFavoritesPath); - Dirrectory with FavouritePath
 
        }
        
        /// <summary>
        /// Opens urls and .html documents using Internet Explorer.
        /// </summary>
        public void OpenWithArguments()
        {
            // url's are not considered documents. They can only be opened
            // by passing them as arguments.
            Process.Start("IExplore.exe", "www.dlubal.cz");
            
            // Start a Web page using a browser associated with .html and .asp files.
            // Process.Start("IExplore.exe", "C:\\myPath\\myFile.htm");
            // Process.Start("IExplore.exe", "C:\\myPath\\myFile.asp");
        }
        
        /// <summary>
        /// Uses the ProcessStartInfo class to start new processes, both in a minimized 
        /// mode.
        /// </summary>
        public void OpenWithStartInfo()
        {
            
            ProcessStartInfo startInfo = new ProcessStartInfo("IExplore.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            
            Process.Start(startInfo);
            
            startInfo.Arguments = "www.dlubal.cz";
            
            Process.Start(startInfo);
            
        }

        
        public static void Main4()
        {
                    // Get the path that stores favorite links.
                    string myFavoritesPath = 
                    Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
                
                    Class7 myProcess = new Class7();
         
            //myProcess.OpenApplication(myFavoritesPath);
            myProcess.OpenWithArguments();
            //myProcess.OpenWithStartInfo();

               } 
         
    }
}







    

