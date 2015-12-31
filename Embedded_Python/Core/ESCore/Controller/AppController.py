from kivy.app import Widget

from ESCore.Controller.Controller import Controller
from ESCore.CoreApplication import CoreApplication


class AppController(Controller):
    __app = None  # type: CoreApplication

    def __init__(self, app: CoreApplication):
        self.__app = app

    def get_widget(self) -> Widget:
        self.__app.app().get_current_screen()

    def on_set(self):
        pass

    def on_unset(self):
        pass