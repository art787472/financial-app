using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using 記帳程式.Services;

namespace 記帳程式.MVP
{
    internal class GraphicFormPresenter : IGraphicFormPresenter
    {
        private IGraphicFormView view;
        private IRepository repository;

        //public void GenerateChart(string chartCategory)
        //{
        //    var begin = new DateTime(DateTime.Now.Year, 8, 1);
        //    var end = new DateTime(DateTime.Now.Year, 8, 31);



        //    var (dataX, dataY) = RecordService.GetRecordsForChart(begin, end);
        //    var seriesList = new List<Series>();
        //    var series = new Series(); 
        //    switch (chartCategory) 
        //    {
        //        case "圓餅圖":
        //            series.ChartType = SeriesChartType.Pie;
        //            series.Points.DataBindXY(dataX, dataY);
        //            seriesList.Add(series);
        //            break;
        //        case "長條圖":
                    
        //            series.ChartType = SeriesChartType.Bar;
        //            series.Points.DataBindXY(dataX, dataY);
        //            seriesList.Add(series);
        //            break;
        //        case "折線圖":
        //            var beginLastYear = new DateTime(DateTime.Now.Year - 1, 8, 1);
        //            var endLastYear = new DateTime(DateTime.Now.Year - 1, 8, 31);
        //            //var series1 = GenerateLineChart(begin, end);
        //            //var series2 = GenerateLineChart(beginLastYear, endLastYear);
        //            //seriesList.Add(series1);
        //            //seriesList.Add(series2);
                    
                    
        //            break;
        //    }
        //    view.GenerateChartFinish(seriesList);
        //}

        public void GenerateChart(string chartCategory, DateTime begin, DateTime end, List<string> categorys, List<string> reasons, List<string> accounts)
        {
            // 取得畫面上篩選的資料
            // 呼叫csv repository 篩選資料
            // 設定圖表類型
            // 群組每一天的資料
            // 將資料轉變為合適的圖表資料
            // 將圖表資料回傳給前端進行渲染

            // HW1: 完成其他圖表的相同操作
            // Hw2: 讀完建造者模式 pattern 並製作簡報
            // Hw3: 思考建造者模式的優點/缺點
            // Hw4: 思考是否可以將建造者模式直接搬來圖表這邊用?


            var (dataX, dataY) = RecordService.GetRecordsForChart(begin, end);
            var seriesList = new List<Series>();
            var series = new Series();
            switch (chartCategory)
            {
                case "圓餅圖":
                    series.ChartType = SeriesChartType.Pie;
                    var (pieDataX, pieDataY) = RecordService.GetRecordsForPieChart(begin, end, categorys, reasons, accounts);
                    series.Points.DataBindXY(pieDataX, pieDataY);
                    seriesList.Add(series);
                    break;
                case "長條圖":
                    {
                        var beginLastYear = new DateTime(DateTime.Now.Year - 1, 8, 1);
                        var endLastYear = new DateTime(DateTime.Now.Year - 1, 8, 31);
                        var series1 = GenerateBarChart(begin, end, categorys, reasons, accounts);
                        var series2 = GenerateBarChart(beginLastYear, endLastYear, categorys, reasons, accounts);
                        seriesList.Add(series1);
                        seriesList.Add(series2);
                    }
                    break;
                case "折線圖":
                    {
                        var beginLastYear = new DateTime(DateTime.Now.Year - 1, 8, 1);
                        var endLastYear = new DateTime(DateTime.Now.Year - 1, 8, 31);
                        var series1 = GenerateLineChart(begin, end, categorys, reasons, accounts);
                        var series2 = GenerateLineChart(beginLastYear, endLastYear, categorys, reasons, accounts);
                        seriesList.Add(series1);
                        seriesList.Add(series2);
                    }

                    break;
            }

            view.GenerateChartFinish(seriesList);
        }
        public void SetView(IGraphicFormView view)
        {
            this.view = view;
            this.repository = DIContainer.GetInstance<IRepository>();
        }

        private Series GenerateLineChart(DateTime begin, DateTime end, List<string> categorys, List<string> reasons, List<string> accounts)
        {
            var series = new Series();
            
            var (lineDataX, lineDataY) = RecordService.GetRecordsForLineChart(begin, end, categorys, reasons, accounts);
            series.ChartType = SeriesChartType.Line;
            series.Points.DataBindXY(lineDataX, lineDataY);
            return series;

        }

        private Series GenerateBarChart(DateTime begin, DateTime end, List<string> categorys, List<string> reasons, List<string> accounts)
        {
            var series = new Series();

            var (lineDataX, lineDataY) = RecordService.GetRecordsForPieChart(begin, end, categorys, reasons, accounts);
            series.ChartType = SeriesChartType.Bar;
            series.Points.DataBindXY(lineDataX, lineDataY);
            return series;
        }
    }
}
