using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.CSharp;
using System.CodeDom.Compiler;
namespace VisualSniffer
{
    public abstract class packetFilter
    {
        public abstract bool pass(ref PacketDotNet.Packet p);
        public static packetFilter operator &(packetFilter f1, packetFilter f2) { return new andFilter(f1, f2); }
        public static packetFilter operator |(packetFilter f1, packetFilter f2) { return new orFilter(f1, f2); }
        public static packetFilter operator ~(packetFilter f1) { return new notFilter(f1); }
    }

    public class filterViewer
    {
        packetFilter filter;
        int index = 0;
        public filterViewer(packetFilter filter)
        {
            this.filter = filter;
        }
        public void reset()
        {
            index = 0;
        }
        public void reset(int index)
        {
            this.index = index;
        }
        public sPacket getCurrent()
        {
            if (index >=0 && index < packetListener.Instance.packList.Count)
                return packetListener.Instance.packList[index];
            return null;
        }

        public sPacket getNext()
        {
            for (index ++ ;index < packetListener.Instance.packList.Count; index ++)
                if (filter.pass(ref (packetListener.Instance.packList[index].packet)))
                    return packetListener.Instance.packList[index];
            return null;
        }
        public sPacket getForward()
        {
            for (index-- ;index >=0; index--)
                if (filter.pass(ref (packetListener.Instance.packList[index].packet)))
                    return packetListener.Instance.packList[index];
            return null;
        }
    }

    public class andFilter : packetFilter
    {
        packetFilter a, b;
        public andFilter(packetFilter aa, packetFilter bb)
        {
            a = aa; b = bb;
        }
        public override bool pass(ref PacketDotNet.Packet p) { return a.pass(ref p) && b.pass(ref p); }
    }
    public class orFilter : packetFilter
    {
        packetFilter a, b;
        public orFilter(packetFilter aa, packetFilter bb)
        {
            a = aa; b = bb;
        }
        public override bool pass(ref PacketDotNet.Packet p) { return a.pass(ref p) || b.pass(ref p); }
    }
    public class notFilter : packetFilter
    {
        packetFilter a;
        public notFilter(packetFilter aa)
        {
            a = aa;
        }
        public override bool pass(ref PacketDotNet.Packet p) { return !a.pass(ref p); }
    }

    public class ipIpFilter : packetFilter
    {
        bool isSource; System.Net.IPAddress ip;
        public ipIpFilter(bool lisSource, System.Net.IPAddress lip)
        {
            isSource = lisSource; ip = lip;
        }

        public override bool pass(ref PacketDotNet.Packet p)
        {
            if (p.PayloadPacket != null && (p.PayloadPacket is PacketDotNet.IpPacket))
                if (isSource)
                {
                    if ((p.PayloadPacket as PacketDotNet.IpPacket).SourceAddress.Equals(ip))
                        return true;
                }
                else
                {
                    if ((p.PayloadPacket as PacketDotNet.IpPacket).DestinationAddress.Equals(ip))
                        return true;
                }
            return false;
        }
    }

    public class tcpFilter : packetFilter
    {
        public override bool pass(ref PacketDotNet.Packet p)
        {
            if (p.PayloadPacket != null && p.PayloadPacket.PayloadPacket != null && p.PayloadPacket.PayloadPacket.GetType() == typeof(PacketDotNet.TcpPacket))
                return true;
            return false;
        }
    }

    public class tcpPortFilter : packetFilter
    {
        bool isSource; ushort port;
        bool careSide;
        public tcpPortFilter(bool lisSource, ushort lport)
        {
            isSource = lisSource; port = lport; careSide = true;
        }

        public override bool pass(ref PacketDotNet.Packet p)
        {
            if (p.PayloadPacket != null && p.PayloadPacket.PayloadPacket != null && p.PayloadPacket.PayloadPacket.GetType() == typeof(PacketDotNet.TcpPacket))
            {
                var tcp = p.PayloadPacket.PayloadPacket as PacketDotNet.TcpPacket;
                if (isSource)
                    return (tcp.SourcePort == port);
                else
                    return (tcp.DestinationPort == port);
            }
            return false;
        }
    }

    public class filterGen
    {
        public static packetFilter genFilter(string filterS)
        {
            var compiler = new Microsoft.CSharp.CSharpCodeProvider();
            var options = new CompilerParameters();
            options.GenerateExecutable = false;
            options.GenerateInMemory = true;
            PacketDotNet.Packet p;
            options.ReferencedAssemblies.Add(typeof(PacketDotNet.Packet).Assembly.Location);
            options.ReferencedAssemblies.Add(typeof(filterGen).Assembly.Location);
            var source = @"
using System;
using VisualSniffer;
public class customFilter : packetFilter
{
public override bool pass(ref PacketDotNet.Packet p)
        {
try{
var ip = p.Extract(typeof(PacketDotNet.IpPacket)) as PacketDotNet.IpPacket;
var tcp = p.Extract(typeof(PacketDotNet.TcpPacket)) as PacketDotNet.TcpPacket;
var udp = p.Extract(typeof(PacketDotNet.UdpPacket))as PacketDotNet.UdpPacket;
return (" + filterS+@");
}
catch(Exception e)
{
return false;
}
        }
} 
            ";
            var result = compiler.CompileAssemblyFromSource(options, source);
            if (result.Errors.HasErrors)
            {
                foreach(var i in result.Errors)
                {
                    System.Diagnostics.Debugger.Log(0, "compile error", i.ToString());
                }
                return null;
            }
            else
                return Activator.CreateInstance(result.CompiledAssembly.GetType("customFilter")) as packetFilter;


        }
    }
}
