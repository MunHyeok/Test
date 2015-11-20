using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace test
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSender _dataSender = new DataSender();
        DataProcessor _dataProcessor = new DataProcessor();
        public MainWindow()
        {
            InitializeComponent();
            btn_emergency.Click += new RoutedEventHandler(btn_emergency_Click);
            btn_nomal.Click += new RoutedEventHandler(btn_nomal_Click);
            btn_Iclick.Click += new RoutedEventHandler(btn_Iclick_Click);

            
        }

        private void btn_Iclick_Click(object sender, RoutedEventArgs e)
        {
            _dataProcessor.SetData(true);
            txt_receive.Text = _dataProcessor.data.situation;
            _dataSender.ReceiveData(_dataProcessor);
            txt_send.Text = _dataSender.settingData.Text;
        }

        void btn_nomal_Click(object sender, RoutedEventArgs e)
        {
            _dataSender.SetData(false);
            txt_send.Text = _dataSender.SendData();
            _dataProcessor.ReceiveData(_dataSender);
            txt_receive.Text = _dataProcessor.data.situation;
        }

        void btn_emergency_Click(object sender, RoutedEventArgs e)
        {
            _dataSender.SetData(true);
            txt_send.Text = _dataSender.SendData();
            _dataProcessor.ReceiveData(_dataSender);
            txt_receive.Text = _dataProcessor.data.situation;
        }

    }



    public class DataModel
    {

        public string location;
        public bool switched;
        public string situation;
    }

    public class DataReceiver
    {
        public DataModel data = new DataModel();

        public DataReceiver()
        {
            SetData(false);
        }

        public void SetData(bool switched)
        {
            Random rd1 = new Random();
            Random rd2 = new Random();

            rd1.Next(1000);
            rd2.Next(1000);
            if (switched)
            {
                data.location = string.Format("{0}, {1}", rd1, rd2);
                data.situation = "살려줘요";
                data.switched = true; 
            }
            else
            {
                data.location = string.Format("{0}, {1}", rd1, rd2);
                data.situation = "아무이상없슴다";
                data.switched = false;
            }
        }

    }


    public class DataProcessor : DataReceiver
    {

        public DataProcessor()
        {
            
        }


        public void DataProcess()
        {
            if (this.data.switched)
            {
            }
            else
            {
                //평시상황일 경우 생기는 상황 처리
            }
        }


        public void ReceiveData(DataSender _dataSender)
        {
            this.data.switched = _dataSender.settingData.switched;
            this.data.situation = _dataSender.settingData.Text;
        }
    }

    public class SettingModel
    {

        public bool switched;
        public string Text;
    }


    public class SettingReceiver
    {
        public SettingModel settingData = new SettingModel();

        public SettingReceiver()
        {
            SetData(false);
        }

        public void SetData(bool switched)
        {
            settingData.switched = switched;
            if (switched)
            {
                settingData.Text = "내아이를 살려줘요";
            }
            else
            {
                settingData.Text = "아무이상 없슴다";
            }

        }

    }

    public class DataSender : SettingReceiver
    {
        public DataSender()
        {

        }

        public string SendData()
        {
            {
                return settingData.Text;
            }

        }

        public void ReceiveData(DataProcessor _dataProcessor)
        {
            this.settingData.switched = _dataProcessor.data.switched;
            this.settingData.Text = "아이가 위험합니다.";
        }

      
    }


}
