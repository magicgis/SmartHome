from xml.dom import minidom

class IXPFile:
    """
        A IXP File is a XML File containing data used for the smart home system.
        This file structure is used for network communication between clients and the server
    """

    # Variables containing data that gets parsed to XML or has been parsed from existing source
    __network_function = ""  # type: str
    __header_names = []  # type: List[str]
    __header_values = []  # type: List[str]
    __info_names = []  # type: List[str]
    __info_values = []  # type: List[str]

    # Constants defining the IXP Layout
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
        """
            Initialized a new IXP File or parses a existing one provided as string
        :param source: Existing IXP String to parse
        """

        # If no source is provided everything is done already
        if source == None:
            return

        self.__header_names = []
        self.__header_values = []
        self.__info_names = []
        self.__info_values = []
        self.__network_function = ""

        # Parse source to minidom xml
        doc = minidom.parseString(source)

        # Read the network function
        nFuncNode = doc.getElementsByTagName(self.__XML_IXP_TARGET)[0]
        self.__network_function = nFuncNode.getAttribute(self.__XML_IXP_TARGET_NFUNCTION)

        # Read headers
        headerNode = doc.getElementsByTagName(self.__XML_IXP_HEADER)[0]
        headerNodes = headerNode.getElementsByTagName(self.__XML_IXP_HEADER_INFO)

        for curNode in headerNodes:
            name = curNode.getAttribute(self.__XML_IXP_HEADER_INFO_NAME)
            value = curNode.getAttribute(self.__XML_IXP_HEADER_INFO_VALUE)
            self.__header_names.append(name)
            self.__header_values.append(value)


        # Read infos
        bodyNode = doc.getElementsByTagName(self.__XML_IXP_BODY)[0]
        bodyNodes = bodyNode.getElementsByTagName(self.__XML_IXP_BODY_INFO)

        for curNode in bodyNodes:
            name = curNode.getAttribute(self.__XML_IXP_BODY_INFO_NAME)
            value = curNode.childNodes[0].nodeValue
            self.__info_names.append(name)
            self.__info_values.append(value)

    def get_xml(self) -> str:
        """
            Parses the object to an IXP String
        :return: Fully parsed string
        """

        # Create a raw IXP File with minidom xml
        doc = minidom.parseString(self.__XML_BASE)

        # Set network function
        nFuncNode = doc.getElementsByTagName(self.__XML_IXP_TARGET)[0]
        self.__network_function = nFuncNode.setAttribute(self.__XML_IXP_TARGET_NFUNCTION, self.__network_function)

        # Set headers
        for index in range(0, self.headers_count()):
            headerNode = doc.createElement(self.__XML_IXP_HEADER_INFO)
            headerNode.setAttribute(self.__XML_IXP_HEADER_INFO_NAME, self.header_name_at(index))
            headerNode.setAttribute(self.__XML_IXP_HEADER_INFO_VALUE, self.header_value_at(index))

            doc.getElementsByTagName(self.__XML_IXP_HEADER)[0].appendChild(headerNode)

        # Set infos
        for index in range(0, self.infos_count()):
            infoNode = doc.createElement(self.__XML_IXP_BODY_INFO)
            infoNode.setAttribute(self.__XML_IXP_BODY_INFO_NAME, self.info_name_at(index))

            infoTextNode = doc.createTextNode(self.info_value_at(index))
            infoNode.appendChild(infoTextNode)

            doc.getElementsByTagName(self.__XML_IXP_BODY)[0].appendChild(infoNode)

        return doc.toxml()

    def get_network_function(self) -> str:
        """
        :return: The network function
        """

        return self.__network_function

    def set_network_function(self, networkFunction: str):
        """
            Sets the network function
        :param networkFunction: The function name
        """

        self.__network_function = networkFunction

    def headers_count(self) -> int:
        """
        :return: Number of headers
        """

        return len(self.__header_names)

    def header_name_at(self, index: int) -> str:
        """
            Provides the name of a header at a specific index
        :param index: The headers index
        :return: The headers name
        """

        if index < 0 or index >= self.headers_count():
            raise IndexError()

        return self.__header_names[index]

    def header_value_at(self, index: int) -> str:
        """
            Provides the value of a header at a specific index
        :param index: The headers index
        :return: The headers value
        """

        if index < 0 or index >= self.headers_count():
            raise IndexError()

        return self.__header_values[index]

    def infos_count(self) -> int:
        """
        :return: Number of infos
        """

        return len(self.__info_names)

    def info_name_at(self, index: int) -> str:
        """
            Provides the name of a info at a specific index
        :param index: The infos index
        :return: The infos name
        """

        if index <  0 or index >= self.infos_count():
            raise IndexError()

        return self.__info_names[index]

    def info_value_at(self, index: int) -> str:
        """
            Provides the value of a info at a specific index
        :param index: The infos index
        :return: The infos value
        """

        if index < 0 or index >= self.infos_count():
            raise IndexError()

        return self.__info_values[index]

    def get_header(self, name: str):
        for index in range(0, self.headers_count()):
            hName = self.header_name_at(index)

            if hName == name:
                return self.header_value_at(index)

        return None

    def get_info(self, name: str):
        for index in range(0, self.infos_count()):
            iName = self.info_name_at(index)

            if iName == name:
                return self.info_value_at(index)

        return None

    def add_info(self, name: str, value: str):
        """
            Adds a new info to the IXP File
        :param name: The infos name
        :param value: The infos value
        """

        self.__info_names.append(name)
        self.__info_values.append(value)

    def add_header(self, name: str, value: str):
        """
            Provides a new header to the IXP File
        :param name: The headers name
        :param value: The headers value
        """

        self.__header_names.append(name)
        self.__header_values.append(value)