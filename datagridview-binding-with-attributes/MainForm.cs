using System.ComponentModel;

namespace datagridview_binding_with_attributes
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();
        internal BindingList<Data> Rows { get; } = new BindingList<Data>();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            dataGridView.AllowUserToAddRows = false;
            dataGridView.DataSource = Rows;
            dataGridView.CellEndEdit += (sender, e) => dataGridView.Refresh();

            #region F O R M A T    C O L U M N S
            Rows.Add(new Data());
            dataGridView.Columns["A"].Width = 50;
            dataGridView.Columns["A"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["A"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["C"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["C"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Rows.Clear();
            #endregion F O R M A T    C O L U M N S

            // Add a few items
            Rows.Add(new Data { A = 1, B = 1000L });
            Rows.Add(new Data { A = 2, B = 2000L });
            Rows.Add(new Data { A = 3, B = 3000L });

            // dataGridView.Columns["C"].Visible = false;
        }
    }
    class Data
    {
        // A visible, editable cell.
        public int A { get; set; }

        // Non-visible because property is declared as internal.
        internal long B { get; set; }

        // Visible, read-only cell 
        public string C => $"A={A} B={B}";

        // Non-visible because of attribute.
        [Browsable(false)]
        public string D { get; set; } = "Not visible";        
    }
}