from ESApi.ServerProvider import ServerProvider

class Networking:
    __server = None  # type:ServerProvider

    def provide_server(self, provider: ServerProvider):
        if self.__server is not None:
            return

        self.__server = provider

    def get_server(self):
        return self.__server

instance = Networking()  # type: Networking
