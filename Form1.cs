using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace filePusher
{
    public partial class FilePusher : Form
    {
        public FilePusher()
        {
            InitializeComponent();
        }

        public string originPath = Properties.Settings.Default.originPath;
        public string destPath = Properties.Settings.Default.destPath;

        private void FilePusher_Load(object sender, EventArgs e)
        {
            GetFiles(originPath);
        }
        private void GetFiles(string opath)
        {
            if (Directory.Exists(opath))
            {
                string[] filePaths = Directory.GetFiles(opath, "*.xml", SearchOption.TopDirectoryOnly);
                int amt = filePaths.Length;
                foreach (var file in filePaths)
                {
                    var f = Path.GetFileName(file);
                    lboFiles.Items.Add(f);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lboFiles.Items.Clear();
            GetFiles(originPath);
        }

        private void btnPushOne_Click(object sender, EventArgs e)
        {
            PushFiles(1, originPath, destPath);
        }

        private void PushFiles(int amt, string opath, string dpath)
        {
            if (Directory.Exists(opath))
            {
                if (Directory.Exists(dpath))
                {
                    string[] filePaths = Directory.GetFiles(opath, "*.xml", SearchOption.TopDirectoryOnly);
                    int len = filePaths.Length;
                    if(amt > len)
                    {
                        amt = len;
                    }
                    for(int i = 0; i < amt; i++)
                    {
                        if (File.Exists(filePaths[i]))
                        {
                            Console.WriteLine(dpath);
                            var f = Path.GetFileName(filePaths[i]);
                            Console.WriteLine(f);
                            File.Move(filePaths[i], dpath + Path.GetFileName(filePaths[i]));
                        }
                    }
                } else
                {
                    MessageBox.Show("Destination Path is invalid.");
                }
            } else
            {
                MessageBox.Show("Origin Path is invalid");
            }
        }

        private void btnPushHundred_Click(object sender, EventArgs e)
        {
            PushFiles(100, originPath, destPath);
        }

        private void btnPushFiveHundred_Click(object sender, EventArgs e)
        {
            PushFiles(500, originPath, destPath);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
