using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PacketDotNet;
using SharpPcap;

namespace VisualSniffer
{
    
    
    static class Program
    {
        public static Type getPacketType(string typename)
        {
            return typeof(EthernetFields).Assembly.GetType(typename);
        }

        public static void getEndPoint(Packet p, out System.Net.IPEndPoint src, out System.Net.IPEndPoint dest)
        {
            if (p.PayloadPacket is IpPacket)
                if (p.PayloadPacket.PayloadPacket is TcpPacket)
                {
                    src = new System.Net.IPEndPoint((p.PayloadPacket as IpPacket).SourceAddress,
                        (p.PayloadPacket.PayloadPacket as TcpPacket).SourcePort);
                    dest = new System.Net.IPEndPoint((p.PayloadPacket as IpPacket).DestinationAddress,
                        (p.PayloadPacket.PayloadPacket as TcpPacket).DestinationPort);
                    return;
                }
            src = null; dest = null;
        }

        public static System.Net.IPEndPoint getSrcEndPoint(Packet p)
        {
            System.Net.IPEndPoint src, dest;
            getEndPoint(p, out src, out dest);
            return src;
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var f = new mainForm();
            var st = new statistics();
            st.Show();
            packetListener.Instance.onParseComplete += new newPacket(f.addNewPacket);
            packetListener.Instance.parkPyDissector(new pyDissector("http.py"));
            //packetListener.Instance.applyOnlineFilter(filterGen.genFilter("tcp.SourcePort == 80 || tcp.DestinationPort == 80"));
            Application.Run(f);
            packetListener.Instance.stopCapture();
        }
    }
}
