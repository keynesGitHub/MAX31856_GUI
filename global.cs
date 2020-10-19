﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_MAX31856
{
    class global
    {
        public const float DECIMAL_POINT = 0.01f;

        public static stru_serialPort vari_serialPort = new stru_serialPort();
        public static stru_main vari_main = new stru_main();
        public static stru_txt vari_txt = new stru_txt();
        public static stru_mysql vari_mysql = new stru_mysql();
        public static stru_pdf vari_pdf = new stru_pdf();
        public static stru_chart vari_chart = new stru_chart();

        public static byte[] ModBusCRCHi = new byte[]
        {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
            0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40
        };

        public static byte[] ModBusCRCLo = new byte[]
        {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4,
            0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
            0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD,
            0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7,
            0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
            0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE,
            0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2,
            0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
            0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB,
            0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91,
            0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
            0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88,
            0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80,
            0x40
        };

        public static int ModBusCRC16(ref byte[] UpData, ushort Length)
        {
            byte CRCHi = 0xFF;
            byte CRCLo = 0xFF;
            int Index;
            ushort i = 0;

            while (Length > 0)
            {
                Length--;
                Index = CRCHi ^ UpData[i++];
                CRCHi = (byte)(CRCLo ^ ModBusCRCHi[Index]);
                CRCLo = ModBusCRCLo[Index];
            }
            return (CRCHi << 8 | CRCLo);
        }

    }




    public struct stru_main
    {
        public bool fg_serialCOM_error;
        public bool fg_judgeCOM_connect;
        public bool fg_judgeMysqlLink;
        public bool fg_dataGV_refresh;
        public bool fg_dataGV_delete;

        public string stri_nowDateTime;
        public string stri_deviceID;
        public string stri_fucCode;
        public string stri_LTC;
        
        public uint u32_LTC;
        public float f32_LTC;

        public ushort u16_dataGV_refresh_ms;
        public ushort u16_dataGV_delete_ms;
    }

    public struct stru_serialPort
    {
        public byte[] u8A_tramtData;
        public byte[] u8A_recivData;
        public byte[] u8A_tramtCRC_conte;
        public byte[] u8A_recivCRC_conte;
        public int s32_tramtCRC;
        public int s32_recivCRC;
        public int s32_recivLength;
        public string stri_comboBox_portName;
        public int s32_comboBox_baudRate;
    }

    public struct stru_txt
    {
        public bool fg_port;
        public bool fg_baudrate;

        public string stri_txtWritePath;
        public string stri_txtWriteDirectory;
        public string stri_txt_YMD;
        public string stri_txtName;
        
        public string[] striA_paths;
        public string stri_txtReadPath;
        public string stri_txtReadLine;
        public string stri_txtConta_port;
        public string stri_txtConfig_port;
        public string stri_txtConta_baudrate;

        public string stri_nowDateTime;
        public string stri_devID;
        public string stri_fc;
        public string stri_data_nbr;
        public string stri_holdRegConte;
        public string stri_CRC16MSB;
        public string stri_CRC16LSB;

        public int s32_txtPortIndex;
        public int s32_txtBaudrateIndex;
        public int s32_txtConfig_baudrate;

    }

    public struct stru_mysql
    {
        public bool fg_judgeMysqlLink;
        public bool fg_INSERT_status;
        

        public string stri_insr_nowDateTime;
        public string stri_insr_temp;
        
        public string stri_INSERT_data;
        public string stri_SELECT_data;
        public string stri_DELETE_data;
        public string stri_read_MAX_item;
        public string stri_ALTER_data;
        public string stri_UPDATE_data;

    }

    public struct stru_pdf
    {
        public bool fg_iPDF;
        public bool fg_rPDF;
        public string stri_pdfWritePath;
        public string stri_pdfWriteDirectory;
        public string stri_pdfDateTime;
        public string stri_pdfName;
        public string stri_startT;
        public string stri_EndT;
        
        public string stri_dspy_C;
        public string[] striA_paths;
    }

    public struct stru_chart
    {
        public string stri_startT;
    }
}
