from xml.dom import minidom

class IXPFile:
    __network_function = ""  # type: str
    __header_names = []  # type: List[str]
    __header_values = []  # type: List[str]
    __info_names = []  # type: List[str]
    __info_values = []  # type: List[str]

    __XML_HEADER = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
    __XML_IXP = "IXP"
    __XML_IXP_TARGET = "Target"
    __XML_IXP_TARGET_NFUNCTION = "nfunction"
    __XML_IXP_HEADER = "Header"
    __XML_IXP_HEADER_INFO = "Info"
    __XML_IXP_HEADER_INFO_NAME = "name"
    __XML_IXP_HEADER_INFO_VALUE = "value"
    __XML_IXP_BODY = "Body"
    __XML_IXP_BODY_INFO = "Info"
    __XML_IXP_BODY_INFO_NAME = "name"

    __XML_BASE = \
            __XML_HEADER + \
            "<" + __XML_IXP + ">" + \
                "<"  + __XML_IXP_TARGET + " " + __XML_IXP_TARGET_NFUNCTION + "=\"\"" + "/>" + \
                "<"  + __XML_IXP_HEADER + ">" + \
                "</" + __XML_IXP_HEADER + ">" + \
                "<"  + __XML_IXP_BODY + ">" + \
                "</" + __XML_IXP_BODY + ">" + \
            "</" + __XML_IXP + ">"

    def __init__(self, source = None):
        if source is None:
            return

        doc = minidom.parseString(source)

        nFuncNode = doc.getElementsByTagName(self.__XML_IXP_TARGET)[0]
        self.__network_function = nFuncNode.getAttribute(self.__XML_IXP_TARGET_NFUNCTION)

        headerNode = doc.getElementsByTagName(self.__XML_IXP_HEADER)[0]
        headerNodes = headerNode.getElementsByTagName(self.__XML_IXP_HEADER_INFO)

        for curNode in headerNodes:
            name = curNode.getAttribute(self.__XML_IXP_HEADER_INFO_NAME)
            value = curNode.getAttribute(self.__XML_IXP_HEADER_INFO_VALUE)
            self.__header_names.append(name)
            self.__header_values.append(value)

        bodyNode = doc.getElementsByTagName(self.__XML_IXP_BODY)[0]
        bodyNodes = bodyNode.getElementsByTagName(self.__XML_IXP_BODY_INFO)


        for curNode in bodyNodes:
            name = curNode.getAttribute(self.__XML_IXP_BODY_INFO_NAME)
            value = curNode.childNodes[0].nodeValue
            self.__info_names.append(name)
            self.__info_values.append(value)

    def get_xml(self) -> str:
        doc = minidom.parseString(self.__XML_BASE)

        nFuncNode = doc.getElementsByTagName(self.__XML_IXP_TARGET)[0]
        self.__network_function = nFuncNode.setAttribute(self.__XML_IXP_TARGET_NFUNCTION, self.__network_function)

        for index in range(0, self.headers_count()):
            headerNode = doc.createElement(self.__XML_IXP_HEADER_INFO)
            headerNode.setAttribute(self.__XML_IXP_HEADER_INFO_NAME, self.header_name_at(index))
            headerNode.setAttribute(self.__XML_IXP_HEADER_INFO_VALUE, self.header_value_at(index))

            doc.getElementsByTagName(self.__XML_IXP_HEADER)[0].appendChild(headerNode)

        for index in range(0, self.infos_count()):
            infoNode = doc.createElement(self.__XML_IXP_BODY_INFO)
            infoNode.setAttribute(self.__XML_IXP_BODY_INFO_NAME, self.info_name_at(index))

            infoTextNode = doc.createTextNode(self.info_value_at(index))
            infoNode.appendChild(infoTextNode)

            doc.getElementsByTagName(self.__XML_IXP_BODY)[0].appendChild(infoNode)

        return doc.toxml()

    def get_network_function(self) -> str:
        return self.__network_function

    def headers_count(self) -> int:
        return len(self.__header_names)

    def header_name_at(self, index: int) -> str:
        if index < 0 or index >= self.headers_count():
            raise IndexError()

        return self.__header_names[index]

    def header_value_at(self, index: int) -> str:
        if index < 0 or index >= self.headers_count():
            raise IndexError()

        return self.__header_values[index]

    def infos_count(self) -> int:
        return len(self.__info_names)

    def info_name_at(self, index: int) -> str:
        if index <  0 or index >= self.infos_count():
            raise IndexError()

        return self.__info_names[index]

    def info_value_at(self, index: int) -> str:
        if index < 0 or index >= self.infos_count():
            raise IndexError()

        return self.__info_values[index]

    def add_info(self, name: str, value: str):
        self.__info_names.append(name)
        self.__info_values.append(value)

    def add_header(self, name: str, value: str):
        self.__header_names.append(name)
        self.__header_values.append(value)