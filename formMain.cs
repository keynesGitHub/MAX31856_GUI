using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//===
using System.IO;


//===pdf===
using iTextSharp.text;
using iTextSharp.text.pdf;


//===chart===
using System.Windows.Forms.DataVisualization.Charting;
//資料來源
//https://www.youtube.com/watch?v=2JNly8dluVg
//https://www.youtube.com/watch?v=hhr2mi-TBLY



namespace TC_MAX31856
{
    public partial class formMain : Form
    {
       
        public formMain()
        {
            InitializeComponent();
        }
        
        public serialPort _serialPortMain = new serialPort();
        public txt txtMain = new txt();
        public pdf pdfMain = new pdf();
        public mysqlDB mysqlDBmain = new mysqlDB();
        

        private void formMain_Load(object sender, EventArgs e)
        {
            formMainInitial();
        }

        public void formMainInitial()
        {
            txtMain.txtInitial();
            txtMain.txtRead();

            pdfMain.pdfInitial();

            _serialPortMain.serialPortInitial();
            _serialPortMain.serialPortConfig();

            deviceCommand();
            _serialPortMain.buildthread_serialCOM();
            _serialPortMain.thread_serialCOM.Start(this);

            //===

            global.vari_mysql.fg_judgeMysqlLink = mysqlDBmain.judgeMysqlLink();//mysql link status
            if (global.vari_mysql.fg_judgeMysqlLink == true) { lbl_mysql_status.BackColor = Color.LightGreen; }
            else if (global.vari_mysql.fg_judgeMysqlLink == false) { lbl_mysql_status.BackColor = Color.Salmon; }

            global.vari_pdf.stri_startT = DateTime.Now.ToString();//pdf first time
            global.vari_chart.stri_startT = DateTime.Now.ToString("yyyyMMddHHmmss");//chart first time

            //===

            var temp_curve = chart_temp.ChartAreas[0];
            temp_curve.AxisX.IntervalType = DateTimeIntervalType.Milliseconds;


            temp_curve.AxisX.LabelStyle.Format = "HH:mm:ss";
            temp_curve.AxisY.LabelStyle.Format = "";
            temp_curve.AxisY.LabelStyle.IsEndLabelVisible = true;

            temp_curve.AxisX.Minimum = 0;
            temp_curve.AxisX.Maximum = DateTime.Now.ToOADate();
            temp_curve.AxisY.Minimum = 0;
            temp_curve.AxisY.Maximum = 50;
            temp_curve.AxisX.Interval = 1;
            temp_curve.AxisY.Interval = 5;

            chart_temp.Series.Add("temp");
            chart_temp.Series["temp"].ChartType = SeriesChartType.Line;
            chart_temp.Series["temp"].Color = Color.Red;
            chart_temp.Series[0].IsVisibleInLegend = false;

            chart_temp.Series["temp"].Points.AddXY(1, 10);
            chart_temp.Series["temp"].Points.AddXY(1.5, 20);
            chart_temp.Series["temp"].Points.AddXY(2, 60);
            chart_temp.Series["temp"].Points.AddXY(2.5, 70);
            chart_temp.Series["temp"].Points.AddXY(3, 30);

            //===

            TC_dataGridView.DataSource = getTC_dataGridV();
            mysqlDBmain.mysql_close();

        }

        private DataTable getTC_dataGridV()
        {
            //資料來源 
            //https://www.youtube.com/watch?v=Dr_-Q11XWZc
            //https://www.youtube.com/watch?v=VhsGGLREqu8

            //===

            DataTable dataTb_TC = new DataTable();     
                                 
            dataTb_TC.Load(mysqlDBmain.mysql_READER_for_dataGridV());

            return dataTb_TC;
        }

