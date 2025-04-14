好的，这里提供一个封装了 NPOI 工作表顺序设置功能的 C# 类。这个类会处理工作簿的打开、创建、添加工作表、设置顺序以及保存。它还实现了 `IDisposable` 接口，以确保资源被正确释放。

```csharp
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel; // .xlsx
using NPOI.HSSF.UserModel; // .xls
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages Excel workbook sheets, focusing on creation, ordering, and saving.
/// Handles both .xls and .xlsx formats.
/// </summary>
public class ExcelSheetManager : IDisposable
{
    private readonly string _filePath;
    private IWorkbook _workbook;
    private bool _isDisposed = false;
    private bool _isModified = false; // Track if changes requiring save occurred

    /// <summary>
    /// Gets the path to the Excel file being managed.
    /// </summary>
    public string FilePath => _filePath;

    /// <summary>
    /// Gets a read-only list of the current sheet names in their current order.
    /// </summary>
    public IReadOnlyList<string> SheetNames
    {
        get
        {
            EnsureNotDisposed();
            var names = new List<string>();
            if (_workbook != null)
            {
                for (int i = 0; i < _workbook.NumberOfSheets; i++)
                {
                    names.Add(_workbook.GetSheetName(i));
                }
            }
            return names.AsReadOnly();
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExcelSheetManager"/> class.
    /// It will open an existing Excel file or create a new one if it doesn't exist.
    /// </summary>
    /// <param name="filePath">The full path to the Excel file (.xls or .xlsx).</param>
    /// <exception cref="ArgumentNullException">Thrown if filePath is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the file exists but cannot be opened or is corrupted.</exception>
    /// <exception cref="IOException">Thrown if there is an issue accessing the file.</exception>
    public ExcelSheetManager(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        _filePath = filePath;
        InitializeWorkbook();
    }

    private void InitializeWorkbook()
    {
        string extension = Path.GetExtension(_filePath).ToLowerInvariant();
        bool isXlsx = extension == ".xlsx";

        if (File.Exists(_filePath))
        {
            try
            {
                // Open with FileAccess.ReadWrite to allow saving later
                // Reading into a MemoryStream first avoids locking the file persistently
                byte[] fileBytes = File.ReadAllBytes(_filePath);
                using (MemoryStream ms = new MemoryStream(fileBytes))
                {
                     // Important: Let NPOI create the appropriate workbook type from the stream
                    _workbook = WorkbookFactory.Create(ms);

                    // // Alternative: Explicit type creation based on extension (less flexible for mixed types)
                    // if (isXlsx)
                    //     _workbook = new XSSFWorkbook(ms);
                    // else if (extension == ".xls")
                    //     _workbook = new HSSFWorkbook(ms);
                    // else
                    //     throw new InvalidOperationException($"Unsupported file extension: {extension}. Only .xls and .xlsx are supported.");
                }
            }
            catch (Exception ex) // Catch more specific exceptions like OldExcelFormatException if needed
            {
                 // Clean up partially created workbook if open failed
                _workbook?.Dispose();
                throw new InvalidOperationException($"Failed to open existing workbook '{_filePath}'. It might be corrupted or in use. See inner exception.", ex);
            }
        }
        else
        {
            // Create a new workbook
            if (isXlsx)
                _workbook = new XSSFWorkbook();
            else if (extension == ".xls")
                _workbook = new HSSFWorkbook();
            else
                throw new InvalidOperationException($"Cannot create workbook with unsupported extension: {extension}. Use .xls or .xlsx.");

             _isModified = true; // New workbook needs saving
             Console.WriteLine($"File '{_filePath}' not found. Created a new workbook instance.");
             // Consider adding a default sheet if required by your logic
             // _workbook.CreateSheet("Sheet1");
        }
    }

    /// <summary>
    /// Adds a new sheet to the workbook.
    /// </summary>
    /// <param name="sheetName">The name for the new sheet.</param>
    /// <returns>The newly created ISheet object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if sheetName is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown if a sheet with the same name already exists.</exception>
    /// <exception cref="ObjectDisposedException">Thrown if the manager has been disposed.</exception>
    public ISheet AddSheet(string sheetName)
    {
        EnsureNotDisposed();
        if (string.IsNullOrWhiteSpace(sheetName))
        {
            throw new ArgumentNullException(nameof(sheetName));
        }
        if (_workbook.GetSheet(sheetName) != null)
        {
            throw new ArgumentException($"A sheet with the name '{sheetName}' already exists in the workbook.", nameof(sheetName));
        }

        ISheet newSheet = _workbook.CreateSheet(sheetName);
        _isModified = true;
        Console.WriteLine($"Added sheet: '{sheetName}'");
        return newSheet;
    }

    /// <summary>
    /// Sets the display order of a specific sheet within the workbook.
    /// </summary>
    /// <param name="sheetName">The name of the sheet to move.</param>
    /// <param name="newPosition">The new zero-based index for the sheet.</param>
    /// <exception cref="ArgumentNullException">Thrown if sheetName is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown if the sheet name does not exist in the workbook.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the newPosition is outside the valid range (0 to NumberOfSheets - 1).</exception>
    /// <exception cref="ObjectDisposedException">Thrown if the manager has been disposed.</exception>
    public void SetSheetOrder(string sheetName, int newPosition)
    {
        EnsureNotDisposed();
        if (string.IsNullOrWhiteSpace(sheetName))
        {
            throw new ArgumentNullException(nameof(sheetName));
        }
        if (newPosition < 0 || newPosition >= _workbook.NumberOfSheets)
        {
            throw new ArgumentOutOfRangeException(nameof(newPosition), $"New position must be between 0 and {_workbook.NumberOfSheets - 1}.");
        }

        // NPOI's SetSheetOrder throws ArgumentException if sheetName is not found
        try
        {
            // Check current position to avoid unnecessary modification flag
            int currentPosition = _workbook.GetSheetIndex(sheetName);
             if (currentPosition < 0) // Should be caught by NPOI, but good practice
             {
                 throw new ArgumentException($"Sheet with name '{sheetName}' not found.", nameof(sheetName));
             }

             if (currentPosition != newPosition)
             {
                _workbook.SetSheetOrder(sheetName, newPosition);
                _isModified = true;
                Console.WriteLine($"Moved sheet '{sheetName}' to position {newPosition}.");
             }
             else
             {
                Console.WriteLine($"Sheet '{sheetName}' is already at position {newPosition}. No change made.");
             }
        }
        catch (ArgumentException ex) // Catch NPOI's exception for not found sheet
        {
             throw new ArgumentException($"Failed to set order for sheet '{sheetName}'. It might not exist. See inner exception.", ex);
        }
    }

    /// <summary>
    /// Saves the changes made to the workbook back to the file specified in the constructor.
    /// This will overwrite the existing file.
    /// </summary>
    /// <exception cref="IOException">Thrown if there is an error writing to the file.</exception>
    /// <exception cref="ObjectDisposedException">Thrown if the manager has been disposed.</exception>
    public void Save()
    {
        EnsureNotDisposed();
        if (!_isModified)
        {
            Console.WriteLine("No modifications detected. Skipping save.");
            return; // Don't save if nothing changed
        }
        if (_workbook == null)
        {
             throw new InvalidOperationException("Workbook is not loaded or has been disposed.");
        }

        try
        {
            // Ensure the directory exists
            string directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
            {
                _workbook.Write(fs);
            }
            _isModified = false; // Reset modification flag after successful save
            Console.WriteLine($"Workbook successfully saved to '{_filePath}'.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error saving workbook to '{_filePath}': {ex.Message}");
            throw; // Re-throw the exception
        }
    }

    /// <summary>
    /// Releases the resources used by the ExcelSheetManager, including the NPOI workbook.
    /// If changes were made but not saved, they will be lost.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this); // Prevent finalizer from running
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                // Dispose managed resources
                if (_workbook != null)
                {
                    _workbook.Dispose(); // NPOI workbooks implement IDisposable
                    _workbook = null;
                     Console.WriteLine("ExcelSheetManager disposed.");
                }
            }

            // Dispose unmanaged resources (if any) - NPOI handles this internally mostly

            _isDisposed = true;
        }
    }

    private void EnsureNotDisposed()
    {
        if (_isDisposed)
        {
            throw new ObjectDisposedException(nameof(ExcelSheetManager));
        }
    }

    // Optional: Add a finalizer as a safety net, although proper `using` is preferred.
    // ~ExcelSheetManager()
    // {
    //     Dispose(false);
    // }
}
```

