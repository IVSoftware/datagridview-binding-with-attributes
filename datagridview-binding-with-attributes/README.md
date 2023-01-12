Your question is **how to bind DataGridView to specific properties** . You mention that you have a collection of `Data` but don't say whether it's observable (for example `BindingList<Data>`). You end your post with *what's the easiest way to do this?*. That's a matter of opinion but "one way" that I personally find very easy is to allow `AutoGenerateColumns` and do column formatting on a bindable source in the `OnLoad` override of the form.

***
**Example**

[![screenshot][1]][1]

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
            Rows.Add(new Data()); // <= Auto-generate columns
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
        }
    }

This gives you fine control in the data class to define the desired behavior including visibility and read-only.

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
        public string? D { get; set; } = "Not visible";        
    }

***
Finally, provided the column exists in the first place, it can be shown-hidden e.g. `dataGridView.Columns["C"].Visible = false`.

  [1]: https://i.stack.imgur.com/j1KEz.png