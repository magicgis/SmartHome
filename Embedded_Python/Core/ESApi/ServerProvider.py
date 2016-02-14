from ESApi.IXPFile import IXPFile


class ConnectionIdentifier:
    """
        Empty object for identifying connections to the server without providing the connections themselves
    """

    pass


class MessageListenerIdentifier:
    """
        Empty object for identifying listeners of network functions
    """

    pass


class ServerProvider:
    """
        Abstract class that defines all the functionality a app can use for communicating with the smart home server
    """

    def request_connection(self) -> ConnectionIdentifier:
        """
            Requests a new connection to the smart home server.
            A connection may be closed at any time so you should always check avaliability before
        :return: Identifier used for request methods of this class
        """

        raise NotImplementedError()

    def free_connection(self, connection: ConnectionIdentifier):
        """
            Closes a connection to the smart home server
        :param connection: Connection identifier created by 'request_connection'
        """

        raise NotImplementedError()

    def free_all_connections(self):
        """
            Closes all connections to the smart home server
        """

        raise NotImplementedError()

    def connection_avaliable(self, connection: ConnectionIdentifier) -> bool:
        """
            Checks if a connection is still avaliable
        :param connection: Connection identifier created by 'request_connection'
        :return: Boolean that indicates avaliability
        """

        raise NotImplementedError()

    def register_message_listener(self, connection: ConnectionIdentifier, function: str, callback) -> MessageListenerIdentifier:
        """
            Registers a listener for a specific type of message sent by the server
        :param connection: Connection identifier created by 'request_connection'
        :param function: Network Function to register for
        :param callback: Callback with a 'message: IXPFile = None' optional parameter
        :return: A identfier for unregistering the listener
        """

        raise NotImplementedError()

    def unregister_message_listener(self, listener: MessageListenerIdentifier):
        """
            Unregisters a listener listening for a specifix type of message
        :param listener: The server to unregister
        """

        raise NotImplementedError()

    def no_response_request(self, connection: ConnectionIdentifier, request: IXPFile):
        """
            A request that does not wait for a response from the server
        :param connection: Connection identifier created by 'request_connection'
        :param request: Request to send
        """

        raise NotImplementedError()

    def simple_int_request(self, connection: ConnectionIdentifier, request: IXPFile) -> int:
        """
            A request that gets back a single int from the server. Waits for the response
        :param connection: Connection identifier created by 'request_connection'
        :param request: Request to send
        :return: int returned from the server
        """

        raise NotImplementedError()

    def simple_string_request(self, connection: ConnectionIdentifier, request: IXPFile) -> int:
        """
            A request that gets back a single string from the server. Waits for the response
        :param connection: Connection identifier created by 'request_connection'
        :param request: Request to send
        :return: string returned from the server
        """

        raise NotImplementedError()

    def simple_bool_request(self, connection: ConnectionIdentifier, request: IXPFile) -> int:
        """
            A request that gets back a single bool from the server. Waits for the response
        :param connection: Connection identifier created by 'request_connection'
        :param request: Request to send
        :return: bool returned from the server
        """

        raise NotImplementedError()

    def ixp_request(self, connection: ConnectionIdentifier, request: IXPFile) -> IXPFile:
        """
            A request that gets back a ixp response from the server. Waits for the response
        :param connection: Connection identifier created by 'request_connection'
        :param request: Request to send
        :return: IXPFile returned from the server
        """

        raise NotImplementedError()

    def simple_int_request_async(self, connection: ConnectionIdentifier, request: IXPFile, callback):
        """
            A request that gets back a single int from the server.
            Returns immediately and calls a callback on completion
        :param connection: Connection identifier created by 'request_connection'
        :param request: Request to send
        :param callback: Method with a 'response: int = -1' optional parameter
        """

        raise NotImplementedError()

    def simple_string_request_async(self, connection: ConnectionIdentifier, request: IXPFile, callback):
        """
            A request that gets back a single string from the server.
            Returns immediately and calls a callback on completion
        :param connection: Connection identifier created by 'request_connection'
        :param request: Request to send
        :param callback: Method with a 'response: str = None' optional parameter
        """

        raise NotImplementedError()

    def simple_bool_request(self, connection: ConnectionIdentifier, request: IXPFile, callback):
        """
            A request that gets back a single bool from the server.
            Returns immediately and calls a callback on completion
        :param connection: Connection identifier created by 'request_connection'
        :param request: Request to send
        :param callback: Method with a 'response: bool = False' optional parameter
        """

        raise NotImplementedError()

    def ixp_request_async(self, connection: ConnectionIdentifier, request: IXPFile, callback):
        """
            A request that gets back a ixp file from the server.
            Returns immediately and calls a callback on completion
        :param connection: Connection identifier created by 'request_connection'
        :param request: Request to send
        :param callback: Method with a 'response: IXPFile = None' optional parameter
        """

        raise NotImplementedError()
