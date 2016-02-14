class System:
    __shutdownCallback = None  # type: callable

    def provide_shutdown_callback(self, callback: callable):
        self.__shutdownCallback = callback

    def shutdown(self):
        if self.__shutdownCallback is None:
            return

        self.__shutdownCallback()

instance = System()