        public void deviceCommand()
        {
            global.vari_serialPort.u8A_tramtData[0] = 0x01;//ID
            global.vari_serialPort.u8A_tramtData[1] = 0x03;//function code
            global.vari_serialPort.u8A_tramtData[2] = 0x00;//start number
            global.vari_serialPort.u8A_tramtData[3] = 0x00;//start number
            global.vari_serialPort.u8A_tramtData[4] = 0x00;//quantity
            global.vari_serialPort.u8A_tramtData[5] = 0x01;//quantity

            for (byte i = 0; i < 6; i++)
            {
                global.vari_serialPort.u8A_tramtCRC_conte[i] = global.vari_serialPort.u8A_tramtData[i];
            }
            global.vari_serialPort.s32_tramtCRC = global.ModBusCRC16(ref global.vari_serialPort.u8A_tramtCRC_conte, 6);

            global.vari_serialPort.u8A_tramtData[6] = (byte)((global.vari_serialPort.s32_tramtCRC & 0xFF00) >> 8);//ModBusCRC MSB
            global.vari_serialPort.u8A_tramtData[7] = (byte)(global.vari_serialPort.s32_tramtCRC & 0x00FF);//ModBusCRC LSB
        }

        public void showlbl_temp()
        {
            global.vari_txt.stri_devID = string.Empty;
            global.vari_txt.stri_fc = string.Empty;
            global.vari_txt.stri_data_nbr = string.Empty;
            global.vari_txt.stri_holdRegConte = string.Empty;
            global.vari_txt.stri_CRC16MSB = string.Empty;
            global.vari_txt.stri_CRC16LSB = string.Empty;
            global.vari_main.stri_LTC = string.Empty;
            recivData_buildUp();

            global.vari_main.stri_LTC = Convert.ToString($"{global.vari_main.f32_LTC:f2}");
            Console.WriteLine(global.vari_main.stri_LTC);

            if (lbl_LTC_temp.InvokeRequired)
            {
                lbl_LTC_temp.Invoke(new MethodInvoker(delegate { lbl_LTC_temp.Text = global.vari_main.stri_LTC; }));
            }

            if ((global.vari_main.fg_dataGV_refresh == false) && (global.vari_main.fg_dataGV_delete == false)) { mysqlDBmain.mysql_INSERT(); }

            txtMain.txtWrite();
        }

        private void recivData_buildUp()
        {
            global.vari_txt.stri_devID = string.Format($"${global.vari_serialPort.u8A_recivData[0]:X2} "); //Convert.ToString($"{ global.vari_serialPort.u8A_recivData[0]:X2}");
            global.vari_txt.stri_fc = string.Format($"${global.vari_serialPort.u8A_recivData[1]:X2} ");
            global.vari_txt.stri_data_nbr = string.Format($"${global.vari_serialPort.u8A_recivData[2]:X2} ");

            uint i;
            for (i = 0; i < global.vari_serialPort.u8A_recivData[2]; i++)
            {
                global.vari_txt.stri_holdRegConte = global.vari_txt.stri_holdRegConte + string.Format($"${global.vari_serialPort.u8A_recivData[i + 3]:X2} ");
            }

            global.vari_txt.stri_CRC16MSB = string.Format($"${global.vari_serialPort.u8A_recivData[i + 3]:X2} ");
            global.vari_txt.stri_CRC16LSB = string.Format($"${global.vari_serialPort.u8A_recivData[i + 4]:X2} ");

            global.vari_main.u32_LTC = global.vari_serialPort.u8A_recivData[3];
            global.vari_main.u32_LTC = (global.vari_main.u32_LTC << 8) + global.vari_serialPort.u8A_recivData[4];
            global.vari_main.f32_LTC = global.vari_main.u32_LTC * global.DECIMAL_POINT;

            global.vari_mysql.stri_insr_temp = Convert.ToString(global.vari_main.u32_LTC);
            
        }

