using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;



namespace PumpDAQ
{
    class SerialQueuer
    {
        private SerialPort comm;
        BlockingCollection<Packet> SQ=new BlockingCollection<Packet>();
        private byte ReadState=0;
        private Packet tempPacket;
        private UInt16 tempCRC;
        private byte len = 0;
        private byte[] buffer = new byte[256];
        public SerialQueuer(String commport, BlockingCollection<Packet> Pqueue)
        {
            comm = new SerialPort(commport, 9600, Parity.None, 8, StopBits.One);
            SQ = Pqueue;
            

        }
        public void OpenPort()
        {
            comm.Open();
            comm.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

        }
        public void ClosePort()
        {
            if (comm.IsOpen)
            {
                comm.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);
                comm.Close();
            }
        }
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            byte indata;
        
            while (sp.BytesToRead > 0)
            {
                switch (ReadState)
                {                    
                    case 1:
                        indata = (byte)sp.ReadByte();
                        tempPacket.setCMD(indata);
                        ReadState = 2;
                        break;
                    case 2:
                        indata = (byte)sp.ReadByte();
                        tempPacket.setLength(indata);
                        buffer = new byte[indata];
                        ReadState = 2;
                        break;
                    case 3:
                        indata = (byte)sp.ReadByte();
                        buffer[len] = indata;
                        if (len == tempPacket.getLength())
                        {
                            tempPacket.setPayload(buffer);
                            ReadState = 4;
                        }
                        len++;
                        break;
                    case 4:
                        indata = (byte)sp.ReadByte();
                        tempCRC = (UInt16)((UInt16)indata << 8);
                        ReadState = 5;
                        break;
                    case 5:
                        indata = (byte)sp.ReadByte();
                        tempCRC = (UInt16)((UInt16)indata | tempCRC);
                        if (tempPacket.checkCRC(tempCRC))
                        {
                            ReadState = 6;
                        }
                        else
                        {
                            ReadState = 0;
                        }
                        break;
                    case 6:
                        indata = (byte)sp.ReadByte();
                        if (Packet.Eflag == indata)
                        {
                            SQ.Add(tempPacket);
                        }
                        ReadState = 0;
                        break;
                    default:
                        indata = (byte)sp.ReadByte();
                        if (indata == Packet.Sflag)
                        {
                            tempPacket = new Packet();
                            ReadState = 1;
                            len = 0;
                        }
                        break;
                }
            }
        }

    }
}
