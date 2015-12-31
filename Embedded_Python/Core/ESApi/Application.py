from ESApi.AppScreen import AppScreen


class Application (object):
    def on_system_boot(self):
        pass

    def get_current_screen(self) -> AppScreen:
        raise NotImplementedError()