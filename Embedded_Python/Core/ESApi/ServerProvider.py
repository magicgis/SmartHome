class ServerProvider:
    def request_connection(self) -> int:
        raise NotImplementedError()

    def free_connection(self, id: int):
        raise NotImplementedError()

    pass