using System.Data;
using System.Windows.Forms;

namespace LiveBook
{
    public partial class frmDataTable : Form
    {
        public frmDataTable(DataTable table)
        {
            InitializeComponent();
            dgvDataGrid.DataSource = table;
        }
    }
}
