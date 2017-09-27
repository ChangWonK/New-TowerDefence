using Excel;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEditor;
using UnityEngine;


public static class ExcelToXML
{
    private const string excelTableRelativePath = "Table/";
    private const string xmlNodeName = "xml_node"; //Editor 폴더 내 스크립트에서 TableHelper쪽에 참조를 할 수 없음.
    private const string xmlSavePath = "Assets/Resources/Table/";

    [MenuItem("Take/Table/Excel To XML - Readable Data", false, 0)]
    private static void TableToData()
    {
        string fullPath = System.IO.Path.GetFullPath(excelTableRelativePath);

        Debug.Log(string.Format("XML StartConvert _ xlsx 폴더: {0}", fullPath));
        //파일 다 가져오기....
        string[] fileArr = Directory.GetFiles(fullPath);
        for (int i = 0; i < fileArr.Length; i++)
        {
            if (Path.GetExtension(fileArr[i]).CompareTo(".xlsx") != 0)
                continue;

            if (Path.GetFileName(fileArr[i]).Substring(0, 2).CompareTo("~$") == 0)
            {
                Debug.LogWarning("You need Cloes the File");
                Debug.LogWarning(fileArr[i]);
                continue;
            }


            ConvertExcelToXML(fileArr[i]);
        }
        Debug.Log(string.Format("xlsx -> xml Convert Complete. Path:{0}", xmlSavePath));            
    }


    //------------------------------------------------------------------------------------------------
    private static void ConvertExcelToXML(string excelPath)
    {

        string xmlName = "";
        string xmlPath = "";

        FileStream fileStream = new FileStream(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        ExcelOpenXmlReader reader = (ExcelOpenXmlReader)Excel.ExcelReaderFactory.CreateOpenXmlReader(fileStream);

        for (int sheet = 0; sheet < reader.ResultsCount; ++sheet)
        {
            if (reader.Name.StartsWith("_"))
            {
                continue;
            }

            xmlName = reader.Name;
            xmlPath = xmlSavePath + xmlName + ".xml";

            XmlTextWriter writer = new XmlTextWriter(xmlPath, Encoding.UTF8);
            writer.Formatting = System.Xml.Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement(xmlName + "_root");

            Dictionary<int, string> excelHeadDic = new Dictionary<int, string>();

            for (int row = 0; reader.Read(); ++row)
            {
                if (0 == row)
                {
                    for (int col = 0; col < reader.FieldCount; ++col)
                    {
                        string name = reader.GetString(col);
                        if (null == name || name.StartsWith("_"))
                        {
                            continue;
                        }
                        excelHeadDic.Add(col, name);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(reader.GetString(0)) == true)
                        break;

                    writer.WriteStartElement(xmlNodeName);
                    for (int col = 0; col < reader.FieldCount; ++col)
                    {
                        if (false == excelHeadDic.ContainsKey(col))
                        {
                            continue;
                        }
                        writer.WriteElementString(excelHeadDic[col], reader.GetString(col));
                    }
                    writer.WriteEndElement();
                }
            }

            writer.WriteEndDocument();
            writer.Close();
            AssetDatabase.Refresh();
            reader.NextResult();
        }

        reader.Close();
    }
}


