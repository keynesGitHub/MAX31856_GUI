using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//===form===
using System.Windows.Forms;

//===mysql===
using MySql.Data.MySqlClient;



namespace TC_MAX31856
{
    //資料來源
    //https://www.youtube.com/watch?v=MBJ-rWojYeE&list=PLFDH5bKmoNqz9-YbH6JZMS5m_MS57_VKX


    public class mysqlDB
    {
        private MySqlConnection mysqlLink = new MySqlConnection("server=localhost; port=3306; database=tc_db; username=root; password=root;");

        public bool judgeMysqlLink()
        {
            try
            {
                mysqlLink.Open();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                mysqlLink.Close();
            }
        }

        public void mysql_close()
        {
            mysqlLink.Close();
        }

        public void mysql_INSERT()
        {
            mysqlLink.Open();

            global.vari_mysql.stri_INSERT_data = "INSERT INTO tc_db.tc_tb (`YMDT`,`temp_dp2`) VALUE( '" + @global.vari_mysql.stri_insr_nowDateTime + "','" + @global.vari_mysql.stri_insr_temp + "')";

            MySqlCommand _mysqlCMD = new MySqlCommand(global.vari_mysql.stri_INSERT_data, mysqlLink);

            try
            {
                _mysqlCMD.ExecuteNonQuery();
                global.vari_mysql.fg_INSERT_status = true;
            }
            catch
            {
                global.vari_mysql.fg_INSERT_status = false;
            }
            finally
            {
                mysqlLink.Close();
            }
        }

        public MySqlDataReader mysql_READER_for_dataGridV()
        {
            mysqlLink.Open();

            global.vari_mysql.stri_SELECT_data = "SELECT * FROM tc_db.tc_tb";
            MySqlCommand _mysqlCMD = new MySqlCommand(global.vari_mysql.stri_SELECT_data, mysqlLink);

            MySqlDataReader _mysqlREADER = _mysqlCMD.ExecuteReader();
            
            return _mysqlREADER;
        }


        public void mysql_DELETE(int i)
        {
            mysqlLink.Open();

            global.vari_mysql.stri_DELETE_data = "DELETE FROM tc_db.tc_tb WHERE item = '" + i + "'";
            MySqlCommand _mysqlCMD = new MySqlCommand(global.vari_mysql.stri_DELETE_data, mysqlLink);

            _mysqlCMD.ExecuteNonQuery();

            mysqlLink.Close();
        }

        public void mysql_ALTER_auto_increment()
        {
            mysqlLink.Open();

            global.vari_mysql.stri_ALTER_data = "ALTER TABLE tc_db.tc_tb auto_increment = 1";
            MySqlCommand _mysqlCMD = new MySqlCommand(global.vari_mysql.stri_ALTER_data, mysqlLink);

            _mysqlCMD.ExecuteNonQuery();

            mysqlLink.Close();
        }

        public string stri_mysql_read_MAX_item()
        {
            mysqlLink.Open();

            global.vari_mysql.stri_SELECT_data = "SELECT MAX(item) from tc_db.tc_tb";
            MySqlCommand _mysqlCMD = new MySqlCommand(global.vari_mysql.stri_SELECT_data, mysqlLink);

            MySqlDataReader _mysqlREADER = _mysqlCMD.ExecuteReader();
            while (_mysqlREADER.Read())
            {
                global.vari_mysql.stri_read_MAX_item = _mysqlREADER[0].ToString();
            }

            if (global.vari_mysql.stri_read_MAX_item == string.Empty) { global.vari_mysql.stri_read_MAX_item = "0"; }

            mysqlLink.Close();

            return global.vari_mysql.stri_read_MAX_item;
        }
    }
}
