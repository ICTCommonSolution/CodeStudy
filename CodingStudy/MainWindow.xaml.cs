using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace CodingStudy
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog obj = new OpenFileDialog();
            if (obj.ShowDialog() == true)
            {
                DllStudy ds = new DllStudy(obj.FileName);

                if (true == ds.FunctionExists(TextBox1.Text))
                {
                    MessageBox.Show(string.Format("The function {0} exists in Dll {1}", TextBox1.Text, obj.FileName));
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (false == System.IO.File.Exists(TextBox2.Text))
                    return;
                TPReader tpr = new TPReader(TextBox2.Text);
                string strErr = string.Empty;
                if (false == TPReader.ValidateTP(TextBox2.Text, out strErr))
                {
                    MessageBox.Show(string.Format("It's not a valid test plan. error message is \"{0}\"", strErr));
                }
            }
            catch (Exception ex)
            { }
        }
    }

    public class DllStudy
    {
        private Assembly m_Ass = null;
        public DllStudy(string fileName)
        {
            m_Ass = Assembly.LoadFrom(fileName);
        }

        public bool FunctionExists(string functionName)//, Dictionary<string, string> paraName)
        {
            foreach (var type in m_Ass.GetTypes())
            {
                MethodInfo[] members = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                foreach (MemberInfo member in members)
                {
                    if (true == member.Name.Equals(functionName))
                    {
                        MessageBox.Show(string.Format("Member.Name={0}\n para={1}", member.Name));
                        return true;
                    }
                    //Console.WriteLine(type.Name + "." + member.Name);
                }
            }
            return false;
        }
    }
}
