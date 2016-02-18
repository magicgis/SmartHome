from ESApi.AppScreen import AppScreen


class Application (object):
    __refreshCallback = None  # type: callable

    """
        Apps are pluggable services that can work in background and provide gui screens for interaction
    """

    def on_system_boot(self, finishedCallback: callable):
        """
            Gets called on system boot when this application gets loaded. This is still within the boot animation and
            should handle everything the application needs to work properly
        """
        pass

    def on_system_shutdown(self):
        """
            Gets challed on system shutdown
        """
        pass

    def get_current_screen(self) -> AppScreen:
        """
            Returns the applications current screen
        """
        raise NotImplementedError()

    def provide_refresh_callback(self, callback: callable):
        self.__refreshCallback = callback

    def refresh_screen(self):
        if self.__refreshCallback is None:
            return

        self.__refreshCallback()

    def on_set(self):
        """
            Called when the app is set active
        """

        pass

    def on_unset(self):
        """
            Called when the app is unset
        """

        pass
