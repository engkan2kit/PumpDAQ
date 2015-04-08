using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpDAQ
{
    class Packet
    {
        public static byte Sflag = 0x96;
        private byte CMD=0;
        private byte Len=0;
        private byte[] Payload;
        private UInt16 CRC=0xFFFF;
        public static byte Eflag=0xA5;

        public Packet()
        {

        }

        public byte[] getPayload()
        {
            return Payload;
        }

        public void setPayload(byte[] payload)
        {
            
            this.Len=(byte)(payload.Length/sizeof(byte));
            this.Payload = new byte [this.Len];
            this.Payload = payload;
            this.CRC = CRC16.ComputeChecksum(this.Payload,this.Len);
        }

        public Boolean checkCRC(UInt16 crc)
        {
            if(this.CRC==crc)
                return true;
            else
                return false;
        }

        public UInt16 getCRC()
        {
            return this.CRC;
        }
        public byte getLength()
        {
            return Len;
        }

        public void setLength(byte len)
        {
            this.Len = len;
        }

        public void setCMD(byte cmd)
        {
            this.CMD = cmd;
        }
        public byte getCMD()
        {
            return CMD;
        }
    }
}
