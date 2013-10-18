using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualSniffer
{
    public partial class snifferOptions : Form
    {
        private snifferOptions()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (filterString.Text != "")
            {
                packetListener.Instance.filterString = filterString.Text;
            }

            packetListener.Instance.devList =new int[devList.CheckedIndices.Count];
            for (var i = 0; i < devList.CheckedIndices.Count; i++)
                packetListener.Instance.devList[i] = devList.CheckedIndices[i];
            Hide();
        }

        private void devList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (devList.SelectedIndices.Count != 0)
            {
                var dev = SharpPcap.CaptureDeviceList.Instance[devList.SelectedIndices[0]] as SharpPcap.WinPcap.WinPcapDevice;
                devTip.Show(dev.ToString(),devList);
            }
        }

        private void snifferOptions_Load(object sender, EventArgs e)
        {
            devList.Items.Clear();
            foreach (var i in SharpPcap.CaptureDeviceList.Instance)
            {
                devList.Items.Add(i.Description);
            }
            if (packetListener.Instance.devList == null)
                return; 
            foreach (var i in packetListener.Instance.devList)
            {
                devList.SetItemChecked(i, true);
            }
        }
    }
}
