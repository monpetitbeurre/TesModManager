/* This file is part of Oblivion Mod Manager.
 * 
 * Oblivion Mod Manager is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Oblivion Mod Manager is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using Path=System.IO.Path;
using Process=System.Diagnostics.Process;
using ServiceController=System.ServiceProcess.ServiceController;
using ServiceControllerStatus=System.ServiceProcess.ServiceControllerStatus;
using System.Windows.Forms;
using System.Collections.Generic;

namespace OblivionModManager.Forms {
    public partial class ProcessKiller : Form {
        /*[Flags]
        private enum PKFlags : uint {
            StopServices=1, StopAllServices=2, CloseProcesses=4, CloseAllProcesses=8, KillProcesses=16, KillAllProcesses=32,
            KillIfCloseFails=64, DisplayLog=128, IgnoreDrivers=256
        }*/

        private int previous=0;
        public ProcessKiller() {
            InitializeComponent();
            foreach(Control c in this.Controls) {
                if(c is CheckBox&&c.Tag!=null) {
                    uint i=uint.Parse((string)c.Tag);
                    c.Tag=i;
                    if((Settings.PKFlags&i)!=0) ((CheckBox)c).Checked=true;
                }
            }
            tbTimeout.Text=Settings.PKTimeOut.ToString();
            rbServicesStop.Checked=true;
        }

        private void tbTimeout_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar!='\b'&&!char.IsDigit(e.KeyChar)) e.Handled=true;
        }

        private void SaveProcessList() {
            switch(previous) {
            case 1:
                Settings.PKProcessesClose=tbProcesses.Lines;
                break;
            case 2:
                Settings.PKProcessesKeep=tbProcesses.Lines;
                break;
            case 3:
                Settings.PKProcessesKill=tbProcesses.Lines;
                break;
            case 4:
                Settings.PKServicesKeep=tbProcesses.Lines;
                break;
            case 5:
                Settings.PKServicesStop=tbProcesses.Lines;
                break;
            }
        }

        private void RadioChanged(object sender, EventArgs e) {
            RadioButton rb=(RadioButton)sender;
            if(!rb.Checked) return;
            SaveProcessList();
            if(rb==rbProcessesClose) {
                tbProcesses.Lines=Settings.PKProcessesClose;
                previous=1;
            } else if(rb==rbProcessesKeep) {
                tbProcesses.Lines=Settings.PKProcessesKeep;
                previous=2;
            } else if(rb==rbProcessesKill) {
                tbProcesses.Lines=Settings.PKProcessesKill;
                previous=3;
            } else if(rb==rbServicesKeep) {
                tbProcesses.Lines=Settings.PKServicesKeep;
                previous=4;
            } else if(rb==rbServicesStop) {
                tbProcesses.Lines=Settings.PKServicesStop;
                previous=5;
            }
        }

        private void CheckboxChanged(object sender, EventArgs e) {
            if(!(sender is CheckBox)) return;
            CheckBox cb=(CheckBox)sender;
            uint i=(uint)cb.Tag;
            if(cb.Checked) {
                Settings.PKFlags|=i;
            } else {
                Settings.PKFlags&=uint.MaxValue-i;
            }
        }

        private void tbTimeout_Leave(object sender, EventArgs e) {
            Settings.PKTimeOut=Convert.ToInt32(tbTimeout.Text);
        }

        private ServiceController[] GetServices() {
            if(cbIgnoreDrivers.Checked) {
                List<ServiceController> TempList=new List<ServiceController>();
                TempList.AddRange(ServiceController.GetDevices());
                TempList.AddRange(ServiceController.GetServices());
                return TempList.ToArray();
            } else {
                return ServiceController.GetServices();
            }
        }

        private void bLaunch_Click(object sender, EventArgs e) {
            DialogResult=DialogResult.Yes;
            SaveProcessList();
            ServiceController[] services=GetServices();
            Process[] processes=Process.GetProcesses();
            List<string> messages=new List<string>();
            string[] iServices=new string[Settings.PKServicesKeep.Length];
            for(int i=0;i<iServices.Length;i++) iServices[i]=Settings.PKServicesKeep[i].ToLower();
            string[] iProcesses=new string[Settings.PKProcessesKeep.Length];
            for(int i=0;i<iProcesses.Length;i++) iProcesses[i]=Settings.PKProcessesKeep[i].ToLower();
            
            if(cbServices.Checked) {
                messages.Add("Stopping specified services");
                foreach(string s in Settings.PKServicesStop) StopService(s,iServices,messages,services);
                messages.Add("");
            }
            if(cbCloseProcesses.Checked) {
                messages.Add("Closing specified processes");
                foreach(string s in Settings.PKProcessesClose) CloseProcess(s,iProcesses, messages, processes);
                messages.Add("");
            }
            if(cbKillProcesses.Checked) {
                messages.Add("Killing specified processes");
                foreach(string s in Settings.PKProcessesKill) KillProcess(s, iProcesses, messages, processes);
                messages.Add("");
            }
            if(cbAllServices.Checked) {
                messages.Add("Killing all services");
                foreach(ServiceController sc in services) StopService(sc, null, iServices, messages, services);
                messages.Add("");
            }
            if(cbCloseAllProcesses.Checked) {
                messages.Add("Closing all processes");
                foreach(Process p in processes) CloseProcess(p, null, iProcesses, messages, processes);
                messages.Add("");
            }
            if(cbKillAllProcesses.Checked) {
                messages.Add("Killing all processes");
                foreach(Process p in processes) KillProcess(p, null, iProcesses, messages, processes);
                messages.Add("");
            }
            messages.Add("Done");
            if(cbDisplayLog.Checked) {
                (new TextEditor("Background process killer log", string.Join(Environment.NewLine, messages.ToArray()), false, false)).ShowDialog();
            }
            System.IO.File.WriteAllLines("tmm process killer log.txt", messages.ToArray());
            Close();
        }

        private void StopService(string s, string[] ignore, List<string> messages, ServiceController[] services) {
            string s2=s.ToLower();
            bool Found=false;
            foreach(ServiceController service in services) {
                if(s2==service.DisplayName.ToLower()||s2==service.ServiceName.ToLower()) {
                    Found=true;
                    StopService(service, s, ignore, messages, services);
                }
            }
            if(!Found) messages.Add("Service '"+s+"' was not found");
        }
        private void StopService(ServiceController service, string s, string[] ignore, List<string> messages, ServiceController[] services) {
            if(service.Status!=ServiceControllerStatus.Running) {
                if(s!=null) messages.Add("Service '"+s+"' was found but was not running");
            } else {
                if(s==null) s=service.DisplayName;
                if(!service.CanStop) {
                    messages.Add("Service '"+s+"' was found and was running, but could not be stopped");
                } else if(Array.IndexOf<string>(ignore, service.DisplayName.ToLower())!=-1||
                          Array.IndexOf<string>(ignore, service.ServiceName.ToLower())!=-1) {
                    messages.Add("Service '"+s+"' was found, but was in the ignore list");
                } else {
                    try {
                        service.Stop();
                        messages.Add("Service '"+s+"' stopped sucessfully");
                    } catch(Exception ex) {
                        messages.Add("Service '"+s+"' could not be stopped");
                        messages.Add("\tError: "+ex.Message);
                    }
                }
            }
        }
        private void CloseProcess(string s, string[] ignore, List<string> messages, Process[] processes) {
            string s2=s.ToLower();
            bool Found=false;
            foreach(Process process in processes) {
                string path;
                try {
                    path=process.MainModule.FileName.ToLower();
                } catch { continue; }
                if(s2==path||s2==Path.GetFileName(path)||s2==Path.GetFileNameWithoutExtension(path)) {
                    Found=true;
                    CloseProcess(process, s, ignore, messages, processes);
                }
            }
            if(!Found) messages.Add("Process '"+s+"' was not found");
        }
        private void CloseProcess(Process p, string s, string[] ignore, List<string> messages, Process[] processes) {
            p.Refresh();
            if(p.HasExited) return;
            try {
                s=p.MainModule.FileName;
            } catch {
                messages.Add("Unable to obtain main module information from process '"+p.Id+"'");
                return;
            }
            messages.Add("Attempting to close process '"+s+"'");
            string s2=s.ToLower();
            if(Array.IndexOf<string>(ignore, s2)!=-1||
                Array.IndexOf<string>(ignore, Path.GetFileName(s2))!=-1||
                Array.IndexOf<string>(ignore, Path.GetFileNameWithoutExtension(s2))!=-1) {
                messages.Add("Process '"+s+"' was found, but was on the ignore list");
                return;
            }
            bool b;
            try { b=p.CloseMainWindow(); } catch(Exception ex) {
                messages.Add("Process '"+s+"' could not be closed");
                messages.Add("\tError: "+ex.Message);
                return;
            }
                if(!b) {
                    messages.Add("Process '"+s+"' could not accept a close request");
                    if(cbKillOnFail.Checked) KillProcess(p, null, ignore, messages, processes);
                    return;
                }
            p.WaitForExit(Settings.PKTimeOut);
            if(!p.HasExited) {
                messages.Add("Timeout exceeded waiting for process '"+s+"' to close");
                if(cbKillOnFail.Checked) KillProcess(p, null, ignore, messages, processes);
            } else {
                messages.Add("Process '"+s+"' closed successfully");
            }
        }
        private void KillProcess(string s, string[] ignore, List<string> messages, Process[] processes) {
            string s2=s.ToLower();
            bool Found=false;
            foreach(Process process in processes) {
                string path;
                try {
                    path=process.MainModule.FileName.ToLower();
                } catch { continue; }
                if(s2==path||s2==Path.GetFileName(path)||s2==Path.GetFileNameWithoutExtension(path)) {
                    Found=true;
                    KillProcess(process, s, ignore, messages, processes);
                }
            }
            if(!Found) messages.Add("Process '"+s+"' was not found");
        }
        private void KillProcess(Process p, string s, string[] ignore, List<string> messages, Process[] processes) {
            p.Refresh();
            if(p.HasExited) return;
            try {
                s=p.MainModule.FileName;
            } catch {
                messages.Add("Unable to obtain main module information from process '"+p.Id+"'");
                return;
            }
            messages.Add("Attempting to kill process '"+s+"'");
            string s2=s.ToLower();
            if(Array.IndexOf<string>(ignore, s2)!=-1||
                Array.IndexOf<string>(ignore, Path.GetFileName(s2))!=-1||
                Array.IndexOf<string>(ignore, Path.GetFileNameWithoutExtension(s2))!=-1) {
                messages.Add("Process '"+s+"' was found, but was on the ignore list");
                return;
            }
            try {
                p.Kill();
            } catch(Exception ex) {
                messages.Add("Process '"+s+"' refused to die");
                messages.Add("\tError: "+ex.Message);
            }
            messages.Add("Process '"+s+"' killed");
        }
    }
}