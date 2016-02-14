from ESApi.ServerProvider import ServerProvider as AbstractServerProvider
from ESApi.ServerProvider import ConnectionIdentifier, MessageListenerIdentifier
from ESApi.IXPFile import IXPFile
from ESCore.BufferedIXPSocket import BufferedIXPSocket

class AsyncListenerIdentifier:
    pass

<<<<<<< HEAD
class ServerProvider(AbstractServerProvider):    
=======
class ServerProvider(AbstractServerProvider):
    __ASYNC_LISTENER_TYPE_IXP = "IXP"  # type: str
    __ASYNC_LISTENER_TYPE_INT = "INT"  # type: str
    __ASYNC_LISTENER_TYPE_STR = "STR"  # type: str
    __ASYNC_LISTENER_TYPE_BOO = "BOO"  # type: str

    __connections = {}  # type: dict

    __listeners = []  # type: list
    __listenerConnections = {}  # type: dict
    __listenerFunctions = {}  # type: dict
    __listenerCallbacks = {}  # type: dict

    __asyncListeners = []  # type: list
    __asyncListenersTypes = {}  # type: dict
    __asyncListenersConnections = {}  # type: dict
    __asyncListenersFunctions = {}  # type: dict
    __asyncListenersCallbacks = {}  # type: dict

>>>>>>> develop
    def request_connection(self) -> ConnectionIdentifier:
        """
            override of superclass method
        """

        identifier = ConnectionIdentifier()
        socket = BufferedIXPSocket("192.168.178.47", 10250)
        socket.register_callback(lambda ixpFile: self.__message_received(identifier, ixpFile))
        self.__connections[identifier] = socket
        return identifier

    def free_connection(self, connection: ConnectionIdentifier):
        """
            override of superclass method
        """

        connection = self.__connections[connection]
        connection.stop()


    def free_all_connections(self):
        """
            override of superclass method
        """

        for identifier in self.__connections:
            self.free_connection(identifier)

    def connection_avaliable(self, connection: ConnectionIdentifier) -> bool:
        """
            override of superclass method
        """

        return True

    def register_message_listener(self, connection: ConnectionIdentifier, function: str, callback) -> MessageListenerIdentifier:
        """
            override of superclass method
        """

        identifier = MessageListenerIdentifier()
        self.__listeners.append(identifier)
        self.__listenerConnections[identifier] = connection
        self.__listenerFunctions[identifier] = function
        self.__listenerCallbacks[identifier] = callback

        return identifier

    def unregister_message_listener(self, listener: MessageListenerIdentifier):
        """
            override of superclass method
        """

        self.__listeners.remove(listener)
        del self.__listenerConnections[listener]
        del self.__listenerFunctions[listener]
        del self.__listenerCallbacks[listener]

    def no_response_request(self, connection: ConnectionIdentifier, request: IXPFile):
        """
            override of superclass method
        """

        socket = self.__connections[connection]  # type: BufferedIXPSocket
        socket.send(request)

    def simple_int_request(self, connection: ConnectionIdentifier, request: IXPFile) -> int:
        """
            override of superclass method
        """

        raise NotImplementedError()

    def simple_string_request(self, connection: ConnectionIdentifier, request: IXPFile) -> int:
        """
            override of superclass method
        """

        raise NotImplementedError()

    def simple_bool_request(self, connection: ConnectionIdentifier, request: IXPFile) -> int:
        """
            override of superclass method
        """

        raise NotImplementedError()

    def ixp_request(self, connection: ConnectionIdentifier, request: IXPFile) -> IXPFile:
        """
            override of superclass method
        """

        raise NotImplementedError()

    def simple_int_request_async(self, connection: ConnectionIdentifier, request: IXPFile, callback):
        """
            override of superclass method
        """

        raise NotImplementedError()

    def simple_string_request_async(self, connection: ConnectionIdentifier, request: IXPFile, callback):
        """
            override of superclass method
        """

        raise NotImplementedError()

    def simple_bool_request(self, connection: ConnectionIdentifier, request: IXPFile, callback):
        """
            override of superclass method
        """

        raise NotImplementedError()

    def ixp_request_async(self, connection: ConnectionIdentifier, request: IXPFile, callback):
        """
            override of superclass method
        """

        identifier = AsyncListenerIdentifier()
        self.__asyncListeners.append(identifier)
        self.__asyncListenersCallbacks[identifier] = callback
        self.__asyncListenersConnections[identifier] = connection
        self.__asyncListenersFunctions[identifier] = request.get_network_function()
        self.__asyncListenersTypes[identifier] = self.__ASYNC_LISTENER_TYPE_IXP

        socket = self.__connections[connection]  # type: BufferedIXPSocket
        socket.send(request)

    def __message_received(self, connection: ConnectionIdentifier, ixpFile: IXPFile):
        for identifier in self.__asyncListeners:
            if connection != self.__asyncListenersConnections[identifier]:
                continue

            if self.__asyncListenersFunctions[identifier] != ixpFile.get_network_function():
                continue

            if self.__asyncListenersTypes[identifier] == self.__ASYNC_LISTENER_TYPE_IXP:
                self.__asyncListenersCallbacks[identifier](ixpFile)
            elif self.__asyncListenersTypes[identifier] == self.__ASYNC_LISTENER_TYPE_BOO:
                pass
            elif self.__asyncListenersTypes[identifier] == self.__ASYNC_LISTENER_TYPE_INT:
                pass
            elif self.__asyncListenersTypes[identifier] == self.__ASYNC_LISTENER_TYPE_STR:
                pass

            self.__asyncListeners.remove(identifier)
            del self.__asyncListenersCallbacks[identifier]
            del self.__asyncListenersConnections[identifier]
            del self.__asyncListenersFunctions[identifier]
            del self.__asyncListenersTypes[identifier]
            return

        for identifier in self.__listeners:
            if connection != self.__listenerConnections[identifier]:
                continue

            func = self.__listenerFunctions[identifier]

            if func == ixpFile.get_network_function():
                self.__listenerCallbacks[identifier](ixpFile)