using System;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Reflection;

namespace FirstHomeWork.Classes
{
    public class ExcelClass
    {
        public string  ExcelOutput(DataTable p_datatable ,string p_fileName)
        {
            DirectoryInfo l_dirTemplate = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.AppSettings["TemplateDirectory"]);
            DirectoryInfo l_dirOutput = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.AppSettings["TempOutput"]);
            FileStream fs = new FileStream(l_dirTemplate.FullName + @"\test.xls", FileMode.Open, FileAccess.Read);

            HSSFWorkbook workbook = new HSSFWorkbook(fs, true);
            DatatableToExcel(p_datatable, ref workbook, 0);

            string l_OutputFileName = @"/"+ p_fileName+"_" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls";
            OutputToFile(l_dirOutput.FullName + l_OutputFileName, ref workbook);
            return  "~/App_Data/excel/TempOutput/" + l_OutputFileName;
        }
        public DataTable CreateDataTableForPropertiesOfType<T>()
        {
            DataTable dt = new DataTable();
            PropertyInfo[] piT = typeof(T).GetProperties();

            foreach (PropertyInfo pi in piT)
            {
                Type propertyType = null;
                if (pi.PropertyType.IsGenericType)
                {
                    propertyType = pi.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    propertyType = pi.PropertyType;
                }
                DataColumn dc = new DataColumn(pi.Name, propertyType);

                if (pi.CanRead)
                {
                    dt.Columns.Add(dc);
                }
            }

            return dt;
        }

        public DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            var table = CreateDataTableForPropertiesOfType<T>();
            PropertyInfo[] piT = typeof(T).GetProperties();

            foreach (var item in items)
            {
                var dr = table.NewRow();

                for (int property = 0; property < table.Columns.Count; property++)
                {
                    if (piT[property].CanRead)
                    {
                        if (piT[property].GetValue(item, null) != null)
                        {
                            dr[property] = piT[property].GetValue(item, null);
                        }
                        else
                        {
                            dr[property] = DBNull.Value;
                        }
                    }
                }

                table.Rows.Add(dr);
            }
            return table;
        }

        public bool DatatableToExcel(DataTable p_datatable, ref HSSFWorkbook workbook, int l_intSheetNo, int p_intStartIndex = 0)
        {
            try
            {
                HSSFSheet sheet;

                sheet = (HSSFSheet)workbook.GetSheetAt(l_intSheetNo);

                #region 排資料進去
                int idxBoxRow = p_intStartIndex + 1;
                int l_ColumnCount = p_datatable.Columns.Count;
                sheet.CreateRow(p_intStartIndex);
                for (int i = 0; i < l_ColumnCount; i++)
                {
                    sheet.GetRow(p_intStartIndex).CreateCell(i).SetCellValue(p_datatable.Columns[i].ColumnName);
                }
                foreach (DataRow row in p_datatable.Rows)
                {

                    sheet.CreateRow(idxBoxRow);
                    for (int i = 0; i < l_ColumnCount; i++)
                    {
                        sheet.GetRow(idxBoxRow).CreateCell(i).SetCellValue(row[i].ToString());
                    }
                    idxBoxRow++;
                }

                #endregion
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DatatableToExcelForMonthlyNotice(DataTable p_datatable, ref HSSFWorkbook workbook, int l_intSheetNo, int p_intStartIndex = 0)
        {
            try
            {

                HSSFSheet sheet;

                sheet = (HSSFSheet)workbook.GetSheetAt(l_intSheetNo);

                #region 排資料進去
                int idxBoxRow = p_intStartIndex + 1;
                int l_ColumnCount = p_datatable.Columns.Count;
                sheet.CreateRow(p_intStartIndex);
                for (int i = 0; i < l_ColumnCount; i++)
                {
                    sheet.GetRow(p_intStartIndex).CreateCell(i).SetCellValue(p_datatable.Columns[i].ColumnName);
                }
                foreach (DataRow row in p_datatable.Rows)
                {
                    if (idxBoxRow == 65535)
                    {
                        idxBoxRow = p_intStartIndex + 1;
                        l_intSheetNo++;
                        sheet = (HSSFSheet)workbook.CreateSheet("每月通知單明細" + l_intSheetNo.ToString());
                        //sheet = (HSSFSheet)workbook.GetSheet(l_intSheetNo.ToString());
                        sheet.CreateRow(p_intStartIndex);
                        for (int i = 0; i < l_ColumnCount; i++)
                        {
                            sheet.GetRow(p_intStartIndex).CreateCell(i).SetCellValue(p_datatable.Columns[i].ColumnName);
                        }
                    }

                    sheet.CreateRow(idxBoxRow);
                    for (int i = 0; i < l_ColumnCount; i++)
                    {
                        sheet.GetRow(idxBoxRow).CreateCell(i).SetCellValue(row[i].ToString());
                    }
                    idxBoxRow++;
                }

                #endregion
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void OutputToFile(string strPathName, ref HSSFWorkbook workbook)
        {
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            FileStream fsOut = new FileStream(strPathName, FileMode.Create, FileAccess.Write);
            byte[] bytes = ms.ToArray();
            fsOut.Write(bytes, 0, bytes.Length);
            fsOut.Close();
            ms.Close();
        }
    }
}