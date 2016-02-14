from ESApi.AppScreen import AppScreen


class Application (object):
    """
        Apps are pluggable services that can work in background and provide gui screens for interaction
    """

    def on_system_boot(self):
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