        private void timer_1ms_Tick(object sender, EventArgs e)
        {
            global.vari_txt.stri_nowDateTime = DateTime.Now.ToString("yyyy/MM/dd     HH : mm : ss     ");
            global.vari_mysql.stri_insr_nowDateTime = DateTime.Now.ToString("yyyy/MM/dd     HH:mm:ss");
            global.vari_chart.stri_startT = DateTime.Now.ToString("yyyyMMddHHmmss");
            
            //===

            if (global.vari_main.fg_serialCOM_error == true) { lbl_COM_status.BackColor = Color.Salmon; }
            else if (global.vari_main.fg_serialCOM_error == false) { lbl_COM_status.BackColor = Color.LightGreen; }

            if (chkbx_LTC.Checked == false) { lbl_LTC_temp.Visible = false; }
            else { lbl_LTC_temp.Visible = true; }

            if (global.vari_mysql.fg_INSERT_status == true) { lbl_mysql_status.BackColor = Color.LightGreen; }
            else if (global.vari_mysql.fg_INSERT_status == false) { lbl_mysql_status.BackColor = Color.Salmon; }

            //===

            if (global.vari_main.fg_dataGV_refresh == true)
            {
                if(global.vari_main.u16_dataGV_refresh_ms > 1)
                {
                    TC_dataGridView.DataSource = getTC_dataGridV();
                    mysqlDBmain.mysql_close();
                    global.vari_main.fg_dataGV_refresh = false;
                }
                else { global.vari_main.u16_dataGV_refresh_ms++; }
            }

            //===

            if(global.vari_main.fg_dataGV_delete == true)
            {
                if(global.vari_main.u16_dataGV_delete_ms > 1)
                {
                    int deleteCount = Convert.ToInt32(mysqlDBmain.stri_mysql_read_MAX_item());
                    int loop;

                    //===progressbar===
                    prgsbar_DELETE.Visible = true;
                    prgsbar_DELETE.Value = 1;
                    prgsbar_DELETE.Step = 1;
                    prgsbar_DELETE.Minimum = 1;
                    prgsbar_DELETE.Maximum = deleteCount;
                    if (prgsbar_DELETE.Maximum <= 0) { prgsbar_DELETE.Maximum = 1; }
                    //===

                    for (int i = 0; i <= deleteCount; i++)
                    {
                        loop = i;
                        mysqlDBmain.mysql_DELETE(loop);
                        prgsbar_DELETE.PerformStep();
                    }

                    mysqlDBmain.mysql_ALTER_auto_increment();

                    global.vari_main.fg_dataGV_delete = false;
                    btn_dataGV_REFRESH.Enabled = true;
                }
                else { global.vari_main.u16_dataGV_delete_ms++; }
            }

            //===
        }

        private void pdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            global.vari_pdf.stri_EndT = DateTime.Now.ToString();//pdf end time
            
            pdfWrite_from_dataGV();
        }

