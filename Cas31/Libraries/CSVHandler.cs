using Excel = Microsoft.Office.Interop.Excel;

namespace Cas31.Libraries
{
    class CSVHandler
    {
        private Excel.Application App;
        private Excel.Workbook Workbook;
        private Excel.Worksheet Sheet;

        public CSVHandler()
        {
            this.App = new Excel.Application();
        }

        public Excel.Worksheet OpenCSV(string CSVFile, string CSVDelimiter = ",")
        {
            this.Workbook = this.App.Workbooks.Open(CSVFile, Format: Excel.XlFileFormat.xlCSV, Delimiter: CSVDelimiter);
            this.Sheet = this.Workbook.ActiveSheet;
            return this.Sheet;
        }

        public void Close()
        {
            this.App.Quit();
        }
    }
}
