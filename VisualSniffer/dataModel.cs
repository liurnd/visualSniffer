using System;
using System.Collections.Generic;
using System.Linq;
using SharpPcap;
using System.Text;

namespace VisualSniffer
{
    public delegate void newPacket(ref sPacket p);
    public class sPacket
    {
        public DateTime timestamp;
        public Type finalType;
        public PacketDotNet.Packet packet;
        public sPacket(ref PacketDotNet.Packet p)
        {
            timestamp = DateTime.Now;

            PacketDotNet.Packet c;
            for (c = p; c.PayloadPacket != null; c = c.PayloadPacket) ;
            finalType = c.GetType();

            packet = p;
        }
    }

    enum listenerStatus
    {
        online,
        offline
    }

    public class packetListener
    {
        public event newPacket onParseComplete;
        public List<sPacket> packList;
        private packetListener() { packList = new List<sPacket>(); }
        public static packetListener Instance = new packetListener();
        private int capDevIndex;
        public int[] devList;
        private listenerStatus status = listenerStatus.offline;
        
        private packetFilter onlineFilter = null;
        private string mFilterString;
        public string filterString
        {
            get { return mFilterString; }
            set { mFilterString = value; }
        }

        public void startCapture()
        {
            if (devList == null)
                foreach (ICaptureDevice dev in CaptureDeviceList.Instance)
                {
                    dev.OnPacketArrival += new PacketArrivalEventHandler(packetArrive);
                    dev.Open();
                    dev.StartCapture();
                }
            else
                startCapture(devList);

        }

        public void startCapture(int[] list)
        {
            foreach (var i in list)
                startCapture(i);
        }

        public void resumeCapture()
        {
            startCapture();
        }

        public void startCapture(int index)
        {
            ICaptureDevice dev = CaptureDeviceList.Instance[index];
            dev.OnPacketArrival += new PacketArrivalEventHandler(packetArrive);

            dev.Open();
            if (mFilterString != null)
                dev.Filter = mFilterString;
            dev.StartCapture();
            status = listenerStatus.online;
        }

        public void stopCapture()
        {
            foreach (ICaptureDevice dev in CaptureDeviceList.Instance)
                if (dev.Started)
                {
                    try
                    {
                        dev.StopCapture();
                        dev.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            
            status = listenerStatus.offline;
        }

        public void applyOnlineFilter(packetFilter f)
        {
            stopCapture();
            onlineFilter = f;
            foreach (var i in packList)
                if (f.pass(ref (i.packet)))
                {
                    var p = i;
                    onParseComplete(ref p);
                }

            resumeCapture();
        }

        void packetArrive(object sender, SharpPcap.CaptureEventArgs packet)
        {
                PacketDotNet.Packet v = PacketDotNet.Packet.ParsePacket(packet.Packet.LinkLayerType, packet.Packet.Data);
                sPacket pp = new sPacket(ref v);
                packList.Add(pp);
                if (onlineFilter == null || (onlineFilter!=null && onlineFilter.pass(ref v)))
                    onParseComplete(ref pp);
        }
    }
}
