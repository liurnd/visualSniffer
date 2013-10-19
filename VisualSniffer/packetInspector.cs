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
    public partial class packetInspector : Form
    {
        public packetInspector()
        {
            InitializeComponent();
        }
        public void showPacket(ref PacketDotNet.Packet p)
        {
            packViewer.Nodes.Clear();
            HexView.ByteProvider = new HexViewer.DynamicByteProvider(p.Bytes);
            
            int offset = 0;
            for (var i = p; i != null; i = i.PayloadPacket)
            {
                packViewer.Nodes.Add(new packView(offset, ref i));
                offset += i.Header.Length;
            }
        }

        private void packViewer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var br = e.Node as byteRange;
            HexView.Select(br.offset, br.length);
        }
    }

    public interface byteRange
    {
        int offset{get; }
        int length{get; }
    }

    public class packView : TreeNode, byteRange
    {
        private class bitFieldArg
        {
            public string fieldName;
            public byte data, bitOffset;
            public bitFieldArg(string mf, byte d, byte b) { fieldName = mf; data = d; bitOffset = b; }
        }

        PacketDotNet.Packet pack;
        Dictionary<string, int> offsetDict = new Dictionary<string, int>();
        Dictionary<string, int> lengthDict = new Dictionary<string, int>();
        Dictionary<string, string> dataDict = new Dictionary<string, string>();
        Dictionary<string, Dictionary<int, bitFieldArg>> bitFieldList = new Dictionary<string,Dictionary<int,bitFieldArg> >();

        private int moffset;
        public packView(int loffset , ref PacketDotNet.Packet targetPack)
        {
            pack = targetPack;
            Text = pack.GetType().Name;
            moffset = loffset;
            listField(ref pack);
            paintNodes();
        }

        #region Field Editor

        void addBitField(string fatherField, string bitFieldName, bool data, byte bitOffset)
        {
            addBitField(fatherField, bitFieldName, (byte)(data ? 1 : 0), bitOffset);
        }
        void addBitField(string fatherField, string bitFieldName, byte data, byte bitOffset)
        {
            if (!bitFieldList.ContainsKey(fatherField))
                bitFieldList[fatherField] = new Dictionary<int, bitFieldArg>();
            bitFieldList[fatherField][bitOffset] = new bitFieldArg(bitFieldName, data, bitOffset);
        }

        void addField(string fieldName, byte[] data, int loffset, int llen)
        {
            addField(fieldName, "Byte stream (" + data.Length + ")",loffset,llen);
        }

        void addField(string fieldName, string data, int loffset, int llen)
        {
            offsetDict[fieldName] = offset + loffset;
            dataDict[fieldName] = data;
            lengthDict[fieldName] = llen;
        }

        void addField(string fieldName, object d, int loffset, int llen)
        {
            addField(fieldName, d.ToString(), loffset, llen);
        }

        void addField( string typeName,string fieldName, string data)
        {
           addField(typeName, fieldName,data, fieldName+"Length");
        }

        void addField(string typeName, string fieldName, string data, string lengthName)
        {
            var vFieldName = fieldName.Replace(" ", "");
            var packetFieldsType = Program.getPacketType("PacketDotNet." + typeName + "Fields");
            var lenField = packetFieldsType.GetField(lengthName);
            addField(fieldName, data, (int)packetFieldsType.GetField(vFieldName + "Position").GetValue(null), (int)lenField.GetValue(null));
        }

        void addField(string typeName, string fieldName, string data,int len)
        {
            var vFieldName = fieldName.Replace(" ", "");
            var packetFieldsType = Program.getPacketType("PacketDotNet." + typeName + "Fields");
            var offset = (int)packetFieldsType.GetField(vFieldName + "Position").GetValue(null);
            addField(fieldName, data, offset, len);
        }
        
        void addField(string typeName, string fieldName, object k)
        {
            var packetFieldsType = Program.getPacketType("PacketDotNet." + typeName + "Fields");
            var vFieldName = fieldName.Replace(" ", "");
            var fieldI = k.GetType().GetProperty(vFieldName);
            string data = fieldI.GetValue(k,null).ToString();
            addField(typeName, fieldName, data, vFieldName+"Length");
        }
        #endregion

        private void listField(ref PacketDotNet.EthernetPacket p)
        {
            addField("Dest Hw Addr", p.DestinationHwAddress, EthernetFields.DestinationMacPosition,EthernetFields.MacAddressLength);
            addField("Source Hw Addr", p.SourceHwAddress, EthernetFields.SourceMacPosition, EthernetFields.MacAddressLength);
            addField("Ethernet", "Type", p);
        }

        void listField(ref ARPPacket p)
        {
            addField("ARP", "Hardware Address Length", p.HardwareAddressLength.ToString(), ARPFields.AddressLengthLength);
            addField("ARP", "Hardware Address Type", p.HardwareAddressType.ToString(),ARPFields.AddressTypeLength);
            addField("ARP", "Operation", p);
            addField("ARP", "Protocol Address Length", p.ProtocolAddressLength.ToString(), ARPFields.AddressLengthLength);
            addField("Protocol Address Type",p.ProtocolAddressType,ARPFields.ProtocolAddressTypePosition, 2);
            addField("Sender Hardware Address", p.SenderHardwareAddress.ToString(),ARPFields.SenderHardwareAddressPosition, p.HardwareAddressLength);
            addField("Target Hardware Address", p.TargetHardwareAddress.ToString(), ARPFields.TargetHardwareAddressPosition, p.HardwareAddressLength);
            addField("Sender Protocol Address", p.SenderProtocolAddress.ToString(), ARPFields.SenderProtocolAddressPosition, p.ProtocolAddressLength);
            addField("Target Protocol Address", p.TargetProtocolAddress.ToString(), ARPFields.TargetProtocolAddressPosition, p.ProtocolAddressLength);
        }

        void listField(ref IPv4Packet p)
        {
            addField("IPv4", "Checksum", p);
            addField("Destination Address", p.DestinationAddress.ToString(), IPv4Fields.DestinationPosition,IPv4Fields.AddressLength);
            addField("IPv4", "TotalLength", p);
            addField("IPv4", "Differentiated Services", p);
            addField("Fragment Flags", p.FragmentFlags.ToString(), IPv4Fields.FragmentOffsetAndFlagsPosition, IPv4Fields.FragmentOffsetAndFlagsLength);
            addField("Fragment Offset", p.FragmentOffset.ToString(), IPv4Fields.FragmentOffsetAndFlagsPosition, IPv4Fields.FragmentOffsetAndFlagsLength);
            addField("Header Length", p.HeaderLength.ToString(), IPv4Fields.VersionAndHeaderLengthPosition, IPv4Fields.VersionAndHeaderLengthLength);
            addField("IPv4", "Id", p);
            addField("IPv4", "Protocol", p);
            addField("Source Address", p.SourceAddress.ToString(), IPv4Fields.SourcePosition,IPv4Fields.AddressLength);
            addField("Time To Live(TTL)", p.TimeToLive.ToString(), IPv4Fields.TtlPosition,IPv4Fields.TtlLength);
        }

        void listField(ref ICMPv4Packet p)
        {
            addField("ICMPv4", "Checksum", p);
            addField("ICMPv4", "ID", p);
            addField("ICMPv4", "Sequence", p);
            addField("ICMPv4", "Type Code", p);
        }

        void listField(ref TcpPacket p)
        {
            addField("Ack Number", p.AcknowledgmentNumber.ToString(), TcpFields.AckNumberPosition, TcpFields.AckNumberLength);
            addField("Flags", p.AllFlags.ToString(), TcpFields.FlagsPosition,TcpFields.FlagsLength);
            addBitField("Flags", "NS",(byte)( p.Header[12] & 1),0xff);
            addBitField("Flags", "CWR", p.CWR, 0);
            addBitField("Flags", "ECE", p.ECN, 1);
            addBitField("Flags", "URG", p.Urg, 2);
            addBitField("Flags", "ACK", p.Ack, 3);
            addBitField("Flags", "PSH", p.Psh, 4);
            addBitField("Flags", "Rst", p.Rst, 5);
            addBitField("Flags", "SYN", p.Syn, 6);
            addBitField("Flags", "FIN", p.Fin, 7);
            addField("Tcp", "Checksum", p);
            addField("Tcp", "Data Offset", p);
            addField("Dest Port", p.DestinationPort.ToString(), TcpFields.DestinationPortPosition, TcpFields.PortLength);
            addField("Tcp", "Sequence Number", p);
            addField("Src Port", p.SourcePort.ToString(), TcpFields.SourcePortPosition, TcpFields.PortLength);
            addField("Tcp", "Urgent Pointer", p);
            addField("Tcp", "Window Size", p);
        }
        private void listField(ref UdpPacket p)
        {
            addField("Udp", "Checksum", p);
            addField("Dest Port", p.DestinationPort.ToString(), UdpFields.DestinationPortPosition, UdpFields.PortLength);
            addField("Src Port", p.SourcePort.ToString(), UdpFields.SourcePortPosition, UdpFields.PortLength);
            addField("Length", p.Length.ToString(), UdpFields.HeaderLengthPosition, UdpFields.HeaderLengthLength);
        }

        private void listFieldEx(ref PacketDotNet.Packet targetPack)
        {
            //Get packet type name
            var packetTypeName = targetPack.GetType().Name;
            var packetFieldTypeName = packetTypeName.Substring(0, packetTypeName.Length - 6) + "Fields";
            var packetFieldsType = Program.getPacketType("PacketDotNet." + packetFieldTypeName);
            //Get packet fields
            var fieldList = packetFieldsType.GetFields();

            foreach (var i in fieldList)
            {
                var fieldTypeName = i.Name;
                if (fieldTypeName.EndsWith("Position"))
                {
                    int foffset = (int)i.GetValue(null);
                    var fieldName = fieldTypeName.Substring(0, fieldTypeName.Length - 8);
                    offsetDict.Add(fieldName, offset + foffset);
                }
                else if (fieldTypeName.EndsWith("Length"))
                {
                    int length = (int)i.GetValue(null);
                    var fieldName = fieldTypeName.Substring(0, fieldTypeName.Length - 6);
                    lengthDict.Add(fieldName, length);
                }
            }
        }

        private void listField(ref PacketDotNet.Packet targetPack)
        {
            if (targetPack is PacketDotNet.InternetLinkLayerPacket)
                this.BackColor = Color.FromArgb(0xffffff);
            else if (targetPack is PacketDotNet.InternetPacket)
                this.BackColor = Color.FromArgb(0xccffff);
            else if (targetPack is TransportPacket)
                this.BackColor = Color.FromArgb(0xccccff);
            
               
            switch (targetPack.GetType().Name)
            {
                case "EthernetPacket":
                    EthernetPacket eth = targetPack as EthernetPacket;
                    listField(ref eth);
                    break;
                case "ARPPacket":
                    ARPPacket arp = targetPack as ARPPacket;
                    listField(ref arp);
                    break;
                case "IPv4Packet":
                    var ipv4 = targetPack as IPv4Packet;
                    listField(ref ipv4);
                    break;
                case "ICMPv4Packet":
                    var ICMPv4 = targetPack as ICMPv4Packet;
                    listField(ref ICMPv4);
                    break;
                case "TcpPacket":
                    var tcp = targetPack as TcpPacket;
                    listField(ref tcp);
                    break;
                case "UdpPacket":
                    var udp = targetPack as UdpPacket;
                    listField(ref udp);
                    break;
            }
            if (targetPack.PayloadPacket == null && targetPack.PayloadData != null)
            {
                addField("Payload Data", targetPack.PayloadData, targetPack.Header.Length, targetPack.BytesHighPerformance.BytesLength-targetPack.Header.Length);
            }
        }

        #region Utils
        void paintNodes()
        {
            foreach (var i in offsetDict.OrderBy(p => p.Value))
                if (lengthDict.ContainsKey(i.Key))
                    if (dataDict.ContainsKey(i.Key))
                    {
                        var field = new fieldView(i.Key, dataDict[i.Key], i.Value, lengthDict[i.Key]);
                        Nodes.Add(field);
                        if (bitFieldList.ContainsKey(i.Key))
                        {
                            foreach (var j in bitFieldList[i.Key])
                                field.Nodes.Add(new bitFieldView(ref field, j.Value.fieldName, j.Value.data, j.Value.bitOffset));
                        }
                        field.BackColor = this.BackColor;
                    }
                    else
                        Nodes.Add(new fieldView(i.Key, i.Value, lengthDict[i.Key]));
        }

        public int offset
        {
            get 
            {
                return moffset;
            }
        }

        public int length
        {
            get { return pack.Header.Length; }
        }
        #endregion
    }

    public class fieldView : TreeNode, byteRange
    {
        int moffset, mlen;
        public fieldView(string fieldName, string ldata, int loffset, int llen)
        {
            moffset = loffset; mlen = llen;
            this.Text = fieldName + ":" + ldata;
        }

        public fieldView(string fieldName,  int loffset, int llen)
        {
            moffset = loffset; mlen = llen;
            this.Text = fieldName + ":Byte stream (" + llen + ")";
        }
        public int offset { get { return moffset; } }
        public int length { get { return mlen; } }
    }

    public class bitFieldView : TreeNode, byteRange
    {
        fieldView parent;
        public bitFieldView(ref fieldView mparent, string name, byte data, byte bitOffset)
        {
            parent = mparent;
            string tmp;
            if (bitOffset == 0xff)
            {
                 tmp = data.ToString() + "........";
            }
            else
            {
                tmp = " ";
                for (var i = 0; i < bitOffset; i++)
                    tmp += ".";
                tmp += data.ToString();
                for (var i = 0; i < 7 - bitOffset; i++)
                    tmp += ".";
            }
            Text = tmp + "   " + name + "    ("+ ((data == 1) ? "Set" : "Reset")+")";
        }

        public int offset { get { return parent.offset; } }
        public int length { get { return 1; } }
    }
}
