using cnOrarend;
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
using System.Windows.Shapes;

namespace Viz_Projekt_Feladat
{
    /// <summary>
    /// Interaction logic for TeacherEditWindow.xaml
    /// </summary>
    public partial class TeacherEditWindow : Window
    {

        public TeacherEditWindow(OrarendContext _context, int id)
        {
            InitializeComponent();
            var lekerdezes = (from s in _context.enFelhasznalos
                              where s.id == id
                              select s.nev).ToList();

            name.Text = lekerdezes[0];
        }
    }
}
