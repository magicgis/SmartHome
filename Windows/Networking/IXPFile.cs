using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Networking {
    public class IXPFile {
        private XmlDocument _doc;

        private static string XML_HEADER = @"<?xml version=""1.0"" encoding=""UTF-8""?>";
        private static string XML_IXP = "IXP";
        private static string XML_IXP_TARGET = "Target";
        private static string XML_IXP_TARGET_NFUNCTION = "nfunction";
        private static string XML_IXP_HEADER = "Header";
        private static string XML_IXP_HEADER_INFO = "Info";
        private static string XML_IXP_HEADER_INFO_NAME = "name";
        private static string XML_IXP_HEADER_INFO_VALUE = "value";
        private static string XML_IXP_BODY = "Body";
        private static string XML_IXP_BODY_INFO = "Info";
        private static string XML_IXP_BODY_INFO_NAME = "name";

        private static string XML_BASE =
            XML_HEADER +
            "<" + XML_IXP + ">" +
                "<" + XML_IXP_TARGET + " " + XML_IXP_TARGET_NFUNCTION + @"=""""" + "/>" +
                "<" + XML_IXP_HEADER + ">" +
                "</" + XML_IXP_HEADER + ">" +
                "<" + XML_IXP_BODY + ">" +
                "</" + XML_IXP_BODY + ">" +
            "</" + XML_IXP + ">";


        /// <summary>
        /// Creates an new blank IXP File
        /// </summary>
        public IXPFile() {
            _doc = new XmlDocument();
            _doc.LoadXml(XML_BASE);
        }
        /// <summary>
        /// Parses a an existing IXP File
        /// </summary>
        /// <param name="source"></param>
        public IXPFile(string source) {
            _doc = new XmlDocument();
            _doc.LoadXml(source);
        }

        /// <summary>
        /// The files raw xml data
        /// </summary>
        public string XML
        {
            get
            {
                return _doc.OuterXml;
            }
        }

        /// <summary>
        /// The files target function
        /// </summary>
        public string NetworkFunction
        {
            get
            {
                return GetTargetNFunctionAttr().Value;
            }
            set
            {
                GetTargetNFunctionAttr().Value = value;
            }
        }
        /// <summary>
        /// A readonly collection of header information
        /// </summary>
        public ReadOnlyCollection<string> Headers
        {
            get
            {
                List<string> headers = new List<string>();

                foreach (XmlNode node in GetHeaderNode().ChildNodes) {
                    headers.Add(GetHeaderInfoNameAttr(node).Value);
                }

                return headers.AsReadOnly();
            }
        }
        /// <summary>
        /// A readonly collection of body information
        /// </summary>
        public ReadOnlyCollection<string> Infos
        {
            get
            {
                List<string> infos = new List<string>();

                foreach (XmlNode node in GetBodyNode().ChildNodes) {
                    infos.Add(GetBodyInfoNameAttr(node).Value);
                }

                return infos.AsReadOnly();
            }
        }

        /// <summary>
        /// Returns the value of a specific header
        /// </summary>
        /// <param name="header">The header</param>
        /// <returns>The value</returns>
        public string GetHeaderValue(string header) {
            XmlNode infoNode = GetHeaderInfoNode(header);
            return GetHeaderInfoValueAttr(infoNode).Value;
        }
        /// <summary>
        /// Returns the value of a specific info
        /// </summary>
        /// <param name="info">The info</param>
        /// <returns>The value</returns>
        public string GetInfoValue(string info) {
            XmlNode infoNode = GetBodyInfoNode(info);
            return infoNode?.InnerText;
        }

        /// <summary>
        /// Puts a value in the header
        /// </summary>
        /// <param name="header">The header name</param>
        /// <param name="value">The header value</param>
        public void PutHeader(string header, string value) {
            XmlNode infoNode = GetHeaderInfoNode(header);

            if (infoNode == null) {
                infoNode = _doc.CreateElement(XML_IXP_HEADER_INFO);
                XmlAttribute nameAttr = _doc.CreateAttribute(XML_IXP_HEADER_INFO_NAME);
                nameAttr.Value = header;
                XmlAttribute valueAttr = _doc.CreateAttribute(XML_IXP_HEADER_INFO_VALUE);
                valueAttr.Value = value;
                infoNode.Attributes.Append(nameAttr);
                infoNode.Attributes.Append(valueAttr);
                GetHeaderNode().AppendChild(infoNode);
            } else {
                GetHeaderInfoValueAttr(infoNode).Value = value;
            }
        }
        /// <summary>
        /// Puts a value in the body
        /// </summary>
        /// <param name="header">The header</param>
        /// <param name="value">The body</param>
        public void PutInfo(string header, string value) {
            XmlNode infoNode = GetBodyInfoNode(header);

            if (infoNode == null) {
                infoNode = _doc.CreateElement(XML_IXP_BODY_INFO);
                XmlAttribute nameAttr = _doc.CreateAttribute(XML_IXP_BODY_INFO_NAME);
                nameAttr.Value = header;
                infoNode.InnerText = value;
                infoNode.Attributes.Append(nameAttr);
                GetBodyNode().AppendChild(infoNode);
            } else {
                infoNode.InnerText = value;
            }
        }

        private XmlNode GetTargetNode() {
            return _doc.DocumentElement[XML_IXP_TARGET];
        }
        private XmlAttribute GetTargetNFunctionAttr() {
            foreach (XmlAttribute attr in GetTargetNode().Attributes) {
                if (attr.Name.Equals(XML_IXP_TARGET_NFUNCTION))
                    return attr;
            }
            return null;
        }
        private XmlNode GetHeaderNode() {
            return _doc.DocumentElement[XML_IXP_HEADER];
        }
        private XmlNode GetHeaderInfoNode(string name) {
            foreach (XmlNode node in GetHeaderNode().ChildNodes) {
                if (GetHeaderInfoNameAttr(node).Name.Equals(name))
                    return node;
            }

            return null;
        }
        private XmlAttribute GetHeaderInfoNameAttr(XmlNode headerInfoNode) {
            foreach (XmlAttribute attr in headerInfoNode.Attributes) {
                if (attr.Name.Equals(XML_IXP_HEADER_INFO_NAME))
                    return attr;
            }
            return null;
        }
        private XmlAttribute GetHeaderInfoValueAttr(XmlNode headerInfoNode) {
            foreach (XmlAttribute attr in headerInfoNode.Attributes) {
                if (attr.Name.Equals(XML_IXP_HEADER_INFO_VALUE))
                    return attr;
            }
            return null;
        }
        private XmlNode GetBodyNode() {
            return _doc.DocumentElement[XML_IXP_BODY];
        }
        private XmlNode GetBodyInfoNode(string name) {
            foreach (XmlNode node in GetBodyNode().ChildNodes) {
                if (GetBodyInfoNameAttr(node).Value.Equals(name))
                    return node;
            }

            return null;
        }
        private XmlAttribute GetBodyInfoNameAttr(XmlNode headerInfoNode) {
            foreach (XmlAttribute attr in headerInfoNode.Attributes) {
                if (attr.Name.Equals(XML_IXP_BODY_INFO_NAME))
                    return attr;
            }
            return null;
        }
    }
}
