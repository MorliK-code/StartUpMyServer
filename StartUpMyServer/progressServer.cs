using System.Text.RegularExpressions;
using static StartUpMyServer.JavaServer;

namespace StartUpMyServer
{
    internal class ProgressServer
    {
        private ProgressBar progressStartUp;
        private Control invokingControl;

        public ProgressServer(ProgressBar progressStartUp, Control invokingControl)
        {
            this.progressStartUp = progressStartUp;
            this.invokingControl = invokingControl; 
        }

        public void LoadingStartUp(string output)
        {
            if (output.Contains("ModLauncher running"))
            {
                UpdateProgressBar(1, ServerStatus.Starting);
            }
            else if (output.Contains("Launching target 'forgeserver'"))
            {
                UpdateProgressBar(5, ServerStatus.Starting);
            }
            else if (output.Contains("libIPN"))
            {
                UpdateProgressBar(10, ServerStatus.Starting);
            }
            else if (output.Contains("Environment:"))
            {
                UpdateProgressBar(15, ServerStatus.Starting);
            }
            else if (output.Contains("advancements"))
            {
                UpdateProgressBar(18, ServerStatus.Starting);
            }
            else if (output.Contains("Starting minecraft server"))
            {
                UpdateProgressBar(25, ServerStatus.Starting);
            }
            else if (output.Contains("Preparing level"))
            {
                UpdateProgressBar(40, ServerStatus.Starting);
            }
            else if (output.Contains("World Settings"))
            {
                UpdateProgressBar(50, ServerStatus.Starting);
            }
            else if (output.Contains("Server permissions file"))
            {
                UpdateProgressBar(progressStartUp.Value + 10, ServerStatus.Starting);
            }
            else if (output.Contains("Preparing spawn area:"))
            {
                var match = Regex.Match(output, @"Preparing spawn area: (\d+)%");
                if (match.Success)
                {
                    var percent = int.Parse(match.Groups[1].Value);
                    var progressValue = 60 + (percent * 35 / 100);
                    UpdateProgressBar(progressValue, ServerStatus.Starting);
                }
            }
            else if (output.Contains("Done"))
            {
                UpdateProgressBar(100, ServerStatus.Done);
            }
        }
        public enum ServerStatus
        {
            Starting,
            Done,
        }

        private void UpdateProgressBar(int value, ServerStatus status)
        {
            invokingControl.Invoke(new Action(() =>
            {
                progressStartUp.Value = value;
            }));
        }
    }
}
