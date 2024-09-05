using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳程式.Models;
using 記帳程式.MVP;
using 記帳程式.Utility;

namespace 記帳程式.Forms
{
    [DisplayName("圖表")]
    public partial class GraphicForm : Form, IGraphicFormView
    {
        private IGraphicFormPresenter presenter;

        public GraphicForm()
        {
            InitializeComponent();

            KeyValuePair<string, string>[] typesStr = new KeyValuePair<string, string>[AppData.typeList.Count];
            AppData.typeList.CopyTo(typesStr);
            var typesStrList = typesStr.ToList();
            typesStrList = typesStrList.Prepend(new KeyValuePair<string, string>("全部", "全部")).ToList();

            filterCheckbox1.SetFilters(new List<string> { "銀行", "Visa", "行動支付" });
            filterCheckbox1.Width = 300;
            filterCheckbox1.onCheckChange = filterChanged;

            CategoryfilterCheckbox.SetFilters(typesStrList.Where(x => x.Key != "全部").Select(x => x.Key).ToList());

            CategoryfilterCheckbox.onCheckChange = onCategoryFilterChange;

            presenter = DIContainer.GetInstance<IGraphicFormPresenter>();
            presenter.SetView(this);
            
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

        private void GraphicForm_Load(object sender, EventArgs e)
        {
            string[] comboBoxNames = new string[3] { "圓餅圖", "長條圖", "折線圖" };
            comboBox1.DataSource = comboBoxNames;
        }
            //HW1: 能夠做到 一樣給定區間跟checkbox 做資料分群
            //HW2: 給予一個下拉選單(圓餅圖 直調圖 折線圖)


        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue is string chartCategory)
            {
                
            }
        }

        public void GenerateChartFinish(List<Series> series)
        {
            chart1.Series.Clear();
            chart1.Legends.Clear();
            foreach(var s in series) 
            {
            
                chart1.Series.Add(s);
            }
            var chartCategory = string.Empty;
            if (!(comboBox1.SelectedValue is string))
                return;
            chartCategory = (string)comboBox1.SelectedValue;
            if (chartCategory == "折線圖")
            {
                chart1.Legends.Add(new Legend("2024-08"));
                chart1.Legends.Add(new Legend("2023-08"));
                chart1.Legends["2024-08"].DockedToChartArea = "ChartArea1";
                chart1.Legends["2023-08"].DockedToChartArea = "ChartArea1";
                
                chart1.Series[0].Legend = "2024-08";
                chart1.Series[1].Legend = "2023-08";
                chart1.Series[0].LegendText = "2024-08";
                chart1.Series[1].LegendText = "2023-08";
                chart1.Series[0].IsVisibleInLegend = true;
                chart1.Series[1].IsVisibleInLegend = true;
            } 
            


            if (chartCategory == "長條圖")
            {
                chart1.Legends.Add(new Legend("2024-08"));
                chart1.Legends.Add(new Legend("2023-08"));
                chart1.Legends["2024-08"].DockedToChartArea = "ChartArea1";
                chart1.Legends["2023-08"].DockedToChartArea = "ChartArea1";

                chart1.Series[0].Legend = "2024-08";
                chart1.Series[1].Legend = "2023-08";
                chart1.Series[0].LegendText = "2024-08";
                chart1.Series[1].LegendText = "2023-08";
                chart1.Series[0].IsVisibleInLegend = true;
                chart1.Series[1].IsVisibleInLegend = true;
                chart1.Series[0].IsValueShownAsLabel = true;
                chart1.Series[1].IsValueShownAsLabel = true;
            }
            //// Set Docking of the Legend chart to the Default Chart Area.
            //chart1.Legends["Legend2"].DockToChartArea = "Default";

            //// Assign the legend to Series1.
            //chart1.Series["Series1"].Legend = "Legend2";
            //chart1.Series["Series1"].IsVisibleInLegend = true;
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            var chartCategory = comboBox1.SelectedValue as string;
            presenter.GenerateChart(
                chartCategory, 
                dateTimePicker1.Value,
                dateTimePicker2.Value,
                CategoryfilterCheckbox.Filters,
                ReasonfilterCheckbox.Filters,
                filterCheckbox1.Filters);
        }
    }
}