**How to Use:**

```csharp
using System;
using System.IO;
using System.Linq; // For LINQ operations like checking sheet existence

public class Program
{
    public static void Main(string[] args)
    {
        string filePath = "managed_sheets_example.xlsx";

        // Use 'using' block to ensure Dispose() is called automatically
        try
        {
            using (var sheetManager = new ExcelSheetManager(filePath))
            {
                Console.WriteLine("Initial sheets: " + string.Join(", ", sheetManager.SheetNames));

                // Add sheets if they don't exist
                if (!sheetManager.SheetNames.Contains("DataSheet"))
                {
                    sheetManager.AddSheet("DataSheet");
                }
                if (!sheetManager.SheetNames.Contains("SummarySheet"))
                {
                    sheetManager.AddSheet("SummarySheet");
                }
                 if (!sheetManager.SheetNames.Contains("ConfigSheet"))
                {
                    sheetManager.AddSheet("ConfigSheet");
                }


                Console.WriteLine("Sheets after adding: " + string.Join(", ", sheetManager.SheetNames));

                // Set the desired order
                // Example: Make SummarySheet first, then DataSheet, then ConfigSheet
                if (sheetManager.SheetNames.Contains("SummarySheet"))
                {
                    sheetManager.SetSheetOrder("SummarySheet", 0);
                }
                if (sheetManager.SheetNames.Contains("DataSheet"))
                {
                    // After moving Summary to 0, DataSheet might be at 1 or 2.
                    // Find its current index before deciding target position (if needed)
                    // Or simply set its desired final position:
                    sheetManager.SetSheetOrder("DataSheet", 1);
                }
                if (sheetManager.SheetNames.Contains("ConfigSheet"))
                {
                    sheetManager.SetSheetOrder("ConfigSheet", 2);
                }

                Console.WriteLine("Sheets after reordering: " + string.Join(", ", sheetManager.SheetNames));

                // Changes are saved automatically when Save() is called.
                // If you don't call Save(), changes are discarded when the 'using' block ends.
                sheetManager.Save();
            } // sheetManager.Dispose() is called here automatically

            Console.WriteLine("\nOperation completed. Check the file: " + filePath);

            // You can create another instance to verify the order
             using (var verifier = new ExcelSheetManager(filePath))
             {
                 Console.WriteLine("\nVerifying saved order: " + string.Join(", ", verifier.SheetNames));
             }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            // Log the full exception details if needed
            // Console.WriteLine(ex.ToString());
        }
    }
}
```

**Key Features of the Encapsulated Class:**

1.  **Constructor Handles File Logic**: Automatically opens an existing file or creates a new one based on the provided path.
2.  **File Format Detection**: Determines whether to use `HSSFWorkbook` (.xls) or `XSSFWorkbook` (.xlsx) based on the file extension (using `WorkbookFactory` makes this more robust).
3.  **`IDisposable` Implementation**: Ensures the `IWorkbook` object is properly disposed of using the `using` statement, releasing file handles and resources.
4.  **Clear Public Methods**: Provides simple methods like `AddSheet`, `SetSheetOrder`, and `Save`.
5.  **State Tracking (`_isModified`)**: Only saves the file if actual changes (adding sheets, reordering) have occurred.
6.  **Error Handling**: Includes basic checks and throws relevant exceptions (e.g., for duplicate sheet names, invalid indices, file access issues).
7.  **Read-Only Sheet Names**: Offers a convenient property (`SheetNames`) to see the current sheets and their order.
8.  **Abstraction**: Hides the specific NPOI implementation details from the code that uses the `ExcelSheetManager`.