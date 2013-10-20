nativeFilterString = "tcp.SourcePort==80 || tcp.DestinationPort==80"
import VisualSniffer
import PacketDotNet
import System
import re

commandLine = re.compile(r"([A-Z]{3,6}) (.*) (HTTP/1.\d)")

def parsePacket(packet):
    tcpPacket = packet.PayloadPacket.PayloadPacket
    byteStream = str(bytes(tcpPacket.PayloadData))
    cline = commandLine.search(byteStream)
    print cline
    if (cline==None):
        return None
    tmp = VisualSniffer.HLPacket(packet,len(byteStream))
    tmp.packetType = "HTTP"
    tmp.info =  "HTTP %s Packet : URI %s" %(cline.group(1),cline.group(2))
    tmp.fieldList["Request Method: %s"%(cline.group(1),)] = (cline.start(1),len(cline.group(1)))
    tmp.fieldList["Request URI: %s"%(cline.group(2),)] = (cline.start(2),len(cline.group(2)))
    tmp.fieldList["Version: %s"%(cline.group(3),)] = (cline.start(3),len(cline.group(3)))
    offset = 0
    fields = byteStream.split('\r\n')
    for i in fields:
        if i.find(':')>=0:
            tmp.fieldList[i] = (offset,len(i))
        offset+=len(i)
    return tmp