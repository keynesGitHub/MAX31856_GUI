using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//===serial port
using System.IO.Ports;
using System.Threading;


namespace TC_MAX31856
{
    public class serialPort
    {
        private SerialPort _serialPort = new SerialPort("COM1", 19200, Parity.None, 8, StopBits.One);

        public Thread thread_serialCOM;


        //===build thread===
        public void buildthread_serialCOM()
        {
            thread_serialCOM = new Thread(serialCOM_thread);
        }
        //===


        //===serialPort Initial===
        public void serialPortInitial()
        {
            _serialPort.Close();

            global.vari_serialPort.u8A_tramtData = new byte[16];
            global.vari_serialPort.u8A_recivData = new byte[1024];

            global.vari_serialPort.u8A_tramtCRC_conte = new byte[16];
            global.vari_serialPort.u8A_recivCRC_conte = new byte[1024];

            _serialPort.WriteTimeout = 500;
            _serialPort.ReadTimeout = 500;
        }
        //===


        //===serialPort Config===
        public void serialPortConfig()
        {
            _serialPort.Close();
            _serialPort.PortName = global.vari_txt.stri_txtConfig_port;
            _serialPort.BaudRate = global.vari_txt.s32_txtConfig_baudrate;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Handshake = Handshake.None;
            
        }
        //===

        public void serialPortWrite(int length)
        {
            _serialPort.Write(global.vari_serialPort.u8A_tramtData, 0, length);
        }

        //===

        public void serialPortClearArrayAndLength()
        {
            Array.Clear(global.vari_serialPort.u8A_recivData, 0, 512);
            global.vari_serialPort.s32_recivLength = 0;
        }

        //===

        public void serialPortOpen()
        {
            if (_serialPort.IsOpen == false) { _serialPort.Open(); }
        }

        //===

        public void serialPortClose()
        {
            _serialPort.Close();
        }


        //===serial thread===
        private void serialCOM_thread(object obj_serialthread)
        {
            formMain _serialthread = (formMain)obj_serialthread;

            for (;;)
            {
                try
                {
                    if (_serialPort.IsOpen == false) { _serialPort.Open(); }

                    _serialPort.Write(global.vari_serialPort.u8A_tramtData, 0, 8);
                    Thread.Sleep(1000);
                    global.vari_serialPort.s32_recivLength = _serialPort.Read(global.vari_serialPort.u8A_recivData, 0, 512);
                    _serialthread.showlbl_temp();


                    global.vari_main.fg_serialCOM_error = false;

                }
                catch
                {
                    global.vari_main.fg_serialCOM_error = true;

                }
                
            }
        }
        //===END===
    }
}
