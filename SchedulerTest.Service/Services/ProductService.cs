using ClosedXML.Excel;
using Newtonsoft.Json;
using SchedulerTest.Domain.Domain;
using SchedulerTest.Domain.IRepo;
using SchedulerTest.Domain.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientService _httpclient;
        private readonly IProductRepo _repo;
        private string filepath;
        public ProductService(IProductRepo repo, IHttpClientService httpclient)
        {
            _repo = repo;
            _httpclient = httpclient;
            filepath = "E:\\Temp";
        }

        public async Task<string> CreateFileWithTxnData(string fileName)
        {
            string filePathName = string.Empty;
            var products = await GetProducts();
            var adaptor_products = await CallAPI(); //_repo.UpdateAdaptorProduct(products);
            var response = await ComapreAndUpdateAdaptorProducts(adaptor_products);
            if (response.Count()!= 0)
                filePathName = WriteDataToFileAsync(response, fileName);
            return filePathName;
        }

        //Private Method 
        public async Task<List<AdaptorProduct>> GetAdaporProducts()
        {
            var adaptorproducts = await _repo.GetAdaptorProducts();
            return adaptorproducts;
        }
        public async Task<List<Product>> GetProducts()
        {
            var products = await _repo.GetProducts();
            return products;
        }
        public async Task<List<ExcelReportModel>> ComapreAndUpdateAdaptorProducts(ScheduleProductResponse adaptorproducts)
        {
            var responselist = new List<ExcelReportModel>();
            var adaptorProducts = await GetAdaporProducts();
            foreach (var item in adaptorproducts.Products)
            {
                var existproducts = adaptorProducts.FirstOrDefault(x => x.Code == item.Code);
                if (existproducts == null)
                {
                    var adaptorProduct = new AdaptorProduct()
                    {
                        Code = item.Code,
                        Name = item.Name,
                        Amount = item.Amount,
                        BillerCode = item.BillerCode,
                        AdaptorCode = item.Code,
                        ProductCode = item.Code
                    };
                    if (_repo.AddAdaptorProduct(adaptorProduct))
                    {
                        var product = new Product()
                        {
                            Code = item.Code,
                            NameEng = item.Name,
                            NameMmr = item.Name,
                            BillerCode = item.BillerCode,
                            DescriptionEng = item.Name,
                            DescriptionMmr = item.Name,
                            Amount = item.Amount??0                               
                        };
                        _repo.AddProduct(product);
                    };
                }
                else if(item.Code == existproducts.Code && item.Amount != existproducts.Amount)
                {
                    var response = new ExcelReportModel();
                    response.ProductName = existproducts.Name;
                    response.ProductCode = existproducts.Code;
                    response.OldAmount = existproducts.Amount??0;
                    response.NewAmount = item.Amount??0;
                    response.UpdateAt = DateTime.Now.ToString("d MMM yyy hh:mm:ss");
                    responselist.Add(response);
                    existproducts.Amount = item.Amount;
                    _repo.Update(existproducts);
                }
            }
            return responselist;
        }
        public async Task<ScheduleProductResponse> CallAPI()
        {
            var endpoint = "https://localhost:7126/api/ProductSchedule";
            var response = await _httpclient.SendAsync<ScheduleProductResponse>(endpoint, "100", HttpMethod.Get);
            return response;
        }
        public string WriteDataToFileAsync(List<ExcelReportModel> data, string fileName)
        {
            EnsureDirectoryExists(filepath);
            var filePathName = Path.Combine(filepath, $"{fileName}_{DateTime.Now:yyyyMMddhhmm}.xlsx");
            GenerateExcel(filePathName, data);
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
