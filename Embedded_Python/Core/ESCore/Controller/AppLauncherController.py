from kivy.app import Widget
from kivy.uix.image import Image

from ESCore.UI.SingleApplicationScreenWidget import SingleApplicationScreenWidget
from ESCore.Controller.Controller import Controller
from ESCore.UI.AppLauncherWidget import ApplicationScreenSubLauncherWidget
from ESCore.CoreApplication import CoreApplication


class AppLauncherController(Controller):
    __widget = None  # type: SingleApplicationWidget
    __child = None  # type: ApplicationScreenSubLauncherWidget
    __image_widget = None  # type: Image

    def initialize(self):
        self.__child = ApplicationScreenSubLauncherWidget()
        self.__widget = SingleApplicationScreenWidget(self.__child, useBottomBar = True, useTopBar = False)

    def get_widget(self) -> Widget:
        return self.__widget

    def on_set(self):
        pass

    def on_unset(self):
        pass

instance = AppLauncherController()