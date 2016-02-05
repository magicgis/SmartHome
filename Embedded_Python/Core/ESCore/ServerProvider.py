from ESApi.ServerProvider import ServerProvider as AbstractServerProvider
from ESApi.ServerProvider import ConnectionIdentifier, MessageListenerIdentifier
from ESApi.IXPFile import IXPFile


<<<<<<< HEAD
class ServerProvider(AbstractServerProvider):    
=======
class ServerProvider(AbstractServerProvider):
    __connections = {}  # type: Dictionary

>>>>>>> develop
    def request_connection(self) -> ConnectionIdentifier:
        """
            override of superclass method
        """

        raise NotImplementedError()

    def free_connection(self, connection: ConnectionIdentifier):
        """
            override of superclass method
        """

        raise NotImplementedError()

    def connection_avaliable(self, connection: ConnectionIdentifier) -> bool:
        """
            override of superclass method
        """

        raise NotImplementedError()

    def register_message_listener(self, connection: ConnectionIdentifier, function: str, callback) -> MessageListenerIdentifier:
        """
            override of superclass method
        """

        raise NotImplementedError()

    def unregister_message_listener(self, listener: MessageListenerIdentifier):
        """
            override of superclass method
        """

        raise NotImplementedError()

    def no_response_request(self, connection: ConnectionIdentifier, request: IXPFile):
        """
            override of superclass method
        """

        raise NotImplementedError()

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

        raise NotImplementedError()