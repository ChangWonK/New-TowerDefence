using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections.Generic;
//using SimpleJSON;


public class TestSheet : ExcelSheet
{
	enum VALUE_TYPE
	{
		NONE = 0,
		INT,
		FLOAT,
		STRING,
	}

	Dictionary<int, VALUE_TYPE> typeList = new Dictionary<int, VALUE_TYPE>();
	Dictionary<int, string> idList = new Dictionary<int, string>();

	public bool isParsingError { get; private set; }

    public TestSheet(string fileName, string sheetName)
        : base(fileName, sheetName)
	{
	}

	override public void SetCell(int row, int col, string value)
	{
		if( row == 0 )
		{
			idList.Add (col, value);
		}
		else if( row == 1 )
		{
			VALUE_TYPE type = VALUE_TYPE.NONE;
			switch( value )
			{
			case "int":
				type = VALUE_TYPE.INT;
				break;
			case "float":
				type = VALUE_TYPE.FLOAT;
				break;
			case "string":
				type = VALUE_TYPE.STRING;
				break;
			default:
                if (string.IsNullOrEmpty(value) == false)
                    Debug.LogWarning(string.Format("'{0}' is not valid type.", value));
				type = VALUE_TYPE.NONE;
				break;
			}
			if( type != VALUE_TYPE.NONE )
				base.SetCell(0, col-GetFrontNoneCount(col), idList[col]);

			typeList.Add(col, type);
		}
		else if( row > 2 )
		{
			AddCellWithVerify(row-1, col, value);
		}
	}

	void AddCellWithVerify(int row, int col, string value)
	{
		switch( typeList[col] )
		{
		case VALUE_TYPE.NONE:
			return;
		case VALUE_TYPE.INT:
			int intValue = 0;
			if( !int.TryParse(value, out intValue) )
			{                        
				EditorUtility.DisplayDialog("Parsing Error",
                                            string.Format("'{0}' is not int.\n\n{1}_{2}:{3}{4}", value, fileName, sheetName, ConvertColToString(col), row + 2), "OK");
				isParsingError = true;
			}
			break;
		case VALUE_TYPE.FLOAT:
			float floatValue = 0;
			if( !float.TryParse(value, out floatValue) )
			{
				EditorUtility.DisplayDialog("Parsing Error",
                                            string.Format("'{0}' is not float.\n\n{1}_{2}:{3}{4}", value, fileName, sheetName, ConvertColToString(col), row + 2), "OK");
				isParsingError = true;
			}
			break;
		case VALUE_TYPE.STRING:
			break;
		}
		base.SetCell(row-1, col-GetFrontNoneCount(col), value);
	}

	string ConvertColToString(int col)
	{
        StringBuilder stringBuilder = new StringBuilder();

        do 
        {
            if (stringBuilder.Length <= 0)
                stringBuilder.Insert(0, (char)(65 + col % 26));
            else
                stringBuilder.Insert(0, (char)(65 + col % 26 - 1));

            col /= 26;
        } while (col > 0);

        return stringBuilder.ToString();
	}

	int GetFrontNoneCount(int col)
	{
		int frontNoneCount = 0;
		for( int i = 0 ; i < col ; ++i )
		{
			if( typeList[i] == VALUE_TYPE.NONE )
				++frontNoneCount;
		}
		return frontNoneCount;
	}
}

[InitializeOnLoad]
public class TableMaker
{
	static ExcelReader<TestSheet> reader;
		
	static TableMaker()
	{
//			EditorApplication.playmodeStateChanged += () =>
//            {
//                if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
//                {
//                    if (!Application.isPlaying)
//                    {
//                        foreach (var loader in GameObject.FindObjectsOfType<ResourceLoader>())
//                        {
//                            GameObject.DestroyImmediate(loader.gameObject);
//                        }
//                    }
//
//                    ConvertXLSToCSV();
//                }
//			};
	}

    public static void ConvertXLSToCSV()
    {
//            FileInfo info = new FileInfo(Application.dataPath + "/Settings/ExcelSetting.json");
//            if (info.Exists == false)
//                return;
//
//            JSONNode directories = JSON.Parse(File.ReadAllText(Application.dataPath + "/Settings/ExcelSetting.json"));
//            JSONArray excelFolderList = directories["Excel Folder List"].AsArray;
//            string outputFolder = directories["CSV Output Folder"];
//
//            if (Directory.Exists(Application.dataPath + "/" + outputFolder) == false)
//            {
//                Debug.LogError("Excel Converter Output Folder(" + outputFolder + ") is Not Exist.");
//                return;
//            }
//
//            foreach (string str in excelFolderList.Childs)
//            {
//                ConvertCsvFromDirectory(str, outputFolder);
//            }
//            AssetDatabase.Refresh();
    }

    static void ConvertCsvFromDirectory(string dir, string outputFolder)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + "/" + dir);
        if (dirInfo.Exists == false)
        {
            Debug.LogWarning("Table Maker Directory("+dir+") Not Found.");
            return;
        }

        DirectoryInfo outputDirInfo = new DirectoryInfo(Application.dataPath + "/" + outputFolder);
        if (outputDirInfo.Exists == false)
        {
            Debug.LogWarning("Table Maker Directory("+outputFolder+") Not Found.");
            return;
        }

        FileInfo[] fileInfos = dirInfo.GetFiles("*.xlsx");
        foreach (FileInfo fileInfo in fileInfos)
        {
            string name = fileInfo.Name;
            string extension = fileInfo.Extension;
            string nameWithoutExt = name.Substring(0, name.Length - extension.Length);

            var outputFiles = outputDirInfo.GetFiles(nameWithoutExt + "_*");
            if (outputFiles.Length > 0 && fileInfo.LastWriteTime < outputFiles[0].LastWriteTime)
                continue;
                    
            if (fileInfo.Name.StartsWith("~"))
                continue;

            Debug.Log(string.Format("Parsing {0} File", name));

            reader = new ExcelReader<TestSheet>();
            reader.Load(fileInfo.FullName);

            foreach (TestSheet sheet in reader.sheets)
            {
                if (sheet.isParsingError)
                    continue;

                string csvName = Application.dataPath + "/" + outputFolder + "/" + nameWithoutExt + "_" + sheet.sheetName + ".txt";
                ExcelConvertHelper.ConvertCSV(sheet, csvName, "" );
            }
        }
    }
}

