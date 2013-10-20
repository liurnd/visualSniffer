using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AJBauer;

namespace VisualSniffer
{
    public partial class statistics : Form
    {
        Timer refreshTimer;
        Dictionary<SharpPcap.ICaptureDevice,Tuple<uint,AJBauer.AGauge>> traffic = new Dictionary<SharpPcap.ICaptureDevice,Tuple<uint,AGauge>>();
        Queue<uint> packCntSeq = new Queue<uint>(60);
        uint lastCnt=0;
        public statistics()
        {
            InitializeComponent();
            refreshTimer = new Timer();
            refreshTimer.Interval = 1000;
            refreshTimer.Tick += new EventHandler(doRefresh);
            for (var i = 0; i < 60; i++)
                packCntSeq.Enqueue(0);
            refreshTimer.Start();
        }

        public void doRefresh(object sender, EventArgs args)
        {
            var nCnt = packetListener.Instance.packetCnt;
            avgMin.Value = (nCnt - packCntSeq.Dequeue())/60f;
            avgSec.Value = nCnt - lastCnt;

            lastCnt = nCnt;
            packCntSeq.Enqueue(nCnt);
            chart1.Series[0].Points.Clear();
            foreach (var tag in packetListener.Instance.packetTypeCnt)
                chart1.Series[0].Points.AddXY(tag.Key, tag.Value);
        }

        private void speedArea_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
