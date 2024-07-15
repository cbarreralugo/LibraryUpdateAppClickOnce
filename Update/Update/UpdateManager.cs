using System;
using System.Deployment.Application;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update
{
    public class UpdateManager
    {
        public static void CheckForUpdates()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                UpdateCheckInfo info = null;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                    if (info.UpdateAvailable)
                    {
                        ShowUpdateNotification();
                    }
                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                }
            }
        }

        private static void ShowUpdateNotification()
        {
            DialogResult result = MessageBox.Show("A new update is available. Do you want to restart and update now?", "Update Available", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                ad.Update();
                Application.Restart();
            }
        }
    }
}
