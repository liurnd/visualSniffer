using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;
using PacketDotNet;
namespace VisualSniffer
{
    public class pyDissector
    {
        private ScriptEngine engine;
        private ScriptScope scope;
        private ScriptSource src;
        private CompiledCode program;
        public packetFilter myFilter;
        private Func<Packet, HLPacket> parseFunc;

        public pyDissector(string fileName)
        {
            engine = Python.CreateEngine();
            scope = engine.CreateScope();
            var runtime = engine.Runtime;
            runtime.LoadAssembly(typeof(PacketDotNet.Packet).Assembly);
            runtime.LoadAssembly(typeof(pyDissector).Assembly);
            src = engine.CreateScriptSourceFromFile(fileName);
            program = src.Compile();
            var result = program.Execute(scope);
            var filterString = scope.GetVariable<string>("nativeFilterString");
            myFilter = filterGen.genFilter(filterString);
            parseFunc = scope.GetVariable<Func<Packet, HLPacket>>("parsePacket");
        }

        public bool parsePacket(Packet p, out HLPacket outP)
        {
            outP = parseFunc(p);
            return (outP != null);
        }

    }
    public class HLPacket : Packet{
        public string packetType;
        public string info;

        public Dictionary<string, IronPython.Runtime.PythonTuple> fieldList = new Dictionary<string,IronPython.Runtime.PythonTuple>();

        public HLPacket(Packet parent, int headerLen)
        {
            var i = parent;
            for (; i.PayloadPacket != null; i = i.PayloadPacket) ;
            header = new PacketDotNet.Utils.ByteArraySegment(i.Bytes, i.Header.Length, headerLen);
            i.PayloadPacket = this;
        }
        public override string ToString()
        {
            return info;
        }
    }
}
