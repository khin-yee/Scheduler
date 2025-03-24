using ClosedXML.Excel;
using Newtonsoft.Json;
using SchedulerTest.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Service
{
    public class ProductService:IProductService
    {
        private readonly IHttpClientService _httpclient;
        private readonly IProductRepo _repo;
        private string filepath;
        public ProductService(IProductRepo repo)
        {
            _repo = repo;
            filepath = "E:\\Temp";
        }
        public List<Product> GetProducts()
        {
            var products =   _repo.GetProducts();
            return products;
        }
        public async Task<string> CreateFileWithTxnData(string fileName)
        {
            string filePathName = string.Empty;
            var products =  GetProducts();
            _repo.UpdateAdaptorProduct(products);
            filePathName = WriteDataToFileAsync(products, fileName);
            return filePathName;
        }
        public string WriteDataToFileAsync(List<Product> transactions, string fileName)
        {
            EnsureDirectoryExists(filepath);
            var filePathName = Path.Combine(filepath, $"{fileName}_{DateTime.Now:yyyyMMddhhmm}.xlsx");
            GenerateExcel<Product>(filePathName, transactions);
            return filePathName;
        }

        public void GenerateExcel<T>(string filepathname, List<T> data)
        {
            byte[] excelFile = new byte[data.Count];
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");
                var dataTable = ConvertToDataTable(data);
                worksheet.Cell(1, 1).InsertTable(dataTable);
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    excelFile = stream.ToArray();
                    File.WriteAllBytes(filepathname, excelFile);
                }
            }
        }
        private DataTable ConvertToDataTable<T>(List<T> data)
        {

            DataTable table = new DataTable(typeof(T).Name);
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var displayName = prop.GetCustomAttributes(typeof(DisplayAttribute), false)
                                      .FirstOrDefault() is DisplayAttribute display
                    ? display.Name
                    : prop.Name;

                table.Columns.Add(displayName, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in data)
            {
                var values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(values);
            }


            return table;
        }
        public string CreateAsync(string content, string filename)
        {
            EnsureDirectoryExists(filepath);
            using (var fileStream = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.None, 4096, useAsync: true))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.WriteLineAsync(content);

                    writer.FlushAsync();

                    return filepath;
                }
            }

        }
        private void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
