using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics
{
    public partial class SHOWDATA : Form
    {
        List<string> strr = new List<string>();
        List<List<int>> listfft = new List<List<int>>();
        public SHOWDATA(List<string> STR, List<List<int>> listt)
        {
            InitializeComponent();
            strr = STR;
            listfft = listt;
        }

        private void SHOWDATA_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < strr.Count; i++)
            {
                dataGridView1.Columns.Add("kero", strr[i]);
            }

            for (int i = 0; i < listfft.Count; i++)
            {
                dataGridView1.Rows.Add("");
                for (int j = 0; j < listfft[i].Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = listfft[i][j].ToString();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
