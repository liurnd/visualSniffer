using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PacketDotNet;

namespace VisualSniffer
{
    public partial class mainForm : Form
    {
        packetInspector f = new packetInspector();
        public mainForm()
        {
            InitializeComponent();
        }

        public delegate void addPacketDelegate(ref sPacket v);
        public void addNewPacket(ref sPacket v)
        {
            addPacketDelegate dl = new addPacketDelegate(doAddNewPacket);
            object[] argv = new object[] { v };
            try
            {
                Invoke(dl, argv);
            }
            catch (InvalidOperationException e){
            }
        }

        public void doAddNewPacket(ref sPacket v)
        {
            packViewItem pvi = new packViewItem(ref v);
            packList.Items.Add(pvi);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
        }

        private void packList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (packList.SelectedItems.Count == 0) return;
            var pk = packList.SelectedItems[0] as packViewItem;

            if (f.IsDisposed)
                f = new packetInspector();
            f.Show();
            f.showPacket(ref pk.pack.packet);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {

        }

        private void followTCPStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (packList.SelectedItems.Count == 0)
                return;
            var p = packList.SelectedItems[0] as packViewItem;
            if (p.pack.packet.Extract(typeof(TcpPacket))!=null)
            {
                var tracer = new streamTracer();
                tracer.showStream(p.pack);
            }
        }

        private void tsStart_Click(object sender, EventArgs e)
        {
            packetListener.Instance.startCapture();
            tsStatus.Text = "Running...";
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsConfig_Click(object sender, EventArgs e)
        {
            snifferOptions.Instance.Show();
        }

        private void tsStop_Click(object sender, EventArgs e)
        {
            tsStatus.Text = "Stopping...";
            packetListener.Instance.stopCapture();
            tsStatus.Text = "Ready";
        }

        private void tsFilter_Click(object sender, EventArgs e)
        {
           tsStatus.Text = "Stopping";
            var needResume = (packetListener.Instance.status == listenerStatus.online);
            if (needResume)
                packetListener.Instance.stopCapture();

            tsStatus.Text = "Compiling Filter ...";
            Update();
            var filter = filterGen.genFilter(tsFilterString.Text);


            tsStatus.Text = "Applying Filter ...";
            Update();
            packList.Items.Clear();
            packetListener.Instance.applyOnlineFilter(filter);
            if (needResume)
            {
                packetListener.Instance.startCapture();
                tsStatus.Text = "Running ...";
            }
        }
    }

    class packViewItem : ListViewItem
    {
        public sPacket pack;
        public packViewItem(ref sPacket v)
        {
            pack = v;
            SubItems.Clear();
            SubItems[0].Text=v.timestamp.ToLocalTime().ToLongTimeString();
            if (v.finalType == typeof(HLPacket))
                SubItems.Add((v.packet.Extract(typeof(HLPacket)) as HLPacket).packetType);
            else
                SubItems.Add(v.finalType.Name);
            string srcaddr, destaddr;
            if (v.packet.PayloadPacket != null && v.packet.PayloadPacket is IpPacket)
            {
                IpPacket tmpP = (IpPacket)v.packet.PayloadPacket;
                srcaddr = tmpP.SourceAddress.ToString();
                destaddr = tmpP.DestinationAddress.ToString();
            }
            else
            {
                var tmpP = (EthernetPacket)v.packet;
                srcaddr = tmpP.SourceHwAddress.ToString();
                destaddr = tmpP.DestinationHwAddress.ToString();
            }
            SubItems.AddRange(new string[] { srcaddr, destaddr });
            SubItems.Add(v.packet.Bytes.Length.ToString());
            SubItems.Add(v.packet.Extract(v.finalType).ToString());
        }
    }

    class ListViewNF : System.Windows.Forms.ListView
    {
        public ListViewNF()
        {
            //Activate double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            //Enable the OnNotifyMessage event so we get a chance to filter out 
            // Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }
        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }
    }
}
