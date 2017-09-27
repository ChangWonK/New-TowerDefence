using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Excel;




public class ExcelReader<Sheet> where Sheet: ExcelSheet
{
public List<Sheet> sheets { get; private set; }

public ExcelReader()
{
	sheets = new List<Sheet>();
}

public void Load(string fileName)
{
	FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
	ExcelOpenXmlReader reader = (ExcelOpenXmlReader)Excel.ExcelReaderFactory.CreateOpenXmlReader (fileStream);

	for( int sheet_iter = 0 ; sheet_iter < reader.ResultsCount ; ++sheet_iter )
	{
        Sheet sheet = (Sheet)Activator.CreateInstance(typeof(Sheet), fileName, reader.Name);

		for ( int row = 0 ; reader.Read() ; ++row )
		{
            if (string.IsNullOrEmpty(reader.GetString(0)))
                continue;

			for( int col = 0 ; col < reader.FieldCount ; ++col )
			{
				string value = reader.GetString(col);
				sheet.SetCell(row, col, value);
			}
		}
		sheets.Add(sheet);
		reader.NextResult ();
	}
			
	reader.Close ();
}
}

public class ExcelSheet
{
public struct Position
{
	public int row;
	public int col;
			
	public Position( int _row, int _col )
	{
		row = _row;
		col = _col;
	}
}

public string fileName { get; private set; }
public string sheetName { get; private set; }
public int lastRowIndex { get; private set; }
public int lastColIndex { get; private set; }

public ExcelSheet(string _fileName, string _sheetName)
{
    fileName = _fileName.Substring(_fileName.LastIndexOf("\\")+1);
    sheetName = _sheetName;
	lastRowIndex = 0;
	lastColIndex = 0;
}

Dictionary<Position, string> cells = new Dictionary<Position,string>();
public string this[int row, int col]
{
	get
	{
        Position posit = new Position(row, col);
		string cellData = null;
		cells.TryGetValue(posit, out cellData);
				
		return cellData;
	}
	set
	{
        Position posit = new Position(row, col);
		string cellData = "";
		if( cells.TryGetValue(posit, out cellData) )
			cellData = value;
		else
		{
            if (lastRowIndex < row)
                lastRowIndex = row;
            if (lastColIndex < col)
				lastColIndex = col;
			cells.Add(posit, value);
		}
	}
}

virtual public void SetCell(int row, int col, string value)
{
	this [row, col] = value;
}
}

public class ExcelConvertHelper
{
//		static public void ConvertCSV(ExcelSheet sheet, string path, string separator = ",")
static public void ConvertCSV(ExcelSheet sheet, string path, string separator )
{
    StringBuilder stringBuilder = new StringBuilder();

    for (int row = 0; row <= sheet.lastRowIndex; ++row)
    {
        for (int col = 0; col <= sheet.lastColIndex; ++col)
        {
            stringBuilder.Append(sheet[row, col]);
            //Debug.Log("col : " + col + ", row : " + row + ", value : " + sheet[col, row]);
            if (col != sheet.lastColIndex)
                stringBuilder.Append(separator);
        }
        if (row != sheet.lastRowIndex)
            stringBuilder.AppendLine();
    }

    File.WriteAllText(path, stringBuilder.ToString());
}
}

