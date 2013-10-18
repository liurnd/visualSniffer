using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using PacketDotNet;
using System.Windows.Forms;

namespace VisualSniffer
{
    public partial class streamTracer : Form
    {
        tcpStream myStream;
        public streamTracer()
        {
            InitializeComponent();
        }
        public void showStream(sPacket p)
        {
            myStream = new tcpStream(ref p);
            System.Net.IPEndPoint currentEndPoint;
            label1.Text = String.Format("TCP stream  Host:{0} Client{1}",new object[]{myStream.host.ToString(), myStream.client.ToString()});
            
            for (var segment = myStream.getNextSegment(out currentEndPoint); segment != null; segment = myStream.getNextSegment(out currentEndPoint))
            {
                if (richTextBox1.SelectionBackColor == Color.Moccasin)
                    richTextBox1.SelectionBackColor = Color.Lavender;
                else
                    richTextBox1.SelectionBackColor = Color.Moccasin;
                richTextBox1.AppendText(System.Text.Encoding.UTF8.GetString(segment));
            }
            Show();
        }

        private void streamTracer_Load(object sender, EventArgs e)
        {

        }
    }

    class tcpStream
    {
        packetFilter streamFilter;
        filterViewer viewer;
        public System.Net.IPEndPoint host, client;

        public byte[] getNextSegment(out System.Net.IPEndPoint endPoint)
        {
            List<KeyValuePair<uint, byte[]> > tmpList = new List<KeyValuePair<uint, byte[]> > ();
            var p = viewer.getCurrent();
            if (p == null)
            {
                endPoint = null;
                return null;
            }
            var firstEndPoint = Program.getSrcEndPoint(p.packet);
            endPoint = firstEndPoint;
            var tcpP = p.packet.Extract(typeof(TcpPacket)) as TcpPacket;
            var seqStartNum = tcpP.SequenceNumber; uint endNum = 0;
            for (; p!=null && Program.getSrcEndPoint(p.packet).Equals(firstEndPoint); p = viewer.getNext())
            {
                tcpP = p.packet.Extract(typeof(TcpPacket)) as TcpPacket;
                endNum = Math.Max(endNum, tcpP.SequenceNumber + (uint)tcpP.BytesHighPerformance.BytesLength);
                tmpList.Add(new KeyValuePair<uint, byte[]>(tcpP.SequenceNumber, tcpP.PayloadData));
                if (tcpP.Fin || tcpP.Rst)
                    break;
            }
            
            var tmpByte = new byte[endNum - seqStartNum];
            foreach (var i in tmpList)
            {
                i.Value.CopyTo(tmpByte, i.Key - seqStartNum);
            }
            return tmpByte;
        }

        public tcpStream(ref sPacket targetPacket)
        {
            //Setup filter
            var tcp = targetPacket.packet.Extract(typeof(PacketDotNet.TcpPacket)) as PacketDotNet.TcpPacket;
            var ip = targetPacket.packet.Extract(typeof(PacketDotNet.IpPacket)) as IpPacket;
            if (tcp == null || ip == null)
                throw new InvalidOperationException("Packet is not a TCP packet");
            streamFilter = (new tcpPortFilter(true, tcp.SourcePort) & new tcpPortFilter(false, tcp.DestinationPort) &
                new ipIpFilter(true, ip.SourceAddress) & new ipIpFilter(false, ip.DestinationAddress)) |
                ((new tcpPortFilter(false, tcp.SourcePort) & new tcpPortFilter(true, tcp.DestinationPort) &
                new ipIpFilter(false, ip.SourceAddress) & new ipIpFilter(true, ip.DestinationAddress)));
            viewer = new filterViewer(streamFilter);
            
            //Search for target packet
            int pindex = 0;
            foreach (var i in packetListener.Instance.packList)
            {
                if (i.Equals(targetPacket))
                    break;
                pindex++;
            }
            viewer.reset(pindex);

            //Search for handshake
            Queue<Packet> handshakeWindow = new Queue<Packet>(3);
            var p = viewer.getCurrent();
            for (; p != null; p = viewer.getForward())
            {
                handshakeWindow.Enqueue(p.packet);
                if (handshakeWindow.Count > 3)
                    handshakeWindow.Dequeue();
                if (isHandshake(ref handshakeWindow,out client,out  host))
                {
                    break;
                }
            }

            if (p == null)
            {
                throw new InvalidOperationException("TCP session is not complete");
            }

            //Skip handshake
            viewer.getNext();viewer.getNext();viewer.getNext();
        }

        internal bool isHandshake(ref Queue<Packet> q, out System.Net.IPEndPoint client, out System.Net.IPEndPoint host)
        {
            if (q.Count != 3)
            {
                client = null; host = null;
                return false;
            }
            foreach (var i in q)
            {
                var pack = i.Extract(typeof(TcpPacket)) as TcpPacket;
                System.Diagnostics.Debug.WriteLine(String.Format("SYN:{0}, ACK:{1}, Seq:{2}, AckNum:{3}", 
                    new object[]{pack.Syn,pack.Ack,pack.SequenceNumber,pack.AcknowledgmentNumber}));
            }
            System.Diagnostics.Debug.WriteLine("");

            var syn =  q.ElementAt(2).Extract(typeof(TcpPacket)) as TcpPacket;
            var acksyn = q.ElementAt(1).Extract(typeof(TcpPacket)) as TcpPacket;
            var ack = q.ElementAt(0).Extract(typeof(TcpPacket)) as TcpPacket;

            if (syn.SourcePort != acksyn.DestinationPort || syn.SourcePort != ack.SourcePort)
            {
                client = null; host = null;
                return false;
            }

            if (syn.Syn && acksyn.Syn && acksyn.Ack && ack.Ack &&
                acksyn.AcknowledgmentNumber == syn.SequenceNumber + 1 && ack.AcknowledgmentNumber == acksyn.SequenceNumber + 1)
            {
                Program.getEndPoint(q.ElementAt(2), out client, out host);
                return true;
            }

            client = null; host = null;
            return false;
        }
        
    }
}
