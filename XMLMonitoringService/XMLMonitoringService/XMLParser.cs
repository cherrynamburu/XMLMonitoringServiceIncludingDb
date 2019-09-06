using System;
using System.Xml;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace XMLMonitoringService
{
    class XMLParser
    {
        private FileInfo[] GetFileNames()
        { 
            if (FileHandler.ValidateInputPath())
            {
                DirectoryInfo directory = new DirectoryInfo(Config.InputSource);
                return directory.GetFiles("*.xml", SearchOption.AllDirectories);
            }
            return null;
        }
        private void SetHeaders()
        {
            FileHandler.Log(DateTime.Now.ToString() + "\n");

            var dataHeaders = String.Format("{0}{1}{2}{3}{4}\n", "File Name".PadRight(85), "Job Title".PadRight(30),
               "Employer".PadRight(30), "Period".PadRight(30), "Description");
            FileHandler.Log(dataHeaders);

            var line = "------------------------------------------------------------------------------------------------------------------\n";
            FileHandler.Log(line);
        }
        internal void ParseXMLFileToLog(XmlDocument xmlDocument)
        {
            foreach (var tag in Config.RequiredTags)
            {
                XmlNodeList tagData = xmlDocument.GetElementsByTagName(tag);
                for (int i = 0; i < tagData.Count; i++)
                {
                    FileHandler.Log(String.Format("{0}", tagData[i].InnerText.PadRight(30)));
                }
            }
            FileHandler.Log("\n\n");
        }
        public void XmlDataParserToLog()
        {
            var fileFlag = true;

            var xmlFiles = GetFileNames();

            if (xmlFiles != null)
            {
                SetHeaders();
                foreach (var xmlfile in xmlFiles)
                {
                    FileHandler.Log(String.Format("{0}", xmlfile.FullName.PadRight(85)));
                    var xmlDocument = new XmlDocument();

                    if (FileHandler.CanAccessFile(xmlfile.FullName))
                    {
                        xmlDocument.Load(xmlfile.FullName);
                        ParseXMLFileToLog(xmlDocument);
                        Console.WriteLine("Parsing " + xmlfile.FullName);
                    }
                    else
                    {
                        fileFlag = false;
                    }
                }
                if (fileFlag)
                {
                    FileHandler.Write();
                }
            }
        }
        internal void ParseXMLFile(XmlDocument xmlDocument)

        {
            Dictionary<string, string> jobElements = new Dictionary<string, string>();

            XmlDocument doc = new XmlDocument();
            XmlDocument periodDoc = new XmlDocument();
            foreach (XmlElement job in xmlDocument.GetElementsByTagName("job"))
            {
                doc.LoadXml(job.OuterXml);
                foreach (var tag in Config.RequiredTags)
                {
                    foreach (XmlElement tagData in doc.GetElementsByTagName(tag))
                    {
                        if (tagData.Name == "period")
                        {
                            periodDoc.LoadXml(tagData.OuterXml);
                            jobElements.Add("from", periodDoc.GetElementsByTagName("from")[0].InnerText);
                            jobElements.Add("to", periodDoc.GetElementsByTagName("to")[0].InnerText);
                            //Console.WriteLine(periodDoc.GetElementsByTagName("from")[0].InnerText);
                            //Console.WriteLine(periodDoc.GetElementsByTagName("to")[0].InnerText);
                        }
                        else
                        {
                            jobElements.Add(tagData.Name, tagData.InnerText);
                            //Console.WriteLine(tagData.Name);
                            //Console.WriteLine(tagData.InnerText);
                        }

                    }
                }
                Console.Write("Job : "+jobElements["jobid"]);
                var empHistoryId = SqlServer.CheckJobExists(Convert.ToInt32(jobElements["jobid"]));
                if ( empHistoryId!= 0)
                {
                    SqlServer.UpdateJob(jobElements, empHistoryId);
                }
                else
                {
                    SqlServer.InsertJob(jobElements);
                }
                jobElements.Clear();

            }
        }
        internal void XmlDataParser()
        {
            var xmlFiles = GetFileNames();

            if (xmlFiles != null)
            {
                foreach (var xmlFile in xmlFiles)
                {
                    var xmlDocument = new XmlDocument();
                    if (FileHandler.CanAccessFile(xmlFile.FullName))
                    {
                        xmlDocument.Load(xmlFile.FullName);
                        Console.WriteLine("Parsing " + xmlFile.FullName);
                        ParseXMLFile(xmlDocument);

                    }
                }

            }

            //if (xmlFiles != null)
            //{
            //    var xmlDocument = new XmlDocument();
            //    if (FileHandler.CanAccessFile(xmlFiles[0].FullName))
            //    {
            //        xmlDocument.Load(xmlFiles[0].FullName);
            //        Console.WriteLine("Parsing " + xmlFiles[0].FullName);
            //        ParseXMLFile(xmlDocument);

            //    }
            //}
        }

        //internal void ParseJob(XmlNode xmlnode)
        //{
        //    if (xmlnode.ChildNodes.OfType<XmlElement>().Any())
        //    {
        //        foreach (XmlNode node in xmlnode.ChildNodes)
        //        {
        //            ParseJob(node);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine(xmlnode.InnerText);
        //    }
        //}
        //private void CommentedCode()
        //{
        //    //XmlNode root = xmlDocument.FirstChild;

        //    ////Console.WriteLine("Root Name -------------"+root.Name);

        //    //if (Config.RequiredTags.Contains(root.Name))
        //    //{
        //    //    //Display the contents of the child nodes.
        //    //    if (root.ChildNodes.OfType<XmlElement>().Any())
        //    //    {
        //    //        //Console.WriteLine("{0} has {1} Childs",root.Name, root.ChildNodes.Count);
        //    //        for (int i = 0; i < root.ChildNodes.Count; i++)
        //    //        {

        //    //            if (root.ChildNodes[i].HasChildNodes)
        //    //            {
        //    //                //Console.WriteLine("Child Node "+root.ChildNodes[i].Name);
        //    //                var doc = new XmlDocument();
        //    //                doc.LoadXml(root.ChildNodes[i].OuterXml);
        //    //                ParseXMLFile(doc);

        //    //            }
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        Console.WriteLine("Inner Text " + root.InnerText);
        //    //    }
        //    //}    
        //}
    }
}
