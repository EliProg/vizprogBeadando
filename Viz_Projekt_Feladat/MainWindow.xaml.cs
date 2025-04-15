﻿using cnOrarend;
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

namespace Viz_Projekt_Feladat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        OrarendContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new OrarendContext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /* LoginWindow barmilehet = new LoginWindow();
             barmilehet.Show(); */

            var window = new TeacherEditWindow(_context, 1);
            window.Show();
        }
    }
}