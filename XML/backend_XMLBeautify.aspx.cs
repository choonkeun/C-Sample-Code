using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

public partial class backend_XMLBeautify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string outText = String.Empty;
        string inText = String.Empty;
        
        if (Request.Files.Count > 0)
        {
            HttpPostedFile postedFile = Request.Files[0];
            inText = new StreamReader(postedFile.InputStream).ReadToEnd();
            outText = "fileName: " + postedFile.FileName + Environment.NewLine; 
        }
        else
        {
            inText = new StreamReader(Request.InputStream).ReadToEnd();
        }
        
        if (inText.Trim().Length > 0) outText += Do_XMLParse(inText);
        Response.Write(outText);
    }

    string xmloutput = String.Empty;

    private string Do_XMLParse(string inText)
    {
        string xmlinput = String.Empty;
        xmlinput = inText;

        System.Xml.XmlDocument oDoc = new System.Xml.XmlDocument();
        try
        {
            oDoc.LoadXml(xmlinput);
            //string jsonText = JsonConvert.SerializeXmlNode(oDoc);
            xmloutput = "";
            display_node_xml(oDoc.ChildNodes, -4);
        }
        catch (Exception ex)
        {
            xmloutput = ex.Message;
        }
        return xmloutput;
    }

    bool isText = false;
    private bool display_node_xml(System.Xml.XmlNodeList Nodes, int Indent)
    {
        Indent += 4;    //Insert space left to make indent

        foreach (System.Xml.XmlNode xNode in Nodes)
        {
            if (xNode.NodeType == XmlNodeType.Element)
            {
                xmloutput += System.Environment.NewLine;
                xmloutput += new String(' ', Indent);
                xmloutput += "<" + xNode.Name;
                if (xNode.Attributes.Count > 0)
                {
                    for (int i = 0; i < xNode.Attributes.Count; i++)
                    {
                        xmloutput += " " + xNode.Attributes[i].Name + "=\"" + xNode.Attributes[i].Value + "\"";
                    }
                    xmloutput += xNode.HasChildNodes ? ">" : "/>";
                }
                else
                    xmloutput += ">";
            }
            else if (xNode.NodeType == XmlNodeType.Text)
            {
                xmloutput += string.IsNullOrEmpty(xNode.Value) ? " " : xNode.Value;
                return true;
            }
            else if (xNode.NodeType == XmlNodeType.XmlDeclaration)
            {
                xmloutput += xNode.OuterXml;
            }

            if (xNode.HasChildNodes)
            {
                isText = display_node_xml(xNode.ChildNodes, Indent);

                if (!isText)
                {
                    xmloutput += System.Environment.NewLine;
                    xmloutput += new String(' ', Indent);
                }
                xmloutput += "</" + xNode.Name + ">";
            }
        }
        return false;
    }
}



