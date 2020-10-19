using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//===txt===
using System.IO;

namespace TC_MAX31856
{
    public class txt
    {
        public void txtInitial()
        {
            global.vari_txt.stri_txtWriteDirectory = @"C:\Users\Keynes\Desktop\txt\log\TC";
            global.vari_txt.stri_txtReadPath = @"C:\Users\Keynes\Desktop\txt\config\TC.txt";

            global.vari_txt.stri_txtConta_port = "port=";
            global.vari_txt.stri_txtConta_baudrate = "baudrate=";
        }

        public void txtWrite()
        {
            global.vari_txt.stri_txt_YMD = DateTime.Now.ToString("yyyyMMdd");
            global.vari_txt.stri_txtName = "device_log" + global.vari_txt.stri_txt_YMD + ".txt";

            global.vari_txt.striA_paths = new string[2] { global.vari_txt.stri_txtWriteDirectory, global.vari_txt.stri_txtName };
            global.vari_txt.stri_txtWritePath = Path.Combine(global.vari_txt.striA_paths);//路徑和檔名合併

            FileStream fs_txtLog = new FileStream(global.vari_txt.stri_txtWritePath, FileMode.Append);
            StreamWriter sw_txtWrite = new StreamWriter(fs_txtLog);

            sw_txtWrite.WriteLine(global.vari_txt.stri_nowDateTime + global.vari_txt.stri_devID + global.vari_txt.stri_fc + global.vari_txt.stri_data_nbr +
                                  global.vari_txt.stri_holdRegConte + global.vari_txt.stri_CRC16MSB + global.vari_txt.stri_CRC16LSB + "\n");
            sw_txtWrite.Flush();
            sw_txtWrite.Close();
        }

        public void txtRead()
        {
            FileStream fs_txtConfig = new FileStream(global.vari_txt.stri_txtReadPath, FileMode.Open);
            StreamReader sw_txtRead = new StreamReader(fs_txtConfig);

            global.vari_txt.stri_txtReadLine = string.Empty;
            while (sw_txtRead.Peek() > 0)
            {
                global.vari_txt.stri_txtReadLine = sw_txtRead.ReadLine();
                
                //===

                global.vari_txt.fg_port = global.vari_txt.stri_txtReadLine.Contains(global.vari_txt.stri_txtConta_port);
                if(global.vari_txt.fg_port)
                {
                    global.vari_txt.s32_txtPortIndex = global.vari_txt.stri_txtReadLine.IndexOf(global.vari_txt.stri_txtConta_port);
                    global.vari_txt.stri_txtConfig_port = global.vari_txt.stri_txtReadLine.Remove(global.vari_txt.s32_txtPortIndex, 5);
                }

                //===

                global.vari_txt.fg_baudrate = global.vari_txt.stri_txtReadLine.Contains(global.vari_txt.stri_txtConta_baudrate);
                if(global.vari_txt.fg_baudrate)
                {
                    global.vari_txt.s32_txtBaudrateIndex = global.vari_txt.stri_txtReadLine.IndexOf(global.vari_txt.stri_txtConta_baudrate);
                    global.vari_txt.s32_txtConfig_baudrate = Convert.ToInt32(global.vari_txt.stri_txtReadLine.Remove(global.vari_txt.s32_txtPortIndex, 9));
                }

                //===

                Console.WriteLine(global.vari_txt.stri_txtReadLine);
                
            }
            sw_txtRead.Close();
        }
    }
}
