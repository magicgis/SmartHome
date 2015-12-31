from ESApi.Application import Application
from ESApi.AppScreen import AppScreen

from ClockSrc.UI.MainScreen import MainScreen


class ClockApp(Application):
    __curScreen = None  # type: AppScreen

    def on_system_boot(self):
        self.__curScreen = MainScreen()

    def get_current_screen(self) -> AppScreen:
        return self.__curScreen
