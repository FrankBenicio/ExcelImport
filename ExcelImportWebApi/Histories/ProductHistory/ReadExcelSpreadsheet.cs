using Domain.Models;
using Infra.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Histories.ProductHistory
{
    public class ReadExcelSpreadsheet
    {
        private readonly ExcelContext _context;
        private IHostingEnvironment _hostingEnvironment;
        public ReadExcelSpreadsheet(ExcelContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task Run(IFormFile excelSpreadsheet)
        {

            var products = new List<Product>();

            var path = UrlPath();

            var fileDoesNotExists = excelSpreadsheet.Length == 0;

            if (fileDoesNotExists)
                throw new ArgumentException("The file does not exist.");

            string sFileExtension = Path.GetExtension(excelSpreadsheet.FileName).ToLower();

            ISheet sheet;

            string fullPath = Path.Combine(path, excelSpreadsheet.FileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                excelSpreadsheet.CopyTo(stream);

                stream.Position = 0;

                sheet = GetSheetVersion(sFileExtension, stream);

                IRow headerRow = sheet.GetRow(0);

                int cellCount = headerRow.LastCellNum;

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                {

                    IRow row = sheet.GetRow(i);

                    if (row is null) continue;

                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                    string nameProduct = "";
                    DateTime deliveryDate = new DateTime();
                    int quantity = 0;
                    decimal priceUnit = 0;

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {


                            ICell cell = headerRow.GetCell(j);

                            if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;

                            if (cell.ToString().Contains("Nome do Produto"))
                            {
                                nameProduct = row.GetCell(j).ToString();
                            }
                            else if (cell.ToString().Contains("Data Entrega"))
                            {
                                deliveryDate = Convert.ToDateTime(row.GetCell(j).ToString());
                            }
                            else if (cell.ToString().Contains("Quantidade"))
                            {
                                quantity = Convert.ToInt32(row.GetCell(j).ToString());
                            }
                            else if (cell.ToString().Contains("Valor Unitário"))
                            {
                                priceUnit = Convert.ToDecimal(row.GetCell(j).ToString());
                            }
                        }
                    }


                    var product = new Product
                        (
                        name: nameProduct,
                        quantity: quantity,
                        priceUnit: priceUnit,
                        deliveryDate: deliveryDate
                        );

                    products.Add(product);

                }


                await _context.AddAsync(products);
                await _context.SaveChangesAsync();
            }
        }

        private static ISheet GetSheetVersion(string sFileExtension, FileStream stream)
        {
            ISheet sheet;
            if (sFileExtension == ".xls")
            {
                HSSFWorkbook hssfwb = new HSSFWorkbook(stream);
                sheet = hssfwb.GetSheetAt(0);
            }
            else
            {
                XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
            }

            return sheet;
        }

        private string UrlPath()
        {
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            return newPath;
        }
    }
}
