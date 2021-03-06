using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SQLite;
using System.IO;

using Sharpdactyl;
using Sharpdactyl.Models.Client;

namespace Pterodactyl2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
    public class databaseCreator
    {
        SQLiteConnection dbConnection;
        SQLiteCommand command;
        string sqlCommand;
        string dbPath = System.Environment.CurrentDirectory + "\\DB";
        string dbFilePath;
        public void createDB()
        {
            if (!string.IsNullOrEmpty(dbPath) && !Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);
                dbFilePath = dbPath + "\\servers.db";
                if (!System.IO.File.Exists(dbFilePath))
                {
                    SQLiteConnection.CreateFile(dbFilePath);
                }
            }
        }

        public String createDBConnection()
        {
            string strCon = string.Format("Date Source={0}", dbFilePath);
            dbConnection = new SQLiteConnection(strCon);
            dbConnection.Open();
            command = dbConnection.CreateCommand();
            return strCon;
        }

        public bool checkIfExist(string TableName)
        {
            command.CommandText = "SELECT name FROM sqlite_master WHERE name='" + TableName + "'";
            var result = command.ExecuteScalar();
            return result != null && result.ToString() == TableName ? true : false;
        }

        public void executeQuery(string sqlCommand)
        {
            SQLiteCommand triggerCommand = dbConnection.CreateCommand();
            triggerCommand.CommandText = sqlCommand;
            triggerCommand.ExecuteNonQuery();
        }

        public bool checkIfTableontainsData(string tableName) {
            command.CommandText = "SELECT count(*) FROM " + tableName;
            var result = command.ExecuteScalar();
            return Convert.ToInt32(result) > 0 ? true : false;
        }

        public void fillTable()
        {
            if (!checkIfTableontainsData("PANELS"))
            {
                sqlCommand = "insert into PANELS (code_test_type) values (999)";
                executeQuery(sqlCommand);
            }
        }
    }
}