        public void pdfWrite_from_dataGV()
        {
            PdfPCell tb_cell;

            global.vari_pdf.stri_pdfDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            global.vari_pdf.stri_pdfName = "TC_Report" + global.vari_pdf.stri_pdfDateTime + ".pdf";
            global.vari_pdf.striA_paths = new string[2] { global.vari_pdf.stri_pdfWriteDirectory, global.vari_pdf.stri_pdfName };
            global.vari_pdf.stri_pdfWritePath = Path.Combine(global.vari_pdf.striA_paths);//路徑和檔名合併

            Document pdf_docu = new Document(PageSize.A4, 20f, 20f, 50f, 50f);
            PdfWriter.GetInstance(pdf_docu, new FileStream(global.vari_pdf.stri_pdfWritePath, FileMode.Append));

            BaseFont bfChinese = BaseFont.CreateFont("C:\\Windows\\Fonts\\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);//標楷體, 編碼(The Unicode encoding with horizontal writing), 是否要將字型嵌入PDF 檔中

            iTextSharp.text.Font chiFont_black = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);//標楷體, 12, normal, 黑色
            iTextSharp.text.Font chiFont_header = new iTextSharp.text.Font(bfChinese, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//標楷體, 20,bold, 黑色

            pdf_docu.Open();

            //===header===
            PdfPTable pdf_tb_header = new PdfPTable(1);
            PdfPCell pdf_headerCell = new PdfPCell(new Paragraph("溫度量測", chiFont_header));

            pdf_headerCell.HorizontalAlignment = Element.ALIGN_CENTER;//水平置中
            pdf_headerCell.VerticalAlignment = Element.ALIGN_CENTER;//垂直置中
            pdf_headerCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pdf_tb_header.AddCell(pdf_headerCell);

            pdf_docu.Add(pdf_tb_header);
            pdf_docu.Add(new Paragraph("\n"));
            //===

            //===condition===
            PdfPTable pdf_tb_condition = new PdfPTable(new float[] { 3f, 4f, 3f, 4f });

            //===1個row，有4欄位
            tb_cell = new PdfPCell(new Paragraph("程式開啟時間:", chiFont_black));//1
            tb_cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pdf_tb_condition.AddCell(tb_cell);

            tb_cell = new PdfPCell(new Paragraph(global.vari_pdf.stri_startT, chiFont_black));//234
            tb_cell.Colspan = 3;//併3欄
            tb_cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pdf_tb_condition.AddCell(tb_cell);
            pdf_tb_condition.CompleteRow();//這行最重要，如果有rowspan, colspan的情況下，一定要加上這個表示完成table
            //===

            //===1個row，有4欄位
            tb_cell = new PdfPCell(new Paragraph("報表產生時間:", chiFont_black));//1
            tb_cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pdf_tb_condition.AddCell(tb_cell);

            tb_cell = new PdfPCell(new Paragraph(global.vari_pdf.stri_EndT, chiFont_black));//234
            tb_cell.Colspan = 3;//併4欄
            tb_cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pdf_tb_condition.AddCell(tb_cell);
            pdf_tb_condition.CompleteRow();//這行最重要，如果有rowspan, colspan的情況下，一定要加上這個表示完成table
            //===
            
            pdf_docu.Add(pdf_tb_condition);
            pdf_docu.Add(new Paragraph("\n"));
            //===condition end===

            //===temp===
            PdfPTable pdf_tb_temp = new PdfPTable(new float[] { 4f, 4f, 4f });

            foreach (DataGridViewColumn column in TC_dataGridView.Columns)
            {
                tb_cell = new PdfPCell(new Paragraph(column.HeaderText, chiFont_black));
                tb_cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tb_cell.VerticalAlignment = Element.ALIGN_CENTER;
                pdf_tb_temp.AddCell(tb_cell);
            }
            
            foreach (DataGridViewRow row in TC_dataGridView.Rows)
            {
                foreach(DataGridViewCell cell in row.Cells)
                {
                    tb_cell = new PdfPCell(new Paragraph(cell.Value.ToString(), chiFont_black));
                    tb_cell.HorizontalAlignment = Element.ALIGN_CENTER;//水平置中
                    tb_cell.VerticalAlignment = Element.ALIGN_CENTER;//垂直置中
                    pdf_tb_temp.AddCell(tb_cell);

                    //pdf_tb_temp.AddCell(new Paragraph(cell.Value.ToString(), chiFont_black));//另種寫法
                }
            }
            pdf_docu.Add(pdf_tb_temp);
            //===


            pdf_docu.Close();
        }

        private void btn_dataGV_REFRESH_Click(object sender, EventArgs e)
        {
            global.vari_main.fg_dataGV_refresh = true;//disable mysql insert and start timer
        }

        private void btn_dataGV_DELETE_Click(object sender, EventArgs e)
        {
            global.vari_main.fg_dataGV_delete = true;//disable mysql insert
            btn_dataGV_REFRESH.Enabled = false;//refresh disable

            //int deleteCount = Convert.ToInt32(mysqlDBmain.stri_mysql_read_MAX_item());
            //int loop;

            ////===progressbar===
            //prgsbar_DELETE.Visible = true;
            //prgsbar_DELETE.Value = 1;
            //prgsbar_DELETE.Step = 1;
            //prgsbar_DELETE.Minimum = 1;
            //prgsbar_DELETE.Maximum = deleteCount;
            //if (prgsbar_DELETE.Maximum <= 0) { prgsbar_DELETE.Maximum = 1; }
            ////===

            //for (int i = 0; i <= deleteCount; i++)
            //{
            //    loop = i;
            //    mysqlDBmain.mysql_DELETE(loop);
            //    prgsbar_DELETE.PerformStep();
            //}

            //mysqlDBmain.mysql_ALTER_auto_increment();

            //global.vari_main.fg_dataGV_delete = false;
            //btn_dataGV_REFRESH.Enabled = true;
        }

        private void formMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _serialPortMain.thread_serialCOM.Abort();
        }

        
    }
}
