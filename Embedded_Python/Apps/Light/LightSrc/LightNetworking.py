import ESApi.Networking as Networking

from ESApi.ServerProvider import ServerProvider, ConnectionIdentifier

class LightNetworking:
    __connection = None  # type: ConnectionIdentifier

    def connect(self):
        self.__connection = Networking.instance.get_server().request_connection()

    def get_connection(self) -> ConnectionIdentifier:
        if not Networking.instance.get_server().connection_avaliable(self.__connection):
            Networking.instance.get_server().free_connection(self.__connection)
            self.__connection = None

        if self.__connection is None:
            self.__connection = Networking.instance.get_server().request_connection()

        return self.__connection

instance = LightNetworking()