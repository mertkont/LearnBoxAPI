using Business.Contracts;
using DataAccess.DTOs;
using DataAccess.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace API.Controllers;

public class ExportController : Controller
{
    private ITodoService _todoService;
    private IBookService _bookService;
    private ICategoryService _categoryService;
    
    public ExportController(ITodoService todoService, IBookService bookService, ICategoryService categoryService)
    {
        _todoService = todoService;
        _bookService = bookService;
        _categoryService = categoryService;
    }
    
    [HttpGet("ExportTodoPDF")]
    public IActionResult ExportTodoPdf()
    {
        List<TodoDetailsDto> todos = _todoService.GetAllTodos();
        MemoryStream stream = new MemoryStream();
        Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 50f, 50f);
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        pdfDoc.Open();
        int rowsPerPage = 20;
        int rowCount = 0;

        foreach (TodoDetailsDto todo in todos)
        {
            pdfDoc.Add(new Paragraph(todo.TodoId + "   " + todo.TodoTopic + "   " + todo.TodoText + "   " + todo.CategoryName + "   " + todo.CreatedDate + "   " + todo.DueDate + "   " + todo.Priority + "   " + todo.IsCompleted));
            rowCount++;
        
            if (rowCount >= rowsPerPage)
            {
                pdfDoc.NewPage();
                rowCount = 0;
            }
        }

        pdfDoc.Close();
        stream.Close();
        writer.Close();
        return File(stream.ToArray(), "application/pdf", "Todos.pdf");
    }
    
    [HttpGet("ExportBookPDF")]
    public IActionResult ExportBookPdf()
    {
        List<BookDetailsDto> books = _bookService.GetAllBooks();
        MemoryStream stream = new MemoryStream();
        Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 50f, 50f);
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        pdfDoc.Open();
        int rowsPerPage = 20;
        int rowCount = 0;

        foreach (BookDetailsDto book in books)
        {
            pdfDoc.Add(new Paragraph(book.BookId + "   " + book.BookName + "   " + book.BuyDate + "   " + book.FinishDate + "   " + book.Priority + "   " + book.CategoryName + "   " + book.IsFinished));
            rowCount++;
        
            if (rowCount >= rowsPerPage)
            {
                pdfDoc.NewPage();
                rowCount = 0;
            }
        }

        pdfDoc.Close();
        stream.Close();
        writer.Close();
        return File(stream.ToArray(), "application/pdf", "Books.pdf");
    }

    
    [HttpGet("ExportCategoryPDF")]
    public IActionResult ExportCategoryPdf()
    {
        List<Category> categories = _categoryService.GetAllCategories();
        MemoryStream stream = new MemoryStream();
        Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 50f, 50f);
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        pdfDoc.Open();
        int rowsPerPage = 20;
        int rowCount = 0;

        foreach (Category category in categories)
        {
            pdfDoc.Add(new Paragraph(category.CategoryId + "   " + category.CategoryName));
            rowCount++;
        
            if (rowCount >= rowsPerPage)
            {
                pdfDoc.NewPage();
                rowCount = 0;
            }
        }

        pdfDoc.Close();
        stream.Close();
        writer.Close();
        return File(stream.ToArray(), "application/pdf", "Categories.pdf");
    }

    [HttpGet("ExportTodoExcel")]
    public IActionResult ExportTodoExcel()
    {
        List<TodoDetailsDto> todos = _todoService.GetAllTodos();
        IWorkbook workbook = new XSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("Todos");
        IRow headerRow = sheet.CreateRow(0);
        headerRow.CreateCell(0).SetCellValue("TodoId");
        headerRow.CreateCell(1).SetCellValue("TodoTopic");
        headerRow.CreateCell(2).SetCellValue("TodoText");
        headerRow.CreateCell(3).SetCellValue("CategoryName");
        headerRow.CreateCell(4).SetCellValue("CreatedDate");
        headerRow.CreateCell(5).SetCellValue("DueDate");
        headerRow.CreateCell(6).SetCellValue("Priority");
        headerRow.CreateCell(7).SetCellValue("IsCompleted");
        
        for (int i = 0; i < todos.Count; i++)
        {
            IRow dataRow = sheet.CreateRow(i + 1);
            dataRow.CreateCell(0).SetCellValue(todos[i].TodoId);
            dataRow.CreateCell(1).SetCellValue(todos[i].TodoTopic);
            dataRow.CreateCell(2).SetCellValue(todos[i].TodoText);
            dataRow.CreateCell(3).SetCellValue(todos[i].CategoryName);
            dataRow.CreateCell(4).SetCellValue(todos[i].CreatedDate.ToString());
            dataRow.CreateCell(5).SetCellValue(todos[i].DueDate.ToString());
            dataRow.CreateCell(6).SetCellValue(todos[i].PriorityText);
            dataRow.CreateCell(7).SetCellValue(todos[i].IsCompleted);
        }

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "todos.xlsx");
        }
    }

    [HttpGet("ExportBookExcel")]
    public IActionResult ExportBookExcel()
    {
        List<BookDetailsDto> books = _bookService.GetAllBooks();
        IWorkbook workbook = new XSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("Books");
        IRow headerRow = sheet.CreateRow(0);
        headerRow.CreateCell(0).SetCellValue("BookId");
        headerRow.CreateCell(1).SetCellValue("BookName");
        headerRow.CreateCell(2).SetCellValue("BuyDate");
        headerRow.CreateCell(3).SetCellValue("FinishDate");
        headerRow.CreateCell(4).SetCellValue("Priority");
        headerRow.CreateCell(5).SetCellValue("CategoryName");
        headerRow.CreateCell(6).SetCellValue("IsFinished");
        
        for (int i = 0; i < books.Count; i++)
        {
            IRow dataRow = sheet.CreateRow(i + 1);
            dataRow.CreateCell(0).SetCellValue(books[i].BookId);
            dataRow.CreateCell(1).SetCellValue(books[i].BookName);
            dataRow.CreateCell(2).SetCellValue(books[i].BuyDate.ToString());
            dataRow.CreateCell(3).SetCellValue(books[i].FinishDate.ToString());
            dataRow.CreateCell(4).SetCellValue(books[i].PriorityText);
            dataRow.CreateCell(5).SetCellValue(books[i].CategoryName);
            dataRow.CreateCell(6).SetCellValue(books[i].IsFinished);
        }

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "books.xlsx");
        }
    }
    
    [HttpGet("ExportCategoryExcel")]
    public IActionResult ExportCategoryExcel()
    {
        List<Category> categories = _categoryService.GetAllCategories();
        IWorkbook workbook = new XSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("Categories");
        IRow headerRow = sheet.CreateRow(0);
        headerRow.CreateCell(0).SetCellValue("CategoryId");
        headerRow.CreateCell(1).SetCellValue("CategoryName");
        
        for (int i = 0; i < categories.Count; i++)
        {
            IRow dataRow = sheet.CreateRow(i + 1);
            dataRow.CreateCell(0).SetCellValue(categories[i].CategoryId);
            dataRow.CreateCell(1).SetCellValue(categories[i].CategoryName);
        }

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "categories.xlsx");
        }
    }
}