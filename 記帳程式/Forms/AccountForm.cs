using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳程式.Models;
using 記帳程式.MVP;
using 記帳程式.Utility;

namespace 記帳程式.Forms
{
    [DisplayName("帳戶設定")]
    public partial class AccountForm : Form, ISearchFormView
    {
        private ISearchFormPresenter _presenter;
        public AccountForm()
        {
            InitializeComponent();
            KeyValuePair<string, string>[] typesStr = new KeyValuePair<string, string>[AppData.typeList.Count];
            AppData.typeList.CopyTo(typesStr);
            var typesStrList = typesStr.ToList();
            typesStrList = typesStrList.Prepend(new KeyValuePair<string, string>("全部", "全部")).ToList();
            
            filterCheckbox1.SetFilters(new List<string> {  "銀行", "Visa", "行動支付" });
            filterCheckbox1.Width = 400;
            filterCheckbox1.onCheckChange = filterChanged;

            CategoryfilterCheckbox.SetFilters(typesStrList.Where(x => x.Key != "全部").Select(x => x.Key).ToList());

            CategoryfilterCheckbox.onCheckChange = onCategoryFilterChange;

            _presenter = DIContainer.GetInstance<ISearchFormPresenter>();
            _presenter.SetView(this);
        }


        

        private void button1_Click(object sender, EventArgs e)
        {
            _presenter.SearchRecords(dateTimePicker1.Value, 
                dateTimePicker2.Value, 
                CategoryfilterCheckbox.Filters/*.Count == 0 ? CategoryfilterCheckbox.AllFilters : CategoryfilterCheckbox.Filters*/, 
                ReasonfilterCheckbox.Filters/*.Count == 0 ? ReasonfilterCheckbox.AllFilters : ReasonfilterCheckbox.Filters*/,
                filterCheckbox1.Filters/*.Count == 0 ? filterCheckbox1.AllFilters : filterCheckbox1.Filters*/);
        }
        public void SearchRecordsFinish(List<Item> list)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            GC.Collect();
            dataGridView1.DataSource = list;
            sumLabel.Text = list.Sum(x => x.price).ToString();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
        }

        private List<string> filters = new List<string>();
        private void filterChanged(List<string> filter)
        {
            filters = filter;
        }

        private List<string> categoryFilters = new List<string>();
        private void onCategoryFilterChange(List<string> filter)
        {
            List<string> checkBoxTexts = new List<string>();
            foreach (string filterItem in filter) 
            {
                
                KeyValuePair<string, string>[] typesStr = new KeyValuePair<string, string>[AppData.typeDictionary[filterItem].Count];
                AppData.typeDictionary[filterItem].CopyTo(typesStr);
                var typesStrList = typesStr.Select(x => x.Key).ToList();
                checkBoxTexts = checkBoxTexts.Concat(typesStrList).ToList();
                
            }
            ReasonfilterCheckbox.SetFilters(checkBoxTexts);
            categoryFilters = filter;
        }

        private List<string> reasonFilters = new List<string>();

        private void onReasonFilterChange(List<string> filter)
        {
            reasonFilters = filter;
        }
    }
}
